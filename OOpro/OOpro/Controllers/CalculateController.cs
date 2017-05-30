using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OOpro.Controllers
{
    public class CalculateController : Controller
    {
        // GET: Calculate
        public ActionResult Index()
        {
            return RedirectToAction("bmi");
        }

        public ActionResult bmi()
        {
            ViewData["ResultBMI"] = Session["ResultBMI"];
            Session["ResultBMI"] = null;

            return View();
        }

        // POST: Calculate/CalculateBMI
        [HttpPost]
        public ActionResult CalculateBMI(int inputHeight, int inputWeight)
        {
            try
            {
                double ResultBMI = (double)inputWeight / ((double)(inputHeight * inputHeight) / 10000);
                Session["ResultBMI"] = Math.Round(ResultBMI).ToString();
                return RedirectToAction("bmi");
            }
            catch
            {
                return RedirectToAction("bmi");
            }
        }

        public ActionResult tdee()
        {
            ViewData["ResultTDEE"] = Session["ResultTDEE"];
            Session["ResultTDEE"] = null;

            return View();
        }

        // POST: Calculate/CalculateTDEE
        [HttpPost]
        public ActionResult CalculateTDEE(FormCollection collection)
        {
            try
            {
                // TODO 不會算
                // double ResultTDEE = (double)collection.GetKey( / ((double)(inputHeight * inputHeight) / 10000);
                // Session["ResultTDEE"] = Math.Round(ResultTDEE).ToString();
                return RedirectToAction("tdee");
            }
            catch
            {
                return RedirectToAction("tdee");
            }
        }

        
    }
}
