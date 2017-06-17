using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OOpro.Models;

namespace OOpro.Controllers
{
    public class SavesController : Controller
    {
        private OOproDB db = new OOproDB();

        // GET: Saves
        public ActionResult Index()
        {
            if (Session["userID"] == null) {
                return RedirectToAction("Login", "Users");
            }
            int user_id = Convert.ToInt32(Session["UserID"]);
            var save = (from s in db.Save where (s.UserID == user_id) select s);
            return View(save.ToList());
        }

        // GET: Saves/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Save save = db.Save.Find(id);
            if (save == null)
            {
                return HttpNotFound();
            }
            return View(save);
        }

        // GET: Saves/Create
        public ActionResult Create()
        {
            int user_id = Convert.ToInt32(Session["UserID"]);
            var save_state = (from l in db.Save where (l.UserID == user_id) select l).ToList();
            if (save_state[0].Money != 0) {
                return RedirectToAction("Index");
            }
                
            ViewBag.UserID = new SelectList(db.User, "ID", "Account");
            return View();
        }

        // POST: Saves/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,Money,Time")] Save save)
        {
            save.Time = DateTime.Now;
           
            if (ModelState.IsValid)
            {
                save.Money = 1;
                db.Save.Add(save);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.User, "ID", "Account", save.UserID);
            return View(save);
        }

        private void MessageBox(string v)
        {
            throw new NotImplementedException();
        }

        // GET: Saves/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Save save = db.Save.Find(id);
            if (save == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.User, "ID", "Account", save.UserID);


           


            return View(save);
        }

        // POST: Saves/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,Money,Time")] Save save)
        {
            int user_id = Convert.ToInt32(Session["UserID"]);
            save.Time = DateTime.Now;
            save.UserID = db.Save.AsNoTracking().FirstOrDefault(a => a.UserID == user_id).UserID;
            save.Money = db.Save.AsNoTracking().FirstOrDefault(a => a.ID == save.ID).Money + save.Money;

            if (ModelState.IsValid)
            {
                db.Entry(save).State = EntityState.Modified;

                db.SaveChanges();
                Session["Money"] = db.Save.AsNoTracking().FirstOrDefault(a => a.UserID == user_id).Money;
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.User, "ID", "Account", save.UserID);


            return View(save);
        }

        // GET: Saves/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Save save = db.Save.Find(id);
            if (save == null)
            {
                return HttpNotFound();
            }
            return View(save);
        }

        // POST: Saves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Save save = db.Save.Find(id);
            db.Save.Remove(save);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
