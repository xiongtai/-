using OA.DAL;
using OA.DALFactory;
using OA.IDAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DalFactory
{
    /// <summary>
    /// Data Session,like a Factory class
    /// </summary>
    public class DBSession
    {
        public DbContext Db
        {
            get
            {
                return DBContextFactory.Create();
            }            
        }
        private IUserInfoDal userInfoDal;
        public IUserInfoDal CreateUserInfoDal
        {
            get
            {
                if (userInfoDal == null)
                {
                    userInfoDal = AbstractFactory
                }
                return userInfoDal;
            }
            set
            {
                userInfoDal = value;
            }
        }

        public bool SaveChanges()
        {
            return Db.SaveChanges() > 0;
        }
    }
}
