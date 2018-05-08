using OA.DALFactory;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OA.IDAL;
using OA.IBLL;

namespace OA.BLL
{
    public class UserInfoService : BaseService<UserInfo>,IUserInfoService
    {
        public bool DeleteEntities(List<int> list)
        {
            var userInfoList = CurrentDBSession.UserInfoDal.LoadEntities(u => list.Contains(u.Id));
            foreach (var user in userInfoList)
            {
                CurrentDBSession.UserInfoDal.DeleteEntity(user);
            }
            return this.CurrentDBSession.SaveChanges();
        }

        public override void SetCurrentDal()
        {
            CurrentDal = CurrentDBSession.UserInfoDal;            
        }
    }
}
