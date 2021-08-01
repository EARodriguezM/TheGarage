using System.ComponentModel.DataAnnotations;

namespace TheGarageAPI.Models.VehicleType
{
    public class RegisterRequest
    {
        [Required]
        public byte VehicleTypeId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Description must be at least 2 characters and maximum 50")]
        public string Description { get; set; }
    }
}