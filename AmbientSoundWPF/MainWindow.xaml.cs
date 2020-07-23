using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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

//custom namespaces


namespace AmbientSoundWPF
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool running = false;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(OnButton)) {
                if (!running)
                {
                    OnButton.Content = "ON";
                    OnButton.Background = new SolidColorBrush(Color.FromArgb(255, (byte)0, (byte)255, (byte)0));
                    running = true;
                }
                else
                {
                    OnButton.Content = "OFF";
                    OnButton.Background = new SolidColorBrush(Color.FromArgb(255, (byte)255, (byte)0, (byte)0));
                    running = false;
                }
            }       
        }       
    }
}
