namespace PrivateClinic.Controllers
{
    using PrivateClinic.DAL;
    using PrivateClinic.Models;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    public class PatientDocController : CommonController
    {
        // GET: /PatientDoc/
        public ActionResult Index(int? patientId)
        {
            if (patientId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var pid = (int)patientId;
            var patientDocs = GetPatientDocsForUser(pid);
            ViewBag.PatientId = patientId;
            return View(patientDocs.ToList());
        }

        // GET: /PatientDoc/Details/5
        public ActionResult Details(int? patientId, int? id)
        {
            if (id == null || patientId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var pid = (int)patientId;
            var patientdoc = GetPatientDocsForUser(pid).FirstOrDefault(d => d.ID == id);
            //PatientDoc patientdoc = db.PatientDocuments.Find(id);
            if (patientdoc == null)
            {
                return HttpNotFound();
            }
            return View(patientdoc);
        }

        // GET: /PatientDoc/Create
        public ActionResult Create(int? patientId)
        {
            if (patientId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.PatientId = patientId;
            return View();
        }

        // POST: /PatientDoc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,PatientId,Date,Diagnosis,Lab,Treatment,Result,Fee")] PatientDoc patientdoc)
        {
            ModelState.Remove("UserId");
            if (ModelState.IsValid)
            {
                patientdoc.UserId = GetCurrentUserId();
                //patientdoc.Patient = db.Patients.Find(patientdoc.PatientId);
                db.PatientDocuments.Add(patientdoc);
                db.SaveChanges();
                return RedirectToAction("Index", new { patientId = patientdoc.PatientId });
            }

            ViewBag.PatientId = patientdoc.PatientId;
            return View(patientdoc);
        }

        // GET: /PatientDoc/Edit/5
        public ActionResult Edit(int? patientId, int? id)
        {
            if (id == null || patientId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //PatientDoc patientdoc = db.PatientDocuments.Find(id);
            var pid = (int)patientId;
            var patientdoc = GetPatientDocsForUser(pid).FirstOrDefault(d => d.ID == id);
            if (patientdoc == null)
            {
                return HttpNotFound();
            }
            
            return View(patientdoc);
        }

        // POST: /PatientDoc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,PatientId,Date,Diagnosis,Lab,Treatment,Result,Fee")] PatientDoc patientdoc)
        {
            ModelState.Remove("UserId");
            if (ModelState.IsValid)
            {
                patientdoc.UserId = GetCurrentUserId();
                db.Entry(patientdoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { patientId = patientdoc.PatientId });
            }
            return View(patientdoc);
        }

        // GET: /PatientDoc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientDoc patientdoc = db.PatientDocuments.Find(id);
            if (patientdoc == null)
            {
                return HttpNotFound();
            }
            return View(patientdoc);
        }

        // POST: /PatientDoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PatientDoc patientdoc = db.PatientDocuments.Find(id);
            db.PatientDocuments.Remove(patientdoc);
            db.SaveChanges();
            return RedirectToAction("Index", new { patientId = patientdoc.PatientId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private IQueryable<PatientDoc> GetPatientDocsForUser(int patientId)
        {
            var currentUserId = GetCurrentUserId();
            var patientDocs = db.PatientDocuments.Where(d => d.UserId == currentUserId && d.PatientId == patientId);
            return patientDocs;
        }
    }
}
