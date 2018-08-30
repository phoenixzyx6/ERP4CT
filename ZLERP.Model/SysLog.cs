using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using ZLERP.Model.Enums;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Web.Script.Serialization;
namespace ZLERP.Model
{
    /// <summary>
    /// 系统日志
    /// </summary>
	public class SysLog : _SysLog
    {
        public static SysLog Create(SysLogType logType, string url, string userId, string userIP, object objectId, object objectData, string memo)
        { 
            return new SysLog {
                LogType = logType.ToString(),
                Url = url, 
                UserIP = userIP, 
                ObjectId = objectId!=null ? objectId.ToString(): string.Empty,
                ObjectData =  SerializeData(objectData),
                ObjectType = objectData!=null? objectData.GetType().FullName : string.Empty,
                Memo = memo,
                Builder = userId,
                BuildTime = DateTime.Now
            };
        }

        static string SerializeData(object data)
        {
            if (data != null)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(data);
            }
            else
                return string.Empty;
        }
         
	}
}