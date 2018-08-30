using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using log4net;

namespace ZLERP.Web.Helpers
{
    public class ZipHelper
    {
        static ILog logger = LogManager.GetLogger(typeof(ZipHelper));
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="zipFileName"></param>
        /// <returns></returns>
        public static bool ZipFile(string sourceFile, string zipFileName)
        {
            try
            {
                using (ZipOutputStream s = new ZipOutputStream(File.Create(zipFileName)))
                {

                    s.SetLevel(9);  

                    byte[] buffer = new byte[4096];
                    ZipEntry entry = new ZipEntry(Path.GetFileName(sourceFile));

                    entry.DateTime = DateTime.Now;
                    s.PutNextEntry(entry);

                    using (FileStream fs = File.OpenRead(sourceFile))
                    {
                        int sourceBytes;
                        do
                        {
                            sourceBytes = fs.Read(buffer, 0, buffer.Length);
                            s.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }


                    s.Finish();
                    s.Close();
                    return true;
                }
            }
            catch(Exception ex) {
                logger.Error("Compress File Error:", ex);
                return false;
            }
        }
    }
}