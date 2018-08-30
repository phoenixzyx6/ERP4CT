using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.IRepository
{
    public interface IGenericTransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
