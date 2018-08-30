using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.Enums
{
    public class SqlErrorNumbers
    {
        /// <summary>
        /// 约束冲突错误（删父表存在子表记录）
        /// </summary>
        public const int ConstraintError = 547;
        /// <summary>
        /// 违反唯一索引约束
        /// </summary>
        public const int UniqueIndexError = 2601;
        /// <summary>
        /// 重复键错误
        /// </summary>
        public const int DuplicateKeyError = 2627;

    }
}
