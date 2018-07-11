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
        public Player Player { get; set; }

        private int _number;
        public int Number
        {
            get { return _number; }
        }

        private int _basePrice;
        public int BasePrice
        {
            get { return _basePrice; }
        }

        private double _priceMuliplier;
        public double PriceMultiplier
        {
            get { return _priceMuliplier; }
            set { _priceMuliplier = value; }
        }

        private int _price;
        public int Price
        {
            get { return _price; }
        }
        public string BuyText
        {
            get { return "Buy a slime (" + Price.ToString() + ")"; }
        }

        private int _plorts;
        public int Plorts
        {
            get { return _plorts; }
            set
            {
                if (value == Plorts)
                    return;
                _plorts = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Plorts"));
            }
        }

        public int Multiplier { get; set; }
        public string MultiplierStr
        {
            get { return "x" + Multiplier.ToString(); }
        }
        public string Picture { get; set; }


        public Slime(string name, int basePrice, double priceMultiplier, int multiplier, string picture, Player player, int plorts = 0, int number = 0)
        {
            Name = name;
            _basePrice = basePrice;
            _priceMuliplier = priceMultiplier;
            Multiplier = multiplier;
            Picture = picture;
            Player = player;
            _plorts = plorts;
            _number = number;

            _price = _basePrice * (int)Math.Pow(Number+1, _priceMuliplier);
        }

        
        public bool TryBuy()
        {
            if (Player.Money >= Price)
            {
                Player.Money -= Price;
                ++_number;
                _price = _basePrice * (int) Math.Pow(Number+1, _priceMuliplier);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Price"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Number"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BuyText"));
                return true;
            }
            return false;
        }

        public void Sell()
        {
            Player.Money += Plorts * Multiplier;
            Plorts = 0;
        }

        internal void Produce()
        {
            Plorts += Number;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Plorts"));
        }
    }
}
