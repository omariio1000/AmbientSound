using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace AmbientSoundWPF
{
    class ButtonHover
    {

        public static void Button_Hover(MainWindow window, object sender, System.Windows.Input.MouseEventArgs e, MainWindow.winState state, bool moveIn = true, bool running = false)
        {
            var converter = new System.Windows.Media.BrushConverter();
            if (sender.Equals(window.OnButton))
            {
                if (moveIn)
                {
                    if (running)
                    {
                        window.OnButton.Background = (Brush) converter.ConvertFromString("#90DDA0");
                    }
                    else
                    {
                        window.OnButton.Background = (Brush)converter.ConvertFromString("#DB9191");   
                    }
                }
                else
                {
                    if (running)
                    {
                        window.OnButton.Background = (Brush)converter.ConvertFromString("#4EC662");
                    }
                    else
                    {
                        window.OnButton.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)228, (byte)58, (byte)58));
                    }
                }
            }
            else if (sender.Equals(window.DeviceRefresh))
            {
                if (moveIn)
                {
                    window.DeviceRefresh.Background = (Brush)converter.ConvertFromString("#64607F");
                }
                else
                {
                    window.DeviceRefresh.Background = (Brush)converter.ConvertFromString("#25232F");
                }
            }

            else if (sender.Equals(window.MainButton))
            {
                if (moveIn)
                {
                    if (state == MainWindow.winState.MAIN)
                    {
                        window.MainButton.Background = (Brush)converter.ConvertFromString("#DB9191");
                    }
                    else
                    {
                        window.MainButton.Background = (Brush)converter.ConvertFromString("#64607F");
                    }
                }
                else
                {
                    if (state == MainWindow.winState.MAIN)
                    {
                        window.MainButton.Background = (Brush)converter.ConvertFromString("#FFF4364A");
                    }
                    else
                    {
                        window.MainButton.Background = (Brush)converter.ConvertFromString("#FF0B0A1D");
                    }
                }
            }

            else if (sender.Equals(window.HelpButton))
            {
                if (moveIn)
                {
                    if (state == MainWindow.winState.HELP)
                    {
                        window.HelpButton.Background = (Brush)converter.ConvertFromString("#DB9191");
                    }
                    else
                    {
                        window.HelpButton.Background = (Brush)converter.ConvertFromString("#64607F");
                    }
                }
                else
                {
                    if (state == MainWindow.winState.HELP)
                    {
                        window.HelpButton.Background = (Brush)converter.ConvertFromString("#FFF4364A");
                    }
                    else
                    {
                        window.HelpButton.Background = (Brush)converter.ConvertFromString("#FF0B0A1D");
                    }
                }
            }
        }

    }
}
