using Net;
using System;

namespace ManagementWindow.ViewModel
{
    public class InIt
    {
        public InIt()
        {
            CNetLog.Instance.Init(AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
