using System;
using System.Windows;
using System.Windows.Data;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Controls;
using Lab2_V1_2_Kuzmina;

namespace Lab2_V1_Kuzmina
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        V1MainCollection mainCollection = new V1MainCollection();
        EnteredV1DataOnGrid enteredV1DataOnGrid = new EnteredV1DataOnGrid();
        public static RoutedCommand AddCustomCommand = new RoutedCommand("AddCustom", typeof(Lab2_V1_Kuzmina.MainWindow));
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = mainCollection;
            enteredV1DataOnGrid = new EnteredV1DataOnGrid(ref mainCollection);
            grid_AddCustom.DataContext = enteredV1DataOnGrid;
        }
        public void CheckingSave()
        {
            if (mainCollection.ChangingAfterSave == true)
            {
                MessageBoxResult result = MessageBox.Show("Предупреждение!\n " +
                    "Внесенные данные будут потеряны. Хотите ли Вы сохранить их?", "WARNING",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
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
        private void OpenCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                CheckingSave();
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                if (dlg.ShowDialog() == true)
                    mainCollection.Load(dlg.FileName);
                DataContext = mainCollection;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Open\n" + ex.Message);
            }
        }
        private void CanSaveCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            if (mainCollection.ChangingAfterSave == true)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }
        private void SaveCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            if (dlg.ShowDialog() == true)
                if (mainCollection.Save(dlg.FileName) == false)
                    MessageBox.Show("unsuccessful saving!");
        }
        private void CanRemoveCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            if (listBox_Main.Items.Contains(listBox_Main.SelectedItem))
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }
        private void RemoveCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            mainCollection.RemoveAt(listBox_Main.SelectedIndex);
        }
        private void CanAddCustomCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            if (grid_AddCustom != null)
            {
                foreach (FrameworkElement child in grid_AddCustom.Children)
                {
                    if (Validation.GetHasError(child) == true)
                    {
                        e.CanExecute = false;
                        return;
                    }
                    e.CanExecute = true;
                }
            }
            else e.CanExecute = false;
        }
        private void AddCustomCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            enteredV1DataOnGrid.AddCustom();
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
            catch (Exception ex)
            {
                MessageBox.Show("MenuItem_AddElementFromFile\n" + ex.Message);
            }
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
    public class EnteredV1DataOnGrid: IDataErrorInfo, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private V1MainCollection collection;
        private string info;
        private int nodesNumber;
        private float minValue;
        private float maxValue;
        public string Info
        {
            get { return info; }
            set 
            {
                info = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Info")); 
            }
        }
        public int NodesNumber 
        {
            get { return nodesNumber; }
            set
            {
                nodesNumber = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("NodesNumber"));
            }
        }
        public float MinValue 
        {
            get { return minValue; }
            set
            {
                minValue = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("MinValue"));
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("MaxValue"));
            }
        }
        public float MaxValue 
        {
            get { return maxValue; }
            set
            {
                maxValue = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("MinValue"));
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("MaxValue"));
            }
        }
        public EnteredV1DataOnGrid () { }
        public EnteredV1DataOnGrid(ref V1MainCollection _collection) 
        {
            collection = _collection;
        }
        public void AddCustom()
        {
            float timeStep = 0.5f; 
            Lab2_V1_2_Kuzmina.Grid grid = new Lab2_V1_2_Kuzmina.Grid(0.0f, timeStep, NodesNumber);
            V1DataOnGrid item = new V1DataOnGrid(Info, DateTime.Now, grid);
            item.InitRandom(MinValue, MaxValue);
            collection.Add(item);
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Info"));
        }
        public string Error { get { return "Error Text"; } }
        public string this[string property]
        {
            get
            {
                string msg = null;
                switch (property)
                {
                    case "Info":
                        if (Info == null || Info.Length <= 0)
                        {
                            msg = "Info is empty";
                        }
                        foreach (V1Data item in collection.data)
                        {
                            if (item.MeasurementInfo == Info)
                            {
                                msg = "Info is empty";
                            }
                        }
                        break;
                    case "NodesNumber":
                        if (NodesNumber <= 2) msg = "Not enough NodesNumber";
                        break;
                    case "MinValue":
                    case "MaxValue":
                        if (MinValue >= MaxValue) msg = "minValue is greater than maxValue";
                        break;
                }
                return msg;
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
