using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HorseBetting
{
    class Mark : Punter
    {
        public Mark()
        {
            Name = "Mark";
            Cash = 50;
            MyBet = new Bet();
        }
    }
}
