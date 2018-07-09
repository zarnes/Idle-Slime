using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigMath;

namespace Idle_Slime.Model
{
    public class Slime : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; set; }
        private int number;
        public int Number
        {
            get { return number; }
            set
            {
                if (value == number)
                    return;
                number = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Number"));
            }
        }

        private int price;
        public int Price
        {
            get { return price; }
            set
            {
                if (value == price)
                    return;
                price = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Price"));
            }
        }
        public string BuyText
        {
            get { return "Buy a slime (" + Price.ToString() + ")"; }
        }

        private int plorts;
        public int Plorts
        {
            get { return plorts; }
            set
            {
                if (value == Plorts)
                    return;
                plorts = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Plorts"));
            }
        }

        public int Multiplier { get; set; }
        public string MultiplierStr
        {
            get { return "x" + Multiplier.ToString(); }
        }
        public string Picture { get; set; }

        public Slime()
        {
            Picture = "Assets/slimes/slime.png";
        }

       

        public bool TryBuy()
        {
            //if ()
            throw new NotImplementedException();
        }

        internal void Produce()
        {
            Plorts += Number;
        }
    }
}
