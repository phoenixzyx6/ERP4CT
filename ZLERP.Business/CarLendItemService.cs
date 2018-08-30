using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZLERP.IRepository;
using ZLERP.NHibernateRepository;
using ZLERP.Model;
using ZLERP.Model.Enums;

namespace ZLERP.Business
{
    public class CarLendItemService : ServiceBase<CarLendItem>
    {
        internal CarLendItemService(IUnitOfWork uow) : base(uow) { }

        public void AddAllCars(string id)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    IList<Car> carInfo = this.m_UnitOfWork.GetRepositoryBase<Car>().All("IsUsed=1 and CarStatus=0", "ID", true);
                    IList<CarLendItem> carLendItems = this.m_UnitOfWork.GetRepositoryBase<CarLendItem>().All(" BackTime is null or CarLendID='" + id + "'", "ID", true);
                    foreach (Car car in carInfo)
                    {
                        //车辆是否已经添加
                        bool isExist = false;
                        foreach (CarLendItem carLendItem in carLendItems)
                        {
                            if (car.ID == carLendItem.CarID)
                            {
                                isExist = true;
                                break;
                            }
                        }
                        if (!isExist)
                        {
                            CarLendItem carLendItem = new CarLendItem();
                            carLendItem.CarLendID = id;
                            carLendItem.CarID = car.ID;
                            this.m_UnitOfWork.GetRepositoryBase<CarLendItem>().Add(carLendItem);
                            car.CarStatus = CarStatus.RentCar;
                            this.m_UnitOfWork.GetRepositoryBase<Car>().Update(car);
                        }
                    }
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public override void Delete(object[] ids)
        {
            using (IGenericTransaction tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    CarLendItem carLendItem = null;
                    CarService carService = new CarService(this.m_UnitOfWork);
                    foreach (object id in ids)
                    {
                        carLendItem = this.Get(id);
                        if (carLendItem != null)
                        {
                            if (carLendItem.BackTime != null)
                            {
                                throw new Exception("已回厂记录不能删除");
                            }
                            carService.ChangeCarStatus(carLendItem.CarID, CarStatus.AllowShipCar);
                        }
                        this.Delete(carLendItem);
                    }
                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }
    }
}
