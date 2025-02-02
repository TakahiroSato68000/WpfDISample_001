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
        private bool _canGetNames;
        private int _selectedIndex = -1;
        private bool _isProcessing;

        public MainViewModel(IGreetingService greetingService, INameListService nameListService)
        {
            _greetingService = greetingService;
            _nameListService = nameListService;
            ShowGreetingCommand = new RelayCommand(async () => await ShowGreetingAsync(), () => CanShowGreeting);
            GetNamesCommand = new RelayCommand(async () => await GetNamesAsync(), () => CanGetNames);
            ClearCommand = new RelayCommand(Clear);
            Names = new ObservableCollection<string>();
            CanGetNames = true; // 初期状態でGetNamesボタンを有効にする
        }

        public bool IsProcessing
        {
            get => _isProcessing;
            set
            {
                if (SetProperty(ref _isProcessing, value))
                {
                    ((RelayCommand)ShowGreetingCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)GetNamesCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public string Greeting
        {
            get => _greeting;
            set => SetProperty(ref _greeting, value);
        }

        public string SelectedName
        {
            get => _selectedName;
            set
            {
                if (SetProperty(ref _selectedName, value))
                {
                    SelectedIndex = Names.IndexOf(value);
                }
            }
        }

        public ObservableCollection<string> Names
        {
            get => _names;
            set => SetProperty(ref _names, value);
        }

        public ICommand ShowGreetingCommand { get; }
        public ICommand GetNamesCommand { get; }
        public ICommand ClearCommand { get; }

        public bool CanShowGreeting
        {
            get => (IsProcessing == false) && ((SelectedIndex >= 0));
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

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (SetProperty(ref _selectedIndex, value))
                {
                    SelectedName = Names.ElementAtOrDefault(value);
                    ((RelayCommand)ShowGreetingCommand).RaiseCanExecuteChanged();
                }
            }
        }

        private async Task ShowGreetingAsync()
        {
            IsProcessing = true;
            Greeting = await _greetingService.GreetAsync(SelectedName);
            IsProcessing = false;
        }

        private async Task GetNamesAsync()
        {
            CanGetNames = false;
            IsProcessing = true;

            // サービスから名前リストを非同期で取得
            var names = await _nameListService.GetNamesAsync();
            Names.Clear();
            foreach (var name in names)
            {
                Names.Add(name);
            }

            // 名前が取得された後、ShowGreetingボタンを有効にする
            CanGetNames = true;
            IsProcessing = false;
        }

        private void Clear()
        {
            IsProcessing = true;
            SelectedName = string.Empty;
            Greeting = string.Empty;
            Names.Clear();
            IsProcessing = false;
        }
    }
}
