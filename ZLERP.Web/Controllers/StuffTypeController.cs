using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;

namespace ZLERP.Web.Controllers
{
    public class StuffTypeController : BaseController<StuffType,string>
    {
        public override ActionResult Add(StuffType entity)
        {
            try
            {
                
                if (entity.TypeID == "StuffType")           //如果是材料，用最终材料类型作为前缀
                {
                    IList<StuffType> StuffTypeList = this.service.GetGenericService<StuffType>().All(" StuffTypeID like '" + entity.FinalStuffType + "%'", "ID", true);
                    if (StuffTypeList.Count == 0)           //如果是首次添加，则不在尾数0
                    {
                        entity.ID = entity.FinalStuffType;
                    }
                    else                                    //判断最大尾数后面直接+1
                    {
                        string maxID = StuffTypeList[StuffTypeList.Count - 1].ID.Trim().Replace(StuffTypeList[StuffTypeList.Count - 1].FinalStuffType.Trim(), "");
                        if (string.IsNullOrEmpty(maxID.Trim()))
                        {
                            maxID = "0";
                        }
                        entity.ID = entity.FinalStuffType + (int.Parse(maxID) + 1).ToString();
                    }
                }
                else                                        //直接用类型ID作为前缀
                {
                    IList<StuffType> StuffTypeList = this.service.GetGenericService<StuffType>().All("StuffTypeID like '" + entity.TypeID + "%'", "ID", true);
                    if (StuffTypeList.Count == 0)
                    {
                        entity.ID = entity.TypeID;
                    }
                    else
                    {
                        string maxID = StuffTypeList[StuffTypeList.Count - 1].ID.Trim().Replace(StuffTypeList[StuffTypeList.Count - 1].TypeID.Trim(), "");
                        if (string.IsNullOrEmpty(maxID.Trim()))
                        {
                            maxID = "0";
                        }
                        entity.ID = entity.TypeID + (int.Parse(maxID) + 1).ToString();
                    }
                }
                return base.Add(entity);

            }
            catch (Exception e)
            {
                return OperateResult(false, e.Message, null);
            }
         }

        /*
         * 修改材料类型的排序时，建议同时修改桶仓的排序
         * 前者影响理论配比，后者影响施工配比排序
         * JM 2013/08/19
        public override ActionResult Update(StuffType entity)
        {
            try
            {
                IList<StuffInfo> stufflist = this.service.StuffInfo.Query().Where(m => m.StuffTypeID == entity.ID).ToList();
                IList<Silo> silolist = null;
                foreach (StuffInfo stuff in stufflist)
                {
                    silolist = this.service.Silo.Query().Where(m => m.StuffID == stuff.ID).ToList();
                    foreach (Silo silo in silolist)
                    {
                        silo.OrderNum = (int)entity.OrderNum;
                        this.service.Silo.Update(silo, null);
                    }
                }
                return base.Update(entity);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw new Exception(ex.Message);
            }            
        }
         * */


       
    }
}
