using System;
using ZLERP.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ZLERP.Model.ViewModel;
namespace  ZLERP.Model.ViewModels
{
    /// <summary>
    /// 任务单的Model视图，用于图形模拟
    /// </summary>
    public class ProjInfoVm
    {
        #region Properties
        public string ProjId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string LinkMan { get; set; }
        public string LinkTel { get; set; }//联系电话
        public double Lat { get; set; }//纬度
        public double Lng { get; set; }//经度
        public string carNos { get; set; }//当前前往工地的车辆
        public List<TasksVm> TskVms { get; set; }//未完成任务单
        public decimal MonCube { get; set; }//本月已完成方量
        public decimal DayCube { get; set; }//本日已完成方量
      //  public List<ProjectPath> ProjectPaths { get; set; }//未完成任务单
        #endregion
    }
}