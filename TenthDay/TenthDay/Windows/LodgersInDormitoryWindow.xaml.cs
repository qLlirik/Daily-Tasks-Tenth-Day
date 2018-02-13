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
    /// Логика взаимодействия для LodgersInDormitoryWindow.xaml
    /// </summary>
    public partial class LodgersInDormitoryWindow : Window
    {
        DB.Entity db = HelpClasses.StaticClass.InicializateDateBase;
        DB.Living EvictLiving = new DB.Living();

        public LodgersInDormitoryWindow()
        {
            InitializeComponent();

            click_Search(null,null);
        }

        private void click_Back(object sender, RoutedEventArgs e)
        {
            new SelectingDormitoryWindow().Show();
            this.Close();
        }

        private void click_Search(object sender, RoutedEventArgs e)
        {
            var qwery = db.Living.Where(w=>w.End == null).Where(w=>w.Rooms.HosteID == HelpClasses.StaticClass.SelectDormitory.Hostel);
            if (tbxSearch.Text.Length != 0)
                qwery = qwery.Where(w => w.Lodgers.Name.Contains(tbxSearch.Text));
            lv.ItemsSource = qwery.ToList();
        }

        private void click_Evict(object sender, RoutedEventArgs e)
        {
            EvictLiving = (DB.Living)((Button)sender).DataContext;
            pp.IsOpen = true;
        }

        private void click_PopupClose(object sender, RoutedEventArgs e)
        {
            pp.IsOpen = false;
        }

        private void click_EvictLiving(object sender, RoutedEventArgs e)
        {
            if (tbxReason.Text.Length == 0)
            {
                Topmost = true;
                MessageBox.Show("Введите причину выселения!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                
                return;
            }

            EvictLiving.End = DateTime.Now;
            EvictLiving.Reason = tbxReason.Text;
            db.SaveChanges();

            MessageBox.Show("Жилец выселен!","Perfect",MessageBoxButton.OK,MessageBoxImage.Information);
            click_PopupClose(null,null);
            click_Search(null,null);
        }
    }
}
