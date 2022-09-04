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

        public AddressController(){}
        public AddressController(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }
        // GET: Address
        public ActionResult Index(int customerId)
        {
            var addresses = _addressRepository.ReadAll(customerId);
            return View(addresses);
        }

        public ActionResult Create()
        {
            throw new NotImplementedException();
        }

        public ActionResult Edit(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult Details(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}