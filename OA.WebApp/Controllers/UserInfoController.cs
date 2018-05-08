using OA.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA.Model;
using OA.IBLL;

namespace OA.WebApp.Controllers
{
    public class UserInfoController : Controller
    {
        public UserInfoService UserService { get; set; }
        // GET: UserInfo
        public ActionResult Index(UserInfo user)
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetUserInfoList()
        {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 2;

            int totalCount;
            var list = UserService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, u => u.DelFlag == false, u => u.Id, true);
            var temp = from u in list
                       select new
                       {
                           Id = u.Id,
                           UName = u.UserName,
                           UPwd = u.UserPass,
                           Email = u.EMail,
                           Time = u.RegTime
                       };
            return Json(new { rows = temp, total = totalCount });
        }

        public ActionResult DeleteUserInfo()
        {
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            foreach (string id in strIds)
            {
                list.Add(Convert.ToInt32(id));
            }
            if (UserService.DeleteEntities(list))
            {
                return Content("OK");
            }
            else
            {
                return Content("NO");
            }
                                                                
        }

        public ActionResult AddUserInfo(UserInfo user)
        {
            user.DelFlag = false;
            user.RegTime = DateTime.Now;
            UserService.AddEntity(user);
            return Content("OK");
        }

        public ActionResult ShowEditInfo()
        {
            int id = int.Parse(Request["id"]);
            UserInfo user = UserService.LoadEntities(u => u.Id == id).FirstOrDefault();
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditUserInfo(UserInfo user)
        {
            if (UserService.EditEntity(user))
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
