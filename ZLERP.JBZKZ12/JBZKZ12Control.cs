using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IControlSystem;
using System.Data.SqlClient;
using System.Configuration;
using log4net;
using System.Transactions;
using ZLERP.Model;
using System.Data;

namespace ZLERP.JBZKZ12
{
    public class JBZKZ12Control : IControl
    {
        private ILog logger = LogManager.GetLogger(typeof(JBZKZ12Control));
        #region IControl 成员

        public bool CreateProduceTask(ZLERP.Model.ProduceTask task)
        {
            throw new NotImplementedException();
        }

        public bool UpdateProduceTask(ZLERP.Model.ProduceTask task)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 生成施工配比
        /// </summary>
        /// <param name="cm"></param>
        /// <returns></returns>
        public bool GenConsMixprop(ZLERP.Model.ConsMixprop cm,IList<ZLERP.Model.ConsMixpropItem> cmItemsList)
        {
            string connStr = GetConnectionString(cm.ProductLineID);
            if (!string.IsNullOrEmpty(connStr))
            {
                string sql = @"BEGIN TRANSACTION 
                               IF EXISTS (SELECT * FROM FormulaMessage WHERE ID = @ID)
                               BEGIN 
                                   UPDATE FormulaMessage
                                   SET     FormulaName = @FormulaName,
                                           FType = @FType,
                                           MixingTime = @MixingTime,
                                           Volume = @Volume,
                                           UpdateTime = GETDATE()
                                   WHERE   ID = @ID
                               END
                               ELSE
                               BEGIN
                                   INSERT INTO FormulaMessage
	                                ( ID ,
	                                  PTaskListID ,
	                                  ProductLineID ,
	                                  FormulaName ,
	                                  SeepLevel ,
	                                  FlexStrength ,
	                                  AdditiveRate ,
	                                  WaterGlue ,
	                                  SandRate ,
	                                  FType ,
	                                  MixingTime ,
	                                  Volume ,
	                                  XZBool ,
	                                  XZRecord ,
	                                  Version ,
	                                  CreateTime ,
	                                  UpdateTime ,
	                                  CreaterID ,
	                                  UpdaterID ,
	                                  Status
	                                )
	                        VALUES  ( @ID , -- ID - varchar(50)
	                                  @PTaskListID , -- PTaskListID - varchar(50)
	                                  @ProductLineID , -- ProductLineID - varchar(50)
	                                  @FormulaName, -- FormulaName - varchar(50)
	                                  NULL , -- SeepLevel - varchar(50)
	                                  NULL , -- FlexStrength - varchar(50)
	                                  0.00 , -- AdditiveRate - numeric
	                                  0.00 , -- WaterGlue - numeric
	                                  0.00 , -- SandRate - numeric
	                                  @FType , -- FType - int
	                                  @MixingTime , -- MixingTime - int
	                                  @Volume , -- Volume - numeric
	                                  NULL , -- XZBool - int
	                                  NULL , -- XZRecord - varchar(255)
	                                  0 , -- Version - int
	                                  GETDATE() , -- CreateTime - datetime
	                                  NULL , -- UpdateTime - datetime
	                                  '' , -- CreaterID - varchar(50)
	                                  '' , -- UpdaterID - varchar(50)
	                                  0  -- Status - int
	                                ) 
                            END ";
                sql += " DELETE FROM FormulaItems WHERE FormulaMessageID='" + cm.ID + "'";
                foreach(ZLERP.Model.ConsMixpropItem cmItems in cmItemsList)
                {
                   string cmItemsID = "P" + cmItems.ID.ToString();
                    sql += " INSERT INTO FormulaItems(";
	                sql += " ID ,  FormulaMessageID,SiloID,StuffID,TypeAmount,Version,CreateTime,UpdateTime,CreaterID, UpdaterID,Status) ";
                    sql += " VALUES  ( '" + cmItemsID + "','" + cmItems.ConsMixpropID + "', '" + cmItems.SiloID + "','" + cmItems.Silo.StuffID + "','" + cmItems.Amount + "',0,'" + DateTime.Now.ToString() + "',";
                    sql += " NULL,NULL,'',0) ";
	             }
                sql += " COMMIT";

            
                SqlParameter[] p = { 
                                    SQLHelper.MakeInParam("@ID", System.Data.SqlDbType.VarChar,50, cm.ID),
                                    SQLHelper.MakeInParam("@PTaskListID", System.Data.SqlDbType.VarChar,20, cm.TaskID),
                                    SQLHelper.MakeInParam("@ProductLineID", System.Data.SqlDbType.VarChar,30,"00000" + cm.ProductLineID),
                                    SQLHelper.MakeInParam("@FormulaName", System.Data.SqlDbType.VarChar,30, cm.ConStrength),
                                    SQLHelper.MakeInParam("@FType", System.Data.SqlDbType.Int,4, cm.IsSlurry?2:1),
                                    SQLHelper.MakeInParam("@MixingTime", System.Data.SqlDbType.Int,4, cm.MixingTime==null? 30:cm.MixingTime ),
                                    SQLHelper.MakeInParam("@Volume", System.Data.SqlDbType.Int,4, 0)
                                };
                return SQLHelper.ExecuteNonQuery(connStr, System.Data.CommandType.Text, sql, p) > 0;
            }
            else
            {
                logger.ErrorFormat("没有找到生产线(ProductLine_{0})的数据库连接配置",   cm.ProductLineID);
                return false;
            }
        }

 

        public bool CreateDispatch(ZLERP.Model.DispatchList disp)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 更新生产登记
        /// </summary>
        /// <param name="disp"></param>
        /// <returns></returns>
        public ResultInfo UpdateDispatch(ZLERP.Model.DispatchList disp)
        {
            return UpdateDispatchListTcp(disp);
        }
        /// <summary>
        /// 删除控制系统指定的生产登记
        /// </summary>
        /// <param name="disp"></param>
        /// <returns></returns>
        public ResultInfo DeleteDispatch(ZLERP.Model.DispatchList disp)
        {
            return  DeleteDispatchListTcp(disp);
             
        }

        

        /// <summary>
        /// 获取控制系统连接字符串
        /// </summary>
        /// <param name="productLineId"></param>
        /// <returns></returns>
        string GetConnectionString(string productLineId) {
            try
            {
                return ConfigurationManager.ConnectionStrings[string.Format("ProductLine_{0}",  productLineId)].ConnectionString;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 取得控制系统IP地址
        /// </summary>
        /// <param name="productLineId"></param>
        /// <returns></returns>
        string GetControlSystemIP(string productLineId) {

            return ConfigurationManager.AppSettings["ProductLine_" +  productLineId];
             
        }


        public bool SwapDispatchOrder(Model.DispatchList disp1, Model.DispatchList disp2)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 新增调度
        /// </summary>
        /// <param name="disp"></param>
        /// <returns></returns>
        public ResultInfo AddDispatch(DispatchList disp) {
            return AddDispatchListTcp(disp);
        }

        /// <summary>
        /// 更新配比
        /// </summary>
        /// <param name="cm"></param>
        /// <param name="cmItemsList"></param>
        /// <returns></returns>
        public ResultInfo UpdateConsMixprop(ConsMixprop cm, IList<ConsMixpropItem> cmItemsList)
        {
            return SendConsmixpropTcp(cm, cmItemsList);
        }
        #endregion

        #region TCP通讯方式具体操作
        /// <summary>
        /// 通过tcp通知控制系统删除生产登记
        /// </summary>
        /// <param name="dispatch"></param>
        /// <returns></returns>
        ResultInfo DeleteDispatchListTcp(DispatchList dispatch)
        {
            string IP = GetControlSystemIP(dispatch.ProductLineID);

            //未指定该生产线IP，不使用TCP通讯,始终返回true;
            if (string.IsNullOrEmpty(IP)) {
                logger.WarnFormat("未配置该生产线[{0}]的IP，不使用TCP通讯[DeleteDispatchListTcp]，直接返回true。", dispatch.ProductLineID);
                return new ResultInfo 
                {
                    Result = true,
                    Message = "未配置该生产线IP，不使用TCP通讯。"
                };
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            DataRow Dr = dt.NewRow();
            Dr["ID"] = dispatch.ID;
            dt.Rows.Add(Dr);
            string CMD = "102";
            
            string Num = IP + ":" + DateTime.Now.ToString("yyyyMMddHHmmss");
            string XML = TcpIpHelper.CombinSendXML(Num, 1, CMD, "ProductRegist", "", dt);

            return TcpIpHelper.TcpSend(XML, CMD, IP);
        }


        ResultInfo AddDispatchListTcp(DispatchList dispatch)
        {
            string IP = GetControlSystemIP(dispatch.ProductLineID);

            //未指定该生产线IP，不使用TCP通讯,始终返回true;
            if (string.IsNullOrEmpty(IP))
            {
                logger.WarnFormat("未配置该生产线[{0}]的IP，不使用TCP通讯[AddDispatchListTcp]，直接返回true。", dispatch.ProductLineID);
                return new ResultInfo
                {
                    Result = true,
                    Message = "未配置该生产线IP，不使用TCP通讯。"
                };
            }


            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("PRegOrder");
            dt.Columns.Add("TaskID");
            dt.Columns.Add("CarID");
            dt.Columns.Add("Driver");

            dt.Columns.Add("CurCarProductNum");
            dt.Columns.Add("CurCarNum");
            dt.Columns.Add("FirNum");
            dt.Columns.Add("FirTrayNum");
            dt.Columns.Add("SecNum");
            dt.Columns.Add("SecTrayNum");
            dt.Columns.Add("IsContRun");
            dt.Columns.Add("RunproductCarNum");
            dt.Columns.Add("CompletetCarNum");
            dt.Columns.Add("ConcreteTrayNum");
            dt.Columns.Add("TraySumNum");
            dt.Columns.Add("FirMortarNum");
            dt.Columns.Add("FirMortarTrayNum");
            dt.Columns.Add("SecMortarTrayNum");
            dt.Columns.Add("SecMortarNum");
            dt.Columns.Add("MortarTrayNum");
            dt.Columns.Add("MortarTraySumNum");
            dt.Columns.Add("ConcreteSumNum");
            dt.Columns.Add("MortarNum");
            dt.Columns.Add("StartTime");
            dt.Columns.Add("EndTime");
            dt.Columns.Add("IsDivideAvg");
            dt.Columns.Add("TrayRate");
            dt.Columns.Add("ProductLineID");
            dt.Columns.Add("PrintStatus");
            dt.Columns.Add("RestStuffNum");
            dt.Columns.Add("TicketNum");
            dt.Columns.Add("FormulaCont");
            dt.Columns.Add("FormulaMor");
            dt.Columns.Add("FinishedAutoDel");
            dt.Columns.Add("Version");
            dt.Columns.Add("CreateTime");
            dt.Columns.Add("UpdateTime");
            dt.Columns.Add("CreaterID");
            dt.Columns.Add("UpdaterID");
            dt.Columns.Add("Status");
            dt.Columns.Add("PrintNum");
            dt.Columns.Add("PrintCarNum");
            dt.Columns.Add("BYField1");
            dt.Columns.Add("BYField2");
            dt.Columns.Add("BYField3");
            dt.Columns.Add("BYField4");
            dt.Columns.Add("BYField5");
            dt.Columns.Add("ShipDocID");


            DataRow Dr = dt.NewRow();
            Dr["ID"] = dispatch.ID;
            Dr["PRegOrder"] = dispatch.DispatchOrder;
            Dr["TaskID"] = dispatch.TaskID;
            Dr["CarID"] = dispatch.CarID;
            Dr["Driver"] = dispatch.Driver;
            Dr["CompletetCarNum"] = dispatch.ProvidedTimes;
            Dr["CurCarProductNum"] = dispatch.ProvidedCube;
            Dr["CurCarNum"] = dispatch.ProduceCube;
            Dr["FirNum"] = dispatch.OneCube;
            Dr["FirTrayNum"] = dispatch.BNextPot;
            Dr["SecNum"] = dispatch.TwoCube;
            Dr["SecTrayNum"] = dispatch.BTotalPot - dispatch.BNextPot;
            Dr["IsContRun"] = 0;
            Dr["RunproductCarNum"] = 0;
            Dr["ConcreteTrayNum"] = dispatch.BTotalPot;
            Dr["TraySumNum"] = dispatch.BTotalPot + dispatch.STotalPot;
            Dr["FirMortarNum"] = dispatch.OneSlurryCube;
            Dr["FirMortarTrayNum"] = dispatch.SNextPot;
            Dr["SecMortarNum"] = dispatch.TwoSlurryCube;
            Dr["SecMortarTrayNum"] = dispatch.STotalPot - dispatch.SNextPot;
            Dr["MortarTrayNum"] = dispatch.STotalPot;
            Dr["MortarTraySumNum"] = dispatch.STotalPot;
            Dr["ConcreteSumNum"] = dispatch.BetonCount;
            Dr["MortarNum"] = dispatch.SlurryCount;
            Dr["StartTime"] = null;
            Dr["EndTime"] = null;
            Dr["IsDivideAvg"] = dispatch.IsAverage;
            Dr["TrayRate"] = dispatch.PCRate;
            Dr["ProductLineID"] = "00000" + dispatch.ProductLineID;// 系统中数据库已经将机组线号改成8位
            //Dr["ProductLineID"] = dispatch.ProductLineID;
            Dr["PrintStatus"] = 0;
            Dr["RestStuffNum"] = 0;
            Dr["TicketNum"] = dispatch.ParCube;
            Dr["FormulaCont"] = dispatch.BetonFormula;
            Dr["FormulaMor"] = dispatch.SlurryFormula;
            Dr["FinishedAutoDel"] = 0;
            Dr["Version"] = 1;

            Dr["CreateTime"] = dispatch.BuildTime;
            Dr["UpdateTime"] = dispatch.ModifyTime;
            Dr["CreaterID"] = dispatch.Builder;
            Dr["UpdaterID"] = dispatch.Modifier;

            Dr["Status"] = 0;
            Dr["PrintNum"] = dispatch.ProvidedCube;
            Dr["PrintCarNum"] = dispatch.ProvidedTimes;
            Dr["BYField1"] = dispatch.ParCube;          //备用1：出票方量
            Dr["BYField2"] = dispatch.ProvidedTimes;    //备用2：累计车次
            Dr["BYField3"] = dispatch.ProvidedCube;     //备用3：已供方量
            Dr["BYField4"] = dispatch.ConStrength;      //备用4：砼强度
            Dr["BYField5"] = string.IsNullOrEmpty(dispatch.PumpName) ? dispatch.CastMode : dispatch.PumpName;   //备用5：浇筑方式或泵名称
            Dr["ShipDocID"] = dispatch.ShipDocID;
            dt.Rows.Add(Dr);
            string CMD = "101";
            
            string Num = IP + ":" + DateTime.Now.ToString("yyyyMMddHHmmss");
            string XML = TcpIpHelper.CombinSendXML(Num, 1, CMD, "ProductRegist", "", dt);
            //XML = "<?xml version='1.0' encoding='utf-8' ?>< Root><Head><Num>192.168.0.1:XXXXX </ Num ><SystemType>1</SystemType><CMD>101</CMD> <Table>ProductRegist</Table> <Count>1</Count> <User>SupperUser</ User > </Head><Data><ID>1</ID></Data></ Root >";
            return TcpIpHelper.TcpSend(XML, CMD, IP);

        }


        ResultInfo UpdateDispatchListTcp(DispatchList dispatch)
        {
            string IP = GetControlSystemIP(dispatch.ProductLineID);

            //未指定该生产线IP，不使用TCP通讯,始终返回true;
            if (string.IsNullOrEmpty(IP))
            {
                logger.WarnFormat("未配置该生产线[{0}]的IP，不使用TCP通讯[UpdateDispatchListTcp]，直接返回true。", dispatch.ProductLineID);
                return new ResultInfo
                {
                    Result = true,
                    Message = "未配置该生产线IP，不使用TCP通讯。"
                };
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("PRegOrder");
            dt.Columns.Add("TaskID");
            dt.Columns.Add("CarID");
            dt.Columns.Add("Driver");

            dt.Columns.Add("CurCarProductNum");
            dt.Columns.Add("CurCarNum");
            dt.Columns.Add("FirNum");
            dt.Columns.Add("FirTrayNum");
            dt.Columns.Add("SecNum");
            dt.Columns.Add("SecTrayNum");
            dt.Columns.Add("IsContRun");
            dt.Columns.Add("RunproductCarNum");
            dt.Columns.Add("CompletetCarNum");
            dt.Columns.Add("ConcreteTrayNum");
            dt.Columns.Add("TraySumNum");
            dt.Columns.Add("FirMortarNum");
            dt.Columns.Add("FirMortarTrayNum");
            dt.Columns.Add("SecMortarTrayNum");
            dt.Columns.Add("SecMortarNum");
            dt.Columns.Add("MortarTrayNum");
            dt.Columns.Add("MortarTraySumNum");
            dt.Columns.Add("ConcreteSumNum");
            dt.Columns.Add("MortarNum");
            dt.Columns.Add("StartTime");
            dt.Columns.Add("EndTime");
            dt.Columns.Add("IsDivideAvg");
            dt.Columns.Add("TrayRate");
            dt.Columns.Add("ProductLineID");
            dt.Columns.Add("PrintStatus");
            dt.Columns.Add("RestStuffNum");
            dt.Columns.Add("TicketNum");
            dt.Columns.Add("FormulaCont");
            dt.Columns.Add("FormulaMor");
            dt.Columns.Add("FinishedAutoDel");
            dt.Columns.Add("Version");
            dt.Columns.Add("CreateTime");
            dt.Columns.Add("UpdateTime");
            dt.Columns.Add("CreaterID");
            dt.Columns.Add("UpdaterID");
            dt.Columns.Add("Status");
            dt.Columns.Add("PrintNum");
            dt.Columns.Add("PrintCarNum");
            dt.Columns.Add("BYField1");
            dt.Columns.Add("BYField2");
            dt.Columns.Add("BYField3");
            dt.Columns.Add("BYField4");
            dt.Columns.Add("BYField5");
            dt.Columns.Add("ShipDocID");

            DataRow Dr = dt.NewRow();
            Dr["ID"] = dispatch.ID;
            Dr["PRegOrder"] = dispatch.DispatchOrder;
            Dr["TaskID"] = dispatch.TaskID;
            Dr["CarID"] = dispatch.CarID;
            Dr["Driver"] = dispatch.Driver;
            Dr["CompletetCarNum"] = dispatch.ProvidedTimes;
            Dr["CurCarProductNum"] = dispatch.ProvidedCube;
            Dr["CurCarNum"] = dispatch.ProduceCube;
            Dr["FirNum"] = dispatch.OneCube;
            Dr["FirTrayNum"] = dispatch.BNextPot;
            Dr["SecNum"] = dispatch.TwoCube;
            Dr["SecTrayNum"] = dispatch.BTotalPot - dispatch.BNextPot;
            Dr["IsContRun"] = 0;
            Dr["RunproductCarNum"] = 0;
            Dr["ConcreteTrayNum"] = dispatch.BTotalPot;
            Dr["TraySumNum"] = dispatch.BTotalPot + dispatch.STotalPot;
            Dr["FirMortarNum"] = dispatch.OneSlurryCube;
            Dr["FirMortarTrayNum"] = dispatch.SNextPot;
            Dr["SecMortarNum"] = dispatch.TwoSlurryCube;
            Dr["SecMortarTrayNum"] = dispatch.STotalPot - dispatch.SNextPot;
            Dr["MortarTrayNum"] = dispatch.STotalPot;
            Dr["MortarTraySumNum"] = dispatch.STotalPot;
            Dr["ConcreteSumNum"] = dispatch.BetonCount;
            Dr["MortarNum"] = dispatch.SlurryCount;
            Dr["StartTime"] = null;
            Dr["EndTime"] = null;
            Dr["IsDivideAvg"] = dispatch.IsAverage;
            Dr["TrayRate"] = dispatch.PCRate;
            Dr["ProductLineID"] = "00000" + dispatch.ProductLineID;// 系统中数据库已经将机组线号改成8位
            //Dr["ProductLineID"] = dispatch.ProductLineID;
            Dr["PrintStatus"] = 0;
            Dr["RestStuffNum"] = 0;
            Dr["TicketNum"] = dispatch.ParCube;
            Dr["FormulaCont"] = dispatch.BetonFormula;
            Dr["FormulaMor"] = dispatch.SlurryFormula;
            Dr["FinishedAutoDel"] = 0;
            Dr["Version"] = 1;

            Dr["CreateTime"] = dispatch.BuildTime;
            Dr["UpdateTime"] = dispatch.ModifyTime;
            Dr["CreaterID"] = dispatch.Builder;
            Dr["UpdaterID"] = dispatch.Modifier;

            //Dr["Status"] = 0;//ERP修改调度不修改控制系统的生产状态
            Dr["PrintNum"] = dispatch.ProvidedCube;
            Dr["PrintCarNum"] = dispatch.ProvidedTimes;
            Dr["BYField1"] = dispatch.ParCube;          //备用1：出票方量
            Dr["BYField2"] = dispatch.ProvidedTimes;    //备用2：累计车次
            Dr["BYField3"] = dispatch.ProvidedCube;     //备用3：已供方量
            Dr["BYField4"] = dispatch.ConStrength;      //备用4：砼强度
            Dr["BYField5"] = string.IsNullOrEmpty(dispatch.PumpName) ? dispatch.CastMode : dispatch.PumpName;   //备用5：浇筑方式或泵名称
            Dr["ShipDocID"] = dispatch.ShipDocID;

            dt.Rows.Add(Dr);
            string CMD = "103";
           
            string Num = IP + ":" + DateTime.Now.ToString("yyyyMMddHHmmss");
            string XML = TcpIpHelper.CombinSendXML(Num, 1, CMD, "ProductRegist", "", dt);

            return TcpIpHelper.TcpSend(XML, CMD, IP);
        }


        ResultInfo SendConsmixpropTcp(ConsMixprop cm, IList<ConsMixpropItem> cmItems)
        {
            string IP = GetControlSystemIP(cm.ProductLineID);

            //未指定该生产线IP，不使用TCP通讯,始终返回true;
            //if (string.IsNullOrEmpty(IP))
            if(true)
            {
                logger.WarnFormat("未配置该生产线[{0}]的IP，不使用TCP通讯[UpdateDispatchListTcp]，直接返回true。", cm.ProductLineID);
                return new ResultInfo
                {
                    Result = true,
                    Message = "未配置该生产线IP，不使用TCP通讯。"
                };
            }
            DataTable dtcm = new DataTable();
            dtcm.Columns.Add("ID");
            dtcm.Columns.Add("PTaskListID");
            dtcm.Columns.Add("ProductLineID");
            dtcm.Columns.Add("FormulaName");
            dtcm.Columns.Add("SeepLevel");

            dtcm.Columns.Add("FlexStrength");
            dtcm.Columns.Add("AdditiveRate");
            dtcm.Columns.Add("WaterGlue");
            dtcm.Columns.Add("SandRate");
            dtcm.Columns.Add("FType");
            dtcm.Columns.Add("MixingTime");
            dtcm.Columns.Add("Volume");
            dtcm.Columns.Add("XZBool");
            dtcm.Columns.Add("XZRecord");
            dtcm.Columns.Add("Version");
            dtcm.Columns.Add("CreateTime");
            dtcm.Columns.Add("UpdateTime");
            dtcm.Columns.Add("CreaterID");
            dtcm.Columns.Add("UpdaterID");
            dtcm.Columns.Add("Status");

            DataRow Dr = dtcm.NewRow();
            Dr["ID"] = cm.ID;
            Dr["PTaskListID"] = cm.TaskID;
            Dr["ProductLineID"] = "00000" + cm.ProductLineID;// 系统中数据库已经将机组线号改成8位
            //Dr["ProductLineID"] = cm.ProductLineID;
            Dr["FormulaName"] = cm.ConStrength;
            Dr["SeepLevel"] = null;
            Dr["FlexStrength"] = null;
            Dr["AdditiveRate"] = 0.00;
            Dr["WaterGlue"] = cm.WCRate;
            Dr["SandRate"] = cm.SCRate;
            Dr["FType"] = cm.IsSlurry ? 2 : 1;
            Dr["MixingTime"] = cm.MixingTime;
            Dr["Volume"] = cm.Weight;
            Dr["XZBool"] = null;
            Dr["XZRecord"] = null;
            Dr["Version"] = 1;
            Dr["CreateTime"] = cm.BuildTime.ToString("yyyy-MM-dd HH:mm:ss");
            Dr["UpdateTime"] = Convert.ToDateTime(cm.ModifyTime).ToString("yyyy-MM-dd HH:mm:ss");
            Dr["CreaterID"] = cm.Builder;
            Dr["UpdaterID"] = cm.Modifier;
            Dr["Status"] = 0;

            dtcm.Rows.Add(Dr);


            DataTable dtcmitems = new DataTable();
            dtcmitems.Columns.Add("ID");
            dtcmitems.Columns.Add("FormulaMessageID");
            dtcmitems.Columns.Add("SiloID");
            dtcmitems.Columns.Add("StuffID");
            dtcmitems.Columns.Add("TypeAmount");
            dtcmitems.Columns.Add("Version");
            dtcmitems.Columns.Add("Status");

            foreach (ConsMixpropItem cmItem in cmItems)
            {
                DataRow DrcmItem = dtcmitems.NewRow();
                DrcmItem["ID"] = cmItem.ID;
                DrcmItem["FormulaMessageID"] = cmItem.ConsMixpropID;
                DrcmItem["SiloID"] = cmItem.SiloID;
                DrcmItem["StuffID"] = cmItem.Silo.StuffID;
                DrcmItem["TypeAmount"] = cmItem.Amount;
                DrcmItem["Version"] = 1;
                DrcmItem["Status"] = 0;
                dtcmitems.Rows.Add(DrcmItem);
            }
            string CMD = "201";
           
            string Num = IP + ":" + DateTime.Now.ToString("yyyyMMddHHmmss");
            string XML = TcpIpHelper.CombinSendXML(Num, 1, CMD, "FormulaItems", dtcmitems.Rows.Count, "", dtcm, dtcmitems);

            return TcpIpHelper.TcpSend(XML, CMD, IP);
        }

        #endregion

    }
}
