using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace ManagementWindow.ViewModel
{
    public  class ShowPersonnelViewModel:ObservableObject
    {
        public AppData AppData { get; set; } = AppData.Instance;
        private string name;
        public string Name { get => name; set =>SetProperty(ref name,value); }

        private string iD;
        public string ID { get => iD; set => SetProperty(ref iD, value); }

        private string startTime;
        public string StartTime { get => startTime; set => SetProperty(ref startTime, value); }

        private string endTime;
        public string EndTime { get => endTime; set => SetProperty(ref endTime, value); }

        private string grade;
        public string Grade { get => grade; set => SetProperty(ref grade, value); }

        private string phone;
        public string Phone { get => phone; set => SetProperty(ref phone, value); }
        private string email;
        public string Email { get => email; set => SetProperty(ref email, value); }


        public void Assignment(string name)
        {
            var mess = AppData.ControlViewModel.showPersonnelManage.Find(e => e.Name == name);
            if (mess != null)
            {
                ID = mess.ID;
                Grade = mess.grade;
                phone = mess.Phone;
                Name = name;
                StartTime = mess.StarTime;
                EndTime = mess.EndTime;
            }
        }
    }
}
