using System.ComponentModel.DataAnnotations;
namespace TheGarageAPI.Models.DataUser
{
    public class UpdateRequest
    {
        [Required]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "ID must be at least 6 characters")]
        public string DataUserId { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be at least 2 characters")]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Second name must be at least 2 characters")]
        public string SecondName { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "First surname must be at least 2 characters")]
        public string FirstSurname { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Second surname must be at least 2 characters")]
        public string SecondSurname { get; set; }

        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; }

        [StringLength(50, MinimumLength = 8, ErrorMessage = "Email must be at least 8 characters")]
        public string Email { get; set; }

        [StringLength(50, MinimumLength = 10, ErrorMessage = "Phone must be at least 10 characters")]
        public string Mobile { get; set; }

        public byte UserTypeId { get; set; }
        
        public byte[] ProfilePicture { get; set; }
    }
}