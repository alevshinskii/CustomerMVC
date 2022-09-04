using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomerManagement.Entities;
using CustomerManagement.Interfaces;
using CustomerManagement.Repositories;

namespace CustomerWebMVC.Controllers
{
    public class AddressController : Controller
    {
        private readonly IRepository<Address> _addressRepository = new AddressRepository();
        private readonly IRepository<Customer> _customerRepository = new CustomerRepository();

        public AddressController(){}
        public AddressController(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public ActionResult Index(int customerId)
        {
            var addresses = _addressRepository.ReadAll(customerId);
            ViewBag.CustomerName = _customerRepository.Read(customerId)?.LastName ?? customerId.ToString();
            ViewBag.CustomerId = customerId;
            return View(addresses);
        }

        public ActionResult Create(int customerId)
        {
            var address = new Address() { CustomerId = customerId };
            return View(address);
        }

        [HttpPost]
        public ActionResult Create(Address address)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Address is not valid";
                return View(address);
            }

            if (_addressRepository.Create(address) != null)
            {
                return RedirectToAction("Index",new {customerId=address.CustomerId});
            }

            ViewBag.Message = "An error occured while adding address in database";
            return View(address);
        }

        public ActionResult Edit(int addressId)
        {
            var address = _addressRepository.Read(addressId);

            if (address != null)
            {
                return View(address);
            }

            return new HttpNotFoundResult();
        }

        [HttpPost]
        public ActionResult Edit(Address address)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Address is not valid";
                return View(address);
            }

            if (_addressRepository.Update(address))
            {
                return RedirectToAction("Index",new {customerId=address.CustomerId});
            }

            ViewBag.Message = "An error occured while updating address in database";
            return View(address);
        }

        public ActionResult Delete(int addressId)
        {
            var address = _addressRepository.Read(addressId);

            if (address != null)
            {
                return View(address);
            }

            return new HttpNotFoundResult();
        }

        [HttpPost]
        public ActionResult Delete(Address address)
        {
            int? customerId = _addressRepository.Read(address.AddressId)?.CustomerId;

            if (_addressRepository.Delete(address.AddressId))
            {
                return RedirectToAction("Index",new {customerId});
            }

            ViewBag.Message = "An error occured while deleting address in database";
            return View(address);
        }
    }
}