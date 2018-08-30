using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
namespace ZLERP.Business
{
    public class SystemMsgService : ServiceBase<SystemMsg>
    {
        internal SystemMsgService(IUnitOfWork uow)
            : base(uow)
        {
        }

        /// <summary>
        /// 根据系统自定义消息ID发送消息，自动匹配用户列表
        /// 用户列表为该自定义消息分配的用户
        /// </summary>
        /// <param name="MsgID">自定义消息ID</param>
        /// <param name="ObjID">操作对象ID，告知用户要操作的对象</param>
        /// <returns></returns>
        public bool SendMsg(string MsgID, string ObjID)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    //1、先找出该消息分配的用户ID列表
                    Msg msg = this.m_UnitOfWork.GetRepositoryBase<Msg>().Get(MsgID);

                    string url = string.Empty;
                    string pid = string.Empty;
                    this.getUrlAndPid(msg.BelongFuncID, out url, out pid);
                    if (msg == null)
                    {
                        throw new Exception("系统消息配置错误：消息ID为 " + MsgID);
                    }
                    IList<MsgUser> MsgUser = this.m_UnitOfWork.GetRepositoryBase<MsgUser>().Query().Where(m => m.MsgID == MsgID).ToList();
                    IList<string> UserList = MsgUser.Select(m => m.UserID).Distinct().ToList();
                    //2、保存消息主体
                    SystemMsg sm = new SystemMsg();
                    sm.OperateObj = ObjID;//此处的内容可以进行修饰，如任务单号为：XXXXXX
                    sm.MsgTitle = msg.MsgTitle;
                    sm.MsgType = msg.MsgType;
                    sm.Url = url;
                    sm.PID = pid;
                    sm = this.Add(sm);
                    //3、将消息分配给用户
                    DispatchMsg(sm, UserList.ToArray());
                    tx.Commit();
                    return true;
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// 根据消息实体发送消息（非系统消息，手动发送）
        /// </summary>
        /// <param name="MsgObj">组合的消息实体</param>
        /// <param name="UserList">指定的用户列表，数组（此列表的用户必须是系统中的用户，否则无法打开）</param>
        /// <returns></returns>
        public bool SendMsg(SystemMsg MsgObj, string[] UserList)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    SystemMsg sm = this.Add(MsgObj);
                    DispatchMsg(sm, UserList);
                    tx.Commit();
                    return true;
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// 根据消息实体发送消息（非系统消息，手动发送）
        /// </summary>
        /// <param name="MsgObj">组合的消息实体</param>
        /// <param name="UserList">指定的用户列表，逗号分隔的字符串（此列表的用户必须是系统中的用户，否则无法打开）</param>
        /// <returns></returns>
        public bool SendMsg(SystemMsg MsgObj, string UserList)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    SystemMsg sm = this.Add(MsgObj);//保存消息主体
                    //如果是保存到草稿箱
                    if (sm.DealStatus == 1)
                    {
                        tx.Commit();
                        return true;
                    }
                    DispatchMsg(sm, UserList.Split(','));
                    tx.Commit();
                    return true;
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// 将消息分发至用户
        /// </summary>
        /// <param name="sm"></param>
        /// <param name="UserList"></param>
        /// <returns></returns>
        public  bool DispatchMsg(SystemMsg sm, string[] UserList)
        {
            foreach (string userid in UserList)
            {
                MyMsg mm = new MyMsg();
                mm.SystemMsgID = sm.ID;
                mm.UserID = userid;
                this.m_UnitOfWork.GetRepositoryBase<MyMsg>().Add(mm);
            }
            //开启另外一个线程进行短信发送
            
            return true;
        }
        /// <summary>
        /// 保存消息主体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override SystemMsg Add(SystemMsg entity)
        {
            if (string.IsNullOrEmpty(entity.Sender))
            {
                entity.Sender = AuthorizationService.CurrentUserID;//此处改为UserID，以方便处理 xyl 2014-02-28
                entity.SendTime = DateTime.Now;
            }
            return base.Add(entity);
        }

        /// <summary>
        /// 撤销已发送
        /// 规则：1、删除已分发用户的消息；2、将主体消息标记为草稿待再次发送
        /// </summary>
        /// <param name="SystemMsgID">待撤销的主体消息编号</param>
        /// <returns>true</returns>
        public bool Revocation(string SystemMsgID) {
            try
            {
                // 1、找到相关的用户消息，然后逐个删除
                IList<MyMsg> mmlist = this.m_UnitOfWork.GetRepositoryBase<MyMsg>().Query().Where(m => m.SystemMsgID == SystemMsgID).ToList();
                foreach (MyMsg mm in mmlist)
                {
                    this.m_UnitOfWork.GetRepositoryBase<MyMsg>().Delete(mm);
                }
                // 2、回置消息主体为草稿
                SystemMsg sm = this.Get(SystemMsgID);
                sm.DealStatus = 1; //1表示草稿；0表示已发送
                this.Update(sm);
                return true;
            }
            catch {
                throw;
            }
        }

        /// <summary>
        /// //根据模块ID查找模块的URL、PID以便页面跳转
        /// </summary>
        /// <param name="BelongFuncID">模块ID</param>
        /// <param name="Url">输出变量</param>
        /// <param name="Pid">输出变量</param>
        public void getUrlAndPid(string BelongFuncID, out string Url, out string Pid)
        {
            SysFunc sf = this.m_UnitOfWork.GetRepositoryBase<SysFunc>().Get(BelongFuncID);
            Url = sf.Urls.FirstOrDefault();
            if (Url.IndexOf("?") > 0)
            {
                Url += "&f=" + BelongFuncID;
            }
            else
            {
                Url += "?f=" + BelongFuncID;
            }
            Pid = sf.ParentID;
        }

        
    }
}
