using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// TODO HTML未完成 靜態或動態網頁

namespace OOpro.Controllers
{
    public class CurriculumController : Controller
    {
        // GET: MemberPost
        public ActionResult Index()
        {
            return View();
        }

        // GET: MemberPost/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MemberPost/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemberPost/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MemberPost/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MemberPost/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MemberPost/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MemberPost/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
