using System;
using System.Collections.Generic;

#nullable disable

namespace TheGarageAPI.Entities
{
    public partial class DataUser
    {
        public DataUser()
        {
            Reservations = new HashSet<Reservation>();
        }

        public string DataUserId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstSurname { get; set; }
        public string SecondSurname { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public byte[] ProfilePicture { get; set; }
        public bool? Status { get; set; }
        public byte UserTypeId { get; set; }

        public virtual UserType UserType { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
