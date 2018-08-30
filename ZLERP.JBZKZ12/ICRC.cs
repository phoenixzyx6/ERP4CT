using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.JBZKZ12
{
    public interface ICRC
    {
        long GetCrc(int bval);
        long GetCrc(string str);
        long GetCrc(byte[] buffer);
        long GetCrc(byte[] buffer, int offset, int count);
    }
}
