using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HealthRecords.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HealthRecords.Controllers
{
    [Authorize]
    public class ReceiptsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        protected UserManager<ApplicationUser> UserManager;
        public ReceiptsController()
        {
            db = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));

        }
        // GET: Receipts
        public ActionResult Index()
        {
            return View(db.Receipts.ToList());
        }
        public ActionResult ShowReceipts()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            Patient patient = db.Patients.Where(z => z.Embg.Equals(user.Embg)).FirstOrDefault();
            List<Receipt> receipts = db.Receipts.Where(z => z.Id.Equals(patient.Id)).ToList();
            return View(receipts);
        }
        public ActionResult AddReceipt(int id)
        {
            var model = new DoctorPatientReceipt();
            model.PatientId = id;
            var user = UserManager.FindById(User.Identity.GetUserId());
            Doctor doctor = db.Doctors.Where(z => z.Embg.Equals(user.Embg)).FirstOrDefault();
            model.DoctorId = doctor.Id;
            return View(model);


        }

        [HttpPost]
        public ActionResult AddReceipt(DoctorPatientReceipt model)
        {
            if (ModelState.IsValid)
            {
                Receipt receipt = new Receipt();
                receipt.Date = model.Date;
                receipt.Description = model.Description;
                var patient = db.Patients.Where(z => z.Id.Equals(model.PatientId)).FirstOrDefault();
                var doctor = db.Doctors.Where(z => z.Id.Equals(model.DoctorId)).FirstOrDefault();
                receipt.Patient = patient;
                receipt.Doctor = doctor;
                db.Receipts.Add(receipt);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(model);
        }
        // GET: Receipts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receipt receipt = db.Receipts.Find(id);
            if (receipt == null)
            {
                return HttpNotFound();
            }
            return View(receipt);
        }

        // GET: Receipts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Receipts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,Description")] Receipt receipt)
        {
            if (ModelState.IsValid)
            {
                db.Receipts.Add(receipt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(receipt);
        }

        // GET: Receipts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receipt receipt = db.Receipts.Find(id);
            if (receipt == null)
            {
                return HttpNotFound();
            }
            return View(receipt);
        }

        // POST: Receipts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Description")] Receipt receipt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(receipt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(receipt);
        }

        // GET: Receipts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receipt receipt = db.Receipts.Find(id);
            if (receipt == null)
            {
                return HttpNotFound();
            }
            return View(receipt);
        }

        // POST: Receipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Receipt receipt = db.Receipts.Find(id);
            db.Receipts.Remove(receipt);
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
