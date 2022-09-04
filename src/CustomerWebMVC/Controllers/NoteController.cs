using System.Net;
using System.Web.Mvc;
using CustomerManagement.Entities;
using CustomerManagement.Interfaces;
using CustomerManagement.Repositories;

namespace CustomerWebMVC.Controllers
{
    public class NoteController : Controller
    {
        private readonly IRepository<Note> _noteRepository = new NoteRepository();
        private readonly IRepository<Customer> _customerRepository = new CustomerRepository();
        public NoteController(){}

        public NoteController(IRepository<Note> noteRepository)
        {
            _noteRepository = noteRepository;
        }
        public ActionResult Index(int customerId)
        {
            var notesList = _noteRepository.ReadAll(customerId);

            return View(notesList);
        }

        [HttpPost]
        public ActionResult Create(int customerId)
        {
            var note = new Note()
            {
                CustomerId = customerId
            };

            return View(note);
        }

        [HttpPost]
        public ActionResult Create(Note note)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Note is not valid";
                return View(note);
            }

            if (_noteRepository.Create(note) != null)
            {
                return RedirectToAction("Index",new {customerId=note.CustomerId});
            }

            ViewBag.Message = "An error occured while adding note in database";
            return View(note);
        }

        public ActionResult Edit(int id)
        {
            var note = _noteRepository.Read(id);
            if(note!=null)
                return View(note);

            return RedirectToRoute("NotFound");
        }

        [HttpPost]
        public ActionResult Edit(Note note)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Note is not valid";
                return View(note);
            }

            if (_noteRepository.Update(note))
            {
                return RedirectToAction("Index",new {customerId=note.CustomerId});
            }

            ViewBag.Message = "An error occured while updating note in database";
            return View(note);
        }

        public ActionResult Delete(int id)
        {
            var note = _noteRepository.Read(id);

            if(note!=null)
                return View(note);

            return RedirectToRoute("NotFound");
        }
        [HttpPost]
        public ActionResult Delete(Note note)
        {
            if (_noteRepository.Delete(note.Id))
            {
                return RedirectToAction("Index",new {customerId=note.CustomerId});
            }

            ViewBag.Message = "An error occured while deleting note in database";
            return View(note);
        }
    }
}