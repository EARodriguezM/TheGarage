using System.ComponentModel.DataAnnotations;

namespace TheGarageAPI.Models.Vehicle
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "Vehicle plate must be at least 6 characters and maximum 10")]
        public string VehiclePlate { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "City of vehicle plate must be at least 2 characters and maximum 50")]
        public string PlateCity { get; set; }

        [Required]
        public byte VehicleTypeId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Brand must be at least 2 characters and maximum 50")]
        public string Brand { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Line must be at least 2 characters and maximum 50")]
        public string Line { get; set; }

        [Required]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Model must be 4 characters")]
        public string Model { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Color must be at least 2 characters and maximum 50")]
        public string Color { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Description must be at least 2 characters and maximum 50")]
        public string Description { get; set; }
    }
}