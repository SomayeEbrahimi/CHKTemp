using HCSampleApp.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HCSampleApp.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<string> TempList { get; set; } = new ObservableCollection<string>();

        public ICommand PairTempCommand { get; set; }
        public ICommand StartTempCommand { get; set; }
        public ICommand StopTempCommand { get; set; }

        ITempruetureManger _tempManager;

        public MainViewModel()
        {
            PairTempCommand = new Command(async () => await PairTemp());
            StartTempCommand = new Command(async () => await StartTemp());
            StopTempCommand = new Command(async () => await StopTemp());

            _tempManager = DependencyService.Get<ITempruetureManger>();
        }

        async Task PairTemp()
        {
            _tempManager.StartPairing();
        }

        async Task StartTemp()
        {
            _tempManager.StartMeasuring();
        }

        async Task StopTemp()
        {
            _tempManager.StopMeasuring();

            TempList.Clear();

            using (var reader = new StreamReader(GetFilePath("temprueture")))
            {
                while (reader.ReadLine() != null)
                    TempList.Add(await reader.ReadLineAsync());
            }
        }

        string GetFilePath(string fileName)
        {
            string rootPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(rootPath, fileName + ".txt");
        }
    }
}
