using CustomerManagement.Entities;
using CustomerManagement.Interfaces;
using CustomerManagement.Repositories;
using System.Linq;
using System.Web.Mvc;
using CustomerManagement.Services;

namespace CustomerWebMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IService<Customer> _customerService=new CustomerService();

        public int ItemsOnPage = 10;

        public CustomerController() { }

        public CustomerController(IService<Customer> customerService)
        {
            _customerService = customerService;
        }


        public ActionResult Index(int? page)
        {
            var customers = _customerService.GetAll();
            ViewBag.PagesCount = customers.Count() / ItemsOnPage + (customers.Count % 10 > 0 ? 1 : 0);
            if (page > 0)
            {
                var customersOnPage = customers.Skip((int)((page - 1) * ItemsOnPage)).Take(ItemsOnPage).ToList();
                return View(customersOnPage);
            }
            else
            {
                var customersOnPage = customers.Take(ItemsOnPage).ToList();
                return View(customersOnPage);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Customer entity is not valid";
                return View();
            }

            if (_customerService.Create(customer) != null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Message = "Can't add new customer to database";
            return View();
        }

        public ActionResult Edit(int id)
        {
            var customer = _customerService.Get(id);

            if (customer != null)
                return View(customer);

            return new HttpNotFoundResult();
        }

        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Customer entity is not valid";
                return View(customer);
            }

            if (_customerService.Update(customer))
            {
                return RedirectToAction("Index");
            }

            ViewBag.Message = "Can't update customer in database";
            return View(customer);
        }

        public ActionResult Details(int id)
        {
            var customer = _customerService.Get(id);

            if (customer != null)
            {
                return View(customer);
            }

            return new HttpNotFoundResult();
        }

        public ActionResult Delete(int id)
        {
            var customer = _customerService.Get(id);

            if (customer != null)
                return View(customer);

            return new HttpNotFoundResult();
        }

        [HttpPost]
        public ActionResult Delete(Customer customer)
        {
            if (_customerService.Delete(customer.Id))
            {
                return RedirectToAction("Index");
            }

            ViewBag.Message = "Can't delete customer from database";
            return View(customer);
        }
    }
}