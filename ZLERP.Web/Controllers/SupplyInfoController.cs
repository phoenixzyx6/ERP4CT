using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class SupplyInfoController : BaseController<SupplyInfo,string>
    {
        /// <summary>
        /// 更新厂商使用状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="usedStatus"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateSupplyInfo(SupplyInfo supplyInfo)
        {
            try
            {
                base.Update(supplyInfo);
                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception ex)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + ex.Message, null);
            }
        }

        /// <summary>
        /// 更新厂商使用状态(批量)
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="usedStatus"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateUsedStatus(string[] ids, bool usedStatus)
        {
            try
            {
                foreach (string id in ids)
                {
                    SupplyInfo supplyInfo = m_ServiceBase.Get(id);
                    supplyInfo.IsUsed = usedStatus;
                    base.Update(supplyInfo);
                }

                return OperateResult(true, Lang.Msg_Operate_Success, null);
            }
            catch (Exception ex)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + ex.Message, null);
            }
        }

        /// <summary>
        /// 获取供应商电话
        /// </summary>
        /// <param name="supplyid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetSupplyTel(string supplyid)
        {
            try
            {
                SupplyInfo supplyInfo = m_ServiceBase.Get(supplyid);

                return OperateResult(true, Lang.Msg_Operate_Success, supplyInfo.LinkTel);
            }
            catch (Exception ex)
            {
                return OperateResult(false, Lang.Msg_Operate_Failed + ex.Message, null);
            }
        }
    }
}
