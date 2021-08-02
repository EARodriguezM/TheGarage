using System.ComponentModel.DataAnnotations;

namespace TheGarageAPI.Models.SlotStatus
{
    public class UpdateRequest
    {
        [Required]
        public byte SlotStatusId { get; set; }

        [StringLength(20, MinimumLength = 2, ErrorMessage = "Description must be at least 2 characters and maximum 20")]
        public string Description { get; set; }

        public bool Status { get; set; }
    }
}