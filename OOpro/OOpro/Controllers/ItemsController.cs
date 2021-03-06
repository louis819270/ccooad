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
    public class ItemsController : Controller
    {
        private OOproDB db = new OOproDB();

        // GET: Items
        public ActionResult Index()
        {
            if (TempData["Search"] != null)
            {
                string search = TempData["Search"].ToString();
                string Type = TempData["Type"].ToString();
                TempData["Search"] = null;
                if (Type != "1")
                {
                    var result1 = (from s in db.Item
                                   where (s.Type == Type)
                                   select s);

                    var result2 = (from s in result1
                                   where (s.Name.Contains(search))
                                   select s).ToList();

                    return View(result2);
                }

                var result = (from s in db.Item
                              where (s.Name.Contains(search))
                              select s).ToList();
                return View(result);

            }
            return View(db.Item.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string a)
        {
            TempData["Search"] = a;
            return RedirectToAction("Index", "Items");
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Item.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(int? id, int count)
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Users", new { area = "" });
            int ItemID = Convert.ToInt32(id);
            int UserID = Convert.ToInt32(Session["UserID"]);
            if (db.Cart.AsNoTracking().FirstOrDefault(a => a.ItemID == ItemID && a.UserID == UserID) != null)
            {
                TempData["message"] = "已存在購物車";
                return RedirectToAction("Index", "Items", new { area = "" });
            }
            if (count > db.Item.AsNoTracking().FirstOrDefault(a => a.ID == id).Count)
            {
                TempData["message"] = "庫存不足";
                return RedirectToAction("Index", "Items", new { area = "" });
            }
            TempData["ItemID"] = ItemID;
            TempData["count"] = count;
            TempData["UserID"] = UserID;
            return RedirectToAction("Create", "ShoppingCart", new { area = "" });
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Picture,Price,Count,Description")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Item.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Item.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Picture,Price,Count,Description")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Item.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Item.Find(id);
            db.Item.Remove(item);
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
