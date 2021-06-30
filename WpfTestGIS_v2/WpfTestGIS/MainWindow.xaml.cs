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

namespace WpfTestGIS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        ApplicationViewModel applicationViewModel = new ApplicationViewModel();
        public static string lineState = null;

        public MainWindow()
        {
            InitializeComponent();

            txtBoxFile.ToolTip = "Введите полный путь до файла и нажмите ENTER"; //подсказка
        }

        private void butOverview_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();        //очищаю все что нарисовано во ViewBox
            var fileDialog = new System.Windows.Forms.OpenFileDialog();
            fileDialog.Filter = "*.txt|*.txt";      //фильтр для выбора только тектовых файлов
            var result = fileDialog.ShowDialog();   //окно выбора файла
            switch (result)
            {
                case System.Windows.Forms.DialogResult.OK:
                    var file = fileDialog.FileName;
                    txtBoxFile.Text = file;
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                default:
                    txtBoxFile.Text = null;
                    break;
            }
            DrawFigure();                   //вызываю метод где будут рисоваться объекты
            TxtBoxState.Text = lineState;   //выводим результат работы с файлом
        }

        private void txtBoxFile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                canvas.Children.Clear();        //очищаю все что нарисовано во ViewBox
                DrawFigure();                   //вызываю метод где будут рисоваться объекты
                TxtBoxState.Text = lineState;   //выводим результат работы с файлом
            }
        }

        private void DrawFigure()
        {
            applicationViewModel.ReadFile(txtBoxFile.Text);         //считываем данные с файла
            foreach (var point in ApplicationViewModel.pointsList)
            {
                Ellipse ellipse = new Ellipse();                    //через элипс я отобраю точку 
                ellipse.Width = 5;
                ellipse.Height = 5;
                ellipse.Fill = Brushes.Black;
                ellipse.Margin = new Thickness(point.X, point.Y, 0, 0);
                canvas.Children.Add(ellipse);
            }
            foreach (var lineModel in ApplicationViewModel.lineList)
            {
                Line line = new Line();         //строим линию
                line.X1 = lineModel.X1;
                line.Y1 = lineModel.Y1;
                line.X2 = lineModel.X2;
                line.Y2 = lineModel.Y2;
                line.Stroke = Brushes.Black;
                canvas.Children.Add(line);
            }
            foreach (var polygonModel in ApplicationViewModel.polygonList)
            {
                Polygon polygon = new Polygon();            //строим полигон
                List<Point> collection = new List<Point>();     //создаю коллекцию точек
                collection.Add(new Point(polygonModel.Point1.X, polygonModel.Point1.Y));        
                collection.Add(new Point(polygonModel.Point2.X, polygonModel.Point2.Y));
                collection.Add(new Point(polygonModel.Point3.X, polygonModel.Point3.Y));
                polygon.Points = new PointCollection(collection);
                polygon.Stroke = Brushes.Black;
                canvas.Children.Add(polygon);
            }
        }
    }
}
