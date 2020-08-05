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
using AudioSwitcher.AudioApi.CoreAudio;
using System.Diagnostics;
using VisioForge.Shared.MediaFoundation;
using VisioForge.Shared.NAudio.CoreAudioApi;
using DataFlow = VisioForge.Shared.NAudio.CoreAudioApi.DataFlow;
using SharpDX.DXGI;
using VisioForge.Shared.WindowsMediaLib;
using System.Drawing;
using System.Windows.Forms;

namespace AmbientSoundWPF
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private WaveIn captureDevice;
        private WaveFileWriter writer;
        System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
        private readonly VisioForge.Shared.NAudio.CoreAudioApi.MMDeviceEnumerator _deviceEnumerator = new VisioForge.Shared.NAudio.CoreAudioApi.MMDeviceEnumerator();
        private readonly VisioForge.Shared.NAudio.CoreAudioApi.MMDevice _playbackDevice;
         

        bool running = false;
        int inputLevel = 0;
        int outputLevel = 0;
        //int userLevel = 0; //preserved level, use to track system volume change
        int currentVolume = 0;
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer volumeChanger = new DispatcherTimer();
        int counter = 0;
        CoreAudioDevice defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;

        public MainWindow()
        {
            InitializeComponent();

            ni.Icon = new System.Drawing.Icon("AmbientSound.ico");
            ni.Visible = true;
            ni.Click +=
                delegate (object sender, EventArgs args)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                };
            ni.Visible = false;

            DeviceSelect.SelectedItem = null;
            DeviceSelect.Text = "Select a Device";
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            volumeChanger.Interval = TimeSpan.FromSeconds(timer.Interval.TotalSeconds / 100);
            volumeChanger.Tick += volume_Tick;
            
            populateDevices();
            Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NAudio"));
            //CoreAudioDevice defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
            //onsole.Out.Write("\nCurrent Volume:" + defaultPlaybackDevice.Volume + "\n\n");

            MinSlider.Value = (int) defaultPlaybackDevice.Volume;
            currentVolume = (int) defaultPlaybackDevice.Volume;
            _playbackDevice = _deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, (VisioForge.Shared.NAudio.CoreAudioApi.Role)ERole.eMultimedia);
            //Console.Out.Write("\nMin Volume:" + (int)MinSlider.Value+ "\n\n");
            //SetVolume((int) MinSlider.Value);
        }

        

        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }

        public void SetVolume(int volumeLevel)
        {
            if (volumeLevel < 0 || volumeLevel > 100)
                throw new ArgumentException("Your volume meter should allow only between 0 and 100.");

            _playbackDevice.AudioEndpointVolume.MasterVolumeLevelScalar = volumeLevel / 100.0f;
        }

        private void OnDataAvailable(object sender, WaveInEventArgs args)
        {
            if (running)
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
            
            
        }

        private void populateDevices()
        {
            DeviceSelect.SelectedItem = null;
            DeviceSelect.Text = "Select a Device";
            List<NAudio.Wave.WaveInCapabilities> sources = new List<NAudio.Wave.WaveInCapabilities>();
            
            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
            {
                sources.Add(NAudio.Wave.WaveIn.GetCapabilities(i));
            }
            bool found = false;
            foreach (var source in sources)
            {
                for (int i = 0; i < DeviceSelect.Items.Count; i++)
                {
                    if (DeviceSelect.Items.OfType<ComboBoxItem>().Any(cbi => cbi.Content.Equals(source.ProductName)))
                    {
                        found = true;
                    }

                }

                if (!found)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = source.ProductName;
                    DeviceSelect.Items.Add(item);
                }
                
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(OnButton)) {
                var outputFolder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NAudio");
                var outputFilePath = System.IO.Path.Combine(outputFolder, "recorded" + counter + ".wav");
                if (!running)
                {
                    //Console.Write("\nON\n\n");
                    OnButton.Content = "ON";
                    OnButton.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)0, (byte)255, (byte)0));
                    running = true;
                    initializeDevices();
                    counter++;
                    timer.Start();
                    
                    if (DeviceSelect.Text != "Select a Device")
                    {
                        writer = null;
                        writer = new WaveFileWriter(outputFilePath, captureDevice.WaveFormat);
                    }      
                    
                }
                else
                {
                    //Console.Write("\nOFF\n\n");
                    OnButton.Content = "OFF";
                    OnButton.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)228, (byte)58, (byte)58));
                    InLevel.Value = 0;
                    OutLevel.Value = 0;
                    running = false;
                    timer.Stop();
                    //DeleteDirectory(outputFolder);
                    //Directory.Delete(outputFolder, true);
                }
            }   
            
            else if (sender.Equals(DeviceRefresh))
            {
                //Console.Write("\nDevices Refreshed\n\n");
                OnButton.Content = "OFF";
                OnButton.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)228, (byte)58, (byte)58));
                running = false;
                InLevel.Value = 0;
                OutLevel.Value = 0;
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

                
                captureDevice = new NAudio.Wave.WaveIn();
                
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
                    System.Windows.MessageBox.Show(msg, "ERROR");
                }
            }
        }

        void timer_Tick (object sender, EventArgs e)
        {
            if (!(MaxSlider.Value > MinSlider.Value))
            {
                OnButton.Content = "OFF";
                InLevel.Value = 0;
                OutLevel.Value = 0;
                OnButton.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)228, (byte)58, (byte)58));
                running = false;
                string msg = "Maximum volume must be larger than minimum!\n\n";
                System.Windows.MessageBox.Show(msg, "ERROR");
                timer.Stop();
            }
            if (DeviceSelect.SelectedItem != DeviceLabel)
            {
                //timer.Stop();


                if (inputLevel < 50)
                {
                    InLevel.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)0, (byte)255, (byte)0));
                }
                else if (inputLevel < 80)
                {
                    InLevel.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)255, (byte)255, (byte)0));
                }
                else
                {
                    InLevel.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)255, (byte)0, (byte)0));
                }

                InLevel.Value = inputLevel;

                if (inputLevel > 0)
                {

                    double range = MaxSlider.Value - MinSlider.Value;
                    Console.Write("\nInput Level: " + inputLevel + "\n\n");
                    outputLevel = (int) (MinSlider.Value + ((range/100) * (double)(inputLevel)));
                    Console.Write("\nOutput Level: " + outputLevel + "\n\n");

                    if (outputLevel < 50)
                    {
                        OutLevel.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)0, (byte)255, (byte)0));
                    }
                    else if (outputLevel < 80)
                    {
                        OutLevel.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)255, (byte)255, (byte)0));
                    }
                    else
                    {
                        OutLevel.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)255, (byte)0, (byte)0));
                    }

                    OutLevel.Value = outputLevel;

                    //SetVolume(outputLevel);
                    
                    volumeChanger.Start();
                    
                }

                //timer.Start();
            }
            else
            {
                OnButton.Content = "OFF";
                InLevel.Value = 0;
                OutLevel.Value = 0;
                OnButton.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)228, (byte)58, (byte)58));
                running = false;
                string msg = "Could not record from audio device!\n\n";
                msg += "Is your microphone plugged in?\n";
                msg += "Is it set as your default recording device?";
                System.Windows.MessageBox.Show(msg, "ERROR");
                timer.Stop();
            }


            //next step: try putthing this in its own timer and changing the min volume after it turns off
            //userLevel = (int)defaultPlaybackDevice.Volume; //record any system volume updates
            //MinSlider.Value = userLevel++; //refresh min value
        }

        private void volume_Tick(object sender, EventArgs e)
        {
            if (currentVolume > outputLevel)
            {
                currentVolume = currentVolume - 1;
                SetVolume(currentVolume);
            }
            else if (currentVolume < outputLevel)
            {
                currentVolume = currentVolume + 1;
                SetVolume(currentVolume);
            }
            else
            {
                volumeChanger.Stop();
            }

            if (currentVolume < 50)
            {
                OutLevel.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)0, (byte)255, (byte)0));
            }
            else if (currentVolume < 80)
            {
                OutLevel.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)255, (byte)255, (byte)0));
            }
            else
            {
                OutLevel.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)255, (byte)0, (byte)0));
            }

            OutLevel.Value = currentVolume;
        }


        private void AmbientSound_Closed(object sender, EventArgs e)
        {
            writer = null;
            running = false;
            var outputFolder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NAudio");
            DeleteDirectory(outputFolder);
            ni.Visible = false;
            ni.Icon = null;
        }

        private void radioButtons_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(IntFast))
            {
                timer.Interval = TimeSpan.FromMilliseconds(100);
                volumeChanger.Interval = TimeSpan.FromMilliseconds(timer.Interval.TotalMilliseconds / 100);
            }

            if (sender.Equals(IntRelaxed))
            {
                timer.Interval = TimeSpan.FromSeconds(1);
                volumeChanger.Interval = TimeSpan.FromSeconds(timer.Interval.TotalSeconds / 100);
            }
        }

        private void AmbientSound_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                ni.BalloonTipTitle = "Ambient Sound";
                ni.BalloonTipText = "Minimized to Tray";
                ni.ShowBalloonTip(400);
                ni.Visible = true;
                Hide();
            }
            else if (this.WindowState == WindowState.Normal)
            {
                ni.Visible = false;
                this.ShowInTaskbar = true;
            }
        }
    }
}
