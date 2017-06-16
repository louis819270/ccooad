﻿using System;
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
    public class OrdersController : Controller
    {
        private OOproDB db = new OOproDB();

        // GET: Orders
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            var order = db.Order.Include(o => o.Item).Include(o => o.User);
            return View(order.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {

            ViewBag.ItemID = new SelectList(db.Item, "ID", "Name");
            ViewBag.UserID = new SelectList(db.User, "ID", "Account");

            return View();
        }

        // POST: Orders/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            ////////////////////////////////////////////////
            Session["UserID"] = 1;
            ////////////////////////////////////////////////
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            int user_id = Convert.ToInt32(Session["UserID"]);

            var shopcart = (from s in db.Cart where (s.UserID == user_id) select s).ToList();

            foreach (var i in shopcart)
            {
                order.Time = DateTime.Now;
                order.Count = i.Number;
                order.TotalPrice = i.Number * i.Item.Price;
                order.ItemID = i.ItemID;
                order.UserID =i.UserID;
                order.State =0;

                if (ModelState.IsValid)
                {
                    db.Order.Add(order);
                    db.SaveChanges();

                    Cart bye = db.Cart.Find(i.ID);
                    db.Cart.Remove(bye);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");

        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemID = new SelectList(db.Item, "ID", "Name", order.ItemID);
            ViewBag.UserID = new SelectList(db.User, "ID", "Account", order.UserID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,ItemID,Count,Time,TotalPrice,State")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemID = new SelectList(db.Item, "ID", "Name", order.ItemID);
            ViewBag.UserID = new SelectList(db.User, "ID", "Account", order.UserID);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Order.Find(id);
            db.Order.Remove(order);
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
