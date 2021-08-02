using System.ComponentModel.DataAnnotations;

namespace TheGarageAPI.Models.Slot
{
    public class UpdateRequest
    {
        [Required]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "First surname must be at least 2 and maximum 10 characteres")]
        public string SlotId { get; set; }
        
        [StringLength(10, MinimumLength = 2, ErrorMessage = "First surname must be at least 2 and maximum 10 characteres")]
        public string Floor { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "First surname must be at least 2 and maximum 50 characteres")]
        public string Location { get; set; }

        public byte SlotStatusId { get; set; }
    }
}