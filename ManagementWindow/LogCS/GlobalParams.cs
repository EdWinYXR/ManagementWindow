using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClass.Tool
{
    public enum ServerMode
    {
        TM,
        MRMS
    }
    public class CGlobalParams
    {
        private static CGlobalParams m_Instance = new CGlobalParams();

        static public CGlobalParams Instance
        {
            get
            {
                return m_Instance;
            }
        }

        public static string CurrentDirectory
        { set; get; }

        public static string DefaultConnectName
        { set; get; }

        public static bool HostEnabled
        { set; get; }

        public static int SysMode
        { set; get; }

        public static ServerMode ServerMode
        { get; set; }
    }
}
