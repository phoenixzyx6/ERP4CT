namespace ZLERP.Internal
{
    internal static class AssemblyInfo
    {
        public const string AssemblyCopyright = "Copyright (c) 2012-2016 ZOOMLION.";
        public const string AssemblyCompany = "中联重科股份有限公司";
        public const string AssemblyPrev = "ZLERP";
        public const int VersionId = 403;
        public const string VersionShort = "4.0.3";
        public const string Version = VersionShort + ".1130";
       
        public const string FileVersion = VersionShort + ".0";
        public const string Description = "\r\n"  
        #if DEBUG
         + "Build=DEBUG\r\n"
        #elif RELEASE	 	 
                + "Build=RELEASE\r\n"	 	 
        #else	 	 
                + "Build=UNKNOW\r\n"	 	 
        #endif
         + "";
    }
}
 