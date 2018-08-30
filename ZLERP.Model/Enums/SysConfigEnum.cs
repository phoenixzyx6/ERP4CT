using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.Enums
{
    /// <summary>
    /// 系统配置项名
    /// </summary>
    public enum SysConfigEnum
    {
        /// <summary>
        /// 交班时间	
        /// </summary>        
        ChangeTime,

        /// <summary>
        /// 公司名称	
        /// </summary> 
        EnterpriseName,
        /// <summary>
        /// 指定局域网IP范围，不允许外网访问的用户只能从设定的范围访问	
        /// </summary> 
        LanIPRange,

        /// <summary>
        /// 调度选择车号显示配置，0显示车辆代号，1显示车辆代号加车牌号，2显示车牌号，3显示车牌号加车辆代号	
        /// </summary>
        CarListView,

        /// <summary>
        /// 发货单回单时小票票号是否允许为空。true表示允许为空，false表示不允许为空
        /// </summary>
        IsAllowBlankForTicketNo,

        /// <summary>
        /// 允许上传的文件类型	
        /// </summary>
        UploadFileTypes,

        /// <summary>
        /// 发车延迟时间	
        /// </summary>
        DelayDeliveryTime,

        /// <summary>
        /// 调度方量设定，0为车辆最大容量，否则默认为指定的方量	
        /// </summary>
        DefaultManufactureCube,

        /// <summary>
        /// 春秋季、夏季、冬季
        /// </summary>
        CurrentSeason,

        /// <summary>
        /// 方量限额方式，0为不受限制，1为按合同起止时间，2为按垫资方量，3为按预付方量	
        /// </summary>
        CubeLimitType,
        /// <summary>
        /// 是否启用登录验证码
        /// </summary>
        EnableLogOnCaptcha,
        /// <summary>
        /// 调度是否自动选择车辆
        /// </summary>
        AutoSelectCarID,

        /// <summary>
        /// 合同是否自动审核
        /// </summary>
        IsAutoAuditForContract,

        /// <summary>
        /// 任务单是否自动审核
        /// </summary>
        IsAutoAuditForTask,
        /// <summary>
        /// 是否启用系统日志记录
        /// </summary>
        EnableSysLog,

        /// <summary>
        /// 盘点审核超期时间
        /// </summary>
        CheckItemLimitTime,

        /// <summary>
        /// 配比容重最大值
        /// </summary>
        FormulaRZMax,
        /// <summary>
        /// 配比容重最小值
        /// </summary>
        FormulaRZMin,
        /// <summary>
        /// 是否锁定第一个生产登记
        /// </summary>
        LockFirstDispatch,
        /// <summary>
        /// 正在生产的任务单是否允许修改配比，true表示允许修改，false表示不允许修改
        /// </summary>
        IsAllowUpdateProducing,
        /// <summary>
        /// 是否允许限制配比参考范围
        /// </summary>
        IsAllowConsMixpropLimit,
        /// <summary>
        /// 是否允许调度同时发送混凝土和砂浆，true表示允许，false表示不允许
        /// </summary>
        IsAllowSendTogether,
        /// <summary>
        /// X分钟未开盘提醒
        /// </summary>
        MinutesBeforeNeedDate,
        /// <summary>
        /// 是否过滤出租车辆，true表示过滤，false表示不过滤
        /// </summary>
        IsCarLendFilter,
        /// <summary>
        /// 单打砂浆的砼强度出票模式，0打印砂浆、1打印砼强度+砂浆、2打印砼强度
        /// </summary>
        SlurryShipDoc,
        /// <summary>
        /// 结算日
        /// </summary>
        ChangeDay,
        /// <summary>
        /// 油料密度
        /// </summary>
        OilDensity,
        /// <summary>
        /// 用户权限方案
        /// </summary>
        AuthScheme
    }
}
