﻿using System;
using System.Collections.Generic;
using System.Text;
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ZLERP.Model
{
    public class PurchaseMain : _PurchaseMain
    {
        public virtual decimal? ContentNum
        { get; set; }
    }
}
