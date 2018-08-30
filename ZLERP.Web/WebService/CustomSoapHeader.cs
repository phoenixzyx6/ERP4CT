using System;
using ZLERP.Web.Controllers;
using ZLERP.Model;
using System.Web.Services.Protocols;

namespace ZLERP.Web
{
    public class CustomSoapHeader:SoapHeader
    {
        public string UserId
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public CustomSoapHeader()
        { 
            
        }

        public CustomSoapHeader(string _UserId, string _Password)
        {
            this.UserId = _UserId;
            this.Password = _Password;
        }
        public bool ValidUser(string UID, string PassWord)
        {
            UserController UserC = new UserController();
            return UserC.ValidUser(UID, PassWord);
        }
    }
}