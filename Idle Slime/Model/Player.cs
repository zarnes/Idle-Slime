using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Idle_Slime.Model
{
    public class Player : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _money;
        public int Money
        {
            get { return _money; }
            set
            {
                if (_money == value) return;
                _money = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Money"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MoneyStr"));
            }
        }
        
        public string MoneyStr
        {
            get { return _money.ToString() + " newbucks"; }
        }


        public List<Slime> Slimes { get; set; }

    }
}
