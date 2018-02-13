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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TenthDay
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DB.Entity db = HelpClasses.StaticClass.InicializateDateBase;

        public MainWindow()
        {
            InitializeComponent();

            click_Search(null,null);
        }

        private void click_Search(object sender, RoutedEventArgs e)
        {
            sp.Children.Clear();
            var qwery = db.Dormitories.Where(w=>w.District != null);
            if (tbxSearch.Text.Length != 0)
                qwery = qwery.Where(w=>w.Address.Contains(tbxSearch.Text));

            foreach(var i in qwery)
            {
                UserControls.DormitoriesUserControl uc = new UserControls.DormitoriesUserControl();
                uc.DataContext = i;
                uc.Cursor = Cursors.Hand;
                uc.BorderBrush = Brushes.Black;
                uc.BorderThickness = new Thickness(0,0,0,1);
                uc.Margin = new Thickness(0, 10, 0, 10);
                uc.MouseLeftButtonDown += Uc_MouseLeftButtonDown;
                sp.Children.Add(uc);
            }
        }

        private void Uc_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HelpClasses.StaticClass.SelectDormitory = (DB.Dormitories)((UserControl)sender).DataContext;
            new Windows.SelectingDormitoryWindow().Show();
            this.Close();
        }

        private void keydown_tbxSearch(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                click_Search(null,null);
        }

        private void click_Add(object sender, RoutedEventArgs e)
        {
            new Windows.AddNewDormitoryWindow().Show();
            this.Close();
        }
    }
}
