using System;
using System.Collections.Generic;

#nullable disable

namespace TheGarageAPI.Entities
{
    public partial class Reservation
    {
        public string ReservationId { get; set; }
        public string DataUserId { get; set; }
        public string VehiclePlate { get; set; }
        public string SlotId { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public byte ReservationStatusId { get; set; }

        public virtual DataUser DataUser { get; set; }
        public virtual ReservationStatus ReservationStatus { get; set; }
        public virtual Slot Slot { get; set; }
        public virtual Vehicle VehiclePlateNavigation { get; set; }
    }
}
