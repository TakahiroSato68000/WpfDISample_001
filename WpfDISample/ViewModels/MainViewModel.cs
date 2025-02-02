using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfDISample.Services;

namespace WpfDISample.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IGreetingService _greetingService;
        private string _greeting;

        public MainViewModel(IGreetingService greetingService)
        {
            _greetingService = greetingService;
            ShowGreetingCommand = new RelayCommand(ShowGreeting);
        }

        public string Greeting
        {
            get => _greeting;
            set
            {
                _greeting = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowGreetingCommand { get; }

        private void ShowGreeting()
        {
            Greeting = _greetingService.Greet("MainWindow");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
