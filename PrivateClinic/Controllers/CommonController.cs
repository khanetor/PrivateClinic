namespace PrivateClinic.Controllers
{
    using Microsoft.AspNet.Identity;
    using PrivateClinic.DAL;
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
    }
}