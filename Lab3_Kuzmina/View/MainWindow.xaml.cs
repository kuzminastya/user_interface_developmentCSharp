using System;
using System.Windows;
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
            DataContext = VM;
            grid_AddCustom.DataContext = VM.enteredV1DataOnGrid;
        }
        public MainWindow()
        {
            InitializeComponent();
            ChangeDataContext();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            VM.CheckingSave();
        }
        private void MenuItem_New(object sender, RoutedEventArgs e)
        {
            VM.CheckingSave();
            VM = new ViewModel(new WPFUIServices());
            ChangeDataContext();
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
