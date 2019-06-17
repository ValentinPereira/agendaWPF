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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AgendaWpf
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            frame.Content = new View.customersList();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new View.addCustomer();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new View.Home();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            frame.Content = new View.customersList();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            frame.Content = new View.addBroker();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            frame.Content = new View.brokersList();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            frame.Content = new View.addAppointment();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            frame.Content = new View.appointmentsList();
        }
    }
}

