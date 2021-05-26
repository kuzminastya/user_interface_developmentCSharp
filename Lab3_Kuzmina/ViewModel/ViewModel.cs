using System;
using Lab3_V1_Kuzmina;
using System.Windows.Input;
using System.ComponentModel;

namespace ViewModel_namespace
{
    public interface IUIServices
    {
        bool? ConfirmOpen(out string fileName);
        bool? ConfirmSave(out string fileName);
        void ShowMessage(string msg);
        bool Warning();
    }
    public class ViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private V1MainCollection mainCollection = new V1MainCollection();
        public IUIServices services;
        public V1MainCollection MainCollection 
        { 
            get => mainCollection; 
            set 
            { 
                mainCollection = value;
                OnPropertyChanged("MainCollection");
            }
        }
        public V1MainCollection DataCollection
        {
            get => mainCollection.OnlyDataCollection();
            set 
            {
                DataCollection = mainCollection.OnlyDataCollection();
                OnPropertyChanged("DataCollection");
            }
        }
        public V1MainCollection DataOnGrid
        {
            get => mainCollection.OnlyDataOnGrid();
            set 
            {
                DataOnGrid = mainCollection.OnlyDataOnGrid();
                OnPropertyChanged("DataOnGrid");
            }
        }
        public int maxNumberOfTests => mainCollection.maxNumberOfTests;
        public void UpdateProperties()
        {
            OnPropertyChanged("MainCollection");
            OnPropertyChanged("DataCollection");
            OnPropertyChanged("DataOnGrid");
            OnPropertyChanged("maxNumberOfTests");
        }
        public EnteredV1DataOnGrid enteredV1DataOnGrid { get; set; }
        public V1Data RemovedItem { get; set; }
        public int RemovedIndex { get; set; }
        public ICommand OpenCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand AddDefaultsCommand { get; private set; }
        public ICommand AddDefaultV1DataCollectionCommand { get; private set; }
        public ICommand AddDefaultV1DataOnGridCommand { get; private set; }
        public ICommand AddElementFromFileCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ViewModel(IUIServices svc)
        {
            services = svc;
            mainCollection = new V1MainCollection();
            enteredV1DataOnGrid = new EnteredV1DataOnGrid(ref mainCollection);
            OpenCommand = new RelayCommand(
                _ => {
                    try
                    {
                        CheckingSave();
                        string filename;
                        if(svc.ConfirmOpen(out filename) == true)
                            mainCollection.Load(filename);
                        UpdateProperties();
                    }
                    catch (Exception ex)
                    {
                        svc.ShowMessage("Open\n" + ex.Message);
                    }
                }
                );
            SaveCommand = new RelayCommand(
                _ =>
                {
                    string filename;
                    if (svc.ConfirmSave(out filename) == true)
                        if (mainCollection.Save(filename) == false)
                            svc.ShowMessage("unsuccessful saving!");
                },
                _ => MainCollection.ChangingAfterSave == true
                );
            AddDefaultsCommand = new RelayCommand(
                _ =>{
                    mainCollection.AddDefaults();
                    UpdateProperties();
                }
                );
            AddDefaultV1DataCollectionCommand = new RelayCommand(
                _ => {
                    V1DataCollection newItem = new V1DataCollection("info_collection_mainCollection", new DateTime(2021, 3, 13));
                    newItem.InitRandom(5, 0, 50, 0, 7);
                    mainCollection.Add(newItem);
                    UpdateProperties();
                }
                );
            AddDefaultV1DataOnGridCommand = new RelayCommand(
                _ => {
                    V1DataOnGrid newItem = new V1DataOnGrid("info_grid_mainCollection", new DateTime(2021, 3, 13), new Grid(0, 1, 3));
                    newItem.InitRandom(0, 10);
                    mainCollection.Add(newItem);
                    UpdateProperties();
                }
                );
            AddElementFromFileCommand = new RelayCommand(
                _ => {
                    try
                    {
                        string filename;
                        if (svc.ConfirmOpen(out filename) == true)
                            mainCollection.Add(new V1DataCollection(filename));
                        UpdateProperties();
                    }
                    catch (Exception ex)
                    {
                        svc.ShowMessage("AddElementFromFile\n" + ex.Message);
                    }
                }
                );
            RemoveCommand = new RelayCommand(
                _ => {
                    mainCollection.RemoveAt(RemovedIndex);
                    UpdateProperties();
                },
                _ => mainCollection.data.Contains(RemovedItem)
                );
        }
        public void CheckingSave()
        {
            if (mainCollection.ChangingAfterSave == true && services.Warning() == true)
            {
                string filename;
                if (services.ConfirmSave(out filename) == true)
                    mainCollection.Save(filename);
            }
        }
        public void AddCustom()
        {
            enteredV1DataOnGrid.AddCustom();
            UpdateProperties();
        }
    }
}
