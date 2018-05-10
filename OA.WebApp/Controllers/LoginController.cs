using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using OA.BLL;
using OA.Common;
using OA.IBLL;

namespace OA.WebApp.Controllers
{
    public class LoginController : Controller
    {
        public IUserInfoService UserService { get; set; }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowValidateCode()
        {
            ValidateCode validateCode = new ValidateCode();
            string code = validateCode.CreateValidateCode(4);
            Session["validateCode"] = code;
            var result = validateCode.CreateValidateGraphic(code);
            return File(result, "image/jpeg");
        }

        public ActionResult UserLogin()
        {
            string validateCode = Session["validateCode"] != null ? Session["validateCode"].ToString() : string.Empty;
            if (string.IsNullOrEmpty(validateCode))
            {
                return Content("ValidateCode Error");
            }
            Session["validateCode"] = null;
            string txtCode = Request["vCode"];
            if (!validateCode.Equals(txtCode, StringComparison.InvariantCultureIgnoreCase))
            {
                return Content("ValidateCode Error");
            }
            string userName = Request["LoginCode"];
            string userPwd = Request["LoginPwd"];
            var user = UserService.LoadEntities(u => u.UserName == userName && u.UserPass == userPwd).FirstOrDefault();
            if (user != null)
            {
                return Content("OK");
            }
            else
            {
                return Content("NO");
            }

        }
    }
}