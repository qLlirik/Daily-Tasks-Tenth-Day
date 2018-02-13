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
    /// Логика взаимодействия для AddNewLivingWindow.xaml
    /// </summary>
    public partial class AddNewLivingWindow : Window
    {
        DB.Entity db = HelpClasses.StaticClass.InicializateDateBase;

        public AddNewLivingWindow()
        {
            InitializeComponent();

            cbxLodgers.ItemsSource = db.Lodgers.Except(db.Living.Where(w=>w.End == null).Select(w=>w.Lodgers)).ToList(); ;
            cbxLodgers.DisplayMemberPath = "Name";
            cbxLodgers.SelectedIndex = 0;

            cbxRooms.ItemsSource = HelpClasses.StaticClass.SelectDormitory.Rooms.Except(db.Living.Where(w=>w.End == null).Select(w=>w.Rooms)).ToList();
            cbxRooms.DisplayMemberPath = "NumberInDormitory";
            cbxRooms.SelectedIndex = 0;

            dpSettlement.SelectedDate = DateTime.Now;
        }

        private void click_Back(object sender, RoutedEventArgs e)
        {
            new SelectingDormitoryWindow().Show();
            this.Close();
        }

        private void click_Settle(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((tbxDocument.Text.Length == 0) || (tbxGiver.Text.Length == 0))
                    throw new Exception();

                db.Living.Add(new DB.Living {
                    Document = tbxDocument.Text,
                    Lodgers = (DB.Lodgers)cbxLodgers.SelectedItem,
                    Rooms = (DB.Rooms)cbxRooms.SelectedItem,
                    Begin = DateTime.Now,
                    Giver = tbxGiver.Text,
                    Comment = tbxComment.Text.Length == 0 ? null : tbxComment.Text,
                    Payment = decimal.Parse(tbxPayment.Text),
                    Settlement = dpSettlement.SelectedDate.Value
                });
                db.SaveChanges();

                if (MessageBox.Show("Жилец заселён в общежитие! Желаете заселить ещё?", "Perfect", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    new AddNewLivingWindow().Show();
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

        private void click_AddNewLodgersWindowOpen(object sender, RoutedEventArgs e)
        {
            new AddNewLodgerWindow().Show();
            this.Close();
        }
    }
}
