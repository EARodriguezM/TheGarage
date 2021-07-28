using System;
using System.Collections.Generic;

#nullable disable

namespace TheGarageAPI.Entities
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Reservations = new HashSet<Reservation>();
        }

        public string VehiclePlate { get; set; }
        public string PlateCity { get; set; }
        public byte VehicleTypeId { get; set; }
        public string Brand { get; set; }
        public string Line { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }

        public virtual VehicleType VehicleType { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
