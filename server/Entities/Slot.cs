using System;
using System.Collections.Generic;

#nullable disable

namespace TheGarageAPI.Entities
{
    public partial class Slot
    {
        public Slot()
        {
            Reservations = new HashSet<Reservation>();
        }

        public string SlotId { get; set; }
        public string Floor { get; set; }
        public string Location { get; set; }
        public byte SlotStatusId { get; set; }

        public virtual SlotStatus SlotStatus { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
