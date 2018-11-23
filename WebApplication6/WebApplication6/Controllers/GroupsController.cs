using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class GroupsController : Controller
    {
        private StudentManagementEntities db = new StudentManagementEntities();

        // GET: Groups
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetGroup()
        {

            var group = db.Groups.OrderBy(a => a.GroupName).ToList();
            return Json(new { data = group }, JsonRequestBehavior.AllowGet);

        }

        // GET: Groups/Details/5


        // GET: Groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,GroupName,Privacy,Description,Session,Faculty,Creator")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(group);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}


        [HttpGet]
        public ActionResult Save(int id)
        {
            var v = db.Groups.Where(a => a.ID == id).FirstOrDefault();
            return View(v);

        }

        [HttpPost]
        public ActionResult Save(Group gr)
        {
            bool status = false;
            if (ModelState.IsValid)
            {

                if (gr.ID > 0)
                {
                    //Edit 
                    var v = db.Groups.Where(a => a.ID == gr.ID).FirstOrDefault();
                    if (v != null)
                    {
                        v.GroupName = gr.GroupName;
                        v.Privacy = gr.Privacy;
                        v.Description = gr.Description;
                        v.Session = gr.Session;
                        v.Faculty = gr.Faculty;
                        v.Creator = gr.Creator;
                    }
                }
                else
                {
                    //Save
                    db.Groups.Add(gr);
                }
                db.SaveChanges();
                status = true;

            }
            return new JsonResult { Data = new { status = status } };
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            var v = db.Groups.Where(a => a.ID == id).FirstOrDefault();
            if (v != null)
            {
                return View(v);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteGroup(int id)
        {
            bool status = false;
            var v = db.Groups.Where(a => a.ID == id).FirstOrDefault();
            if (v != null)
            {
                db.Groups.Remove(v);
                db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

    }
}
