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
            ViewModel VM = new ViewModel(new TestUIServices(true, true));
            VM.AddDefaultV1DataOnGridCommand.Execute(null);
            Assert.Equal(1, VM.MainCollection.Count);
            VM.AddDefaultV1DataCollectionCommand.Execute(null);
            Assert.Equal(2, VM.MainCollection.Count);
            Assert.True(VM.MainCollection.ChangingAfterSave);
        }
        [Fact]
        public void Test_FromFile()
        {
            ViewModel VM = new ViewModel(new TestUIServices(true, true));
            VM.AddElementFromFileCommand.Execute(null);
            Assert.Equal("data_from_file", VM.MainCollection.data[0].MeasurementInfo);
            Assert.Equal(new DateTime(2020, 11, 18), VM.MainCollection.data[0].MeasurementDate);
        }
        [Fact]
        public void Test_CustomData()
        {
            ViewModel VM = new ViewModel(new TestUIServices(true, true));
            VM.enteredV1DataOnGrid.AddCustom();
            Assert.Equal("Info is empty", VM.enteredV1DataOnGrid["Info"]);
        }
        [Fact]
        public void Test_Open()
        {
            ViewModel VM = new ViewModel(new TestUIServices(false, true));
            VM.AddDefaultsCommand.Execute(null);
            Assert.True(VM.MainCollection.ChangingAfterSave);
            VM.OpenCommand.Execute(null);
            Assert.Equal(6, VM.MainCollection.Count);
        }
        [Fact]
        public void Test_Filter()
        {
            ViewModel VM = new ViewModel(new TestUIServices(true, true));
            VM.AddDefaultV1DataCollectionCommand.Execute(null);
            Assert.Equal(1, VM.DataCollection.Count);
            VM.AddDefaultsCommand.Execute(null);
            Assert.Equal(3, VM.DataOnGrid.Count);
        }
    }
    public class TestUIServices: IUIServices
    {
        private readonly bool open;
        private readonly bool save;
        public TestUIServices(bool open, bool save)
        {
            this.open = open;
            this.save = save;
        }

        public bool? ConfirmOpen(out string fileName)
        {
            fileName = "test_fromFile.txt";
            return open;
        }

        public bool? ConfirmSave(out string fileName)
        {
            fileName = " ";
            return save;
        }

        public void ShowMessage(string msg)
        {
            throw new NotImplementedException();
        }

        public bool Warning()
        {
            return false; // îòêàçûâàåìñÿ ñîõðàíÿòü íåñîõðàíåííûå äàííûå
        }
    }
}
