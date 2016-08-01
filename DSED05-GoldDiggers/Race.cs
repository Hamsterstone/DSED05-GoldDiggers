using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace DSED05_GoldDiggers
{
    class Race
    {
        private int raceTrackLength = 50;

        GoldDigger[] goldDiggers = new GoldDigger[3];
        GoldMiner[] goldMiners = new GoldMiner[4];
        private int winner = -1;

        public void LoadData()
        {
            goldDiggers[0] = new GoldDigger() {Name = "Candi"};
            goldDiggers[1] = new GoldDigger() {Name = "Mandi" };
            goldDiggers[2] = new GoldDigger() {Name = "Sandi" };
            foreach (GoldDigger goldDigger in goldDiggers)
            {
                goldDigger.Cash = 50;
            }
            goldMiners[0] = new GoldMiner() { Name = "Jim" };
            goldMiners[1] = new GoldMiner() { Name = "Kelly" };
            goldMiners[2] = new GoldMiner() { Name = "Lee" };
            goldMiners[3] = new GoldMiner() { Name = "George" };
           ResetRacersPosition();
        }

        public void SetRaceLength()
        {
            foreach (GoldMiner goldMiner in goldMiners)
            {
                goldMiner.RacetrackLength = raceTrackLength;
            }
        }
        public void ResetRacersPosition()
        {
            foreach (GoldMiner goldMiner in goldMiners)
            {
                goldMiner.TakeStartingPosition();

            }
        }

        public void PlaceBets()
        {
            //todo take bet info from uwp and pass to GoldDigger.placebet
        }

        public void StartTheRace()
        {
            if (CheckAllBetsIn())
            {
                RunTheRace();
            }
            else new MessageDialog("All bets not placed");
        }

        private void RunTheRace()
        {
            int currentRacer = -1;
            bool winStatus = false;
            do
            {
                List<int> racerOrder = RandomiseRaceOrder();
                foreach (int racer in racerOrder)
                {
                    currentRacer = racer;
                    winStatus = goldMiners[racer].Run();
                }
            } while (!winStatus);
            winner = currentRacer;

        }

        private void EndTheRace()
        {
            string raceWinnerName = goldMiners[winner].Name;
            string betWinnerNames = BetWinners();
            new MessageDialog(raceWinnerName+" won the race, "+betWinnerNames+" won their bet.");
            foreach (GoldDigger goldDigger in goldDiggers)
            {
                goldDigger.Collect(winner);
            }

        }

        private string BetWinners()
        {
            string betWinners = "";
            List<string> betWinnerNames = new List<string>();
            foreach (var goldDigger in goldDiggers)
            {
                if (goldDigger.MyBet.Digger == winner)
                {
                    betWinnerNames.Add(goldDigger.Name);
                }
            }
            switch (betWinnerNames.Count)
            {
                case 0:
                    betWinners = "Nobody";
                    return betWinners;
                case 1:
                    betWinners = betWinnerNames[0];
                    return betWinners;
                case 2:

                    betWinners = betWinnerNames[0] + " and " + betWinnerNames[1];
                    return betWinners;
                case 3:
                    betWinners = betWinnerNames[0] + ", " + betWinnerNames[1] + ", and " + betWinnerNames[2];
                    return betWinners;
            }
            return "Impossible to get here, something is wrong";
        }
        private List<int> RandomiseRaceOrder()
        {
         
            List<int> racerNumbers = new List<int>() {0,1,2,3};
            List<int> raceOrder = new List<int>();
            Random randomizer = new Random(Guid.NewGuid().GetHashCode());
            //take  cardorder in, randomise into a new card order, return new cardorder
            do
            {
                //pick a card
                int racer = randomizer.Next(0, racerNumbers.Count);

                raceOrder.Add(racerNumbers[racer]);
                racerNumbers.RemoveAt(racer);
            } while (racerNumbers.Count != 0);
           
              return raceOrder;
        
       
        }
        private bool CheckAllBetsIn()
        {
            throw new NotImplementedException();
        }
    }
}
