using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using System.Collections.Specialized;
using ZLERP.Model.Enums;
using System.Web;
using ZLERP.Resources;
using System.IO;　
namespace ZLERP.Business
{
    public  class AttachmentService : ServiceBase<Attachment>
    {

        internal AttachmentService(IUnitOfWork uow)
            : base(uow) 
        { 
        }

        public void Delete(int id) {
            var att = this.Get(id);
            if (att != null) {
                try
                {//尝试删除文件
                    string filePath = System.Web.HttpContext.Current.Server.MapPath(att.FileUrl);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
                catch (Exception ex) {
                    logger.Error(Lang.Attachment_Error_DeleteFileFailed, ex);
                }

                this.Delete(att);
            }
        }
    }
}

