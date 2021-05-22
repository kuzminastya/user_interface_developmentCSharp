using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Lab3_V1_Kuzmina
{
    public class EnteredV1DataOnGrid : IDataErrorInfo, INotifyPropertyChanged
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
        public EnteredV1DataOnGrid() { }
        public EnteredV1DataOnGrid(ref V1MainCollection _collection)
        {
            collection = _collection;
        }
        public void AddCustom()
        {
            float timeStep = 0.5f;
            Grid grid = new Grid(0.0f, timeStep, NodesNumber);
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
}
