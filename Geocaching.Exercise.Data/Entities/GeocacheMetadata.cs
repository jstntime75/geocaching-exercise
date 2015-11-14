using System.ComponentModel.DataAnnotations;

namespace Geocaching.Exercise.Data.Entities
{
    public class GeocacheMetadata
    {
        [Required]
        [StringLength(50)]
        [RegularExpression("^([a-zA-Z0-9 ]+)$")]
        public string Name { get; set; }

        [Required]
        [StringLength(1024)]
        public string Description { get; set; }

        [Required]
        [Range(-90.0, 90.0)]
        public double Latitude { get; set; }

        [Required]
        [Range(-180.0, 180.0)]
        public double Longitude { get; set; }
    }

    [MetadataType(typeof(GeocacheMetadata))]
    public partial class Geocache
    {
    }
}
