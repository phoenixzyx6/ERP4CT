using System;
using ZLERP.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace ZLERP.Model.ViewModel
{
    /// <summary>
    /// 任务单的Model视图，用于图形模拟
    /// </summary>
    public class TasksVm
    {
        #region Properties
        //【@project.ContractNo】@project.Name，Distance，TaskNo，PlanCube，ConsPos
        //，GradeName，times//次数，SingleTime.Value.ToString("{0:HH小时mm分}")
        public int TaskID { get; set; }
        public string ProjInfo { get; set; }
        public double Distance { get; set; }
        public string SingleTime { get; set; }//单次耗时
        public string TaskNo { get; set; }
        public decimal PlanCube { get; set; }
        public decimal ProvidedCube { get; set; }
        public string Grade { get; set; }
        public string ConsPos { get; set; }
        public string TransName { get; set; }
        public int Times { get; set; }//已送料次数
        public DateTime? CtTime { get; set; }//最后发货时间
        #endregion
    }
}