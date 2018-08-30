using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.Enums
{
    /// <summary>
    /// 一级字典键值
    /// </summary>
    public enum DicEnum
    {
        PurchasePlan_state,
        /// <summary>
        /// 车别
        /// </summary>
        CarBelong,
        /// <summary>
        /// 车辆类型
        /// </summary>
        CarType,
        /// <summary>
        /// 施工部位
        /// </summary>
        ConsPos,
        /// <summary>
        /// 混凝土种类
        /// </summary>
        CType,
        /// <summary>
        /// 料种	FType
        /// </summary>
        FType,
        bangcha,
        /// <summary>
        /// 特性类型
        /// </summary>
        IdenType,
        /// <summary>
        /// 计划班组
        /// </summary>
        PlanClass,
        /// <summary>
        /// 泵车类型
        /// </summary>
        PumpType,
        /// <summary>
        /// 施工季节
        /// </summary>
        Season,
        /// <summary>
        /// 坍落度
        /// </summary>
        Slump,
        /// <summary>
        /// 材料大类
        /// </summary>
        SType,
        /// <summary>
        /// 轮胎位置
        /// </summary>
        TyrePos,
        /// <summary>
        /// 轮胎类型
        /// </summary>
        TyreType,
        /// <summary>
        /// 级配
        /// </summary>
        JP,
        /// <summary>
        /// 浇筑方式
        /// </summary>
        CastMode,
        /// <summary>
        /// 合同类型
        /// </summary>
        ContractType,
        /// <summary>
        /// 付款类型
        /// </summary>
        PayType,
        /// <summary>
        /// 合同付款类型
        /// </summary>
        ContractPayType,
        /// <summary>
        /// 合同账款调整类型
        /// </summary>
        ContractAccAdjust,
        /// <summary>
        /// 合同状态
        /// </summary>
        ContractStatus,
        /// <summary>
        /// 计价方式
        /// </summary>
        ValuationType,
        /// <summary>
        /// 任务单类型
        /// </summary>
        TType,
        /// <summary>
        /// 厂商类型
        /// </summary>
        SuType,
        /// <summary>
        /// 客户类型
        /// </summary>
        CustType,
        /// <summary>
        /// 税率
        /// </summary>
        TaxRate,
        /// <summary>
        /// 付款方式
        /// </summary>
        PaymentType,
        /// <summary>
        /// 合同限制类型
        /// </summary>
        CubeLimit,
        /// <summary>
        /// 审核状态
        /// </summary>
        AuditStatus,
        /// <summary>
        /// 配比复制操作
        /// </summary>
        CopyOp,
        /// <summary>
        /// 剩退类别，剩退料处理
        /// </summary>
        ReturnType,
        /// <summary>
        /// 处理方式，剩退料处理
        /// </summary>
        ActionType,
        /// <summary>
        /// 用户性别
        /// </summary>
        Gender,
        /// <summary>
        /// 职务（用户类别）
        /// </summary>
        UserType,
        /// <summary>
        /// 车辆状态
        /// </summary>
        CarStatus,
        /// <summary>
        /// 试验类型
        /// </summary>
        ExamType,
        /// <summary>
        /// 判定结论
        /// </summary>
        Judge,
        /// <summary>
        /// 轮胎品牌
        /// </summary>
        TyreBreed,
        /// <summary>
        /// 轮胎型号
        /// </summary>
        TyreModel,
        /// <summary>
        /// 轮胎状态
        /// </summary>
        TyreStatus,
        /// <summary>
        /// 轮胎更换原因
        /// </summary>
        TyreChangeReason,
        /// <summary>
        /// 计算方式
        /// </summary>
        CalcType,
        /// <summary>
        /// 维修类型
        /// </summary>
        RepairType,
        /// <summary>
        /// 加价项目
        /// </summary>
        PriceType,
        /// <summary>
        /// 强度评定统计方法
        /// </summary>
        StatMethod,
        /// <summary>
        /// 手自动
        /// </summary>
        AH,
        /// <summary>
        /// 配件分类
        /// </summary>
        PartClass,
        /// <summary>
        /// 配件生产厂牌
        /// </summary>
        Production,
        /// <summary>
        /// 配件用途厂牌
        /// </summary>
        PartApplication,
        /// <summary>
        /// 运输费用计价方式
        /// </summary>
        TransPriceMethod,
        /// <summary>
        /// 类别类型
        /// </summary>
        ClassType,
        /// <summary>
        /// 领用类别
        /// </summary>
        DrawType,
        SystemUnit,
        /// <summary>
        /// 单价类型
        /// </summary>
        Ptype,
        /// <summary>
        /// 计量单位
        /// </summary>
        SystemUnit01,
        /// <summary>
        /// 石子类型
        /// </summary>
        CAType,
        /// <summary>
        /// 卡片类型
        /// </summary>
        CardType,
        /// <summary>
        /// 外租车辆车队
        /// </summary>
        RentMC,
        /// <summary>
        /// 保养延迟原因
        /// </summary>
        MtDelayReason,
        /// <summary>
        /// 系统配置类别
        /// </summary>
        SetClass,
        /// <summary>
        /// 出货辅助类型
        /// </summary>
        AssType,
        /// <summary>
        /// 水泥等级
        /// </summary>
        CG,
        /// <summary>
        /// 发货单类型
        /// </summary>
        ShipDocType,
        /// <summary>
        /// 工地联系人列表
        /// </summary>
        LinkMan,
        /// <summary>
        /// 事故分类
        /// </summary>
        AccClass,
        /// <summary>
        /// 材料品种
        /// </summary>
        ST,
        /// <summary>
        /// 材料品种
        /// </summary>
        DianziType,
        /// <summary>
        /// 车辆维修类型
        /// </summary>
        CarRepairType,
        /// <summary>
        /// 砼标记
        /// </summary>
        BetonTag,
        /// <summary>
        /// 罐容比
        /// </summary>
        PCRate,
        /// 奖惩类别
        /// </summary>
        RewardsType,
        /// <summary>
        /// 奖惩方式
        /// </summary>
        RewardsMode,
        /// <summary>
        /// 培训成绩
        /// </summary>
        TrainCredit,
        /// <summary>
        /// 模版类型
        /// </summary>
        TplType

        , SupplyUnitSupplyUnit
        , PurchasePlanByEquip_state
        /// <summary>
        /// 数据类型
        /// </summary>
        ,DataType
        /// <summary>
        /// 业务类型
        /// </summary>
        , BusinessType
        /// <summary>
        /// 里程价格类型
        /// </summary>
        , KmPriceType
        /// <summary>
        /// 工程结算方式
        /// </summary>
        , ProjectPay
        /// <summary>
        /// 班次类型
        /// </summary>
        , DriverClassType
        /// <summary>
        /// 租赁扣除费用类型
        /// </summary>
        , CarLeaseCostType

    }
}
