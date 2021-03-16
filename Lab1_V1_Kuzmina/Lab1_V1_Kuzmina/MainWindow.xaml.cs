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
using Lab2_V1_2_Kuzmina;

namespace Lab1_V1_Kuzmina
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private V1MainCollection mainCollection = new V1MainCollection();
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //CheckingSave();
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
            //TODO
        }
    }
}
