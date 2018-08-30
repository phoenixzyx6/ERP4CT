using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ZLERP.Model
{
    /// <summary>
    /// 可作为树形Grid数据
    /// </summary>
    interface ITreeGridable
    {
        /// <summary>
        /// 当前层级，从0开始
        /// </summary>
        int level { get; }
        /// <summary>
        /// 父节点ID
        /// </summary>
        string parent { get; }
        /// <summary>
        /// 是否叶子节点
        /// </summary>       
        bool leaf { get;  }
        /// <summary>
        /// 是否展开
        /// </summary>
        bool expanded { get; }
        /// <summary>
        /// 是否已加载，为true时，点击该节点将不发送请求至服务端
        /// </summary>
        bool loaded { get; }
    }
}
