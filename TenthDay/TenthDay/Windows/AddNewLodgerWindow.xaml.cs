using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TenthDay.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddNewLodgerWindow.xaml
    /// </summary>
    public partial class AddNewLodgerWindow : Window
    {
        DB.Entity db = HelpClasses.StaticClass.InicializateDateBase;

        public AddNewLodgerWindow()
        {
            InitializeComponent();

            dpPassprtDate.SelectedDate = DateTime.Now;
        }

        private void clickAdd(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((tbxName.Text.Length == 0) || (tbxPassport.Text.Length == 0) || (tbxRegion.Text.Length == 0) || (tbxWork.Text.Length == 0))
                    throw new Exception();

                db.Lodgers.Add(new DB.Lodgers {
                    Name = tbxName.Text,
                    Passport = tbxPassport.Text,
                    PassportDate = dpPassprtDate.SelectedDate.Value,
                    Region = tbxRegion.Text,
                    Work = tbxWork.Text,
                    Children = chbxChildren.IsChecked == true ? true : false
                });
                db.SaveChanges();

                if (MessageBox.Show("Добавление нового жильца прошла успешно! Желаете добавить ещё?", "Perfect", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    new AddNewLodgerWindow().Show();
                    this.Close();
                }
                else
                    click_Back(null, null);
            }
            catch
            {
                MessageBox.Show("Введите корректные данные!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void click_Back(object sender, RoutedEventArgs e)
        {
            new AddNewLivingWindow().Show();
            this.Close();
        }
    }
}
