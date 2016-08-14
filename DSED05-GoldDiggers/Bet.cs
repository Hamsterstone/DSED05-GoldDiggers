using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSED05_GoldDiggers
{
    public class Bet
    {
        public int Amount;
        public int Digger;
        public GoldDigger ThisBettor;

        public string GetDescription()
        {
            string description = ThisBettor.Name+" bet " + Amount + " on " + Digger;
            return description;
        }

        public int PayOut(int Winner)
        {
            int winnings = 0;
            if (Digger == Winner)
            {
                winnings = Amount;
            }
            return winnings;
        }
    }
}
