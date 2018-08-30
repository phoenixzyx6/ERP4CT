using System;
using System.Collections.Generic;
using System.Text;
using ZLERP.Model.Generated;
namespace ZLERP.Model
{
    /// <summary>
    /// 验收取样记录
    /// </summary>
    public class Lab_ADM:_Lab_ADM
    {
        public virtual string StuffName
        {
            get
            {
                return this.Lab_Record == null ? "" : this.Lab_Record.StuffName;
            }
        }
    }
}