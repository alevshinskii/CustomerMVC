using System.Web.Mvc;

namespace CustomerWebMVC.Controllers
{
    public class NoteController : Controller
    {
        public ActionResult Index(int customerId)
        {
            return View();
        }

        public ActionResult Create()
        {
            throw new System.NotImplementedException();
        }

        public ActionResult Edit(int id)
        {
            throw new System.NotImplementedException();
        }

        public ActionResult Details(int id)
        {
            throw new System.NotImplementedException();
        }

        public ActionResult Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}