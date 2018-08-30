using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZLERP.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        void Flush();
        bool IsInActiveTransaction { get; }

        IGenericTransaction BeginTransaction();
        IGenericTransaction BeginTransaction(IsolationLevel isolationLevel);

        IRepositoryBase<TEntity> GetRepositoryBase<TEntity>();

        /// <summary>
        /// 施工配比Repository
        /// </summary>
        IConsMixpropRepository ConsMixpropRepository { get; }

        /// <summary>
        /// 施工配比子项Repository
        /// </summary>
        IConsMixpropItemRepository ConsMixpropItemRepository { get; }
        /// <summary>
        /// 结算单Repo
        /// </summary>
        ISettlementRepository SettlementRepository { get; }
        /// <summary>
        /// 材料入库Repository
        /// </summary>
        IStuffInRepository StuffInRepository { get; }

        /// <summary>
        /// 发货单Repository
        /// </summary>
        IShippingDocumentRepository ShippingDocumentRepository { get; }

        /// <summary>
        /// 年帐套Repository
        /// </summary>
        IYearAccountRepository YearAccountRepository { get; }

        /// <summary>
        /// 月结Repository
        /// </summary>
        IMonthAccountRepository MonthAccountRepository { get; }

        /// <summary>
        /// 入库记录单价调整Repository
        /// </summary>
        IStuffInPriceAdjustRepository StuffInPriceAdjustRepository { get; }
    }
}
