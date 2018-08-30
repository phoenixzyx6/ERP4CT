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
    public class ShippingDocumentRepository : NHRepositoryBase<ShippingDocument>, IShippingDocumentRepository
    {
        public ShippingDocumentRepository(ISession session)
            : base(session)
        {

        }


        #region IShippingDocumentRepository 成员

        public IList<DailyReport> ExecuteShipDoc(string StartDateTime, string EndDateTime, out int total)
        {
            string sp = "exec sp_rpt_dd_daily @StartDateTime=:StartDateTime, @EndDateTime=:EndDateTime, @ProductLineID=:ProductLineID";
            var query = this._session.CreateSQLQuery(sp);
            query.SetString("StartDateTime", StartDateTime);
            query.SetString("EndDateTime", EndDateTime);
            query.SetString("ProductLineID", null);
            IList<object> obj = query.List<object>();
            IList<DailyReport> list = new List<DailyReport>();
            decimal SendCubes = 0;
            decimal SignInCubes = 0;
            foreach (object o in obj)
            {
                Object[] ob = o as Object[];
                DailyReport dr = new DailyReport();
                dr.TaskID = ob[0] == null ? "" : ob[0].ToString();
                dr.CustName = ob[1] == null ? "" : ob[1].ToString();
                dr.ProjectAddr = ob[2] == null ? "" : ob[2].ToString();
                dr.ConsPos = ob[3] == null ? "" : ob[3].ToString();
                dr.ProjectName = ob[4] == null ? "" : ob[4].ToString();
                dr.ConStrength = ob[5] == null ? "" : ob[5].ToString();
                dr.BetonCount = ob[6] == null ? "0" : ob[6].ToString();
                dr.SlurryCount = ob[7] == null ? "0" : ob[7].ToString();
                dr.CastMode = ob[8] == null ? "" : ob[8].ToString();
                dr.Remark = ob[9] == null ? "" : ob[9].ToString();
                dr.SendCube = ob[10] == null ? "0" : ob[10].ToString();
                dr.Parcube = ob[11] == null ? "0" : ob[11].ToString();
                dr.SignInCube = ob[12] == null ? "0" : ob[12].ToString();
                dr.TransferCube = ob[13] == null ? "0" : ob[13].ToString();
                dr.IsAudit = ob[23] == null ? false : Convert.ToBoolean(ob[23].ToString());
                SendCubes += Convert.ToDecimal(dr.SendCube);
                SignInCubes += Convert.ToDecimal(dr.SignInCube);
                list.Add(dr);
            }
            DailyReport tmp = new DailyReport();
            tmp.ConsPos = "合计";
            tmp.SendCube = SendCubes.ToString();
            tmp.SignInCube = SignInCubes.ToString();
            tmp.IsAudit = true;
            list.Add(tmp);
            total = list.Count;
            return list;
        }

       /// <summary>
        /// 运输单批量审核
       /// </summary>
       /// <param name="idstrs">运输单号组</param>
       /// <param name="CurrentUserID">当前用户</param>
       /// <returns></returns>
        public bool ShipDocMultiAudit(string idstrs, string CurrentUserID)
        {
            string sp = "exec sp_ShipDocMultiAudit  @ShipDocIds=:ShipDocIds,@CurrentUserID=:CurrentUserID";
            var query = this._session.CreateSQLQuery(sp);
            query.SetString("ShipDocIds", idstrs);
            query.SetString("CurrentUserID", CurrentUserID);
            object ret = query.UniqueResult();
            if (ret == null)
                return false;
            return true;

        }

        #endregion
    }
}
