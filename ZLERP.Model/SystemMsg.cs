using System;
using System.Collections.Generic;
using System.Text;
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
namespace ZLERP.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemMsg : _SystemMsg
    {
        [ScriptIgnore]
        public virtual string Url
        {
            get;
            set;
        }

        public virtual string PID
        {
            get;
            set;
        }
        [DisplayName("存放草稿信件的收件人")]
        [StringLength(500)]
        public virtual string UserID
        {
            get;
            set;
        }
        /// <summary>
        /// 附件列表
        /// </summary>
        public virtual IList<Attachment> Attachments { get; set; }

        public virtual bool HasAttachments
        {
            get {
                if (Attachments != null && Attachments.Count > 0)
                {
                    return true;
                }
                else {
                    return false;
                }
            }
            
        }

        public virtual int DuringTime
        {
            get { 
                //先计算天数
                int days = DateTime.Today.Subtract(Convert.ToDateTime(SendTime).Date).Days;
                return days > 1 ? 2 : days;
            }
            
        }
    }
}