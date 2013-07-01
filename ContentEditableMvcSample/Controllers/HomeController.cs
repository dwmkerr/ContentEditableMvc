using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContentEditableMvcSample.Models;

namespace ContentEditableMvcSample.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //  Create our example repository.
            var repository = new ExampleRepository();

            return View(repository.LoadModel());
        }

        [HttpPost]
        public ActionResult EditContent(string id, string name, string value)
        {
            //  Get the model.
            var model = (new ExampleRepository()).LoadModel();

            //  Check the property to change.
            if (name == "Title")
                model.Title = value;
            else if (name == "Subtitle")
                model.Subtitle = value;
            else if (name == "ParagraphText")
                model.ParagraphText = value;

            //  Save the changes.
            (new ExampleRepository()).SaveModel(model);

            return Json(new {success = true});
        }
    }
}
