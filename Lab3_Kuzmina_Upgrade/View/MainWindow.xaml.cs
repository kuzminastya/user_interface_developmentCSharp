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
        ViewModel VM = new ViewModel(new WPFUIServices());
        public static RoutedCommand AddCustomCommand = new RoutedCommand("AddCustom", typeof(View.MainWindow));
        public void ChangeDataContext()
        {
            DataContext = VM.MainCollection;
            commandsPanel.DataContext = VM;
            commandsMenu1.DataContext = VM;
            commandsMenu2.DataContext = VM;
            commandsMenu3.DataContext = VM;
            grid_AddCustom.DataContext = VM.enteredV1DataOnGrid;
        }
        public MainWindow()
        {
            InitializeComponent();
            ChangeDataContext();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            VM.CheckingSave(new WPFUIServices());
        }
        private void MenuItem_New(object sender, RoutedEventArgs e)
        {
            VM.CheckingSave(new WPFUIServices());
            VM = new ViewModel(new WPFUIServices());
            ChangeDataContext();
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
            VM.MainCollection.RemoveAt(listBox_Main.SelectedIndex);
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
            VM.enteredV1DataOnGrid.AddCustom();
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
    public class WPFUIServices : IUIServices
    {
        public bool? ConfirmOpen(out string fileName)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            bool? result = dlg.ShowDialog();
            fileName = dlg.FileName;
            return result;
        }
        public void ShowMessage(string msg)
        {
            MessageBox.Show(msg);
        }
        public bool Warning()
        {
            return MessageBox.Show("Предупреждение!\n " +
                "Внесенные данные будут потеряны. Хотите ли Вы сохранить их?", "WARNING",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes;
        }
        public bool? ConfirmSave(out string fileName)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            bool? result = dlg.ShowDialog();
            fileName = dlg.FileName;
            return result;
        }
    }
}
