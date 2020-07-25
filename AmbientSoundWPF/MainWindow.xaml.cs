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
using System.Text.RegularExpressions;
using System.IO;



namespace AmbientSoundWPF
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private WaveIn captureDevice;
        private WaveFileWriter writer;
        private string outputFilename;
        private readonly string outputFolder;

        bool running = false;
        int inputLevel = 0;
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            
            InitializeComponent();
            DeviceSelect.SelectedItem = null;
            DeviceSelect.Text = "Select a Device";
            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Tick += timer_Tick;
            populateDevices();

            
        }

        private void OnDataAvailable(object sender, WaveInEventArgs args)
        {
            writer.Write(args.Buffer, 0, args.BytesRecorded);

            float max = 0;
            // interpret as 16 bit audio
            for (int index = 0; index < args.BytesRecorded; index += 2)
            {
                short sample = (short)((args.Buffer[index + 1] << 8) |
                                        args.Buffer[index + 0]);
                // to floating point
                var sample32 = sample / 32768f;
                // absolute value 
                if (sample32 < 0) sample32 = -sample32;
                // is this the max value?
                if (sample32 > max) max = sample32;
            }
            if (running) {
                inputLevel = (int) (100 * max);
            }
            else
            {
                inputLevel = 0;
            }
            
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
                    initializeDevices();
                    timer.Start();
                    
                }
                else
                {
                    //Console.Write("\nOFF\n\n");
                    OnButton.Content = "OFF";
                    OnButton.Background = new SolidColorBrush(Color.FromArgb(255, (byte)255, (byte)0, (byte)0));
                    InLevel.Value = 0;
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
                InLevel.Value = 0;
                timer.Stop();
                populateDevices();
            }
        }

        private void initializeDevices()
        {
            //throw new NotImplementedException();
            if (DeviceSelect.SelectedItem != DeviceLabel)
            {
                //Console.Write("\nInput Selected\n\n");
                //timer.Stop();

                List<NAudio.Wave.WaveInCapabilities> sources = new List<NAudio.Wave.WaveInCapabilities>();

                for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
                {
                    sources.Add(NAudio.Wave.WaveIn.GetCapabilities(i));
                }

                NAudio.Wave.WaveInCapabilities device;
                int count = 0;
                int devNum = 0;

                foreach (var source in sources)
                {
                    if (DeviceSelect.Text == source.ProductName)
                    {
                        devNum = count;
                        Console.Write("\n" + source.ProductName + " selected\n\n");
                        device = source;
                    }
                    count++;
                }
                
                var outputFolder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "NAudio");
                Directory.CreateDirectory(outputFolder);
                var outputFilePath = System.IO.Path.Combine(outputFolder, "recorded.wav");
                captureDevice = new NAudio.Wave.WaveIn();
                writer = new WaveFileWriter(outputFilePath, captureDevice.WaveFormat);
                captureDevice.DeviceNumber = devNum;
                captureDevice.DataAvailable += OnDataAvailable;

                try
                {
                    captureDevice.StartRecording();
                    Console.Write("\nStarted Recording\n\n");
                }
                catch
                {
                    string msg = "Could not record from audio device!\n\n";
                    msg += "Is your microphone plugged in?\n";
                    msg += "Is it set as your default recording device?";
                    MessageBox.Show(msg, "ERROR");
                }
            }
        }

        void timer_Tick (object sender, EventArgs e)
        {
            if (DeviceSelect.SelectedItem != DeviceLabel)
            {
                if (inputLevel < 50)
                {
                    InLevel.Foreground = new SolidColorBrush(Color.FromArgb(255, (byte)0, (byte)255, (byte)0));
                }
                else if (inputLevel < 80)
                {
                    InLevel.Foreground = new SolidColorBrush(Color.FromArgb(255, (byte)255, (byte)255, (byte)0));
                }
                else
                {
                    InLevel.Foreground = new SolidColorBrush(Color.FromArgb(255, (byte)255, (byte)0, (byte)0));
                }

                InLevel.Value = inputLevel;
                
            }
            else
            {
                string msg = "Could not record from audio device!\n\n";
                msg += "Is your microphone plugged in?\n";
                msg += "Is it set as your default recording device?";
                MessageBox.Show(msg, "ERROR");
                OnButton.Content = "OFF";
                InLevel.Value = 0;
                OnButton.Background = new SolidColorBrush(Color.FromArgb(255, (byte)255, (byte)0, (byte)0));
                running = false;
                timer.Stop();
            }
            
        }
        
    }
}
