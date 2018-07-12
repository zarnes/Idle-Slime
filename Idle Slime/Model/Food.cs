using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Idle_Slime.Model
{
    public class Food : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; set; }
        public string NameAndSeeds
        {
            get { return Name + " (" + _seeds.ToString() + (_type == Type.meat ? " coop" : " seed") + (_seeds > 1 ? "s" : "") + ")"; }
        }

        public Player Player { get; set; }

        public enum Type
        {
            veggie,
            fruit,
            meat
        }
        private Type _type;
        public Type FoodType
        {
            get { return _type; }
        }
        public string TypeStr
        {
            get { return _type.ToString(); }
        }

        private int _seeds;
        public int Seeds
        {
            get { return _seeds; }
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
            get { return "Buy a " + Name + " " + (_type == Type.meat ? "coop" : "seed") + " (" + Price.ToString() + " newbucks)"; }
        }

        private int number;
        public int Number
        {
            get { return number; }
            set
            {
                if (value == Number)
                    return;
                number = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Number"));
            }
        }

        public string Picture { get; set; }


        public Food(string name, int basePrice, double priceMultiplier, string picture, Player player, Type foodType, int number = 0, int seeds = 0)
        {
            Name = name;
            _basePrice = basePrice;
            _priceMuliplier = priceMultiplier;
            Picture = picture;
            Player = player;
            _type = foodType;
            this.number = number;
            _seeds = seeds;

            _price = _basePrice * (int)Math.Pow(Seeds + 1, _priceMuliplier);
        }


        public bool TryBuy()
        {
            if (Player.Money >= Price)
            {
                Player.Money -= Price;
                ++_seeds;
                _price = _basePrice * (int)Math.Pow(Seeds + 1, _priceMuliplier);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Price"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NameAndSeeds"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BuyText"));
                return true;
            }
            return false;
        }

        internal void Produce()
        {
            Number += Seeds;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Number"));
        }
    }
}
