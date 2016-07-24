using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSED05_GoldDiggers
{
    public class GoldDigger
    {
        public string Name;
        public Bet MyBet;
        public int Cash;
        // public RadioButton MyRadioButton;
        //public Label MyLabel;
        public void UpdateLabels()
        {

        }

        public void ClearBet()
        {

        }

        public bool PlaceBet(int amount, int digger)
        {
            if (amount <= Cash)
            {
                MyBet.Amount = amount;
                MyBet.Digger = digger;
                return true;
            }
            //  MessageBox.Show("Bet exceeds available cash!");
            return false;


        }

        public void Collect(int Winner)
        {

        }
    }
}