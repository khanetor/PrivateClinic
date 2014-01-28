namespace PrivateClinic.Controllers
{
    using PrivateClinic.Models;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class PatientController : CommonController
    {
        // GET: /Patient/
        public async Task<ActionResult> Index()
        {
            var patients = GetPatientsForCurrentUser();
            return View(await patients.ToListAsync());
        }

        // GET: /Patient/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            // Patient patient = db.Patients.Find(id);
            var patient = await GetPatientsForCurrentUser().FirstOrDefaultAsync(p => p.ID == id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: /Patient/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Patient/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ID,Name,DOB,Phone,Email,Address")] Patient patient)
        {
            ModelState.Remove("UserId");
            if (ModelState.IsValid)
            {
                patient.UserId = GetCurrentUserId();
                db.Patients.Add(patient);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        // GET: /Patient/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var patient = await GetPatientsForCurrentUser().FirstOrDefaultAsync(p => p.ID == id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: /Patient/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ID,Name,DOB,Phone,Email,Address")] Patient patient)
        {
            ModelState.Remove("UserId");
            if (ModelState.IsValid)
            {
                patient.UserId = GetCurrentUserId();
                db.Entry(patient).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        // GET: /Patient/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = await GetPatientsForCurrentUser().FirstOrDefaultAsync(p => p.ID == id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: /Patient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Patient patient = await GetPatientsForCurrentUser().FirstOrDefaultAsync(p => p.ID == id);
            db.Patients.Remove(patient);
            await db.SaveChangesAsync();
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

        // Get a list of patients that belong to the current user
        private IQueryable<Patient> GetPatientsForCurrentUser()
        {
            var currentUserId = GetCurrentUserId();
            var patients = db.Patients.Where(p => p.UserId == currentUserId);
            return patients;
        }
    }
}
