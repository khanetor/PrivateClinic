namespace PrivateClinic.Controllers
{
    using PrivateClinic.Models;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class PatientDocController : CommonController
    {
        // GET: /PatientDoc/
        public async Task<ActionResult> Index(int? patientId)
        {
            if (patientId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var patientDocs = GetPatientDocsForCurrentUser(patientId);
            ViewBag.PatientId = patientId;
            return View(await patientDocs.ToListAsync());
        }

        // GET: /PatientDoc/Details/5
        public async Task<ActionResult> Details(int? patientId, int? id)
        {
            if (id == null || patientId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var patientdoc = await GetPatientDocsForCurrentUser(patientId).FirstOrDefaultAsync(d => d.ID == id);
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
        public async Task<ActionResult> Create([Bind(Include="ID,PatientId,Date,Diagnosis,Symptom,Lab,Treatment,Result,Fee")] PatientDoc patientdoc)
        {
            ModelState.Remove("UserId");
            if (ModelState.IsValid)
            {
                patientdoc.UserId = GetCurrentUserId();
                db.PatientDocuments.Add(patientdoc);

                // Also update the date of last visit
                var patient = await GetPatientsForCurrentUser().FirstOrDefaultAsync(p => p.ID == patientdoc.PatientId);
                patient.DOLV = patientdoc.Date;
                db.Entry(patient).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Index", new { patientId = patientdoc.PatientId });
            }

            ViewBag.PatientId = patientdoc.PatientId;
            return View(patientdoc);
        }

        // GET: /PatientDoc/Edit/5
        public async Task<ActionResult> Edit(int? patientId, int? id)
        {
            if (id == null || patientId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var patientdoc = await GetPatientDocsForCurrentUser(patientId).FirstOrDefaultAsync(d => d.ID == id);
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
        public async Task<ActionResult> Edit([Bind(Include="ID,PatientId,Date,Diagnosis,Symptom,Lab,Treatment,Result,Fee")] PatientDoc patientdoc)
        {
            ModelState.Remove("UserId");
            if (ModelState.IsValid)
            {
                patientdoc.UserId = GetCurrentUserId();
                db.Entry(patientdoc).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { patientId = patientdoc.PatientId });
            }
            return View(patientdoc);
        }

        // GET: /PatientDoc/Delete/5
        public async Task<ActionResult> Delete(int? patientId, int? id)
        {
            if (id == null || patientId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var patientdoc = await GetPatientDocsForCurrentUser(patientId).FirstOrDefaultAsync(d => d.ID == id);
            if (patientdoc == null)
            {
                return HttpNotFound();
            }
            return View(patientdoc);
        }

        // POST: /PatientDoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int patientId, int id)
        {
            var patientdoc = await GetPatientDocsForCurrentUser(patientId).FirstOrDefaultAsync(d => d.ID == id);
            db.PatientDocuments.Remove(patientdoc);
            await db.SaveChangesAsync();
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

        
    }
}
