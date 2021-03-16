using System;
using System.Numerics;
using System.Globalization;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.Serialization;

namespace Lab2_V1_2_Kuzmina
{
    [Serializable]
    public struct DataItem: ISerializable
    {
        public float T { get; set; }
        public Vector3 Field { get; set; }
        public DataItem(float t, Vector3 field)
        {
            T = t;
            Field = field;
        }
        public void GetObjectData(SerializationInfo info, StreamingContext streamingContext)
        {
            info.AddValue("field_X", Field.X);
            info.AddValue("field_Y", Field.Y);
            info.AddValue("field_Z", Field.Z);
            info.AddValue("T", T);
        }
        public DataItem(SerializationInfo info, StreamingContext streamingContext)
        {
            float x = info.GetSingle("field_X");
            float y = info.GetSingle("field_Y");
            float z = info.GetSingle("field_Z");
            Field = new Vector3(x, y, z);
            T = info.GetSingle("T");
        }
        public override string ToString()
        {
            return "В МОМЕНТ ВРЕМЕНИ " + T + " КОМПОНЕНТЫ ПОЛЯ:\n" +
                "X: " + Field.X + "\nY: " + Field.Y + "\nZ: " + Field.Z;
        }
        public string ToString(string format)
        {
            CultureInfo.CurrentCulture = new CultureInfo(format);
            string res = ToString() + "\n Длина вектора поля: " + Field.Length();
            CultureInfo.CurrentCulture = CultureInfo.InstalledUICulture;
            return res;
        }
    }
    public struct Grid
    {
        public float TimeInitial { get; set; }
        public float TimeStep { get; set; }
        public int GridAmount { get; set; }
        public Grid(float initial, float step, int amount)
        {
            TimeInitial = initial;
            TimeStep = step;
            GridAmount = amount;
        }
        public override string ToString()
        {
            return "ПАРАМЕТРЫ СЕТКИ:\n Начальный момент времени: " + TimeInitial +
                "\n Шаг по времени: " + TimeStep + "\n Число узлов сетки: " + GridAmount;
        }
        public string ToString(string format)
        {
            CultureInfo.CurrentCulture = new CultureInfo(format);
            string res = ToString();
            CultureInfo.CurrentCulture = CultureInfo.InstalledUICulture;
            return res;
        }
    }
    [Serializable]
    public abstract class V1Data: IEnumerable<DataItem>
    {
        public string MeasurementInfo { get; set; }
        public DateTime MeasurementDate { get; set; }
        public virtual List<DataItem> DataItems { get; set; }
        public V1Data() {}
        public V1Data(string info, DateTime date)
        {
            MeasurementInfo = info;
            MeasurementDate = date;
        }
        public abstract float[] NearZero(float eps);
        public abstract string ToLongString();
        public override string ToString()
        {
            return "ПАРАМЕТРЫ ДАННЫХ:\n Информация об измерениях и идентификации множества данных: " +
                MeasurementInfo + "\n Дата, когда были выполнены измерения: " + MeasurementDate;
        }
        public abstract string ToLongString(string format);
        public IEnumerator<DataItem> GetEnumerator()
        {
            return DataItems.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return DataItems.GetEnumerator();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}