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
namespace ZLERP.NHibernateRepository
{
    public class ConsMixpropRepository : NHRepositoryBase<ConsMixprop>, IConsMixpropRepository
    {
        public ConsMixpropRepository(ISession session)
        :base(session)
        { 
        
        }

         /// <summary>
        ///  检查施工配比是否超出计量范围
        /// </summary> 
        /// <param name="taskId"></param>
        /// <param name="productLineId"></param>
        /// <param name="concreteRZ">混凝土容重</param>
        /// <param name="slurryRZ">砂浆容重</param>
        /// <param name="checkConcrete">是否检查混凝土配比</param>
        /// <param name="checkSlurry">是否检查砂浆配比</param>
        /// <returns>未超出返回null, 否则返回超秤的错误信息</returns>
        public string CheckMesureScale(string taskId, string productLineId, decimal concreteRZ, decimal slurryRZ, bool checkConcrete, bool checkSlurry)
        {
            string sp = "exec sp_dispatch_check_measurescale @TaskID=:TaskID, @ProductLineID=:ProductLineID, @ConcreteRZ=:ConcreteRZ, @SlurryRZ=:SlurryRZ, @CheckConcrete=:CheckConcrete, @CheckSlurry=:CheckSlurry";

            var query = this._session.CreateSQLQuery(sp);
            query.SetString("TaskID", taskId);
            query.SetString("ProductLineID", productLineId);
            query.SetDecimal("ConcreteRZ", concreteRZ);
            query.SetDecimal("SlurryRZ", slurryRZ);
            query.SetBoolean("CheckConcrete", checkConcrete);
            query.SetBoolean("CheckSlurry", checkSlurry);

            object ret = query.UniqueResult();
            if (ret == null)
                return null;
            else
                return ret.ToString();

        }
    }
}
