using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseBetting
{
    class Cristina : Punter
    {
        public Cristina()
        {
            Name = "Cristina";
            Cash = 50;
            MyBet = new Bet();
        }
    }
}
