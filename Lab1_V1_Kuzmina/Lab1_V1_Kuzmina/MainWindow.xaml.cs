using System;
using System.Windows;
using System.Windows.Data;
using Lab2_V1_2_Kuzmina;

namespace Lab1_V1_Kuzmina
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        V1MainCollection mainCollection = new V1MainCollection();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = mainCollection;
        }
        public void CheckingSave()
        {
            if (mainCollection.ChangingAfterSave == true)
            {
                MessageBoxResult result = MessageBox.Show("Предупреждение!\n " +
                    "Внесенные данные будут потеряны. Хотите ли Вы сохранить их?", "WARNING",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(result == MessageBoxResult.Yes)
                {
                    Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                    if (dlg.ShowDialog() == true)
                        mainCollection.Save(dlg.FileName);
                }
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            CheckingSave();
        }
        private void MenuItem_New(object sender, RoutedEventArgs e)
        {
            CheckingSave();
            mainCollection = new V1MainCollection();
            DataContext = mainCollection;
        }
        private void MenuItem_Open(object sender, RoutedEventArgs e)
        {
            CheckingSave();
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            if (dlg.ShowDialog() == true)
                mainCollection = mainCollection.Load(dlg.FileName);
            DataContext = mainCollection;
        }
        private void MenuItem_Save(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            if (dlg.ShowDialog() == true)
                if (mainCollection.Save(dlg.FileName) == false)
                    MessageBox.Show("unsuccessful saving!");
        }
        private void MenuItem_AddDefaults(object sender, RoutedEventArgs e)
        {
            mainCollection.AddDefaults();
        }
        private void MenuItem_AddDefaultV1DataCollection(object sender, RoutedEventArgs e)
        {
            V1DataCollection newItem = new V1DataCollection("info_collection_mainCollection", new DateTime(2021, 3, 13));
            newItem.InitRandom(5, 0, 50, 0, 7);
            mainCollection.Add(newItem);
        }
        private void MenuItem_AddDefaultV1DataOnGrid(object sender, RoutedEventArgs e)
        {
            V1DataOnGrid newItem = new V1DataOnGrid("info_grid_mainCollection", new DateTime(2021, 3, 13), new Lab2_V1_2_Kuzmina.Grid(0, 1, 3));
            newItem.InitRandom(0, 10);
            mainCollection.Add(newItem);
        }
        private void MenuItem_AddElementFromFile(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                if (dlg.ShowDialog() == true)
                    mainCollection.Add(new V1DataCollection(dlg.FileName));
            }
            catch(Exception ex)
            {
                MessageBox.Show("MenuItem_AddElementFromFile\n" + ex.Message);
            }
        }
        private void MenuItem_Remove(object sender, RoutedEventArgs e)
        {
            mainCollection.RemoveAt(listBox_Main.SelectedIndex);
        }
        private void FilterDataCollection(object sender, FilterEventArgs args)
        {
            V1Data item = args.Item as V1Data;
            if (item != null)
            {
                if (item.GetType() == typeof(V1DataCollection)) 
                    args.Accepted = true;
                else args.Accepted = false;
            }
        }
        private void FilterDataOnGrid(object sender, FilterEventArgs args)
        {
            V1Data item = args.Item as V1Data;
            if (item != null)
            {
                if (item.GetType() == typeof(V1DataOnGrid))
                    args.Accepted = true;
                else args.Accepted = false;
            }
        }
    }
    [ValueConversion(typeof(DataItem), typeof(string))]
    public class DataItemConverter1 : IValueConverter
    {
        public object Convert(object value, Type targetType, 
            object parameter, System.Globalization.CultureInfo culture)
        {
            DataItem dataitem = (DataItem)value;
            return dataitem.T;
        }
        public object ConvertBack(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
    [ValueConversion(typeof(DataItem), typeof(string))]
    public class DataItemConverter2 : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            DataItem item = (DataItem)value;
            string res = "X: " + item.Field.X;
            res += "\nY: " + item.Field.Y;
            res += "\nZ: " + item.Field.Z;
            res += "\nМодуль поля = " + item.Field.Length();
            return res;
        }
        public object ConvertBack(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    [ValueConversion(typeof(V1DataOnGrid), typeof(string))]
    public class DataOnGridConverter1 : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            V1DataOnGrid item = (V1DataOnGrid)value;
            if (item != null && item.FieldValues.Length > 0)
            {
                float tInit = item.TimeGrid.TimeInitial;
                return "ПЕРВЫЙ УЗЕЛ СЕТКИ: " + tInit.ToString() +
                    "\nКомпоненты поля:\nX: " + item.FieldValues[0].X +
                    "\nY: " + item.FieldValues[0].Y + "\nZ: " + item.FieldValues[0].Z;
            }
            else return "";
        }
        public object ConvertBack(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
    [ValueConversion(typeof(V1DataOnGrid), typeof(string))]
    public class DataOnGridConverter2 : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            V1DataOnGrid item = (V1DataOnGrid)value;
            if (item != null && item.FieldValues.Length > 0)
            {
                int n = item.TimeGrid.GridAmount - 1; //номер последнего узла
                float tInit = item.TimeGrid.TimeInitial;
                float tLast = tInit + n * item.TimeGrid.TimeStep;
                return "ПОСЛЕДНИЙ УЗЕЛ СЕТКИ: " + tLast +
                    "\nКомпоненты поля:\nX: " + item.FieldValues[n].X +
                    "\nY: " + item.FieldValues[n].Y + "\nZ: " + item.FieldValues[n].Z;
            }
            else return "";
        }
        public object ConvertBack(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
