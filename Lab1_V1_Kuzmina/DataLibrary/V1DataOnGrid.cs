using System;
using System.Collections.Generic;
using System.Numerics;
using System.Globalization;
using System.Collections;
using System.Runtime.Serialization;

namespace Lab2_V1_2_Kuzmina
{
    [Serializable]
    public class V1DataOnGrid : V1Data, IEnumerable<DataItem>, ISerializable
    {
        public Grid TimeGrid { get; set; }
        public Vector3[] FieldValues { get; set; }
        public V1DataOnGrid(string info, DateTime date, Grid grid) : base(info, date)
        {
            TimeGrid = grid;
            FieldValues = new Vector3[TimeGrid.GridAmount];
        }
        
        public void InitRandom(float minValue, float maxValue)
        {
            Random rnd = new Random();
            for (int i = 0; i < FieldValues.Length; i++)
            {
                FieldValues[i].X = (float)rnd.NextDouble() * (maxValue - minValue) + minValue;
                FieldValues[i].Y = (float)rnd.NextDouble() * (maxValue - minValue) + minValue;
                FieldValues[i].Z = (float)rnd.NextDouble() * (maxValue - minValue) + minValue;
            }
        }
        static public explicit operator V1DataCollection(V1DataOnGrid v1DataOnGrid)
        {
            V1DataCollection resData = new V1DataCollection(v1DataOnGrid.MeasurementInfo, v1DataOnGrid.MeasurementDate);
            List<DataItem> newList = new List<DataItem>();
            for (int i = 0; i < v1DataOnGrid.TimeGrid.GridAmount; i++)
            {
                newList.Add(new DataItem(v1DataOnGrid.TimeGrid.TimeInitial + i * v1DataOnGrid.TimeGrid.TimeStep,
                    v1DataOnGrid.FieldValues[i]));
            }
            resData.DataItems = newList;
            return resData;
        }
        public override float[] NearZero(float eps)
        {
            float[] res = new float[0];
            for (int i = 0; i < TimeGrid.GridAmount; i++)
            {
                float x = FieldValues[i].X;
                float y = FieldValues[i].Y;
                float z = FieldValues[i].Z;
                float length = (float)Math.Sqrt(x * x + y * y + z * z);
                if (length < eps)
                {
                    Array.Resize<float>(ref res, res.Length + 1);
                    res[res.Length - 1] = TimeGrid.TimeInitial + i * TimeGrid.TimeStep;
                }
            }
            return res;
        }
        public override string ToString()
        {
            string typeName = "V1DataOnGrid\n";
            string baseClass = base.ToString() + "\n";
            string grid = TimeGrid.ToString() + "\n";
            return typeName + baseClass + grid;
        }
        public override string ToLongString()
        {
            string components = "";
            if (FieldValues.Length != 0)
            {
                components += "КОМПОНЕНТЫ ПОЛЯ:";
            }
            for (int i = 0; i < FieldValues.Length; i++)
            {
                components += "\n " + i + "-ый узел сетки: X=" + FieldValues[i].X +
                    "   Y=" + FieldValues[i].Y + "   Z=" + FieldValues[i].Z;
            }
            return this.ToString() + components + "\n";
        }
        public override string ToLongString(string format)
        {
            CultureInfo.CurrentCulture = new CultureInfo(format);
            string components = "";
            if (FieldValues.Length != 0)
            {
                components += "КОМПОНЕНТЫ ПОЛЯ:";
            }
            for (int i = 0; i < FieldValues.Length; i++)
            {
                components += "\n " + i + "-ый узел сетки: X=" + FieldValues[i].X +
                    "   Y=" + FieldValues[i].Y + "   Z=" + FieldValues[i].Z +
                    "\n Длина вектора поля: " + FieldValues[i].Length();
            }
            string res = this.ToString() + components + "\n";
            CultureInfo.CurrentCulture = CultureInfo.InstalledUICulture;
            return res;
        }
        public IEnumerator<DataItem> GetEnumerator()
        {
            return ((V1DataCollection)this).DataItems.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((V1DataCollection)this).DataItems.GetEnumerator(); ;
        }
    }
}
