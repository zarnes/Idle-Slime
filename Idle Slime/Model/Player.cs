using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Idle_Slime.Model
{
    public class Player
    {
        public BigInteger Money { get; set; }
        public List<Slime> Slimes { get; set; }
    }
}
