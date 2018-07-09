using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Idle_Slime.Model
{
    class Food
    {
        public enum FoodType
        {
            Vegetable,
            Fruit,
            Meat
        }

        public string Name { get; set; }
        public FoodType Type { get; set; }
        public BigInteger Number { get; set; }
    }
}
