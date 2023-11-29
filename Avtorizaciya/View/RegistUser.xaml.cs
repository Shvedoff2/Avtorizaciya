using Avtorizaciya.Model;
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
    /// <summary>
    /// Логика взаимодействия для Regist.xaml
    /// </summary>
    public partial class RegistUser : Window
    {
        public RegistUser()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(new LoginService(), new RegistService(), new ProverkaService());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
