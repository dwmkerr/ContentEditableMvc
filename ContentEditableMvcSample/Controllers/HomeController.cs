using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ContentEditableMvc;
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
        /*
        [HttpPost]
        public ActionResult EditContent(string name, string value, string modelData)
        {
            var dataDictionary = (new JavaScriptSerializer()).Deserialize<Dictionary<string, string>>(modelData);

            
        }
        */

        [HttpPost]
        public ActionResult EditContent(ContentEditModel contentEdit)
        {
            var modelId = contentEdit.ModelData["id"];

            //  Get the model.
            var model = (new ExampleRepository()).LoadModel();

            //  Check the property to change.
            if (contentEdit.PropertyName == "Title")
                model.Title = contentEdit.NewValue;
            else if (contentEdit.PropertyName == "Subtitle")
                model.Subtitle = contentEdit.NewValue;
            else if (contentEdit.PropertyName == "ParagraphText")
                model.ParagraphText = contentEdit.NewValue;

            //  Save the changes.
            (new ExampleRepository()).SaveModel(model);

            return Json(new { success = true });
        }
    }
}
