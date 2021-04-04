using System;
using System.Collections.Generic;
using System.Numerics;
using System.IO;
using System.Collections;
using System.Globalization;

namespace Lab2_V1_2_Kuzmina
{
    [Serializable]
    public class V1DataCollection : V1Data, IEnumerable<DataItem>
    {
        public override List<DataItem> DataItems { get; set; }
        public V1DataCollection(string info, DateTime date) : base(info, date)
        {
            DataItems = new List<DataItem>();
        }
        public V1DataCollection(string filename)
         /* в файле данные хранятся следующим образом (на русском языке):
            строка для поля базового класса info \n
            дата - поле базового класса date (дата в формате ДД.ММ.ГГГГ) \n
            затем N точек поля: одна точка это значение t и вектор значений поля
            (одно значение на одной строке, то есть для одной точки - 4 строки) */
        {
            CultureInfo.CurrentCulture = new CultureInfo("ru-RU");
            float ReadSingle(StreamReader streamReader)
            {
                float res;
                if (!streamReader.EndOfStream)
                    res = Convert.ToSingle(streamReader.ReadLine());
                else
                    throw new Exception("Ожидалось чтение координаты");
                return res;
            }
            DataItems = new List<DataItem>();
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Open);
                StreamReader streamReader = new StreamReader(fs);
                string info;
                if (!streamReader.EndOfStream)
                    info = streamReader.ReadLine();
                else
                    throw new Exception("Ожидалось чтение строки с данными базового класса");
                DateTime date;
                if (!streamReader.EndOfStream)
                    date = Convert.ToDateTime(streamReader.ReadLine());
                else
                    throw new Exception("Ожидалось чтение даты");
                base.MeasurementInfo = info;
                base.MeasurementDate = date;
                while (!streamReader.EndOfStream)
                {
                    float t = ReadSingle(streamReader);
                    float x = ReadSingle(streamReader);
                    float y = ReadSingle(streamReader);
                    float z = ReadSingle(streamReader);
                    Vector3 field = new Vector3(x, y, z);
                    DataItems.Add(new DataItem(t, field));
                }
                streamReader.Close();
            }
            /*catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }*/ // исключение обработается в приложении
            finally
            {
                if (fs != null)
                    fs.Close();
            }
            CultureInfo.CurrentCulture = CultureInfo.InstalledUICulture;
        }
        public void InitRandom(int nItems, float tmin, float tmax, float minValue, float maxValue)
        {
            Random rnd = new Random();
            for (int i = 0; i < nItems; i++)
            {
                float time = (float)(rnd.NextDouble() * (tmax - tmin) + tmin);
                float x = (float)(rnd.NextDouble() * (maxValue - minValue) + minValue);
                float y = (float)(rnd.NextDouble() * (maxValue - minValue) + minValue);
                float z = (float)(rnd.NextDouble() * (maxValue - minValue) + minValue);
                Vector3 field = new Vector3(x, y, z);
                DataItems.Add(new DataItem(time, field));
            }
        }
        public override float[] NearZero(float eps)
        {
            float[] res = new float[0];
            for (int i = 0; i < DataItems.Count; i++)
            {
                float x = DataItems[i].Field.X;
                float y = DataItems[i].Field.Y;
                float z = DataItems[i].Field.Z;
                float length = (float)Math.Sqrt(x * x + y * y + z * z);
                if (length < eps)
                {
                    Array.Resize<float>(ref res, res.Length + 1);
                    res[res.Length - 1] = DataItems[i].T;
                }
            }
            return res;
        }
        public override string ToString()
        {
            string typeName = "V1DataCollection \n";
            string baseClass = base.ToString() + "\n";
            string amount = "ЧИСЛО ЗНАЧЕНИЙ ПОЛЯ НА НЕРАВНОМЕРНОЙ СЕТКЕ: " +
                DataItems.Count + "\n";
            return typeName + baseClass + amount;
        }
        public override string ToLongString()
        {
            string components = "";
            if (DataItems.Count != 0)
            {
                components += "КОМПОНЕНТЫ ПОЛЯ:";
            }
            for (int i = 0; i < DataItems.Count; i++)
            {
                components += "\n В момент времени " + DataItems[i].T +
                    ": X=" + DataItems[i].Field.X +
                    "   Y=" + DataItems[i].Field.Y + "   Z=" + DataItems[i].Field.Z;
            }
            return this.ToString() + components + "\n";
        }
        public override string ToLongString(string format)
        {
            CultureInfo.CurrentCulture = new CultureInfo(format);
            string components = "";
            if (DataItems.Count != 0)
            {
                components += "КОМПОНЕНТЫ ПОЛЯ:";
            }
            for (int i = 0; i < DataItems.Count; i++)
            {
                components += "\n В момент времени " + DataItems[i].T +
                    ": X=" + DataItems[i].Field.X +
                    "   Y=" + DataItems[i].Field.Y + "   Z=" + DataItems[i].Field.Z +
                    "\n Длина вектора поля: " + DataItems[i].Field.Length();
            }
            string res = this.ToString() + components + "\n";
            CultureInfo.CurrentCulture = CultureInfo.InstalledUICulture;
            return res;
        }
        public IEnumerator<DataItem> GetEnumerator()
        {
            return DataItems.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return DataItems.GetEnumerator();
        }
    }
}
