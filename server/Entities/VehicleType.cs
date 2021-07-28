using System;
using System.Collections.Generic;

#nullable disable

namespace TheGarageAPI.Entities
{
    public partial class VehicleType
    {
        public VehicleType()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        public byte VehicleTypeId { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
