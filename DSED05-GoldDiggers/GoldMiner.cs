using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace DSED05_GoldDiggers
{
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

        public bool Run()
        {
            Location += Randomizer.Next(20, 51);
            if (Location >= RacetrackLength)
            {
                return true;
            }
            return false;
        }

        public void TakeStartingPosition()
        {
            Location = 0;
        }
    }

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
