using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using NHibernate;
using NHibernate.Cfg;
using System.Xml;
using System.IO;
using NHibernate.Context;
using System.Collections.Specialized;
 

namespace ZLERP.NHibernateRepository
{
    
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {       
        private static ISession _currentSession;
        private static ISessionFactory _sessionFactory;
        private static Configuration _configuration; 

         

        public IUnitOfWork Create()
        {
            
            ISession session = CreateSession();
            session.FlushMode = FlushMode.Commit;
            _currentSession = session;
            return new UnitOfWorkImpl(this, session);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public  void Configuration()
        {
            if (_configuration == null)
            {
                _configuration = new Configuration().Configure();

                object section = System.Configuration.ConfigurationManager.GetSection("nhibernate.interceptors");
                if (section != null)
                {
                   var  interceptors = (NameValueCollection)section;
                    foreach (string k in interceptors)
                    {
                        _configuration.SetInterceptor((IInterceptor)Activator.CreateInstance(Type.GetType(interceptors[k])));                        
                    }
                }

            }
            //HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();//调用NHProfiler调试,不调用可注释掉
            _configuration.SetProperty("connection.connection_string_name", "ZLERP");
            _sessionFactory = _configuration.BuildSessionFactory();
        }

        public void Configuration(string dbname)
        {
            if (_configuration == null)
            {
                _configuration = new Configuration().Configure();

                object section = System.Configuration.ConfigurationManager.GetSection("nhibernate.interceptors");
                if (section != null)
                {
                    var interceptors = (NameValueCollection)section;
                    foreach (string k in interceptors)
                    {
                        _configuration.SetInterceptor((IInterceptor)Activator.CreateInstance(Type.GetType(interceptors[k])));
                    }
                }

            }

            _configuration.SetProperty("connection.connection_string_name", dbname);
            _sessionFactory = _configuration.BuildSessionFactory();

        }

        private Configuration NHConfiguration {
            get {
                if (_configuration == null)
                {
                    Configuration();
                }
                return _configuration;
            }
        }

        public ISessionFactory SessionFactory
        {
            get
            {
               // return  System.Web.HttpContext.Current.Items["ZLERP.SessionFactory"] as ISessionFactory;
                
                if (_sessionFactory == null)
                    _sessionFactory = NHConfiguration.BuildSessionFactory();
                return _sessionFactory;
                 
            }
        }

        public ISession CurrentSession
        {
            get
            {
                if (_currentSession == null)
                    throw new InvalidOperationException("You are not in a unit of work.");
                return _currentSession;
            }
            set { _currentSession = value; }
        }

        public void DisposeUnitOfWork(IUnitOfWork adapter)
        {
            CurrentSession = null;
            UnitOfWork.DisposeUnitOfWork(adapter);
          //  adapter.Dispose();
         
            //close session
          //  CurrentSessionContext.Unbind(SessionFactory).Dispose();
        }

        private ISession CreateSession()
        {
            //new session
          //  CurrentSessionContext.Bind(SessionFactory.OpenSession());
           // return SessionFactory.GetCurrentSession();
            return SessionFactory.OpenSession();
        }
    }

}
