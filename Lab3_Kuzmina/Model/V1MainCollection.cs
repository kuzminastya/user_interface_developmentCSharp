using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;

namespace Lab3_V1_Kuzmina
{
    [Serializable]
    public class V1MainCollection : IEnumerable<V1Data>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        public List<V1Data> data = new List<V1Data>();
        public bool ChangingAfterSave { get; set; }
        public int maxNumberOfTests
        {
            get
            {
                int selector(V1Data x)
                {
                    var query = from dataitem in x
                                select dataitem;
                    return query.Count();
                }
                if (Count > 0)
                    return data.Max(selector);
                else
                    return 0;
            }
        }
        public int Count
        {
            get
            {
                if (data == null)
                    return 0;
                else
                    return data.Count;
            }
        }
        public V1MainCollection()
        {
            ChangingAfterSave = false;
            CollectionChanged += ObrabCollectionChanged;
        }
        public void Add(V1Data item)
        {
            data.Add(item);
            OnCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
        public bool Remove(string id, DateTime dateTime)
        {
            bool result = false;
            for (int i = 0; i < Count; i++)
            {
                if ((id == data[i].MeasurementInfo) && (dateTime == data[i].MeasurementDate))
                {
                    data.RemoveAt(i);
                    i--;
                    result = true;
                }
            }
            OnCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            return result;
        }
        public void RemoveAt(int index)
        {
            if (index >= 0 && index < data.Count())
                data.RemoveAt(index);
            OnCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
        public void AddDefaults()
        {
            if (Count == 0)
            {
                data = new List<V1Data>();
            }
            Grid newGrid = new Grid(0, 1, 3);
            V1DataOnGrid newItem1 = new V1DataOnGrid("info_grid_mainCollection", new DateTime(2020, 9, 30), newGrid);
            newItem1.InitRandom(0, 10);
            Add(newItem1);
            newItem1 = new V1DataOnGrid("info_grid_mainCollection", new DateTime(2020, 9, 30), newGrid);
            newItem1.InitRandom(0, 15);
            Add(newItem1);
            newGrid = new Grid(0, 1, 0); // 0 элементов
            newItem1 = new V1DataOnGrid("info_grid_mainCollection", new DateTime(2020, 9, 30), newGrid);
            newItem1.InitRandom(0, 7);
            Add(newItem1);

            V1DataCollection newItem2 = new V1DataCollection("info_collection_mainCollection", new DateTime(2020, 9, 30));
            newItem2.InitRandom(5, 0, 50, 0, 7);
            Add(newItem2);
            newItem2 = new V1DataCollection("info_collection_mainCollection", new DateTime(2020, 9, 30));
            newItem2.InitRandom(7, 0, 110, 0, 20);
            Add(newItem2);
            newItem2 = new V1DataCollection("info_collection_mainCollection", new DateTime(2020, 9, 30));
            newItem2.InitRandom(0, 0, 10, 0, 20);
            Add(newItem2);
        }
        public bool Save(string filename)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = File.Create(filename);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, data);
                OnCollectionChanged(binaryFormatter, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Save\n " + ex.Message);
                return false;
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Close();
            }
        }
        public void Load(string filename)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = File.OpenRead(filename);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                data = binaryFormatter.Deserialize(fileStream) as List<V1Data>;
                OnCollectionChanged(binaryFormatter, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Close();
            }
        }
        public V1MainCollection OnlyDataCollection()
        {
            V1MainCollection coll = new V1MainCollection();
            foreach (V1Data item in data)
                if (item.GetType() == typeof(V1DataCollection)) coll.Add(item);
            return coll;
        }
        public V1MainCollection OnlyDataOnGrid()
        {
            V1MainCollection coll = new V1MainCollection();
            foreach (V1Data item in data)
                if (item.GetType() == typeof(V1DataOnGrid)) coll.Add(item);
            return coll;
        }
        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < Count; i++)
            {
                result += "\n";
                result += data[i].ToString();
                //result += data[i].ToLongString();
            }
            return result;
        }
        public string ToLongString(string format)
        {
            string res = "";
            for (int i = 0; i < Count; i++)
            {
                res += data[i].ToLongString(format);
            }
            return res;
        }
        public IEnumerator<V1Data> GetEnumerator()
        {
            return data.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }
        public void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (CollectionChanged != null)
                CollectionChanged(sender, args);
        }
        public void ObrabCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (sender.GetType() == typeof(BinaryFormatter))
                ChangingAfterSave = false;
            else
                ChangingAfterSave = true;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("maxNumberOfTests"));
                PropertyChanged(this, new PropertyChangedEventArgs("Count"));
            }
        }
    }
}
