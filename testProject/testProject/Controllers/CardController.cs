using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Serialization;
using testProject.Models;
using testProject.Validators;

namespace testProject.Controllers
{
    public class CardController : Controller
    {
        public ActionResult CardList([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                List<Card> cards = new List<Card>();
                XDocument doc = XDocument.Load(Server.MapPath("~/" + "file.xml"));
                foreach (var card in doc.Element("cards").Elements("card"))
                {
                    cards.Add(new Card(
                        card.Element("bills").Value,
                        card.Element("amout").Value
                        ));
                }
                DataSourceResult result = cards.ToDataSourceResult(request);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult Upload()
        {
            string ext = "";
            foreach (string file in Request.Files)
            {
                var upload = Request.Files[file];
                if (upload != null)
                {
                    string fileName = System.IO.Path.GetFileName(upload.FileName);
                    string[] temp = fileName.Split('.');
                     ext = temp[temp.Length - 1];
                    XDocument doc = XDocument.Load(new StreamReader(upload.InputStream));
                    foreach (var card in doc.Element("cards").Elements("card"))
                    {
                        var bills = card.Element("bills").Value;
                        if (bills.Length != 0)
                        {
                            if (!Validate.BillsValidation(bills))
                                throw new Exception("Error Bills Validation!");
                        }
                        var amout = card.Element("amout").Value;
                        if (amout.Length != 0)
                        {
                            if (!Validate.AmoutValidation(amout))
                                throw new Exception("Error Amout Validation!");
                        }
                    }
                    if (ext == "xml")
                        upload.SaveAs(Server.MapPath("~/" + "file.xml"));
                }
            }
            if (ext == "xml")
            {
                return Json("File uploaded.");
            }
            else return Json("Error upload file");
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}