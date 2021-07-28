using System;
using System.Collections.Generic;

#nullable disable

namespace TheGarageAPI.Entities
{
    public partial class ReservationStatus
    {
        public ReservationStatus()
        {
            Reservations = new HashSet<Reservation>();
        }

        public byte ReservationStatusId { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
