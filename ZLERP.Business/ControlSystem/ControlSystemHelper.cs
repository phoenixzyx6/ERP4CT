using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IControlSystem;
using System.Configuration;
using ZLERP.Model;
using log4net;

namespace ZLERP.Business.ControlSystem
{
    public class ControlSystemHelper
    {
        ILog logger = LogManager.GetLogger(typeof(ControlSystemHelper));
        IControl cs;
        ResultInfo _TrueResult = new ResultInfo { Result = true, Message = string.Empty };
        public ControlSystemHelper() {
            string csType = ConfigurationManager.AppSettings["ControlSystemType"];
            if (!string.IsNullOrEmpty(csType))
            {
                try
                {
                    cs = (IControl)Activator.CreateInstance(Type.GetType(csType));
                }
                catch (Exception ex) {
                    logger.Error("初始化控制系统访问类失败！[ControlSystemType]", ex);
                }
            }
        }

       
        /// <summary>
        /// 删除调度
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultInfo DeleteDispatch(DispatchList disp)
        {
            if (cs != null) {
                return cs.DeleteDispatch(disp);
            }
            return _TrueResult;
        }
        /// <summary>
        /// 交换生产登记位置
        /// </summary>
        /// <param name="disp1"></param>
        /// <param name="disp2"></param>
        /// <returns></returns>
        public bool SwapDispatch(DispatchList disp1, DispatchList disp2) {
            if (cs != null) {
                return cs.SwapDispatchOrder(disp1, disp2);
            }
            return true;
        }
        /// <summary>
        /// 更新生产登记
        /// </summary>
        /// <param name="disp"></param>
        /// <returns></returns>
        public ResultInfo UpdateDispatch(DispatchList disp) {
            if (cs != null) {
                return cs.UpdateDispatch(disp);
            }
            return _TrueResult;
        }
        /// <summary>
        /// 新增调度
        /// </summary>
        /// <param name="disp"></param>
        /// <returns></returns>
        public ResultInfo AddDispatch(DispatchList disp) {
            if (cs != null)
            {
                return cs.AddDispatch(disp);
            }
            return _TrueResult;
        }
        public bool GenConsmixprop(ConsMixprop cm, IList<ConsMixpropItem> cmItemsList)
        {
            if (cs != null)
            {
                return cs.GenConsMixprop(cm, cmItemsList);
            }
            return true;
        }
        /// <summary>
        /// 修改控制系统配比
        /// </summary>
        /// <param name="cm"></param>
        /// <param name="cmItemsList"></param>
        /// <returns></returns>
        public ResultInfo UpdateConsMixprop(ConsMixprop cm, IList<ConsMixpropItem> cmItemsList)
        {
            if (cs != null)
            {
                return cs.UpdateConsMixprop(cm, cmItemsList);
            }
            return _TrueResult;
        }
    }
}
