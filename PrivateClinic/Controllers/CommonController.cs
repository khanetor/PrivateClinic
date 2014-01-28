namespace PrivateClinic.Controllers
{
    using Microsoft.AspNet.Identity;
    using PrivateClinic.DAL;
    using PrivateClinic.Models;
    using System.Linq;
    using System.Web.Mvc;

    public class CommonController : Controller
    {
        protected PrivateClinicContext db = new PrivateClinicContext();


        // Get the current user id
        protected string GetCurrentUserId()
        {
            var currentUserId = User.Identity.GetUserId();
            return currentUserId;
        }

        // Get a list of patients that belong to the current user
        protected IQueryable<Patient> GetPatientsForCurrentUser()
        {
            var currentUserId = GetCurrentUserId();
            var patients = db.Patients.Where(p => p.UserId == currentUserId);
            return patients;
        }

        // Get a list of patient documents that belong to the current user
        protected IQueryable<PatientDoc> GetPatientDocsForCurrentUser(int patientId)
        {
            var currentUserId = GetCurrentUserId();
            var patientDocs = db.PatientDocuments.Where(d => d.UserId == currentUserId && d.PatientId == patientId);
            return patientDocs;
        }

        // Get a list of patient documents that belong to the current user
        protected IQueryable<PatientDoc> GetPatientDocsForCurrentUser(int? patientId)
        {
            if (patientId == null)
            {
                return null;
            }
            var pid = (int)patientId;
            return GetPatientDocsForCurrentUser(pid);
        }
    }
}