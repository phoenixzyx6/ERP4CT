using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using ZLERP.IRepository;

namespace ZLERP.Business
{
    public class ConStrAssessService : ServiceBase<ConStrAssess>
    {
        internal ConStrAssessService(IUnitOfWork uow)
            : base(uow)
        {
        }

        public bool HandleStat(string id)
        {
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    ConStrAssess obj = this.Get(id);
                    IList<M1AssessItem> mitems = obj.M1AssessItems;
                    foreach (M1AssessItem m in mitems)
                    {
                        this.m_UnitOfWork.GetRepositoryBase<M1AssessItem>().Delete(m);
                        this.m_UnitOfWork.Flush();
                    }
                    IList<ConStrAssessItem> items = obj.ConStrAssessItems;
                    decimal? minValue = 100;
                    decimal? maxValue = 0;
                    decimal? AvaValue = 0;
                    decimal? SumValue = 0;

                    int ifcuk = Convert.ToInt16(obj.ConStrength.Substring(1, 2));

                    if (items.Count == 0)
                    {
                        throw new Exception("没有数据源");
                    }
                    else
                    {
                        foreach (ConStrAssessItem item in items)
                        {
                            SumValue += (item.Exam1Str + item.Exam2Str + item.Exam3Str);
                            

                            AvaValue = SumValue / items.Count;

                            if (obj.StatMethod.Contains("M1"))
                            {
                                M1AssessItem temp = new M1AssessItem();
                                temp.ConStrAssessID = id;
                                temp.Fcuk = ifcuk;
                                if (temp.Fcuk > 20)
                                {
                                    temp.AFcuk = temp.Fcuk * (decimal)0.9;
                                }
                                else
                                {
                                    temp.AFcuk = temp.Fcuk * (decimal)0.85;
                                }
                                temp.Exam1Str = item.Exam1Str;
                                minValue = temp.Exam1Str;
                                maxValue = temp.Exam1Str;
                                temp.Exam2Str = item.Exam2Str;
                                if (temp.Exam2Str < minValue)
                                    minValue = temp.Exam1Str;
                                if (temp.Exam2Str > maxValue)
                                    maxValue = temp.Exam2Str;
                                temp.Exam3Str = item.Exam3Str;
                                if (temp.Exam3Str < minValue)
                                    minValue = temp.Exam3Str;
                                if (temp.Exam3Str > maxValue)
                                    maxValue = temp.Exam3Str;

                                temp.Fcumin = minValue;
                                temp.Fcumax = maxValue;
                                temp.mFcu = (temp.Exam1Str + temp.Exam2Str + temp.Exam3Str) / 3;
                                temp.FcukAddPar = temp.Fcuk + (decimal)0.7 * obj.StanDiff;
                                temp.FcukSubPar = temp.Fcuk - (decimal)0.7 * obj.StanDiff;
                                if (temp.Fcumin >= temp.AFcuk && temp.Fcumin >= temp.FcukSubPar && temp.mFcu >= temp.FcukAddPar)
                                {
                                    temp.Result = "合格";
                                }
                                else
                                {
                                    temp.Result = "不合格";
                                }
                                this.m_UnitOfWork.GetRepositoryBase<M1AssessItem>().Add(temp);
                                this.m_UnitOfWork.Flush();
                            }
                            else
                            {
                                if (AvaValue >= (decimal)1.15 * ifcuk && minValue >= (decimal)0.95 * ifcuk)
                                {
                                    obj.StatResult = "合格";
                                }
                                else
                                {
                                    obj.StatResult = "不合格";
                                }
                            }
                            this.Update(obj, null);

                        }
                    }
                    tx.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message);
                    return false;
                }
            }
        }

        public override ConStrAssess Add(ConStrAssess entity)
        {
            ConStrAssess temp = base.Add(entity);
            using (var tx = this.m_UnitOfWork.BeginTransaction())
            {
                try
                {
                    if (temp.StrCount > 0)
                    {
                        for (int i = 0; i < temp.StrCount; i++)
                        {
                            ConStrAssessItem item = new ConStrAssessItem();
                            item.ConStrAssessID = temp.ID;
                            item.Exam1Str = 0;
                            this.m_UnitOfWork.GetRepositoryBase<ConStrAssessItem>().Add(item);
                            this.m_UnitOfWork.Flush();
                        }
                    }
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    logger.Error(ex.Message);
                }
            }

            return temp;
        }
    }
}
