using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.ViewModels
{
    public class CarAndDriver
    {
        public Car Car{get;set;}

        public DriverForCar DriverForCar{get; set;}

        public User User { get; set; }
    }
}
