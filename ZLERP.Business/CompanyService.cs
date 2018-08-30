using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;

namespace ZLERP.Business
{
    public class CompanyService : ServiceBase<Company>
    {
        internal CompanyService(IUnitOfWork uow) :base(uow){ 
            
        }



        /// <summary>
        /// 得到登录用户所在的公司
        /// </summary>
        /// <returns></returns>
        public Company GetCurrentCompany()
        {
            Company factory = null;
            int? currentCompanyID = null;
            if (AuthorizationService.CurrentUserInfo.Department != null)
            {
                Department currentDepartment = AuthorizationService.CurrentUserInfo.Department;
                if (currentDepartment.Company != null)
                {
                    currentCompanyID = currentDepartment.Company.ID;
                }
            }
            if (currentCompanyID == null)
            {
                factory = this.Query().ToList().FirstOrDefault();
            }
            else
            {
                factory = this.Query().FirstOrDefault(p => p.ID == currentCompanyID);
            }
            if (factory.Longtide == null || factory.Latitude == null)
            {
                factory.Longtide = 112.88677443;
                factory.Latitude = 28.21513581;
            }
            if (factory.Range == null)
                factory.Range = 500;
            
            return factory;
        }
    }

   
}
