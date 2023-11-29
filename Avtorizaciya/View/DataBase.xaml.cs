﻿using Avtorizaciya.Model;
using Avtorizaciya.ViewModel;
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

namespace Avtorizaciya.View
{
    public partial class DataBase : Window
    {
        public DataBase()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(new LoginService(), new RegistService(), new ProverkaService());
        }
        private void Window_click(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}