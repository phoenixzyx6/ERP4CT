using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace ZLERP.Model
{
    public class MyRequiredAttributeAdapter : RequiredAttributeAdapter
    {
        public MyRequiredAttributeAdapter(ModelMetadata metadata, ControllerContext context,RequiredAttribute attribute) : base(metadata,context,attribute) { }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            string errorMsg = string.Format("{0} 是必须的", Metadata.DisplayName);
            return new[] {new ModelClientValidationRequiredRule(errorMsg)};
        }
    }
}
