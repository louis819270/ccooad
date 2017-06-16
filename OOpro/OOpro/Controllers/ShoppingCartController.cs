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
    public class ShoppingCartController : Controller
    {
        private OOproDB db = new OOproDB();

        // GET: ShoppingCart
        public ActionResult Index()
        {
            ////////////////////////////////////////////////
            Session["UserID"] = 1;
            ////////////////////////////////////////////////
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }
            var cart = db.Cart.Include(c => c.Item).Include(c => c.User);
            return View(cart.ToList());
        }

        // GET: ShoppingCart/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Cart.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // GET: ShoppingCart/Create
        public ActionResult Create()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Users", new { area = "" });
            if (TempData["UserID"] == null || TempData["ItemID"] == null || TempData["count"] == null)
                return RedirectToAction("Index", "Home", new { area = "" });

            Cart cart = new Cart();
            cart.UserID = Convert.ToInt32(TempData["UserID"]);
            cart.ItemID = Convert.ToInt32(TempData["ItemID"]);
            cart.Number = Convert.ToInt32(TempData["count"]);
            TempData.Clear();
            if (ModelState.IsValid)
            {
                db.Cart.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Index", "Items", new { area = "" });
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // POST: ShoppingCart/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,ItemID,Number")] Cart cart)
        {

            if (ModelState.IsValid)
            {
                db.Cart.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemID = new SelectList(db.Item, "ID", "Name", cart.ItemID);
            ViewBag.UserID = new SelectList(db.User, "ID", "Account", cart.UserID);
            return View(cart);
        }

        // GET: ShoppingCart/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Cart.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemID = new SelectList(db.Item, "ID", "Name", cart.ItemID);
            ViewBag.UserID = new SelectList(db.User, "ID", "Account", cart.UserID);
            return View(cart);
        }

        // POST: ShoppingCart/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,ItemID,Number")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemID = new SelectList(db.Item, "ID", "Name", cart.ItemID);
            ViewBag.UserID = new SelectList(db.User, "ID", "Account", cart.UserID);
            return View(cart);
        }

        // GET: ShoppingCart/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Cart.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }

            db.Cart.Remove(cart);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: ShoppingCart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }
            Cart cart = db.Cart.Find(id);
            db.Cart.Remove(cart);
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
