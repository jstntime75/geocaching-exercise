angular.module('geocaching.exercise.home', ['ngAnimate', 'ui.bootstrap', 'toastr'])
    .controller('CachesCtrl', [
        '$scope', '$http', '$uibModal', 'toastr', function ($scope, $http, $uibModal, toastr) {
            $scope.caches = [];
            $scope.selectedCache = null;
            $scope.map = null;
            $scope.hasSearched = false;

            $scope.delete = function (geocache) {
                if (window.confirm('Are you sure you want to delete ' + geocache.Name + '?')) {
                    $http.delete('/api/geocache/' + geocache.Id)
                        .success(function (data, status, headers, config) {
                            if (data.HasError) {
                                toastr.error(data.Message);
                            }
                            else {
                                if (data.Results == null) {
                                    toastr.info(data.Message);
                                }
                                else {
                                    toastr.success(geocache.Name + ' was successfully deleted.');
                                    if (geocache.Id === $scope.selectedCache.Id) {
                                        $scope.map = null;
                                        $scope.selectedCache = null;
                                    }

                                    $scope.caches.splice(_.findIndex($scope.caches, { 'Id': geocache.Id }), 1);
                                }
                            }
                        }).error(function (data, status, headers, config) {
                            console.log(data);
                            toastr.error('Could not delete geocache id: ' + geocache.Id);
                        });
                }
            };

            $scope.getCaches = function() {
                $scope.caches = [];

                $http.get("/api/geocache")
                    .success(function (data, status, headers, config) {
                        if (data.HasError) {
                            toastr.error(data.Message);
                        }
                        else {
                            if (data.Results == null) {
                                toastr.info(data.Message);
                            }
                            else {
                                $scope.caches = data.Results;
                                $scope.hasSearched = true;
                            }
                        }
                    }).error(function (data, status, headers, config) {
                        console.log(data);
                        toastr.error('Could not retrieve geocaches');
                    });
            };

            $scope.setCache = function (cache) {
                $scope.selectedCache = cache;

                if (null == cache) {
                    return;
                }

                var mapOptions = {
                    zoom: 14,
                    center: new google.maps.LatLng(cache.Latitude, cache.Longitude),
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                }

                $scope.map = new google.maps.Map(document.getElementById('cacheMap'), mapOptions);


                var infoWindow = new google.maps.InfoWindow();

                var marker = new google.maps.Marker({
                    map: $scope.map,
                    position: new google.maps.LatLng(cache.Latitude, cache.Longitude),
                    title: cache.Name
                });
                marker.content = '<div class="infoWindowContent">' + cache.Description + '</div>';

                google.maps.event.addListener(marker, 'click', function () {
                    infoWindow.setContent('<h2>' + marker.title + '</h2>' + marker.content);
                    infoWindow.open($scope.map, marker);
                });
            };

            $scope.openAddCache = function () {
                var modalInstance = $uibModal.open({
                    animation: true,
                    templateUrl: 'cache-create-dialog.html',
                    controller: 'NewCacheCtrl',
                    size: 'lg'
                });

                modalInstance.result.then(function (newValue) {
                    if (null == newValue) {
                        return;
                    }

                    $http.post(
                            "/api/geocache",
                             JSON.stringify(newValue),
                            {
                                headers: {
                                    'Content-Type': 'application/json'
                                }
                            })
                      .success(function (data, status, headers, config) {
                          if (data.HasError) {
                              toastr.error(data.Message);
                          }
                          else {
                              if (data.Results == null) {
                                  toastr.info(data.Message);
                              }
                              else {
                                  if ($scope.hasSearched) {
                                      $scope.caches.push(data.Results);
                                      $scope.caches.sort(function (a, b) {
                                          return a.Name.toLowerCase().localeCompare(b.Name.toLowerCase());
                                      });
                                  }
                                  toastr.success('Geocache added successfully.');
                              }
                          }
                      }).error(function (data, status, headers, config) {
                          console.log(data);
                          toastr.error('Could not add geocache');
                      });
                });
            };
        }
    ]).controller('NewCacheCtrl', ["$scope", "$uibModalInstance", function ($scope, $uibModalInstance) {
        $scope.name = '';
        $scope.description = '';
        $scope.latitude = 47.607136391712906;
        $scope.longitude = -122.3324203491211;
        $scope.map = {};
        $scope.markers = [];

        function setMapOnAll(map) {
            for (var i = 0; i < $scope.markers.length; i++) {
                $scope.markers[i].setMap(map);
            }
        }

        function clearMarkers() {
            setMapOnAll(null);
        }

        function deleteMarkers() {
            clearMarkers();
            $scope.markers = [];
        }

        function addMarker(location) {
            deleteMarkers();

            var marker = new google.maps.Marker({
                position: location
            });

            marker.setMap($scope.map);
            $scope.markers.push(marker);
        }

        function initialize(center) {
            var mapProp = {
                center: center,
                zoom: 12,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };

            $scope.map = new google.maps.Map(document.getElementById("newMap"), mapProp);
            addMarker(center);

            $scope.map.addListener('click', function (event) {
                addMarker(event.latLng);
                $scope.latitude = event.latLng.lat();
                document.getElementById('latitude').value = $scope.latitude;
                $scope.longitude = event.latLng.lng();
                document.getElementById('longitude').value = $scope.longitude;
            });
        }

        $scope.ok = function () {
            if ($scope.name.length === 0) {
                alert('Name is required.');
                return;
            }

            if ($scope.name.length > 50) {
                alert('Name must be 50 characters or less.');
                return;
            }

            if ($scope.description.length === 0) {
                alert('Description is required.');
                return;
            }

            if ($scope.description.length > 1024) {
                alert('Description must be 1024 characters or less.');
                return;
            }

            var re = new RegExp("^([a-zA-Z0-9 ]+)$");
            if (!re.test($scope.name)) {
                alert('Name has invalid characters.');
                return;
            }

            $uibModalInstance.close({
                'Name': $scope.name,
                'Description': $scope.description,
                'Latitude': $scope.latitude,
                'Longitude': $scope.longitude
            });
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

        setTimeout(function () {
            initialize(new google.maps.LatLng($scope.latitude, $scope.longitude));
        }, 500);
    }]);
