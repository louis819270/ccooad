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

        public ActionResult bmr()
        {
            ViewData["ResultBMRB"] = Session["ResultBMRB"];
            ViewData["ResultBMRG"] = Session["ResultBMRG"];
            Session["ResultBMRB"] = null;
            Session["ResultBMRG"] = null;

            return View();
        }

        // POST: Calculate/CalculateTDEE
        [HttpPost]
        public ActionResult CalculateBMR(int inputHeight, int inputWeight, int inputAge)
        {
            try
            {
                double ResultBMRB = (10 * (double)inputWeight) + (6.25 * (double)inputHeight) - (5 * (double)inputAge) + 5;
                double ResultBMRG = (10 * (double)inputWeight) + (6.25 * (double)inputHeight) - (5 * (double)inputAge) - 161;
                Session["ResultBMRB"] = Math.Round(ResultBMRB).ToString();
                Session["ResultBMRG"] = Math.Round(ResultBMRG).ToString();
                return RedirectToAction("bmr");
            }
            catch
            {
                return RedirectToAction("bmr");
            }
        }
    }
}