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
            player.Slimes = new List<Slime>()
            {
                new Slime() {Name = "Slime", Picture = "Assets/slimes/slime.png", Multiplier = 1, Number = 5 },
                new Slime() {Name = "Tabby", Picture = "Assets/slimes/tabby.png", Multiplier = 5 },
                new Slime() {Name = "Rock", Picture = "Assets/slimes/rock.png", Multiplier = 25},
                new Slime() {Name = "Phosphore", Picture = "Assets/slimes/phosphore.png", Multiplier = 100 },
            };

            ObservableCollection<Slime> dataSlimes = new ObservableCollection<Slime>();
            foreach(Slime slime in player.Slimes)
            {
                dataSlimes.Add(slime);
            }
            /*Slimes.ItemsSource = dataSlimes;
            Slimes2.ItemsSource = dataSlimes;*/

            TimeSpan period = TimeSpan.FromSeconds(1);

            ThreadPoolTimer PlortProduce = ThreadPoolTimer.CreatePeriodicTimer(async (source) =>
            {
                //
                // TODO: Work
                //

                //
                // Update the UI thread by using the UI core dispatcher.
                //
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    foreach (Slime slime in player.Slimes)
                    {
                        slime.Produce();
                        Debug.WriteLine(slime.Name + " produced " + slime.Plorts);
                        Slimes.ItemsSource = dataSlimes;
                    }
                });

            }, period);
        }
    }
}
