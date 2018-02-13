using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для ImportRoomsWindow.xaml
    /// </summary>
    public partial class ImportRoomsWindow : Window
    {
        DB.Entity db = HelpClasses.StaticClass.InicializateDateBase;

        public ImportRoomsWindow()
        {
            InitializeComponent();
        }

        private void click_Back(object sender, RoutedEventArgs e)
        {
            new AddNewRoomsWindow().Show();
            this.Close();
        }

        private void click_SelectCSVFiles(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = "CSV|*.csv";
            if (ofd.ShowDialog() == true)
                tbxPath.Text = ofd.FileName;
        }

        private void click_Import(object sender, RoutedEventArgs e)
        {
            if (tbxPath.Text.Length == 0)
            {
                MessageBox.Show("Выберите файл!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }

            var str = File.ReadAllLines(tbxPath.Text);
            int GoodCount = 0;
            int BadCount = 0;
            foreach(var i in str)
            {
                try
                {
                    var rm = i.Split(';');
                    db.Rooms.Add(new DB.Rooms {
                        Square = double.Parse(rm[0]),
                        Comment = rm[1],
                        RoomBeds = byte.Parse(rm[2]),
                        TypeID = byte.Parse(rm[2]) > 3 ? 3 : byte.Parse(rm[2]),
                        Storey = byte.Parse(rm[3]),
                        Dormitories = HelpClasses.StaticClass.SelectDormitory,
                        NumberInDormitory = HelpClasses.StaticClass.SelectDormitory.Rooms.OrderBy(w=>w.NumberInDormitory).ToList().Last().NumberInDormitory + 1
                    });
                    db.SaveChanges();
                    GoodCount++;
                }
                catch
                {
                    BadCount++;
                }
            }
            txbGoodCount.Text = GoodCount + "";
            txbBadCount.Text = BadCount + "";
            string mes = "";
            if (BadCount == 0)
                mes = "Импорт комнат прошёл успешно!";
            else if (GoodCount == 0)
                mes = "Импорт комнат не произошёл из-за полученных ошибок!";
            else
                mes = "Импорт комнат прошёл успешно с незначительными ошибками";
            MessageBox.Show(mes, "Perfect",MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
