using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;
using ZLERP.Model.Enums;

namespace ZLERP.Web.Controllers
{
    public class IdentitySettingController : BaseController<IdentitySetting, int>
    {
       
        public override ActionResult Add(IdentitySetting IdentitySetting)
        {
            return base.Add(IdentitySetting);
        }

        /// <summary>
        /// 查询合同明细的特性设定
        /// 一次获取所有特性，并按IdentType分组
        /// </summary>
        /// <param name="contractItemId"></param>
        /// <param name="taskId">指定TaskId则查询该task是否有该特性，并将特性的checked=true</param>
        /// <returns></returns>
        public ActionResult FindIdentitySettings(int contractItemId, string taskId) {
            var identityTypes = this.service.Dic.FindDics(DicEnum.IdenType.ToString());//查询特性字典
            //合同项的所有特性
            IList<IdentitySetting> identitySettings = this.service.GetGenericService<IdentitySetting>()
                .All(string.Format("ContractItemsID={0}", contractItemId), "IdentityType", true); //查询合同子项(砼)的特性设置

            IDictionary<string, IList<IdentitySetting>> contractItemIdentitySettings 
                = new Dictionary<string, IList<IdentitySetting>>();//创建字典
             
            foreach (var ident in identitySettings) {
                if (!contractItemIdentitySettings.ContainsKey(ident.IdentityType))//填充字典
                {
                    contractItemIdentitySettings[ident.IdentityType] = new List<IdentitySetting>();
                }
                contractItemIdentitySettings[ident.IdentityType].Add(ident);
            }
            var checkedIdentSettings = new List<int?>();
            if (!string.IsNullOrEmpty(taskId)) {
                //指定任务单号则查询任务单存在的特性
                var task = this.service.ProduceTask.Get(taskId);
                if (task != null)
                {
                    checkedIdentSettings = task.TaskIdentities.Select(t => t.ID)
                        .ToList();
                }
            }
            var data =contractItemIdentitySettings.Select(
                p=> new{
                    IdentType = identityTypes.Where(t=>t.TreeCode == p.Key).FirstOrDefault(), 
                    IdentItems = p.Value.Select(i=>new{
                        text=i.IdentityName, 
                        value=i.ID,
                        @checked = checkedIdentSettings.Contains(i.ID)
                    })
                }
                );
            return Json(data, JsonRequestBehavior.AllowGet);

        }


    }
}
