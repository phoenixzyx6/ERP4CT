using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using ZLERP.IRepository;

namespace ZLERP.NHibernateRepository
{
    public interface IUnitOfWorkFactory
    {
        /// <summary>
        /// 初始化配置uowfactory
        /// </summary>
        void Configuration();
        void Configuration(string tzid);
        ISessionFactory SessionFactory { get; }
        ISession CurrentSession { get; set; }

        IUnitOfWork Create();
        void DisposeUnitOfWork(IUnitOfWork adapter);
    }
}
