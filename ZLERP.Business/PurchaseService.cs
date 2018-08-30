using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using System.Collections.Specialized;
using ZLERP.Model.Enums;　

namespace ZLERP.Business
{
    public class PurchaseService : ServiceBase<Purchase>
    {
        internal PurchaseService(IUnitOfWork uow)
            : base(uow) 
        { 
            
        }

        public string InGoods(string id, decimal num)
        {
            lock (obj)
            {
                using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
                {
                    try
                    {
                        Purchase p = this.Get(Convert.ToInt32(id));
                        if (ps == null)
                            ps = new PublicService();

                        //数量累加
                        PurchaseMain pm = p.PurchaseMain;
                        if (pm == null)
                        {
                            tx.Rollback();
                            return "找不到PurchaseMain数据。";
                        }
                        pm.Main_num1 += num;
                        ps.PurchaseMain.Update(pm);

                        //修改数量
                        
                        if (p == null)
                            return "找不到ID为" + id + "的信息";
                        if (p.Purchase_Num < p.Purchase_Num1 + num)
                            return "超过额定量";
                        if (p.Purchase_State == 1)
                            return "任务已经完成";
                        p.Purchase_Num1 += num;
                        if (p.Purchase_Num == p.Purchase_Num1)//满表示完成了
                        {
                            p.Purchase_State = 1;
                            if (p.PurchaseContracts != null)
                            {
                                //更新合同完成
                                p.PurchaseContracts[0].PurchaseContract_state = 1;
                                this.ps.PurchaseConstract.Update(p.PurchaseContracts[0]);
                            }
                        }
                        this.Update(p);

                        //入库存 AddByPurchase方法有写库存
                        //GoodsInfo ginfo = this.m_UnitOfWork.GetRepositoryBase<GoodsInfo>().Query().Where(m => m.ID == p.GoodsID).FirstOrDefault();
                        //if (ginfo == null)
                        //{
                        //    tx.Rollback();
                        //    return "找不到" + p.GoodsID + "的物资";
                        //}
                        //ginfo.ContentNum += num;                        
                        //this.ps.GoodsInfo.Update(ginfo);

                        //入库信息
                        GoodsIn gIn = new GoodsIn();
                        gIn.GoodsID = p.GoodsID;
                        gIn.InNum = num;
                        gIn.SupplyName = p.PurchaseContracts[0].PurchaseContract_Supply;
                        gIn.InPrice = p.Purchase_Price;
                        gIn.InTime = DateTime.Now;
                        gIn.Operator = AuthorizationService.CurrentUserID;
                        gIn.TransportName = p.PurchaseContracts[0].PurchaseContract_Supply1;
                        if (p.PurchaseContracts != null)
                        {
                            //添加合同
                            gIn.PurchaseContract_ID = p.PurchaseContracts[0].ID;
                            gIn.PurchaseContract_Name = p.PurchaseContracts[0].PurchaseContract_Name;
                        }
                        this.ps.GoodsIn.AddByPurchase(gIn);
                        
                        tx.Commit();
                        return "";

                    }
                    catch (Exception e)
                    {
                        tx.Rollback();
                        return e.Message;
                    }
                }
            }
        }
        /// <summary>
        /// 获取姓名和联系电话
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string[] GetNameAndTel(string id)
        { 
            string[] arr=new string[2];
            if (string.IsNullOrEmpty(id))
                return arr;
           User obj = this.m_UnitOfWork.GetRepositoryBase<User>().Query().Where(m => m.ID == id).FirstOrDefault();
           if (obj == null)
               return arr;
           else
           {
               arr[0] = obj.ID;
               arr[1] = obj.Tel;
               return arr;
           }
        }
        /// <summary>
        /// 获取最新联系电话
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetLastTel(string name)
        {
            if(string.IsNullOrEmpty(name))
                return "";
            PurchaseContract obj = this.m_UnitOfWork.GetRepositoryBase<PurchaseContract>().Query().Where(m => m.PurchaseContract_Supply == name).FirstOrDefault();
            if (obj == null)
                return "";
            else
                return obj.PurchaseContract_SupplyTel;
        }

        public string UpdatePurchase(string[] arry)
        {
            lock (obj)
            {
                string msg = "";//错误信息
                if (arry == null || arry.Length == 0)
                    return msg = "传递参数为空";

                using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
                {
                    try
                    {
                        Purchase purchase = this.Get(Convert.ToInt32(arry[0]));
                        if (purchase == null)
                        {
                            msg = "找不到编号为" + arry[0] + "的子表数据";
                            return msg;
                        }
                        //创建采购子表
                        if (purchase.PurchaseMain == null)
                        {
                            msg = "找不到主表数据";
                            return msg;
                        }
                        PurchaseMain mainobj = purchase.PurchaseMain;

                        //判断数量
                        if (mainobj.Purchases == null || mainobj.Purchases.Count == 0)
                        { }
                        else
                        {
                            decimal num = 0;
                            for (int i = 0; i < mainobj.Purchases.Count; i++)
                            {
                                if (mainobj.Purchases[i].ID.ToString() != arry[0])
                                    num += mainobj.Purchases[i].Purchase_Num;
                            }
                            num += Convert.ToDecimal(arry[1]);
                            if (num > mainobj.Main_Num)
                            {
                                //数量超标了
                                msg = string.Format("采购计划数量{0},本次购买{1},超标{2}", mainobj.Main_Num, Convert.ToDecimal(arry[1]), num - mainobj.Main_Num);
                                return msg;
                            }
                        }

                        purchase.Purchase_Num = Convert.ToDecimal(arry[1]);
                        purchase.Purchase_Price = Convert.ToDecimal(arry[2]);
                        purchase.Purchase_Money = Convert.ToDecimal(arry[3]);
                        purchase.Purchase_ManTel = arry[4];
                        purchase.Purchase_Man = arry[5];
                        purchase.Purchase_Date = Convert.ToDateTime(arry[6]);
                        purchase.Purchase_StickMoney = Convert.ToDecimal(arry[7]);
                        purchase.Purchase_NoMoney = Convert.ToDecimal(arry[8]);
                        purchase.Purchase_StickNo = arry[9];
                        //purchase.Purchase_Num1 = Convert.ToDecimal(arry[18]);
                        this.Update(purchase);


                        //合同表
                        if (purchase.PurchaseContracts == null || purchase.PurchaseContracts.Count == 0)
                        {
                            msg = "找不到合同数据!";
                            tx.Rollback();
                            return msg;
                        }
                        PurchaseContract purchaseContract = purchase.PurchaseContracts[0];
                        purchaseContract.PurchaseContract_Name = arry[10];
                        purchaseContract.PurchaseContract_SupplyTel = arry[11];
                        purchaseContract.PurchaseContract_Supply = arry[12];
                        purchaseContract.PurchaseContract_SupplyTel1 = arry[18];
                        purchaseContract.PurchaseContract_Supply1 = arry[19];
                        purchaseContract.PurchaseContract_Price = Convert.ToDecimal(arry[2]);
                        purchaseContract.PurchaseContract_Num = Convert.ToDecimal(arry[1]);
                        purchaseContract.PurchaseContract_Money = Convert.ToDecimal(arry[3]);
                        purchaseContract.PurchaseContract_One = arry[13];
                        purchaseContract.PurchaseContract_Tow = arry[14];
                        purchaseContract.PurchaseContract_Date = Convert.ToDateTime(arry[15]);
                        purchaseContract.PurchaseContract_StartDate = Convert.ToDateTime(arry[16]);
                        purchaseContract.PurchaseContract_EndDate = Convert.ToDateTime(arry[17]);
                        purchaseContract.PurchaseContract_SupplyID = arry[20];
                        if (ps == null)
                            ps = new PublicService();

                        ps.PurchaseConstract.Update(purchaseContract);

                        tx.Commit();
                    }
                    catch (Exception e)
                    {
                        tx.Rollback();
                        return e.Message;
                    }
                }
                return msg;
            }
        }

        public string[] GetObj(int id)
        {
            string[] arr = new string[21];
            Purchase purchase = this.Get(id);
            if (purchase == null)
            {
                arr[0] = "-1";//找不到子表信息
                return arr;
            }
            arr[0] = purchase.ID.ToString();//子表ID
            arr[1] = purchase.Purchase_Num.ToString("0.00");
            //arr[18] = purchase.Purchase_Num1.ToString("0.00");
            arr[2] = purchase.Purchase_Price.ToString("0.00");
            arr[3] = purchase.Purchase_Money.ToString("0.00");
            arr[4] = purchase.Purchase_ManTel;
            arr[5] = purchase.Purchase_Man;
            arr[6] = purchase.Purchase_Date.ToString("yyyy-MM-dd hh:mm:ss");
            arr[7] = purchase.Purchase_StickMoney.ToString("0.00");
            arr[8] = purchase.Purchase_NoMoney.ToString("0.00");
            arr[9] = purchase.Purchase_StickNo;
            
            if (purchase.PurchaseContracts != null && purchase.PurchaseContracts.Count > 0)
            {
                arr[10] = purchase.PurchaseContracts[0].PurchaseContract_Name;
                arr[11] = purchase.PurchaseContracts[0].PurchaseContract_SupplyTel;
                arr[12] = purchase.PurchaseContracts[0].PurchaseContract_Supply;
                arr[13] = purchase.PurchaseContracts[0].PurchaseContract_One;
                arr[14] = purchase.PurchaseContracts[0].PurchaseContract_Tow;
                arr[15] = purchase.PurchaseContracts[0].PurchaseContract_Date.ToString("yyyy-MM-dd hh:mm:ss");
                arr[16] = purchase.PurchaseContracts[0].PurchaseContract_StartDate.ToString("yyyy-MM-dd hh:mm:ss");
                arr[17] = purchase.PurchaseContracts[0].PurchaseContract_EndDate.ToString("yyyy-MM-dd hh:mm:ss");
                arr[18] = purchase.PurchaseContracts[0].PurchaseContract_SupplyTel1;
                arr[19] = purchase.PurchaseContracts[0].PurchaseContract_Supply1;
                arr[20] = purchase.PurchaseContracts[0].PurchaseContract_SupplyID;
            }
            return arr;
        }

        public string SubmitPurchase(int id)
        {            
            lock (obj)
            {
                string msg = "";
                try
                {
                    Purchase purchase = this.Get(id);
                    if (purchase == null)
                        return "找不到id为" + id + "的采购子项信息。";
                    if (purchase.Purchase_State == 1)
                        return "采购已经提交，不能重复提交。";
                    purchase.Purchase_State = 1;
                    purchase.Purchase_Num1 = purchase.Purchase_Num;
                    this.Update(purchase);
                }
                catch (Exception e)
                {
                    msg = e.Message;
                }
                return msg;
            }
            
        }

        static readonly object obj = new object();
        private PublicService ps;
        public string AddPurchase(string[] arry)
        {
            lock (obj)
            {
                string msg = "";//错误信息
                if (arry == null || arry.Length == 0)
                    return msg = "传递参数为空";

                using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
                {
                    try
                    {
                        //创建采购子表
                        string main_id = arry[0];
                        PurchaseMain mainobj = this.m_UnitOfWork.GetRepositoryBase<PurchaseMain>().Query().Where(m => (m.ID).ToString() == main_id).FirstOrDefault();
                        if (mainobj == null)
                        {
                            msg = "找不到编号为" + main_id + "的主表数据";
                            return msg;
                        }

                        //判断数量
                        if (mainobj.Purchases == null || mainobj.Purchases.Count == 0)
                        { }
                        else
                        {
                            decimal num = 0;
                            for (int i = 0; i < mainobj.Purchases.Count; i++)
                            {
                                num += mainobj.Purchases[i].Purchase_Num;
                            }
                            num += Convert.ToDecimal(arry[1]);
                            if (num > mainobj.Main_Num)
                            {
                                //数量超标了
                                msg = string.Format("采购计划数量{0},本次购买{1},超标{2}", mainobj.Main_Num, Convert.ToDecimal(arry[1]), num - mainobj.Main_Num);
                                return msg;
                            }
                        }

                        Purchase purchase = new Purchase();
                        purchase.PurchaseMain = mainobj;
                        purchase.Purchase_Num =Convert.ToDecimal(arry[1]);
                        //purchase.Purchase_Num1 = Convert.ToDecimal(arry[18]);
                        purchase.Purchase_Price = Convert.ToDecimal(arry[2]);
                        purchase.Purchase_Money = Convert.ToDecimal(arry[3]);
                        purchase.Purchase_ManTel = arry[4];
                        purchase.Purchase_Man = arry[5];
                        purchase.Purchase_Date = Convert.ToDateTime(arry[6]);
                        purchase.Purchase_StickMoney = Convert.ToDecimal(arry[7]);
                        purchase.Purchase_NoMoney = Convert.ToDecimal(arry[8]);
                        purchase.Purchase_StickNo = arry[9];
                        purchase.Purchase_State = 0;//未完成
                        purchase.Remark = "";
                        purchase.GoodsID = mainobj.GoodsID;
                        purchase.GoodsName = mainobj.GoodsName;
                        purchase.Main_ID = (int)mainobj.ID;
                        //purchase.Purchase_Num1 = 0.00m;
                        this.Add(purchase);


                        //创建合同表
                        PurchaseContract purchaseContract = new PurchaseContract();
                        purchaseContract.Purchase = purchase;
                        purchaseContract.PurchaseContract_Name = arry[10];
                        purchaseContract.PurchaseContract_SupplyTel = arry[11];
                        purchaseContract.PurchaseContract_Supply = arry[12];
                        purchaseContract.PurchaseContract_Price = Convert.ToDecimal(arry[2]);
                        purchaseContract.PurchaseContract_Num = Convert.ToDecimal(arry[1]);
                        purchaseContract.PurchaseContract_Money = Convert.ToDecimal(arry[3]);
                        purchaseContract.PurchaseContract_One = arry[13];
                        purchaseContract.PurchaseContract_Tow = arry[14];
                        purchaseContract.PurchaseContract_Date = Convert.ToDateTime(arry[15]);
                        purchaseContract.PurchaseContract_StartDate = Convert.ToDateTime(arry[16]);
                        purchaseContract.PurchaseContract_EndDate = Convert.ToDateTime(arry[17]);
                        purchaseContract.GoodsID = mainobj.GoodsID;
                        purchaseContract.GoodsName = mainobj.GoodsName;
                        purchaseContract.Purchase_ID = (int)purchase.ID;
                        purchaseContract.PurchaseContract_SupplyTel1 = arry[18];
                        purchaseContract.PurchaseContract_Supply1 = arry[19];
                        purchaseContract.PurchaseContract_SupplyID=arry[20];
                        if (ps == null)
                            ps = new PublicService();

                        ps.PurchaseConstract.Add(purchaseContract);

                        tx.Commit();
                    }
                    catch (Exception e)
                    {
                        tx.Rollback();
                        return e.Message;
                    }
                }
                return msg;
            }
        }
    }
}
