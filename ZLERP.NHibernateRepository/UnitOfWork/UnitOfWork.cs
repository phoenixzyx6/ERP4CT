using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using ZLERP.IRepository;
using System.Data;
using NHibernate.Cfg;

namespace ZLERP.NHibernateRepository
{
    public  class UnitOfWork
    {
        private static readonly IUnitOfWorkFactory _unitOfWorkFactory = new UnitOfWorkFactory();

        public const string CurrentUnitOfWorkKey = "CurrentUnitOfWork.Key";

        private static IUnitOfWork CurrentUnitOfWork
        {
            get { return Local.Data[CurrentUnitOfWorkKey] as IUnitOfWork; }
            set { Local.Data[CurrentUnitOfWorkKey] = value; }
        }

        public static IUnitOfWork Current
        {
            get
            {               
                var unitOfWork = CurrentUnitOfWork;
                if (unitOfWork == null)
                    throw new InvalidOperationException("You are not in a unit of work");
                return unitOfWork;
            }
        }

        public static bool IsStarted
        {
            get { return CurrentUnitOfWork != null; }
        }

        public static ISession CurrentSession
        {
            get { return _unitOfWorkFactory.CurrentSession; }
            internal set { _unitOfWorkFactory.CurrentSession = value; }
        }

        public static IUnitOfWork Start()
        {
            if (CurrentUnitOfWork != null)
                throw new InvalidOperationException("You cannot start more than one unit of work at the same time.");

            var unitOfWork = _unitOfWorkFactory.Create();
            CurrentUnitOfWork = unitOfWork;
            return unitOfWork;
        }

        internal static void DisposeUnitOfWork(IUnitOfWork unitOfWork)
        {
            CurrentUnitOfWork = null;
        }
    }
}
