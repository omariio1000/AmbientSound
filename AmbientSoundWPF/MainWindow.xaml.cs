using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using NAudio.Wave;
using NAudio.CoreAudioApi;
using System.Management;
using System.Numerics;
using System.Windows.Threading;


namespace AmbientSoundWPF
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        bool running = false;
        DispatcherTimer timer = new DispatcherTimer();
        

        public MainWindow()
        {
            InitializeComponent();
            DeviceSelect.SelectedItem = null;
            DeviceSelect.Text = "Select a Device";
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += timer_Tick;
            populateDevices();

            

        }

        private void populateDevices()
        {
            List<NAudio.Wave.WaveInCapabilities> sources = new List<NAudio.Wave.WaveInCapabilities>();
            
            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
            {
                sources.Add(NAudio.Wave.WaveIn.GetCapabilities(i));
            }

            foreach (var source in sources)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = source.ProductName;
                DeviceSelect.Items.Add(item);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(OnButton)) {
                if (!running)
                {
                    //Console.Write("\nON\n\n");
                    OnButton.Content = "ON";
                    OnButton.Background = new SolidColorBrush(Color.FromArgb(255, (byte)0, (byte)255, (byte)0));
                    running = true;
                    timer.Start();
                    
                }
                else
                {
                    //Console.Write("\nOFF\n\n");
                    OnButton.Content = "OFF";
                    OnButton.Background = new SolidColorBrush(Color.FromArgb(255, (byte)255, (byte)0, (byte)0));
                    running = false;
                    timer.Stop();
                }
            }   
            
            else if (sender.Equals(DeviceRefresh))
            {
                //Console.Write("\nDevices Refreshed\n\n");
                OnButton.Content = "OFF";
                OnButton.Background = new SolidColorBrush(Color.FromArgb(255, (byte)255, (byte)0, (byte)0));
                running = false;
                timer.Start();
                //populateDevices();
            }
        }

        void timer_Tick (object sender, EventArgs e)
        {
            if (DeviceSelect.SelectedItem != DeviceLabel)
            {
                //Console.Write("\nInput Selected\n\n");
                //timer.Stop();
            }
        }
        
    }
}
