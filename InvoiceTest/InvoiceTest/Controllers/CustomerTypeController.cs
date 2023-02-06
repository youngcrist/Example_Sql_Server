using DataAccessLayer;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceTest.Controllers
{
    public class CustomerTypeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CustomerTypeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public RedirectToActionResult SaveChangeCustomerType(CustomerType newCustomerType)
        {
            CustomerType customerType = new CustomerType();
            if (newCustomerType.Id == 0)
            {
                _context.Add(newCustomerType);
                _context.SaveChanges();
            }
            else
            {
                customerType = _context.CustomerTypes.Find(newCustomerType.Id);
                customerType.Description = newCustomerType.Description;

                _context.Entry(customerType).State = EntityState.Modified;
                _context.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }
        public CustomerType GetCustomerType(int Id)
        {
            var customerType = _context.CustomerTypes.Where(x => x.Id == Id).FirstOrDefault();
            return customerType;
        }
        public List<CustomerType> ListCustomerType()
        {
            var list = _context.CustomerTypes.ToList();

            return list;
        }
        public RedirectToActionResult DeleteCustomerType(int Id)
        {
            var customer = _context.CustomerTypes.Find(Id);
            _context.Remove(customer);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
