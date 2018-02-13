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
    /// Логика взаимодействия для AddNewDormitoryWindow.xaml
    /// </summary>
    public partial class AddNewDormitoryWindow : Window
    {
        DB.Entity db = HelpClasses.StaticClass.InicializateDateBase;

        public AddNewDormitoryWindow()
        {
            InitializeComponent();
        }

        private void click_Back(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
        
        private byte[] ImageToByte(string uri)
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(new BitmapImage(new Uri(uri,UriKind.Relative))));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            encoder.Save(ms);
            return ms.ToArray();
        }

        private void click_SelectIamge(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = "All Images|*.png;*.jpg;*.bmp";
            if (ofd.ShowDialog() == true)
                tbxPhoto.Text = ofd.FileName;
        }

        private void click_Add(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((tbxAddress.Text.Length == 0) || (tbxDistrict.Text.Length == 0) || (tbxOwner.Text.Length == 0))
                    throw new Exception();

                db.Dormitories.Add(new DB.Dormitories {
                    Address = tbxAddress.Text,
                    District = tbxDistrict.Text,
                    Picture = ImageToByte(tbxPhoto.Text),
                    Owner = tbxOwner.Text,
                    RoomCount = int.Parse(tbxRoomCount.Text),
                    Beds = int.Parse(tbxBeds.Text)
                });
                db.SaveChanges();

                if (MessageBox.Show("Общежитие добавлено! Желаете добавить ещё?", "Perfect", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    new AddNewDormitoryWindow().Show();
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
    }
}
