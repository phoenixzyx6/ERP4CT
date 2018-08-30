using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ZLERP.Model
{
    public interface IEntityBase
    {
        // Methods
        int GetHashCode();
        bool Equals(object obj);
        object GetId();
    }
    /// <summary>
    /// 公共类，泛型类型限制
    /// </summary>
    public abstract class Entity : IEntityBase
    {
        #region Properties


        /// <summary>
        /// 版本
        /// </summary>
        [ScaffoldColumn(false)]
        public virtual int Version { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [ScaffoldColumn(false)]
        public virtual string Builder { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [ScaffoldColumn(false)]
        public virtual DateTime BuildTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [ScaffoldColumn(false)]
        public virtual string Modifier { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [ScaffoldColumn(false)]
        public virtual DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 生命周期状态
        /// </summary>

        [ScaffoldColumn(false)]
        public virtual int Lifecycle { get; set; }
        #endregion

        #region IEntityBase Members


        public abstract object GetId();


        #endregion
    }
    /// <summary>
    /// Base for all business objects.
    /// 
    /// </summary>
    /// <typeparam name="T">DataType of the primary key.</typeparam>
    public abstract class EntityBase<T> : Entity,ICloneable
    {
        #region Declarations

        private T _id = default(T);

        #endregion

        #region Methods

        public abstract override int GetHashCode();
        public override bool Equals(object obj)
        {
            return (obj != null)                                                    // 1) Object is not null.
                && (obj.GetType() == this.GetType())                                // 2) Object is of same Type.
                && (MatchingIds((EntityBase<T>)obj) || MatchingHashCodes(obj));   // 3) Ids or Hashcodes match.
        }
        private bool MatchingIds(EntityBase<T> obj)
        {
            return (this.ID != null && !this.ID.Equals(default(T)))                 // 1) this.Id is not null/default.
                && (obj.ID != null && !obj.ID.Equals(default(T)))                   // 1.5) obj.Id is not null/default.
                && (this.ID.Equals(obj.ID));                                        // 2) Ids match.
        }
        private bool MatchingHashCodes(object obj)
        {
            return this.GetHashCode().Equals(obj.GetHashCode());                    // 1) Hashcodes match.
        }
        public override object GetId()
        {
            return this._id;
        }


        #endregion

        #region Properties
        /// <summary>
        /// 编号
        /// </summary>
        public virtual T ID
        {
            get { return _id; }
            set { _id = value; }
        }
        #endregion

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
