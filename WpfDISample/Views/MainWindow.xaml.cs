﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfDISample.Services;

namespace WpfDISample.Views
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IGreetingService _GreetingService;

        public MainWindow(IGreetingService GreetingService)
        {
            InitializeComponent();
            _GreetingService = GreetingService;
            DisplayServiceData();
        }

        private void DisplayServiceData()
        {
            MessageBox.Show(_GreetingService.Greet(this.Title));
        }
    }
}
