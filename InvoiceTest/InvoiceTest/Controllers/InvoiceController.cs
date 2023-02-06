using DataAccessLayer;
using DataAccessLayer.Model;
using InvoiceTest.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceTest.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;
        public InvoiceController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Customer = _context.Customers.Where(x => x.Status == true);
            return View();
        }
        public InvoiceViewModal SaveChangeInvoice(InvoiceViewModal newInVoice)
        {

            using (var trasaccion = _context.Database.BeginTransaction())
            {
                try {
                    Invoice invoice = new Invoice()
                    {
                        CustomerId = newInVoice.CustomerId,
                        TotalItbis = newInVoice.TotalItbis,
                        SubTotal = newInVoice.SubTotal,
                        Total = newInVoice.Total
                    };
                    _context.Add(invoice);
                    _context.SaveChanges();


                    foreach (InvoiceDetailViewModel item in newInVoice.InvoiceDetails)
                    {
                        InvoiceDetail invoiceDetail = new InvoiceDetail()
                        {
                            InvoiceId = invoice.Id,
                            Qty = item.Qty,
                            Price = item.Price,
                            TotalItbis = item.TotalItbis,
                            SubTotal = item.SubTotal,
                            Total = item.Total,
                        };
                        _context.Add(invoiceDetail);
                    }
                    _context.SaveChanges();

                    trasaccion.Commit();
                }
                catch (Exception e)
                {
                    trasaccion.Rollback();
                }

                return newInVoice;
            }
        }
        public List<InvoiceDetail> GetDetail(int ID){

            var list = _context.InvoiceDetails.Where(x=> x.InvoiceId == ID).ToList();

            return list;
        }
        public List<Invoices2ViewModel> ListInvoice()
        {
            List<Invoices2ViewModel> listInvoice = new List<Invoices2ViewModel>();
           var list = (from a in _context.Invoices
                        join b in _context.Customers
                        on a.CustomerId equals b.Id
                        select new {
                            Id = a.Id,
                            CusName = b.CustName,
                            TotalItbis = a.TotalItbis,
                            SubTotal = a.SubTotal,
                            Total = a.Total
                        }).ToList();

            foreach(var item in list)
            {
                Invoices2ViewModel invoice = new Invoices2ViewModel();
                invoice.Id = item.Id;
                invoice.CustName = item.CusName;
                invoice.TotalItbis = item.TotalItbis;
                invoice.SubTotal = item.SubTotal;
                invoice.Total = item.Total;

                listInvoice.Add(invoice);
            }

            return listInvoice.ToList();
        }
    }
}
