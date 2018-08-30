using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;

namespace ZLERP.IControlSystem
{
    public interface IControl
    {
        /// <summary>
        /// 创建任务单
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        bool CreateProduceTask(ProduceTask task);
        /// <summary>
        /// 修改任务单
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        bool UpdateProduceTask(ProduceTask task);
        /// <summary>
        /// 下发配比
        /// </summary>
        /// <param name="cm"></param>
        /// <returns></returns>
        bool GenConsMixprop(ConsMixprop cm,IList<ConsMixpropItem> cmItemsList);
        /// <summary>
        /// 修改配比
        /// </summary>
        /// <param name="cm"></param>
        /// <param name="cmItemsList"></param>
        /// <returns></returns>
        ResultInfo UpdateConsMixprop(ConsMixprop cm,IList<ConsMixpropItem> cmItemsList);
        /// <summary>
        /// 发送调度
        /// </summary>
        /// <param name="disp"></param>
        /// <returns></returns>
        bool CreateDispatch(DispatchList disp);
        /// <summary>
        /// 修改调度
        /// </summary>
        /// <param name="disp"></param>
        /// <returns></returns>
        ResultInfo UpdateDispatch(DispatchList disp);
        /// <summary>
        /// 交换两个生产登记顺序
        /// </summary>
        /// <param name="disp1"></param>
        /// <param name="disp2"></param>
        /// <returns></returns>
        bool SwapDispatchOrder(DispatchList disp1, DispatchList disp2);
        /// <summary>
        /// 删除调度
        /// </summary>
        /// <param name="disp"></param>
        /// <returns></returns>
        ResultInfo DeleteDispatch(DispatchList disp);

        /// <summary>
        /// 新增调度
        /// </summary>
        /// <param name="disp"></param>
        /// <returns></returns>
        ResultInfo AddDispatch(DispatchList disp);
    }
}
