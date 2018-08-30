using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;

namespace ZLERP.Business
{
    public class ProjectService : ServiceBase<Project>
    {
        internal ProjectService(IUnitOfWork uow) : base(uow) { }

        public override Project Add(Project entity)
        {
            try
            {
                Project temp = base.Add(entity);
                if (base.GPSSwitch("ZLZKGPS"))
                { //若开启了中联GPS同步功能，则需往GPS中添加工程信息
                    string projectId = temp.ID;
                    string tokenKey;
                    tokenKey = base.GPSLogin();
                    GPSService.EntryServiceClient gpsclient = new GPSService.EntryServiceClient();
                    string ProjectXML = gpsclient.ProjectTryGetInfo(tokenKey, projectId);
                    if (ProjectXML == string.Empty) {
                        gpsclient.ProjectAdd(tokenKey, temp.ID, temp.ProjectAddr, temp.ProjectName, temp.BuildUnit, temp.ConstructUnit, temp.LinkMan, temp.Tel, (double?)temp.Latitude, (double?)temp.Longitude, (double?)temp.PlaceRange,temp.CPOrder?? 0, temp.IsShow);
                    }
                }
                return temp;
            }catch(Exception ex){
                throw ex;
            }
        }
    }
}
