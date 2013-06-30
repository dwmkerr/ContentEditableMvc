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

        public ActionResult SaveChanges(string entityId, string propertyName, string propertyValue)
        {
            //  Get the model.
            var model = (new ExampleRepository()).LoadModel();

            //  Check the property to change.
            if (propertyName == "Title")
                model.Title = propertyValue;
            else if (propertyName == "Subtitle")
                model.Subtitle = propertyValue;
            else if (propertyName == "ParagraphText")
                model.ParagraphText = propertyValue;

            //  Save the changes.
            (new ExampleRepository()).SaveModel(model);

            return Json(new {success = true});
        }
    }
}
