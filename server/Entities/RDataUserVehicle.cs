using System;
using System.Collections.Generic;

#nullable disable

namespace TheGarageAPI.Entities
{
    public partial class RDataUserVehicle
    {
        public string RelationId { get; set; }
        public string DataUserId { get; set; }
        public string VehiclePlate { get; set; }
        public bool? Status { get; set; }

        public virtual DataUser DataUser { get; set; }
        public virtual Vehicle VehiclePlateNavigation { get; set; }
    }
}
