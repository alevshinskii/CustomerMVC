using System.Linq;
using CustomerManagement.Entities;
using CustomerManagement.Interfaces;
using CustomerManagement.Repositories;
using System.Web.Mvc;

namespace CustomerWebMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IRepository<Customer> _customerRepository=new CustomerRepository();
        private readonly IRepository<Address> _addressRepository=new AddressRepository();
        private readonly IRepository<Note> _noteRepository=new NoteRepository();

        public int ItemsOnPage = 10;

        public CustomerController() { }

        public CustomerController(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public ActionResult Index()
        {
            var customers = _customerRepository.ReadAll().Take(ItemsOnPage).ToList();
            
            return View(customers);
        }

        public ActionResult Index(int page)
        {
            var customers = _customerRepository.ReadAll();

            return View(customers);
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

            if (_customerRepository.Create(customer) != null)
            {
                return RedirectToAction("Index");
            }
                
            ViewBag.Message = "Can't add new customer to database";
            return View();
        }

        public ActionResult Edit(int id)
        {
            var customer = _customerRepository.Read(id);

            if(customer!=null)
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

            if (_customerRepository.Update(customer))
            {
                return RedirectToAction("Index");
            }

            ViewBag.Message = "Can't update customer in database";
            return View(customer);
        }

        public ActionResult Details(int id)
        {
            var customer = _customerRepository.Read(id);

            if (customer != null)
            {
                customer.Addresses = _addressRepository.ReadAll(id);
                customer.Notes = _noteRepository.ReadAll(id);

                return View(customer);
            }

            return new HttpNotFoundResult();
        }

        public ActionResult Delete(int id)
        {
            var customer = _customerRepository.Read(id);

            if(customer!=null)
                return View(customer);

            return new HttpNotFoundResult();
        }

        [HttpPost]
        public ActionResult Delete(Customer customer)
        {
            if (_customerRepository.Delete(customer.Id))
            {
                return RedirectToAction("Index");
            }

            ViewBag.Message = "Can't delete customer from database";
            return View(customer);
        }
    }
}