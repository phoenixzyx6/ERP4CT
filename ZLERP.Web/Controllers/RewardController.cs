using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    public class RewardController : BaseController<Reward, int>
    {
        int? _NoticeID;
        public override ActionResult Add(Reward entity)
        {
            //如果需要发公告通知，则先发通知公告再保存奖惩信息
            
            if (entity.IsNotice && !string.IsNullOrEmpty((_NoticeID = AddNotice(entity)).ToString()))
            {
                entity.IsNotice = true;
                entity.NoticeID = _NoticeID;
            }
            entity.IsEffective = true;
            entity.ExcuteMan = AuthorizationService.CurrentUserName;
            return base.Add(entity);
        }

        public override ActionResult Update(Reward entity)
        {
            //如果需要发公告通知，则先发通知公告再保存奖惩信息
            if (entity.IsNotice && !string.IsNullOrEmpty((_NoticeID = AddNotice(entity)).ToString()))
            {
                entity.IsNotice = true;
                entity.NoticeID = _NoticeID;
            }
            entity.ExcuteMan = AuthorizationService.CurrentUserName;
            return base.Update(entity);
        }

        private int? AddNotice(dynamic entity)
        {
            Notice notice = new Notice();
            notice.Title = entity.RewardsType;
            notice.IsTop = true;
            string Dep = this.service.User.Get(entity.UserID).Department.DepartmentName;
            notice.Content = Dep + " " + entity.UserID + "，" + entity.RewardsReason + "。鉴此，公司做如下处理：" + entity.ProcessingResult;
            notice.Builder = AuthorizationService.CurrentUserName;
            notice.BuildTime = DateTime.Now;
            return this.service.GetGenericService<Notice>().Add(notice).ID;
        }

        public ActionResult Revocation(Reward entity) {
            return base.Update(entity);
        }
    }    
}
