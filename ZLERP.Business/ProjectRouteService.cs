using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;

namespace ZLERP.Business
{
    public class ProjectRouteService : ServiceBase<ProjectRoute>
    {
        internal ProjectRouteService(IUnitOfWork uow) : base(uow) { }

        public void SavePath(string ProjectId, double? Distance, string LonLatStr)
        {
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    ProjectRoute PR = new ProjectRoute();
                    PR.ProjectId = ProjectId;
                    PR.Source = "E";
                    PR.IsPrimary = false;
                    PR = this.Add(PR);


                    this.m_UnitOfWork.Flush();
                    RouteDetail RD = new RouteDetail();
                    RD.Distance = Distance;
                   
                    RD.LonLatStr = LonLatStr;
                    RD.RouteId = PR.ID;
                    RD.ProjectID = PR.ProjectId;
                    RD.ProjectRoute = PR;
                    PublicService ps = new PublicService();
                    ps.GetGenericService<RouteDetail>().Add(RD);
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message, ex);
                    throw ex;
                }
            }
        }
    }
}
