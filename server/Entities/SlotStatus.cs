using System;
using System.Collections.Generic;

#nullable disable

namespace TheGarageAPI.Entities
{
    public partial class SlotStatus
    {
        public SlotStatus()
        {
            Slots = new HashSet<Slot>();
        }

        public byte SlotStatusId { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Slot> Slots { get; set; }
    }
}
