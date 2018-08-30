using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.Enums
{
    public class Consts
    {
        /// <summary>
        /// 合同限额类型，此常量必须与字典表中的合同限额数据一致
        /// </summary>
        public const string LimitNone = "limit0";//不受限制
        public const string LimitContractTime = "limit1";//受合同起止时间限制
        public const string LimitMatCube = "limit2";//受垫资方量限制
        public const string LimitPrepayCube = "limit3";//受预付方量限制

        /// <summary>
        /// 合同状态，此常量必须与字典表中的合同限额数据一致
        /// </summary>
        public const string ContractStatusAuditing = "1";
        public const string ContractStatusBeginning = "2";
        public const string ContractStatusCompleted = "3";
        public const string ContractStatusInvalid = "4";
        public const string ContractStatusPause = "5";
        public const string ContractStatusPigeonhole = "6";


        /// <summary>
        /// 自动，手动
        /// </summary>
        public const string Auto = "Auto";
        public const string Handle = "Handle";
        /// <summary>
        /// 验证信息
        /// </summary>
        public const string ValidatEmail = "请输入正确的Email格式\n示例：abc@123.com";
    }
}
