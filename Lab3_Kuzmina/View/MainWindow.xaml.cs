using System;
using System.Windows;
using System.Windows.Data;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Controls;
using ViewModel_namespace;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel VM = new ViewModel();
        public static RoutedCommand AddCustomCommand = new RoutedCommand("AddCustom", typeof(View.MainWindow));
        public MainWindow()
        {
            InitializeComponent();
            DataContext = VM.mainCollection;
            grid_AddCustom.DataContext = VM.enteredV1DataOnGrid;
        }
        public void CheckingSave()
        {
            if (VM.ChangingAfterSave == true)
            {
                MessageBoxResult result = MessageBox.Show("Предупреждение!\n " +
                    "Внесенные данные будут потеряны. Хотите ли Вы сохранить их?", "WARNING",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                    if (dlg.ShowDialog() == true)
                        VM.mainCollection.Save(dlg.FileName);
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
            VM = new ViewModel();
            DataContext = VM.mainCollection;
        }
        private void OpenCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                CheckingSave();
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                if (dlg.ShowDialog() == true)
                    VM.mainCollection.Load(dlg.FileName);
                DataContext = VM.mainCollection;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Open\n" + ex.Message);
            }
        }
        private void CanSaveCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            if (VM.mainCollection.ChangingAfterSave == true)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }
        private void SaveCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            if (dlg.ShowDialog() == true)
                if (VM.mainCollection.Save(dlg.FileName) == false)
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
            VM.mainCollection.RemoveAt(listBox_Main.SelectedIndex);
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
            VM.AddCustom();
        }
        private void MenuItem_AddDefaults(object sender, RoutedEventArgs e)
        {
            VM.mainCollection.AddDefaults();
        }
        private void MenuItem_AddDefaultV1DataCollection(object sender, RoutedEventArgs e)
        {
            VM.AddDefaultV1DataCollection();
        }
        private void MenuItem_AddDefaultV1DataOnGrid(object sender, RoutedEventArgs e)
        {
            VM.AddDefaultV1DataOnGrid();
        }
        private void MenuItem_AddElementFromFile(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                if (dlg.ShowDialog() == true)
                    VM.AddFromFile(dlg.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("MenuItem_AddElementFromFile\n" + ex.Message);
            }
        }
        private void FilterDataCollection(object sender, FilterEventArgs args)
        {
            VM.FilterDataCollection(ref args);
        }
        private void FilterDataOnGrid(object sender, FilterEventArgs args)
        {
            VM.FilterDataOnGrid(ref args);
        }

    }
}
