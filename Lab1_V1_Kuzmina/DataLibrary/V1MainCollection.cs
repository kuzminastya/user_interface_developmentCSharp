using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;

namespace Lab2_V1_2_Kuzmina
{
    [Serializable]
    public class V1MainCollection : IEnumerable<V1Data>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        [field:NonSerialized]
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        public List<V1Data> data = new List<V1Data>();
        public bool ChangingAfterSave { get; set; }
        public int maxNumberOfTests
        {
            get
            {
                //OnPropertyChanged(this, new PropertyChangedEventArgs(null));
                int selector (V1Data x)
                {
                    var query = from dataitem in x
                                select dataitem;
                    return query.Count();
                }
                return data.Max(selector);
            }
        }
        public IEnumerable<DataItem> EnumerationLength
        {
            get
            {
                var query = from v1data in data
                            from dataitem in v1data
                            orderby dataitem.Field.Length() descending
                            select dataitem;
                return query;
            }
        }
        public IEnumerable<float> EnumerationTime
        {
            get
            {
                var TGroup = from v1data in data
                            from dataitem in v1data
                            group dataitem by dataitem.T;
                var query = from gr in TGroup
                            where gr.Count() == 1
                            select gr.Key;
                return query;
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
        public V1MainCollection ()
        {
            CollectionChanged += ObrabCollectionChanged;
            PropertyChanged += ObrabPropertyChanged;
            //OnCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
        public void Add(V1Data item)
        {
            data.Add(item);
            List<V1Data> newItems = new List<V1Data>();
            newItems.Add(item);
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
            OnCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove));
            return result;
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
        public void Save(string filename)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = File.Create(filename);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, data);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Save\n " + ex.Message);
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Close();
            }
            ChangingAfterSave = false;
        }
        public void Load(string filename)
        {
            FileStream fileStream = null;
            List<V1Data> res = null;
            try
            {
                fileStream = File.OpenRead(filename);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                res = binaryFormatter.Deserialize(fileStream) as List<V1Data>;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Load\n " + ex.Message);
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Close();
            }
            data = res;
            OnCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
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
            for(int i = 0; i < Count; i++)
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
        public void OnCollectionChanged (object sender, NotifyCollectionChangedEventArgs args)
        {
            if (CollectionChanged != null)
                CollectionChanged(sender, args); 
        }
        public void ObrabCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            ChangingAfterSave = true;
        }
        public void OnPropertyChanged (object sender, PropertyChangedEventArgs args)
        {
            if (PropertyChanged != null)
                PropertyChanged(sender, args);
        }
        public void ObrabPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            Console.WriteLine("ObrabPropertyChanged");
        }
    }
}
