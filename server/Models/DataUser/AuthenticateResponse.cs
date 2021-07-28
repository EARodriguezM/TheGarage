namespace TheGarageAPI.Models.DataUser
{
    public class AuthenticateResponse
    {
        public string DataUserId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstSurname { get; set; }
        public string SecondSurname { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public byte[] ProfilePicture { get; set; }

        public string Token { get; set; }

        public AuthenticateResponse(TheGarageAPI.Entities.DataUser dataUser, string token)
        {
            DataUserId = dataUser.DataUserId;
            FirstName = dataUser.FirstName;
            SecondName = dataUser.SecondName;
            FirstSurname = dataUser.FirstSurname;
            SecondSurname = dataUser.SecondSurname;
            Email = dataUser.Email;
            Mobile = dataUser.Mobile;
            ProfilePicture = dataUser.ProfilePicture;

            Token = token;
        }
    }
}