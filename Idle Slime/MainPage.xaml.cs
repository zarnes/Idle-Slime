using Idle_Slime.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Idle_Slime
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public Player player { get; set; }

        public MainPage()
        {
            this.InitializeComponent();

            player = new Player();
            player.Money = 1;

            player.Slimes = new List<Slime>()
            {
                new Slime("Slime", 1, 2, 1, "Assets/slimes/slime.png", player),
                new Slime("Tabby", 25, 2, 5, "Assets/slimes/tabby.png", player),
                new Slime("Rock", 125, 2, 25, "Assets/slimes/rock.png", player),
                new Slime("Phosphore", 500, 2, 100, "Assets/slimes/phosphore.png", player)
            };
            Slimes.ItemsSource = player.Slimes;
            
            TimeSpan period = TimeSpan.FromSeconds(1);
            ThreadPoolTimer PlortProduce = ThreadPoolTimer.CreatePeriodicTimer(async (source) =>
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    foreach (Slime slime in player.Slimes)
                    {
                        slime.Produce();
                    }
                });

            }, period);
        }

        private void buy_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Slime slime = player.Slimes.Find(s => s.Name == btn.Tag.ToString());
            if (slime != null)
                slime.TryBuy();
        }

        private void sell_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Slime slime = player.Slimes.Find(s => s.Name == btn.Tag.ToString());
            if (slime != null)
                slime.Sell();
        }
    }
}
