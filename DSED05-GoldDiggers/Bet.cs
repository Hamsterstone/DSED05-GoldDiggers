using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSED05_GoldDiggers
{
    public class Bet
    {
        // Fields
        public int Amount;
        public int Digger;
        public GoldDigger ThisBettor;

        // Returns a string describing the bet
        public string GetDescription()
        {
            string description = ThisBettor.Name+" bet " + Amount + " on " + Race.goldMiners[Digger].Name;
            return description;
        }

        //Returns double the bet if the race winner matches the Bet.Digger field, 0 otherwise
        public int PayOut(int Winner)
        {
            int winnings = 0;
            if (Digger == Winner)
            {
                winnings = 2*Amount;
            }
            return winnings;
        }
    }
}
