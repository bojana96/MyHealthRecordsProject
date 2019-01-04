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
    public class DiagnosesController : Controller
    {
        private ApplicationDbContext db;
        protected UserManager<ApplicationUser> UserManager;
        public DiagnosesController()
        {
            db = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));

        }
        // GET: Diagnoses
        public ActionResult Index()
        {
            return View(db.Diagnoses.ToList());
        }

        public ActionResult AddDiagnose (int id)
        {
            var model = new DoctorPatientDiagnose();
            model.PatientId = id;
            var user = UserManager.FindById(User.Identity.GetUserId());
            Doctor doctor = db.Doctors.Where(z => z.Embg.Equals(user.Embg)).FirstOrDefault();
            model.DoctorId = doctor.Id;
            return View(model);


        }

        [HttpPost]
        public ActionResult AddDiagnose (DoctorPatientDiagnose model)
        {
            if (ModelState.IsValid)
            {
                Diagnose diagnose = new Diagnose();
                diagnose.Date = model.Date;
                diagnose.Description = model.Description;
                var patient = db.Patients.Where(z => z.Id.Equals(model.PatientId)).FirstOrDefault();
                var doctor = db.Doctors.Where(z => z.Id.Equals(model.DoctorId)).FirstOrDefault();
                diagnose.Patient = patient;
                diagnose.Doctor = doctor;
                db.Diagnoses.Add(diagnose);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(model);
        }

        public ActionResult ShowDiagnoses()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            Patient patient = db.Patients.Where(z => z.Embg.Equals(user.Embg)).FirstOrDefault();
            List<Diagnose> diagnoses = db.Diagnoses.Where(z => z.Id.Equals(patient.Id)).ToList();
            return View(diagnoses);
        }

       
        // GET: Diagnoses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diagnose diagnose = db.Diagnoses.Find(id);
            if (diagnose == null)
            {
                return HttpNotFound();
            }
            return View(diagnose);
        }

        // GET: Diagnoses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Diagnoses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,Description")] Diagnose diagnose)
        {
            if (ModelState.IsValid)
            {
                db.Diagnoses.Add(diagnose);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(diagnose);
        }

        // GET: Diagnoses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diagnose diagnose = db.Diagnoses.Find(id);
            if (diagnose == null)
            {
                return HttpNotFound();
            }
            return View(diagnose);
        }

        // POST: Diagnoses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Description")] Diagnose diagnose)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diagnose).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(diagnose);
        }

        // GET: Diagnoses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diagnose diagnose = db.Diagnoses.Find(id);
            if (diagnose == null)
            {
                return HttpNotFound();
            }
            return View(diagnose);
        }

        // POST: Diagnoses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Diagnose diagnose = db.Diagnoses.Find(id);
            db.Diagnoses.Remove(diagnose);
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
