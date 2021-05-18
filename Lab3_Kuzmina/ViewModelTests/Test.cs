using System;
using Xunit;
using ViewModel_namespace;

namespace Lab3_Kuzmina.Tests
{
    public class Test
    {
        [Fact]
        public void Test_Init()
        {
            ViewModel VM = new ViewModel();
            VM.AddDefaultV1DataOnGrid();
            Assert.Equal(1, VM.mainCollection.Count);
            VM.AddDefaultV1DataCollection();
            Assert.Equal(2, VM.mainCollection.Count);
            Assert.True(VM.ChangingAfterSave);
        }
        [Fact]
        public void Test_FromFile()
        {
            ViewModel VM = new ViewModel();
            VM.AddFromFile("test_fromFile.txt");
            Assert.Equal("data_from_file", VM.mainCollection.data[0].MeasurementInfo);
            Assert.Equal(new DateTime(2020, 11, 18), VM.mainCollection.data[0].MeasurementDate);
        }
        [Fact]
        public void Test_CustomData()
        {
            ViewModel VM = new ViewModel();
            VM.AddCustom();
            Assert.Equal("Info is empty", VM.enteredV1DataOnGrid["Info"]);
        }
    }
}
