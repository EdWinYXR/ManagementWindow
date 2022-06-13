using ManagementWindow.BaseClass;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ManagementWindow.ViewModel
{
    public class PersonnelManagementViewModel : ObservableObject
    {
        public RelayCommand<ListView> DeletePersonnel
        {
            get
            {
                return new RelayCommand<ListView>((res) =>
                {
                    Staff staff = res.SelectedItem as Staff;
                    if (MessageBox.Show(string.Format("是否删除 {0} 的员工信息",staff.Name), "警告", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        OrCale orCale = new OrCale();
                        string deletest = "";
                        int i = orCale.Change(deletest);
                        if (i != 0)
                        {
                            res.Items.Remove(staff);
                        }
                    }
                    else
                    {
                        return;
                    }
                });
            }
        }

        public RelayCommand AddPersonnel
        {
            get
            {
                return new RelayCommand(() =>
                {
                    View.AddPersonnel add = new View.AddPersonnel();
                    add.ShowDialog();
                });
            }
        }

        public RelayCommand BindingProject
        {
            get
            {
                return new RelayCommand(() =>
                {

                });
            }
        }
    }
}
