using System;
using Lab3_V1_Kuzmina;
using System.Windows.Data;

namespace ViewModel_namespace
{

    [ValueConversion(typeof(DataItem), typeof(string))]
    public class DataItemConverter1 : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            DataItem dataitem = (DataItem)value;
            return dataitem.T;
        }
        public object ConvertBack(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
    [ValueConversion(typeof(DataItem), typeof(string))]
    public class DataItemConverter2 : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            DataItem item = (DataItem)value;
            string res = "X: " + item.Field.X;
            res += "\nY: " + item.Field.Y;
            res += "\nZ: " + item.Field.Z;
            res += "\nМодуль поля = " + item.Field.Length();
            return res;
        }
        public object ConvertBack(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    [ValueConversion(typeof(V1DataOnGrid), typeof(string))]
    public class DataOnGridConverter1 : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            V1DataOnGrid item = (V1DataOnGrid)value;
            if (item != null && item.FieldValues.Length > 0)
            {
                float tInit = item.TimeGrid.TimeInitial;
                return "ПЕРВЫЙ УЗЕЛ СЕТКИ: " + tInit.ToString() +
                    "\nКомпоненты поля:\nX: " + item.FieldValues[0].X +
                    "\nY: " + item.FieldValues[0].Y + "\nZ: " + item.FieldValues[0].Z;
            }
            else return "";
        }
        public object ConvertBack(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
    [ValueConversion(typeof(V1DataOnGrid), typeof(string))]
    public class DataOnGridConverter2 : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            V1DataOnGrid item = (V1DataOnGrid)value;
            if (item != null && item.FieldValues.Length > 0)
            {
                int n = item.TimeGrid.GridAmount - 1; //номер последнего узла
                float tInit = item.TimeGrid.TimeInitial;
                float tLast = tInit + n * item.TimeGrid.TimeStep;
                return "ПОСЛЕДНИЙ УЗЕЛ СЕТКИ: " + tLast +
                    "\nКомпоненты поля:\nX: " + item.FieldValues[n].X +
                    "\nY: " + item.FieldValues[n].Y + "\nZ: " + item.FieldValues[n].Z;
            }
            else return "";
        }
        public object ConvertBack(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

}