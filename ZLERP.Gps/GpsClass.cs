using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;

namespace ZLERP.Gps
{
    public class GpsClass
    {
        private string LoginKey;

        public GpsClass()
        {
            LoginKey = GetLoginKey(ConfigurationManager.AppSettings["GpsUserName"], ConfigurationManager.AppSettings["GpsPassword"]);
        }

        public GpsClass(string LoginName, string Password)
        {
            LoginKey = GetLoginKey(LoginName, Password);
        }

        /// <summary>
        /// 根据登录名和密码获取登录凭证
        /// </summary>
        /// <param name="LoginName">GPS登录帐号</param>
        /// <param name="Password">GPS登录密码</param>
        /// <returns>返回登录凭证字符串</returns>
        public string GetLoginKey(string LoginName, string Password)
        {
            using (GpsService.ServiceSoapClient service = new GpsService.ServiceSoapClient())
            {
                return service.getLoginKey(LoginName, Password);
            }
        }

        /// <summary>
        /// 增加客户信息
        /// </summary>
        /// <param name="CustomerName">客户名称</param>
        /// <param name="Contacter">联系人</param>
        /// <param name="TEL">联系电话</param>
        /// <param name="Address">联系地址</param>
        /// <param name="Remark">备注</param>
        /// <returns>返回CustomerID 成功，-1 失败</returns>
        public int AddCustomer(string CustomerName, string Contacter, string TEL, string Address, string Remark)
        {
            using (GpsService.ServiceSoapClient service = new GpsService.ServiceSoapClient())
            {
                return service.EMV_Add_Customer_Info(LoginKey, CustomerName, Contacter, TEL, Address, Remark);
            }
        }

        /// <summary>
        /// 更新客户信息
        /// </summary>
        /// <param name="CustomerID">客户ID</param>
        /// <param name="CustomerName">客户名称</param>
        /// <param name="Contacter">联系人</param>
        /// <param name="TEL">联系电话</param>
        /// <param name="Address">联系地址</param>
        /// <param name="Remark">备注</param>
        /// <returns>返回 0 成功，-1 失败</returns>
        public int UpdateCustomer(int CustomerID, string CustomerName, string Contacter, string TEL, string Address, string Remark)
        {
            using (GpsService.ServiceSoapClient service = new GpsService.ServiceSoapClient())
            {
                return service.EMV_Update_Customer_Info(LoginKey, CustomerID, CustomerName, Contacter, TEL, Address, Remark);
            }
        }

        /// <summary>
        /// 更新客户信息(客户名称没有修改的情况下使用)
        /// </summary>
        /// <param name="CustomerName">客户名称</param>
        /// <param name="Contacter">联系人</param>
        /// <param name="TEL">联系电话</param>
        /// <param name="Address">联系地址</param>
        /// <param name="Remark">备注</param>
        /// <returns>返回 0 成功，-1 失败</returns>
        public int UpdateCustomer(string CustomerName, string Contacter, string TEL, string Address, string Remark)
        {
            using (GpsService.ServiceSoapClient service = new GpsService.ServiceSoapClient())
            {
                DataSet dataSet = service.EMV_Get_Customer_Info(LoginKey, CustomerName);
                int CustomerID = 0;
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    CustomerID = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
                }
                return service.EMV_Update_Customer_Info(LoginKey, CustomerID, CustomerName, Contacter, TEL, Address, Remark);
            }
        }

        /// <summary>
        /// 删除客户信息
        /// </summary>
        /// <param name="CustomerID">客户ID</param>
        /// <returns>返回 0 成功，-1 失败</returns>
        public int DeleteCustomer(int CustomerID)
        {
            using (GpsService.ServiceSoapClient service = new GpsService.ServiceSoapClient())
            {
                return service.EMV_Delete_Customer_Info(LoginKey, CustomerID);
            }
        }

        /// <summary>
        /// 删除客户信息(客户名称没有修改的情况下使用)
        /// </summary>
        /// <param name="CustomerName">客户名称</param>
        /// <returns>返回 0 成功，-1 失败</returns>
        public int DeleteCustomer(string CustomerName)
        {
            using (GpsService.ServiceSoapClient service = new GpsService.ServiceSoapClient())
            {
                DataSet dataSet = service.EMV_Get_Customer_Info(LoginKey, CustomerName);
                int CustomerID = 0;
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    CustomerID = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
                }
                return service.EMV_Delete_Customer_Info(LoginKey, CustomerID);
            }
        }

        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="CustomerName">客户名称</param>
        /// <returns>返回CustomerID</returns>
        public int GetCustomer(string CustomerName)
        {
            using (GpsService.ServiceSoapClient service = new GpsService.ServiceSoapClient())
            {
                DataSet dataSet = service.EMV_Get_Customer_Info(LoginKey, CustomerName);
                int CustomerID = 0;
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    CustomerID = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
                }
                return CustomerID;
            }
        }

        /// <summary>
        /// 增加工地信息
        /// </summary>
        /// <param name="CSName">工地名称</param>
        /// <param name="Address">工地地址</param>
        /// <param name="CustomerID">所属客户ID</param>
        /// <param name="Contactor">联系人</param>
        /// <param name="TEL">联系电话</param>
        /// <param name="Remark">备注</param>
        /// <param name="dMaxLng">工地区域最大经度</param>
        /// <param name="dMaxLat">工地区域最大纬度</param>
        /// <param name="dMinLng">工地区域最小经度</param>
        /// <param name="dMinLat">工地区域最小纬度</param>
        /// <returns>返回工地ID 成功，-1 失败</returns>
        public int AddConstructionSite(string CSName, string Address, int CustomerID, string Contactor, string TEL, string Remark, double dMaxLng, double dMaxLat, double dMinLng, double dMinLat)
        {
            using (GpsService.ServiceSoapClient service = new GpsService.ServiceSoapClient())
            {
                return service.EMV_Add_Construction_Site_Info(LoginKey, CSName, Address, CustomerID, Contactor, TEL, Remark, dMaxLng, dMaxLat, dMinLng, dMinLat);
            }
        }

        /// <summary>
        /// 增加工地信息
        /// </summary>
        /// <param name="CSName">工地名称</param>
        /// <param name="Address">工地地址</param>
        /// <param name="CustomerID">所属客户ID</param>
        /// <param name="Contactor">联系人</param>
        /// <param name="TEL">联系电话</param>
        /// <param name="Remark">备注</param>
        /// <returns>返回工地ID 成功，-1 失败</returns>
        public int AddConstructionSite(string CSName, string Address, int CustomerID, string Contactor, string TEL, string Remark)
        {
            return AddConstructionSite(CSName, Address, CustomerID, Contactor, TEL, Remark, 0, 0, 0, 0);
        }

        /// <summary>
        /// 更新工地信息
        /// </summary>
        /// <param name="CSID">工地ID</param>
        /// <param name="CSName">工地名称</param>
        /// <param name="Address">工地地址</param>
        /// <param name="CustomerID">所属客户ID</param>
        /// <param name="Contactor">联系人</param>
        /// <param name="TEL">联系电话</param>
        /// <param name="Remark">备注</param>
        /// <param name="dMaxLng">工地区域最大经度</param>
        /// <param name="dMaxLat">工地区域最大纬度</param>
        /// <param name="dMinLng">工地区域最小经度</param>
        /// <param name="dMinLat">工地区域最小纬度</param>
        /// <returns>返回 0 成功，-1 失败</returns>
        public int UpdateConstructionSite(int CSID, string CSName, string Address, int CustomerID, string Contactor, string TEL, string Remark, double dMaxLng, double dMaxLat, double dMinLng, double dMinLat)
        {
            using (GpsService.ServiceSoapClient service = new GpsService.ServiceSoapClient())
            {
                return service.EMV_Update_Construction_Site_Info(LoginKey, CSID, CSName, Address, CustomerID, Contactor, TEL, Remark, dMaxLng, dMaxLat, dMinLng, dMinLat);
            }
        }

        /// <summary>
        /// 更新工地信息
        /// </summary>
        /// <param name="CSID">工地ID</param>
        /// <param name="CSName">工地名称</param>
        /// <param name="Address">工地地址</param>
        /// <param name="CustomerID">所属客户ID</param>
        /// <param name="Contactor">联系人</param>
        /// <param name="TEL">联系电话</param>
        /// <param name="Remark">备注</param>
        /// <returns>返回 0 成功，-1 失败</returns>
        public int UpdateConstructionSite(int CSID, string CSName, string Address, int CustomerID, string Contactor, string TEL, string Remark)
        {
            return UpdateConstructionSite(CSID, CSName, Address, CustomerID, Contactor, TEL, Remark, 0, 0, 0, 0);
        }

        /// <summary>
        /// 更新工地信息(工地名称没有修改的情况下使用)
        /// </summary>
        /// <param name="CSName">工地名称</param>
        /// <param name="Address">工地地址</param>
        /// <param name="CustomerID">所属客户ID</param>
        /// <param name="Contactor">联系人</param>
        /// <param name="TEL">联系电话</param>
        /// <param name="Remark">备注</param>
        /// <returns>返回 0 成功，-1 失败</returns>
        public int UpdateConstructionSite(string CSName, string Address, int CustomerID, string Contactor, string TEL, string Remark)
        {
            using (GpsService.ServiceSoapClient service = new GpsService.ServiceSoapClient())
            {
                DataSet dataSet = service.EMV_Get_Construction_Site_Info(LoginKey, CSName);
                int CSID = 0;
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    CSID = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
                }
                return service.EMV_Update_Construction_Site_Info(LoginKey, CSID, CSName, Address, CustomerID, Contactor, TEL, Remark, 0, 0, 0, 0);
            }
        }

        /// <summary>
        /// 删除工地信息
        /// </summary>
        /// <param name="CSID">工地ID</param>
        /// <returns>返回 0 成功，-1 失败</returns>
        public int DeleteConstructionSite(int CSID)
        {
            using (GpsService.ServiceSoapClient service = new GpsService.ServiceSoapClient())
            {
                return service.EMV_Delete_Construction_Site_Info(LoginKey, CSID);
            }
        }

        /// <summary>
        /// 删除工地信息(工地名称没有修改的情况下使用)
        /// </summary>
        /// <param name="CSName">工地名称</param>
        /// <returns>返回 0 成功，-1 失败</returns>
        public int DeleteConstructionSite(string CSName)
        {
            using (GpsService.ServiceSoapClient service = new GpsService.ServiceSoapClient())
            {
                DataSet dataSet = service.EMV_Get_Construction_Site_Info(LoginKey, CSName);
                int CSID = 0;
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    CSID = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
                }
                return service.EMV_Delete_Construction_Site_Info(LoginKey, CSID);
            }
        }

        /// <summary>
        /// 获取工地信息
        /// </summary>
        /// <param name="CSName">工地名称</param>
        /// <returns>返回工地ID</returns>
        public int GetConstructionSite(string CSName)
        {
            using (GpsService.ServiceSoapClient service = new GpsService.ServiceSoapClient())
            {
                DataSet dataSet = service.EMV_Get_Construction_Site_Info(LoginKey, CSName);
                int CSID = 0;
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    CSID = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
                }
                return CSID;
            }
        }

        /// <summary>
        /// 增加任务单信息
        /// </summary>
        /// <param name="TaskCode">任务单编号</param>
        /// <param name="CSID">工地ID</param>
        /// <param name="PartName">工程部位</param>
        /// <param name="BetonGrade">砼强度</param>
        /// <param name="FeedingWay">输送方式,如:0=直送 1=泵送</param>
        /// <param name="PlanAmount">计划方量</param>
        /// <param name="PlanTime">计划到场时间</param>
        /// <param name="iUnloadMinutes">卸料时限(单位：分钟)</param>
        /// <param name="iReturnMinutes">卸料时限(单位：分钟)</param>
        /// <param name="ScoutMan">跟单员</param>
        /// <param name="iStatus">任务状态(0=未完成 1=已完成)</param>
        /// <param name="Remark">备注</param>
        /// <returns>返回任务单ID 成功，-1 失败</returns>
        public int AddTask(string TaskCode, int CSID, string PartName, string BetonGrade, int FeedingWay, int PlanAmount, string PlanTime, int iUnloadMinutes, int iReturnMinutes, string ScoutMan, int iStatus, string Remark)
        {
            using (GpsService.ServiceSoapClient service = new GpsService.ServiceSoapClient())
            {
                DataSet dataSet = service.EMV_Get_ProjectPart(LoginKey);
                int PartID = 2901;
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        if (PartName == dataSet.Tables[0].Rows[i][1].ToString())
                        {
                            PartID = Convert.ToInt32(dataSet.Tables[0].Rows[i][0]);
                            break;
                        }
                    }
                }
                dataSet = service.EMV_Get_BetonGrade(LoginKey);
                int BetonGradeID = 3051;
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        if (BetonGrade == dataSet.Tables[0].Rows[i][1].ToString())
                        {
                            BetonGradeID = Convert.ToInt32(dataSet.Tables[0].Rows[i][0]);
                            break;
                        }
                    }
                }
                return service.EMV_Add_Task_Info(LoginKey, TaskCode, CSID, PartID, BetonGradeID, FeedingWay, PlanAmount, PlanTime, iUnloadMinutes, iReturnMinutes, ScoutMan, iStatus, Remark);
            }
        }

        /// <summary>
        /// 更新任务单信息
        /// </summary>
        /// <param name="TaskID">任务单ID</param>
        /// <param name="TaskCode">任务单编号</param>
        /// <param name="CSID">工地ID</param>
        /// <param name="PartID">工程部位ID</param>
        /// <param name="BetonGradeID">砼强度ID</param>
        /// <param name="FeedingWay">输送方式,如:0=直送 1=泵送</param>
        /// <param name="PlanAmount">计划方量</param>
        /// <param name="PlanTime">计划到场时间</param>
        /// <param name="iUnloadMinutes">卸料时限(单位：分钟)</param>
        /// <param name="iReturnMinutes">卸料时限(单位：分钟)</param>
        /// <param name="ScoutMan">跟单员</param>
        /// <param name="iStatus">任务状态(0=未完成 1=已完成)</param>
        /// <param name="Remark">备注</param>
        /// <returns>返回 0 成功，-1 失败</returns>
        public int UpdateTask(int TaskID, string TaskCode, int CSID, int PartID, int BetonGradeID, int FeedingWay, int PlanAmount, string PlanTime, int iUnloadMinutes, int iReturnMinutes, string ScoutMan, int iStatus, string Remark)
        {
            using (GpsService.ServiceSoapClient service = new GpsService.ServiceSoapClient())
            {
                return service.EMV_Update_Task_Info(LoginKey, TaskID, TaskCode, CSID, PartID, BetonGradeID, FeedingWay, PlanAmount, PlanTime, iUnloadMinutes, iReturnMinutes, ScoutMan, iStatus, Remark);
            }
        }

        /// <summary>
        /// 更新任务单信息
        /// </summary>
        /// <param name="TaskCode">任务单编号</param>
        /// <param name="CSID">工地ID</param>
        /// <param name="PartName">工程部位</param>
        /// <param name="BetonGrade">砼强度</param>
        /// <param name="FeedingWay">输送方式,如:0=直送 1=泵送</param>
        /// <param name="PlanAmount">计划方量</param>
        /// <param name="PlanTime">计划到场时间</param>
        /// <param name="iUnloadMinutes">卸料时限(单位：分钟)</param>
        /// <param name="iReturnMinutes">卸料时限(单位：分钟)</param>
        /// <param name="ScoutMan">跟单员</param>
        /// <param name="iStatus">任务状态(0=未完成 1=已完成)</param>
        /// <param name="Remark">备注</param>
        /// <returns>返回 0 成功，-1 失败</returns>
        public int UpdateTask(string TaskCode, int CSID, string PartName, string BetonGrade, int FeedingWay, int PlanAmount, string PlanTime, int iUnloadMinutes, int iReturnMinutes, string ScoutMan, int iStatus, string Remark)
        {
            using (GpsService.ServiceSoapClient service = new GpsService.ServiceSoapClient())
            {
                DataSet dataSet = service.EMV_Get_ProjectPart(LoginKey);
                int PartID = 2901;
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        if (PartName == dataSet.Tables[0].Rows[i][1].ToString())
                        {
                            PartID = Convert.ToInt32(dataSet.Tables[0].Rows[i][0]);
                            break;
                        }
                    }
                }
                dataSet = service.EMV_Get_BetonGrade(LoginKey);
                int BetonGradeID = 3051;
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        if (BetonGrade == dataSet.Tables[0].Rows[i][1].ToString())
                        {
                            BetonGradeID = Convert.ToInt32(dataSet.Tables[0].Rows[i][0]);
                            break;
                        }
                    }
                }
                dataSet = service.EMV_Get_Task_Info(LoginKey, TaskCode);
                int TaskID = 0;
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    TaskID = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
                }
                return service.EMV_Update_Task_Info(LoginKey, TaskID, TaskCode, CSID, PartID, BetonGradeID, FeedingWay, PlanAmount, PlanTime, iUnloadMinutes, iReturnMinutes, ScoutMan, iStatus, Remark);
            }
        }

        /// <summary>
        /// 删除任务单信息
        /// </summary>
        /// <param name="TaskID">任务单ID</param>
        /// <returns>返回 0 成功，-1 失败</returns>
        public int DeleteTask(int TaskID)
        {
            using (GpsService.ServiceSoapClient service = new GpsService.ServiceSoapClient())
            {
                return service.EMV_Delete_Task_Info(LoginKey, TaskID);
            }
        }

        /// <summary>
        /// 删除任务单信息
        /// </summary>
        /// <param name="TaskCode">任务单编号</param>
        /// <returns>返回 0 成功，-1 失败</returns>
        public int DeleteTask(string TaskCode)
        {
            using (GpsService.ServiceSoapClient service = new GpsService.ServiceSoapClient())
            {
                DataSet dataSet = service.EMV_Get_Task_Info(LoginKey, TaskCode);
                int TaskID = 0;
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    TaskID = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
                }
                return service.EMV_Delete_Task_Info(LoginKey, TaskID);
            }
        }

        /// <summary>
        /// 获取任务单信息
        /// </summary>
        /// <param name="TaskCode">任务单编号</param>
        /// <returns>返回任务单ID</returns>
        public int GetTask(string TaskCode)
        {
            using (GpsService.ServiceSoapClient service = new GpsService.ServiceSoapClient())
            {
                DataSet dataSet = service.EMV_Get_Task_Info(LoginKey, TaskCode);
                int TaskID = 0;
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    TaskID = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
                }
                return TaskID;
            }
        }

        /// <summary>
        /// 增加调度单信息
        /// </summary>
        /// <param name="DispatchCode">调度单编号</param>
        /// <param name="VehicleNum">车牌</param>
        /// <param name="TaskID">任务单ID</param>
        /// <param name="dFactAmount">运输量</param>
        /// <param name="PlanLeaveTime">计划离场时间</param>
        /// <param name="Driver">驾驶员</param>
        /// <param name="ShiftType">班次,如：白班,晚班</param>
        /// <param name="Remark">备注</param>
        /// <returns>返回 0 成功，-1 失败</returns>
        public int AddDispatch(string DispatchCode, string VehicleNum, string TaskID, double dFactAmount, string PlanLeaveTime, string Driver, int ShiftType, string Remark)
        {
            using (GpsService.ServiceSoapClient service = new GpsService.ServiceSoapClient())
            {
                return service.EMV_Add_Dispatch_Info(LoginKey, DispatchCode, VehicleNum, TaskID, dFactAmount, PlanLeaveTime, Driver, ShiftType, Remark);
            }
        }

        /// <summary>
        /// 更新调度单信息
        /// </summary>
        /// <param name="DispatchID">调度单ID</param>
        /// <param name="DispatchCode">调度单编号</param>
        /// <param name="VehicleNum">车牌</param>
        /// <param name="TaskID">任务单ID</param>
        /// <param name="dFactAmount">运输量</param>
        /// <param name="PlanLeaveTime">计划离场时间</param>
        /// <param name="Driver">驾驶员</param>
        /// <param name="ShiftType">班次,如：白班,晚班</param>
        /// <param name="Remark">备注</param>
        /// <returns>返回 0 成功，-1 失败</returns>
        public int UpdateDispatch(int DispatchID, string DispatchCode, string VehicleNum, string TaskID, double dFactAmount, string PlanLeaveTime, string Driver, int ShiftType, string Remark)
        {
            using (GpsService.ServiceSoapClient service = new GpsService.ServiceSoapClient())
            {
                return service.EMV_Update_Dispatch_Info(LoginKey, DispatchID, DispatchCode, VehicleNum, TaskID, dFactAmount, PlanLeaveTime, Driver, ShiftType, Remark);
            }
        }

        /// <summary>
        /// 更新调度单信息
        /// </summary>
        /// <param name="DispatchCode">调度单编号</param>
        /// <param name="VehicleNum">车牌</param>
        /// <param name="TaskID">任务单ID</param>
        /// <param name="dFactAmount">运输量</param>
        /// <param name="PlanLeaveTime">计划离场时间</param>
        /// <param name="Driver">驾驶员</param>
        /// <param name="ShiftType">班次,如：白班,晚班</param>
        /// <param name="Remark">备注</param>
        /// <returns>返回 0 成功，-1 失败</returns>
        public int UpdateDispatch(string DispatchCode, string VehicleNum, string TaskID, double dFactAmount, string PlanLeaveTime, string Driver, int ShiftType, string Remark)
        {
            using (GpsService.ServiceSoapClient service = new GpsService.ServiceSoapClient())
            {
                DataSet dataSet = service.EMV_Get_Dispatch_Info(LoginKey, DispatchCode);
                int DispatchID = 0;
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    DispatchID = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
                }
                return service.EMV_Update_Dispatch_Info(LoginKey, DispatchID, DispatchCode, VehicleNum, TaskID, dFactAmount, PlanLeaveTime, Driver, ShiftType, Remark);
            }
        }

        /// <summary>
        /// 删除调度单信息
        /// </summary>
        /// <param name="DispatchID">调度单ID</param>
        /// <returns>返回 0 成功，-1 失败</returns>
        public int DeleteDispatch(int DispatchID)
        {
            using (GpsService.ServiceSoapClient service = new GpsService.ServiceSoapClient())
            {
                return service.EMV_Delete_Dispatch_Info(LoginKey, DispatchID);
            }
        }

        /// <summary>
        /// 删除调度单信息
        /// </summary>
        /// <param name="DispatchCode">调度单编号</param>
        /// <returns>返回 0 成功，-1 失败</returns>
        public int DeleteDispatch(string DispatchCode)
        {
            using (GpsService.ServiceSoapClient service = new GpsService.ServiceSoapClient())
            {
                DataSet dataSet = service.EMV_Get_Dispatch_Info(LoginKey, DispatchCode);
                int DispatchID = 0;
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    DispatchID = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
                }
                return service.EMV_Delete_Dispatch_Info(LoginKey, DispatchID);
            }
        }

        /// <summary>
        /// 获取调度单信息
        /// </summary>
        /// <param name="DispatchCode">调度单编号</param>
        /// <returns>返回调度单ID</returns>
        public int GetDispatch(string DispatchCode)
        {
            using (GpsService.ServiceSoapClient service = new GpsService.ServiceSoapClient())
            {
                DataSet dataSet = service.EMV_Get_Dispatch_Info(LoginKey, DispatchCode);
                int DispatchID = 0;
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    DispatchID = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
                }
                return DispatchID;
            }
        }

        /// <summary>
        /// 获取工程部位列表
        /// </summary>
        /// <returns>返回工程部位列表</returns>
        public DataSet GetProjectPart()
        {
            using (GpsService.ServiceSoapClient service = new GpsService.ServiceSoapClient())
            {
                return service.EMV_Get_ProjectPart(LoginKey);
            }
        }

        /// <summary>
        /// 获取砼强度列表
        /// </summary>
        /// <returns>返回砼强度列表</returns>
        public DataSet GetBetonGrade()
        {
            using (GpsService.ServiceSoapClient service = new GpsService.ServiceSoapClient())
            {
                return service.EMV_Get_BetonGrade(LoginKey);
            }
        }
    }
}
