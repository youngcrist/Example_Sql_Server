using DataAccessLayer;
using DataAccessLayer.Model;
using InvoiceTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceTest.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.CustomerType = _context.CustomerTypes.ToList();
            return View();
        }
        [HttpGet]
        public List<Customer2ViewModel> ListCustomers()
        {
            List<Customer2ViewModel> listCustomer = new List<Customer2ViewModel>();

            var list = (from a in _context.Customers
                        join b in _context.CustomerTypes
                        on a.CustomerTypeId equals b.Id
                        select new {
                            Id = a.Id,
                            CustName=a.CustName,
                            Address = a.Adress,
                            Status = a.Status,
                            CustomerType = b.Description
                        }).ToList();

            foreach(var item in list)
            {
                Customer2ViewModel customer = new Customer2ViewModel();
                customer.Id = item.Id;
                customer.CustName = item.CustName;
                customer.Address = item.Address;
                customer.Status = item.Status;
                customer.CustomerType = item.CustomerType;

                listCustomer.Add(customer);
            }

            return listCustomer;
        }

        public Customer GetCustomer(int id)
        {
            var customer = _context.Customers.Where(x => x.Id == id).FirstOrDefault();
            return customer;
        }
        public RedirectToActionResult SaveChangeCustomer(CustomerViewModel newCustomer)
        {
            Customer customer = new Customer();

            if (newCustomer.Id == 0)
            {

                customer.CustName = newCustomer.CustName;
                customer.Adress = newCustomer.Adress;
                customer.Status = newCustomer.Status;
                customer.CustomerTypeId = newCustomer.CustomerTypeId;

                _context.Add(customer);
                _context.SaveChanges();
            }
            else
            {
                customer = _context.Customers.Find(newCustomer.Id);
                customer.CustName = newCustomer.CustName;
                customer.Adress = newCustomer.Adress;
                customer.Status = newCustomer.Status;
                customer.CustomerTypeId = newCustomer.CustomerTypeId;

                _context.Entry(customer).State = EntityState.Modified;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public RedirectToActionResult DeleteCustomer(int ID)
        {
            var customer  = _context.Customers.Find(ID);

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
