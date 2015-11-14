"use strict";

angular.module('geocaching.exercise', [
    'ui.router',
    'toastr',
    'geocaching.exercise.home'
])
.config(['$compileProvider', function ($compileProvider) {
    $compileProvider.debugInfoEnabled(true);
}])
.config(['$logProvider', function ($logProvider) {
    $logProvider.debugEnabled(true);
}])
.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.defaults.headers.common["X-Requested-With"] = 'XMLHttpRequest';
}])
.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.otherwise('/');

    $stateProvider.state('home', {
        url: '/',
        views: {
            'body': {
                templateUrl: '/templates/home/index.html'
            }
        }
    });
}])
.config(['toastrConfig', function (toastrConfig) {
    angular.extend(toastrConfig, {
        allowHtml: false,
        autoDismiss: false,
        closeButton: true,
        closeHtml: '<button>&times;</button>',
        containerId: 'toast-container',
        extendedTimeOut: 1000,
        iconClasses: {
            error: 'toast-error',
            info: 'toast-info',
            success: 'toast-success',
            warning: 'toast-warning'
        },
        maxOpened: 0,
        messageClass: 'toast-message',
        newestOnTop: true,
        onHidden: null,
        onShown: null,
        positionClass: 'toast-bottom-right',
        preventDuplicates: false,
        progressBar: false,
        tapToDismiss: false,
        target: 'body',
        templates: {
            toast: 'directives/toast/toast.html',
            progressbar: 'directives/progressbar/progressbar.html'
        },
        timeOut: 5000,
        titleClass: 'toast-title',
        toastClass: 'toast'
    });
}]);

