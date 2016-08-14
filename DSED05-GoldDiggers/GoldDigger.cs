using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls.Primitives;

namespace DSED05_GoldDiggers
{
    public abstract class GoldDigger
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
            MyBet.Amount = 0;
            MyBet.Digger = 0;
        }

        public bool PlaceBet(int amount, int digger)
        {
            if (amount <= Cash)
            {
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
        }
    }

    public class Mandi : GoldDigger {}

    public class Candi : GoldDigger {}

    public class Sandi : GoldDigger {}
}