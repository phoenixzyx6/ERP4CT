
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization;
using ZLERP.Model;
using ZLERP.Web.Helpers;
using System.Configuration;
using System.Xml;
using System.Web.Mvc;
using ZLERP.Resources;

namespace ZLERP.Web.Controllers
{
    public class YearAccountController : BaseController<YearAccount, string>
    {
        public override System.Web.Mvc.ActionResult Add(YearAccount entity)
        {
            try
            {
                

                string filename = entity.DBName != null ? entity.DBName : "ZLERP" + entity.ID;
                
                ActionResult ar = base.Add(entity);
                
                string v_path = ConfigurationManager.AppSettings["AccountPath"];
                string d_path = Server.MapPath(v_path);
                this.service.YearAccount.BuildAccount(d_path, filename, entity.DBPath);

                entity.DBName = filename;
                base.Update(entity);
                

                string config = Server.MapPath("/") + @"\web.config";
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(config);


                XmlNode xnl = xmldoc.SelectSingleNode("/configuration/connectionStrings");

                XmlNode tmp = xnl.FirstChild;
                string conn = tmp.Attributes[1].Value;

                //conn="Server=.; Database=ZLERP; Uid=erp; Pwd=r123;"
                //Database=ZLERP
                string _database = "Database=";
                string dbname = conn.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim().Remove(0, _database.Length);

                XmlNode node = xmldoc.CreateNode(XmlNodeType.Element, "add","");
                XmlAttribute att1 = xmldoc.CreateAttribute("name");
                att1.Value = filename;
                XmlAttribute att2 = xmldoc.CreateAttribute("connectionString");
                att2.Value = conn.Replace(dbname, filename);
                node.Attributes.Append(att1);
                node.Attributes.Append(att2);

                xnl.AppendChild(node);

                xmldoc.Save(config);

                return ar;
            }
            catch (Exception ex)
            {
                return HandleExceptionResult(ex);
            }

        }

        public System.Web.Mvc.ActionResult Run(YearAccount entity)
        {
            try
            {
                if (entity.IsRun)
                {
                    this.service.YearAccount.Run(entity);
                }
                return base.Update(entity);
            }
            catch (Exception ex)
            {
                return HandleExceptionResult(ex);
            }
        }
    }    
}
