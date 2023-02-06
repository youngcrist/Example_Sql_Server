using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceTest.Models
{
    public class Customer2ViewModel
    {
        public int Id { get; set; }
        public string CustName { get; set; }
        public string Address { get; set; }
        public bool? Status { get; set; }
        public string CustomerType { get; set; }
    }
}
