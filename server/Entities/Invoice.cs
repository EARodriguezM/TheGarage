using System;
using System.Collections.Generic;

#nullable disable

namespace TheGarageAPI.Entities
{
    public partial class Invoice
    {
        public string InvoiceId { get; set; }
        public string ReservationId { get; set; }
        public decimal Price { get; set; }
        public DateTime PaidDate { get; set; }
        public byte InvoiceStatusId { get; set; }

        public virtual InvoiceStatus InvoiceStatus { get; set; }
        public virtual Reservation Reservation { get; set; }
    }
}
