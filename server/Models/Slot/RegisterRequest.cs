using System.ComponentModel.DataAnnotations;
namespace TheGarageAPI.Models.Slot
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "First surname must be at least 2 and maximum 10 characteres")]
        public string SlotId { get; set; }
        
        [Required]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "First surname must be at least 2 and maximum 10 characteres")]
        public string Floor { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First surname must be at least 2 and maximum 50 characteres")]
        public string Location { get; set; }

        [Required]
        public byte SlotStatusId { get; set; }
    }
}