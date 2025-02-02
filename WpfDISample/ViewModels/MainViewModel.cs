using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfDISample.Services;

namespace WpfDISample.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly IGreetingService _greetingService;
        private string _greeting;
        private string _selectedName;
        private ObservableCollection<string> _names;

        public MainViewModel(IGreetingService greetingService)
        {
            _greetingService = greetingService;
            ShowGreetingCommand = new RelayCommand(ShowGreeting);
            GetNamesCommand = new RelayCommand(GetNames);
            Names = new ObservableCollection<string>();
        }

        public string Greeting
        {
            get => _greeting;
            set => SetProperty(ref _greeting, value);
        }

        public string SelectedName
        {
            get => _selectedName;
            set => SetProperty(ref _selectedName, value);
        }

        public ObservableCollection<string> Names
        {
            get => _names;
            set => SetProperty(ref _names, value);
        }

        public ICommand ShowGreetingCommand { get; }
        public ICommand GetNamesCommand { get; }

        private void ShowGreeting()
        {
            Greeting = _greetingService.Greet(SelectedName);
        }

        private void GetNames()
        {
            // サンプルデータを追加
            Names.Clear();
            Names.Add("Alice");
            Names.Add("Bob");
            Names.Add("Charlie");
        }
    }
}
