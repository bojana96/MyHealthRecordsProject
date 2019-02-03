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
    public class AppointmentsController : Controller
    {
        private ApplicationDbContext db;
        protected UserManager<ApplicationUser> UserManager;
        public AppointmentsController()
        {
            db = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));

        }

        // GET: Appointments
        public ActionResult Index()
        {
            return View(db.Appointments.ToList());
        }
        [Authorize(Roles ="Patient")]
        public ActionResult RequestAppointment(int id)
        {
            var model = new DoctorPatientAppointment();
            model.DoctorId= id;
            var user = UserManager.FindById(User.Identity.GetUserId());
            Patient patient = db.Patients.Where(z => z.Embg.Equals(user.Embg)).FirstOrDefault();
            model.PatientId = patient.Id;
            return View(model);


        }
        [Authorize(Roles ="Doctor")]
        public ActionResult ShowAppointments()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            Doctor doctor = db.Doctors.Where(z => z.Embg.Equals(user.Embg)).FirstOrDefault();
            List<Appointment> appointments = db.Appointments.Where(z => z.doctor.Id.Equals(doctor.Id)).ToList();
            return View(appointments);
        }
        [Authorize(Roles ="Patient")]
        public ActionResult ShowAppointmentsPatient()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            Patient patient = db.Patients.Where(z => z.Embg.Equals(user.Embg)).FirstOrDefault();
            List<Appointment> appointments = db.Appointments.Where(z => z.patient.Id.Equals(patient.Id)).ToList();
            return View(appointments);
        }
        [HttpPost]
        [Authorize(Roles ="Patient")]
        public ActionResult RequestAppointment(DoctorPatientAppointment model)
        {
            if (ModelState.IsValid)
            {
                Appointment appointment = new Appointment();

                appointment.Date = model.Date;
                appointment.FirstName = model.FirstName;
                appointment.LastName = model.LastName;
                appointment.Condition = model.Condition;
                appointment.Age = model.Age;
                var patient = db.Patients.Where(z => z.Id.Equals(model.PatientId)).FirstOrDefault();
                var doctor = db.Doctors.Where(z => z.Id.Equals(model.DoctorId)).FirstOrDefault();
                appointment.patient = patient;
                appointment.doctor = doctor;
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(model);
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Age,Date,Condition")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Age,Date,Condition")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        [Authorize(Roles ="Patient")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
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
