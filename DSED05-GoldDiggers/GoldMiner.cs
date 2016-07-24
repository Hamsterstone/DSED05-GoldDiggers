using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSED05_GoldDiggers
{
    public class GoldMiner
    {
        public int StartingPosition;
        public int RacetrackLength;
       // public PictureBox MyPictureBox = null;
        public int Location = 0;
        public Random Randomizer;

        public bool Run()
        {
            Location += Randomizer.Next(1, 4);
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
}
