using Idle_Slime.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
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
            LoadAsync();
            
            
            TimeSpan period = TimeSpan.FromSeconds(1);
            ThreadPoolTimer PlortProduce = ThreadPoolTimer.CreatePeriodicTimer(async (source) =>
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    if (player.Slimes == null) return;
                    foreach (Slime slime in player.Slimes)
                    {
                        slime.Produce();
                    }

                    if (player.Aliments == null) return;
                    foreach (Food food in player.Aliments)
                    {
                        food.Produce();
                    }
                });

            }, period);
        }

        private void buyFood_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Food food = player.Aliments.FirstOrDefault(el => el.Name == btn.Tag.ToString());
            if (food != null)
                food.TryBuy();

            SaveAsync();
        }

        private void buy_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Slime slime = player.Slimes.First(s => s.Name == btn.Tag.ToString());
            if (slime != null)
                slime.TryBuy();

            SaveAsync();
        }

        private void sell_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Slime slime = player.Slimes.First(s => s.Name == btn.Tag.ToString());
            if (slime != null)
                slime.Sell();

            SaveAsync();
        }

        private async System.Threading.Tasks.Task SaveAsync()
        {
           
            string json = JsonConvert.SerializeObject(player, new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                Formatting = Formatting.Indented
            });
            Console.WriteLine("");

            StorageFolder roaming = ApplicationData.Current.RoamingFolder;
            StorageFile file = await roaming.CreateFileAsync("player", CreationCollisionOption.ReplaceExisting);

            var stream = await file.OpenAsync(FileAccessMode.ReadWrite);
            using (var os = stream.GetOutputStreamAt(0))
            {
                using (DataWriter dw = new DataWriter(os))
                {
                    dw.UnicodeEncoding = UnicodeEncoding.Utf8;
                    dw.WriteString(json);

                    await dw.StoreAsync();
                    await os.FlushAsync();
                }
            }
            stream.Dispose();
        }

        private async void LoadAsync()
        {
            StorageFolder roaming = ApplicationData.Current.RoamingFolder;
            try
            {
                StorageFile file = await roaming.GetFileAsync("player");
                string json = await FileIO.ReadTextAsync(file);
                Player pl = JsonConvert.DeserializeObject<Player>(json);

                if (pl == null || pl.Slimes == null || pl.Aliments == null || true)
                    CreatePlayer();
                else
                    player = pl;
            }
            catch (FileNotFoundException e)
            {
                CreatePlayer();
            }
            
            Slimes.ItemsSource = player.Slimes;
            Aliments.ItemsSource = player.Aliments;
        }

        private void CreatePlayer()
        {
            player.Money = 2;
            player.Slimes = new ObservableCollection<Slime>()
            {
                new Slime("Slime", 1, 2, 1, "Assets/slimes/slime.png", player, true, true, true, ""),
                new Slime("Tabby", 25, 2, 5, "Assets/slimes/tabby.png", player, false, false, true, "Stony Hen"),
                new Slime("Rock", 125, 2, 25, "Assets/slimes/rock.png", player, false, true, false, "Heart Beet"),
                new Slime("Phosphore", 500, 2, 100, "Assets/slimes/phosphore.png", player, true, false, false, "Cuberry")
            };

            player.Aliments = new ObservableCollection<Food>()
            {
                new Food("Carrot", 1, 2, "Assets/food/Carrot.png", player, Food.Type.veggie),
                new Food("Heart Beet", 2, 2, "Assets/food/Heart_Beet.png", player, Food.Type.veggie),
                new Food("Oca Oca", 4, 2, "Assets/food/Oca_Oca.png", player, Food.Type.veggie),
                new Food("Odd Onion", 8, 2, "Assets/food/OddOnion_SP.png", player, Food.Type.veggie),
                new Food("Silver Parsnip", 16, 2, "Assets/food/Silver_Parsnip.png", player, Food.Type.veggie),

                new Food("Pogo Fruit", 1, 2, "Assets/food/Pogo_fruit.png", player, Food.Type.fruit),
                new Food("Cuberry", 2, 2, "Assets/food/Cuberry.png", player, Food.Type.fruit),
                new Food("Mint Mango", 4, 2, "Assets/food/Mint_mango.png", player, Food.Type.fruit),
                new Food("Phase Lemon", 8, 2, "Assets/food/PhaseLemon_SP.png", player, Food.Type.fruit),
                new Food("Prickle Pear", 16, 2, "Assets/food/Prickle_Pear.png", player, Food.Type.fruit),

                new Food("Hen Hen", 1, 2, "Assets/food/Hen_hen.png", player, Food.Type.meat),
                new Food("Stony Hen", 2, 2, "Assets/food/Stony_hen.png", player, Food.Type.meat),
                new Food("Briar Hen", 4, 2, "Assets/food/Briar_hen.png", player, Food.Type.meat),
                new Food("Painted Hen", 8, 2, "Assets/food/Painted_Hens.png", player, Food.Type.meat),
                new Food("Roostro", 16, 2, "Assets/food/Roostro.png", player, Food.Type.meat),
            };
        }
    }
}
