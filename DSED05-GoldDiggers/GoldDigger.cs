using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace DSED05_GoldDiggers
{
    public abstract class GoldDigger
    {
        public string Name;
        public Bet MyBet;
        public int Cash;
        public bool Busted=false;

        public int Amount { get;  set; }
        public int Miner { get;  set; }

        public RadioButton MyRadioButton;
        public TextBlock MyLabel;
        public void UpdateLabels(string text)
        {
            MyLabel.Text = text;
        }

        public void ClearBet()
        {
            MyBet.Amount = 0;
            MyBet.Digger = 0;
        }

        public bool PlaceBet(int amount, int digger)
        {
            if (amount <= Cash)
            {
                MyBet = new Bet() {ThisBettor = this};
                Cash -= amount;
                MyBet.Amount = amount;
                MyBet.Digger = digger;
                return true;
            }
              new MessageDialog("Bet exceeds available cash!");
            
            return false;


        }

        public void Collect(int Winner)
        {
            Cash += MyBet.PayOut(Winner);
            MyBet = null;
        }
    }

    public class Mandi : GoldDigger
    {
        public Mandi() 
        {
            Name = "Mandi";
        }
    }

    public class Candi : GoldDigger
    {
        public Candi()
        {
            Name = "Candi";
        }
    }

    public class Sandi : GoldDigger {
        public Sandi()
        {
            Name = "Sandi";
        }
    }
}