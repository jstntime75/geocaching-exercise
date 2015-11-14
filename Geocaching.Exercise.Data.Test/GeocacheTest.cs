using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Geocaching.Exercise.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Geocaching.Exercise.Data.Test
{
    [TestClass]
    public class GeocacheTest
    {
        [ClassInitialize]
        public static void Startup(TestContext context)
        {
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(Geocache), typeof(GeocacheMetadata)), typeof(Geocache));
        }

        [TestMethod]
        public void Validate_Geocache_NoError()
        {
            var cache = new Geocache();
            cache.Id = 1;
            cache.Name = "name";
            cache.Description = "description";

            // Act
            var validationResult = ValidationHelper.ValidateEntity<Geocache>(cache);

            // Assert
            Assert.IsFalse(validationResult.HasError);
        }

        [TestMethod]
        public void Validate_Geocache_Name_Empty()
        {
            var cache = new Geocache();
            cache.Id = 1;
            cache.Description = "description";

            // Act
            var validationResult = ValidationHelper.ValidateEntity<Geocache>(cache);

            // Assert
            Assert.IsTrue(validationResult.HasError);
        }

        [TestMethod]
        public void Validate_Geocache_Name_Long()
        {
            var cache = new Geocache();
            cache.Id = 1;
            cache.Name = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"; //51
            cache.Description = "description";

            // Act
            var validationResult = ValidationHelper.ValidateEntity<Geocache>(cache);

            // Assert
            Assert.IsTrue(validationResult.HasError);
        }

        [TestMethod]
        public void Validate_Geocache_Name_InvalidCharacters()
        {
            var cache = new Geocache();
            cache.Id = 1;
            cache.Name = "abcd-1234";
            cache.Description = "description";

            // Act
            var validationResult = ValidationHelper.ValidateEntity<Geocache>(cache);

            // Assert
            Assert.IsTrue(validationResult.HasError);
        }

        [TestMethod]
        public void Validate_Geocache_Description_Empty()
        {
            var cache = new Geocache();
            cache.Id = 1;
            cache.Name = "name";

            // Act
            var validationResult = ValidationHelper.ValidateEntity<Geocache>(cache);

            // Assert
            Assert.IsTrue(validationResult.HasError);
        }

        [TestMethod]
        public void Validate_Geocache_Description_Long()
        {
            var cache = new Geocache();
            cache.Id = 1;
            cache.Name = "name";
            cache.Description = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";

            // Act
            var validationResult = ValidationHelper.ValidateEntity<Geocache>(cache);

            // Assert
            Assert.IsTrue(validationResult.HasError);
        }

        [TestMethod]
        public void Validate_Geocache_Latitude_OutOfRangeHigh()
        {
            var cache = new Geocache();
            cache.Id = 1;
            cache.Name = "name";
            cache.Description = "description";
            cache.Latitude = 90.1D;
            cache.Longitude = 0D;

            // Act
            var validationResult = ValidationHelper.ValidateEntity<Geocache>(cache);

            // Assert
            Assert.IsTrue(validationResult.HasError);
        }

        [TestMethod]
        public void Validate_Geocache_Latitude_OutOfRangeLow()
        {
            var cache = new Geocache();
            cache.Id = 1;
            cache.Name = "name";
            cache.Description = "description";
            cache.Latitude = -90.1D;
            cache.Longitude = 0D;

            // Act
            var validationResult = ValidationHelper.ValidateEntity<Geocache>(cache);

            // Assert
            Assert.IsTrue(validationResult.HasError);
        }

        [TestMethod]
        public void Validate_Geocache_Longitude_OutOfRangeHigh()
        {
            var cache = new Geocache();
            cache.Id = 1;
            cache.Name = "name";
            cache.Description = "description";
            cache.Latitude = 0D;
            cache.Longitude = 180.1D;

            // Act
            var validationResult = ValidationHelper.ValidateEntity<Geocache>(cache);

            // Assert
            Assert.IsTrue(validationResult.HasError);
        }

        [TestMethod]
        public void Validate_Geocache_Longitude_OutOfRangeLow()
        {
            var cache = new Geocache();
            cache.Id = 1;
            cache.Name = "name";
            cache.Description = "description";
            cache.Latitude = 0D;
            cache.Longitude = -180.1D;

            // Act
            var validationResult = ValidationHelper.ValidateEntity<Geocache>(cache);

            // Assert
            Assert.IsTrue(validationResult.HasError);
        }
    }
}
