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
        private readonly INameListService _nameListService;
        private string _greeting;
        private string _selectedName;
        private ObservableCollection<string> _names;
        private bool _canShowGreeting;
        private bool _canGetNames;

        public MainViewModel(IGreetingService greetingService, INameListService nameListService)
        {
            _greetingService = greetingService;
            _nameListService = nameListService;
            ShowGreetingCommand = new RelayCommand(async () => await ShowGreetingAsync(), () => CanShowGreeting);
            GetNamesCommand = new RelayCommand(async () => await GetNamesAsync(), () => CanGetNames);
            Names = new ObservableCollection<string>();
            CanGetNames = true; // 初期状態でGetNamesボタンを有効にする
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

        public bool CanShowGreeting
        {
            get => _canShowGreeting;
            set
            {
                if (SetProperty(ref _canShowGreeting, value))
                {
                    ((RelayCommand)ShowGreetingCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public bool CanGetNames
        {
            get => _canGetNames;
            set
            {
                if (SetProperty(ref _canGetNames, value))
                {
                    ((RelayCommand)GetNamesCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand ShowGreetingCommand { get; }
        public ICommand GetNamesCommand { get; }

        private async Task ShowGreetingAsync()
        {
            CanShowGreeting = false;

            Greeting = await _greetingService.GreetAsync(SelectedName);

            CanShowGreeting = true;
        }

        private async Task GetNamesAsync()
        {
            CanGetNames = false;
            CanShowGreeting = false;

            // サービスから名前リストを非同期で取得
            var names = await _nameListService.GetNamesAsync();
            Names.Clear();
            foreach (var name in names)
            {
                Names.Add(name);
            }

            // 名前が取得された後、ShowGreetingボタンを有効にする
            CanShowGreeting = true;
            CanGetNames = true;
        }
    }
}
