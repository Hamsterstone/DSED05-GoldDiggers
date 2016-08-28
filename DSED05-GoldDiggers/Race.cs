using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace DSED05_GoldDiggers
{
    public static class Race
    {
        private static int raceTrackLength = 780;
        public static int CurrentDigger;
        public static int CurrentMiner;
        public static int currentBet;
        public static GoldDigger[] goldDiggers = new GoldDigger[3];
        public static GoldMiner[] goldMiners = new GoldMiner[4];
        private static int winner = -1;//set to -1 as a check value

        //  private static DispatcherTimer dispatcherTimer = new DispatcherTimer(); //obsolete, using delay within method instead

        //Method populates godDiggers and goldMiners arrays, 
        //sets starting cash, race length, and starting positions
        public static void LoadData()
        {
            goldDiggers[0] = new Candi();
            goldDiggers[1] = new Mandi();
            goldDiggers[2] = new Sandi();
            foreach (GoldDigger goldDigger in goldDiggers)
            {
                goldDigger.Cash = 50;
            }
            goldMiners[0] = new Jim();
            goldMiners[1] = new Kelly();
            goldMiners[2] = new Lee();
            goldMiners[3] = new George();
            SetRaceLength();
            SetRacersPosition();
        }
        //Method called to set goldMiner images to normal
        public static void SetImages()
        {

            foreach (var goldMiner in goldMiners)
            {
                goldMiner.MyImage.Source = new BitmapImage(new Uri(@"ms-appx:///Assets/GoldMinerWaitSmall.jpg", UriKind.Absolute));
            }
        }
        //Tells each goldMiner the racetrack length
        public static void SetRaceLength()
        {
            foreach (GoldMiner goldMiner in goldMiners)
            {
                goldMiner.RacetrackLength = raceTrackLength;
            }
        }

        public static void SetRacersPosition()
        {
            foreach (GoldMiner goldMiner in goldMiners)
            {
                goldMiner.TakeStartingPosition();
            }
        }

        public static void ResetRacersPosition()
        {
            //Reset winner variable
            winner = -1;


            foreach (GoldMiner goldMiner in goldMiners)
            {
                //Send racers to the start
                goldMiner.TakeStartingPosition();
                //Move the racer's image
                UpdateRacePosition(goldMiner.Number);
                //set the images to normal
                goldMiner.MyImage.Source = new BitmapImage(new Uri(@"ms-appx:///Assets/GoldMinerWaitSmall.jpg", UriKind.Absolute));
            }
        }

        public static void PlaceBets()
        {
            //Sends bet information to the golsDigger.PlaceBet() method
            goldDiggers[CurrentDigger].PlaceBet(currentBet, goldMiners[CurrentMiner].Number);
        }

        public static async void StartTheRace()
        {
            if (CheckAllBetsIn())
            {
                RunTheRace();
            }
            else
            {
                //set up dialog box
                MessageDialog dialog = new MessageDialog("All bets not placed");
                //show dialog box
                await dialog.ShowAsync();
            }
        }

        private static async void RunTheRace()
        {
            //set initial parameters
            int currentRacer = -1;
            bool winStatus = false;
            do //while !winstatus
            {
                //randomise the order racers run
                List<int> racerOrder = RandomiseRaceOrder();
                foreach (int racer in racerOrder)
                {
                    currentRacer = racer;
                    //if no racer has won, current racer runs. Avoids racers moving after race is already won.
                    if (!winStatus) { winStatus = goldMiners[racer].Run(); }
                    //if someone has won but the global variable winner has not been set
                    if (winStatus && winner == -1)
                    {
                        //set the global winner variable
                        winner = currentRacer;
                    }
                    //move the image of the current racer
                    UpdateRacePosition(racer);

                }
                //wait 200ms so the race is actually watchable
                await Task.Delay(200);

            } while (!winStatus);
            //Once someone has won the race, end the race
            EndTheRace();
        }
        //Method updates image location of racer number received
        public static void UpdateRacePosition(int racer)
        {
            switch (racer)
            {
                case 0:
                    goldMiners[0].movePlayer.X = goldMiners[0].Location;
                    break;
                case 1:
                    goldMiners[1].movePlayer.X = goldMiners[1].Location;
                    break;
                case 2:
                    goldMiners[2].movePlayer.X = goldMiners[2].Location;
                    break;
                case 3:
                    goldMiners[3].movePlayer.X = goldMiners[3].Location;
                    break;
            }
        }
        private static async void EndTheRace()
        {
            //change the winning racers image
            goldMiners[winner].MyImage.Source = new BitmapImage(new Uri(@"ms-appx:///Assets/GoldMinerWonSmall.jpg", UriKind.Absolute));
            //set up strings for dialog box
            string raceWinnerName = goldMiners[winner].Name;
            string betWinnerNames = BetWinners();
            //set up dialog box
            MessageDialog dialog = new MessageDialog(raceWinnerName + " won the race, " + betWinnerNames + " won their bet.");
            //show dialog box
            await dialog.ShowAsync();

            foreach (GoldDigger goldDigger in goldDiggers)
            {
                if (!goldDigger.Busted)
                {
                    //collect winnings(0 sent back if not a winner)
                    goldDigger.Collect(winner);
                    //If goldDigger is out of money
                    if (goldDigger.Cash == 0)
                    {
                        goldDigger.Busted = true;
                        goldDigger.UpdateLabels("Busted!");
                        CheckForAllBust();
                        goldDigger.MyRadioButton.IsEnabled = false;
                    }
                }
            }

        }

        private async static void CheckForAllBust()
        {
            //If all goldDiggers are out of money
            if (goldDiggers[0].Busted && goldDiggers[1].Busted && goldDiggers[2].Busted)
            {
                //Set up a dialog box
                MessageDialog dialog = new MessageDialog("All golddiggers are bust. Play again?");
                dialog.Commands.Clear();
                dialog.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
                dialog.Commands.Add(new UICommand { Label = "No", Id = 1 });


                var res = await dialog.ShowAsync();
                if ((int)res.Id == 0)//Yes is clicked
                {
                    RestartGame();
                }

                if ((int)res.Id == 1)//No is clicked
                {
                    //Do nothing because programatically closing a windows app is bad
                    //https://msdn.microsoft.com/en-us/library/windows/apps/jj569366.aspx
                    //  Application.Current.Exit(); //Obsolete. Don't do this
                }


            }
        }

        // Resets all parameters to starting conditions
        private static void RestartGame()
        {
            ResetRacersPosition();
            foreach (var goldDigger in goldDiggers)
            {
                goldDigger.Busted = false;
                goldDigger.MyRadioButton.IsEnabled = true;
                goldDigger.Cash = 50;
                goldDigger.MyLabel.Text = "";
                goldDigger.UpdateLabels("");

            }
        }
        //Builds a string based on which bettors won
        private static string BetWinners()
        {
            string betWinners = "";
            List<string> betWinnerNames = new List<string>();
            //populate betWinnerNames with the successful bettors
            foreach (var goldDigger in goldDiggers)
            {
                if (!goldDigger.Busted)
                {
                    if (goldDigger.MyBet.Digger == winner)
                    {
                        betWinnerNames.Add(goldDigger.Name);
                    }
                }
            }
            //Build the string of bettor names based on how many there are in the list
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
            return "Impossible to get here, something is wrong - BetWinners() broke";
        }
        private static List<int> RandomiseRaceOrder()
        {

            List<int> racerNumbers = new List<int>() { 0, 1, 2, 3 };
            List<int> raceOrder = new List<int>();
            Random randomizer = new Random(Guid.NewGuid().GetHashCode());
            //take  racer order in, randomise into a new racer order, return new racer order
            do //while (racerNumbers.Count != 0);
            {
                //pick a racer
                int racer = randomizer.Next(0, racerNumbers.Count);
                // add racer to new order
                raceOrder.Add(racerNumbers[racer]);
                //remove racer from available choices
                racerNumbers.RemoveAt(racer);
            } while (racerNumbers.Count != 0);

            return raceOrder;


        }
        //Checks each better has placed a bet or is bust and can't
        private static bool CheckAllBetsIn()
        {

            foreach (GoldDigger goldDigger in goldDiggers)
            {
                if (goldDigger.MyBet == null && !goldDigger.Busted)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
   