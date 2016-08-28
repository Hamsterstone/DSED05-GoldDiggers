using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace DSED05_GoldDiggers
{
    //Abstract class "GoldMiner" racers inherit from
    public abstract class GoldMiner
    {
        public string Name;
        public int Number;
        public int StartingPosition;
        public int RacetrackLength;
        public Image MyImage = null;
        public int Location = 0;
        public TranslateTransform movePlayer { get; set; }
        public Random Randomizer = new Random(Guid.NewGuid().GetHashCode());
        //Method called to move the racer, returns true/false if the racer has reached the finish line
        public bool Run()
        {
            //move 20-50 location units(pixels)
            Location += Randomizer.Next(20, 51);
            //check if racer has won the race
            if (Location >= RacetrackLength)
            {
                return true;
            }
            return false;
        }
        //Method resets location to start
        public void TakeStartingPosition()
        {
            Location = 0;
        }
    }
    //Actual punters that will be instantiated with set names and racer numbers
    public class Jim : GoldMiner
    {
        public Jim()
        {
            Name = "Jim";
            Number = 0;
        }
    }

    public class Kelly : GoldMiner
    {
        public Kelly()
        {
            Name = "Kelly";
            Number = 1;
        }
    }

    public class Lee : GoldMiner
    {
        public Lee()
        {
            Name = "Lee";
            Number = 2;
        }
    }

    public class George : GoldMiner
    {
        public George()
        {
            Name = "George";
            Number = 3;
        }
    }
}
