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
    /// Логика взаимодействия для AddNewRoomsWindow.xaml
    /// </summary>
    public partial class AddNewRoomsWindow : Window
    {
        DB.Entity db = HelpClasses.StaticClass.InicializateDateBase;

        public AddNewRoomsWindow()
        {
            InitializeComponent();

            cbxType.ItemsSource = db.RoomType.ToList();
            cbxType.DisplayMemberPath = "Name";
            cbxType.SelectedIndex = 0;
        }

        private void click_Back(object sender, RoutedEventArgs e)
        {
            new SelectingDormitoryWindow().Show();
            this.Close();
        }

        private void click_Add(object sender, RoutedEventArgs e)
        {
            try
            {
                db.Rooms.Add(new DB.Rooms {
                    Square = double.Parse(tbxSquare.Text),
                    Comment = tbxComment.Text.Length == 0 ? null : tbxComment.Text,
                    RoomBeds = byte.Parse(tbxRoomBeds.Text),
                    RoomType = (DB.RoomType)cbxType.SelectedItem,
                    Storey = byte.Parse(tbxStorey.Text),
                    Dormitories = HelpClasses.StaticClass.SelectDormitory,
                    NumberInDormitory = HelpClasses.StaticClass.SelectDormitory.Rooms.ToList().Last().NumberInDormitory + 1
                });
                db.SaveChanges();

                if (MessageBox.Show("Комната в общежитие была успешно добавлена! Желаете добавить ещё?", "Perfect", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    new AddNewRoomsWindow().Show();
                    this.Close();
                }
                else
                    click_Back(null,null);
            }
            catch 
            {
                MessageBox.Show("Введите корректные данные!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }            
        }

        private void click_Import(object sender, RoutedEventArgs e)
        {
            new ImportRoomsWindow().Show();
            this.Close();
        }
    }
}
