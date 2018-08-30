using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.Model;
using NHibernate.Criterion;
using NHibernate.Transform;
using NHibernate.Linq;
using NHibernate;
using System.Diagnostics;
namespace ZLERP.NHibernateRepository
{
    public class YearAccountRepository : NHRepositoryBase<YearAccount>, IYearAccountRepository
    {
        public YearAccountRepository(ISession session)
        :base(session)
        { 
        
        }


        #region IYearAccountRepository 成员

        public void ExecuteCreate(string path,string name,string spath)
        {
            try
            {
                IUnitOfWorkFactory factory = new UnitOfWorkFactory();

                string sp = "exec sp_BuildDB @path=:path,@name=:name,@spath=:spath";
                var query = this._session.CreateSQLQuery(sp);
                query.SetString("path", path);
                query.SetString("name", name);
                query.SetString("spath", spath);
                //query.ExecuteUpdate();

                object ret = query.UniqueResult();

                if (ret == null)
                    throw new Exception("帐套建立失败，请确认相关信息是否正确！");
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Run(YearAccount entity)
        {
            try
            {
                ///TO DO：
                ///根据时间删除数据
                IUnitOfWorkFactory factory = new UnitOfWorkFactory();
                string sp = "exec sp_ResetDB @BeginDate=:BeginDate";
                var query = this._session.CreateSQLQuery(sp);
                query.SetString("BeginDate", entity.BeginDate.ToString());
                query.ExecuteUpdate();  
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        
    }
}
