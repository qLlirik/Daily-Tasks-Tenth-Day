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
    /// Логика взаимодействия для SelectingDormitoryWindow.xaml
    /// </summary>
    public partial class SelectingDormitoryWindow : Window
    {
        public SelectingDormitoryWindow()
        {
            InitializeComponent();

            spDormitory.DataContext = HelpClasses.StaticClass.SelectDormitory;
        }

        private void click_Back(object sender, RoutedEventArgs e)
        {
            HelpClasses.StaticClass.SelectDormitory = null;
            new MainWindow().Show();
            this.Close();
        }

        private void click_LodgersInDormitoryWindowOpen(object sender, RoutedEventArgs e)
        {
            new LodgersInDormitoryWindow().Show();
            this.Close();
        }

        private void click_AddNewLivingWindowOpen(object sender, RoutedEventArgs e)
        {
            new AddNewLivingWindow().Show();
            this.Close();
        }

        private void click_AddNewRooms(object sender, RoutedEventArgs e)
        {
            new AddNewRoomsWindow().Show();
            this.Close();
        }
    }
}
