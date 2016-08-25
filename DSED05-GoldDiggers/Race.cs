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
        private static int winner = -1;
        private static DispatcherTimer dispatcherTimer = new DispatcherTimer();


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
        public static void SetImages() { 

    foreach (var goldMiner in goldMiners)
            {
                goldMiner.MyImage.Source = new BitmapImage(new Uri(@"ms-appx:///Assets/GoldMinerWaitSmall.jpg", UriKind.Absolute));
            }
        }

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
            winner = -1;
            foreach (GoldMiner goldMiner in goldMiners)
            {
                goldMiner.TakeStartingPosition();
                UpdateRacePosition(goldMiner.Number);
                goldMiner.MyImage.Source = new BitmapImage(new Uri(@"ms-appx:///Assets/GoldMinerWaitSmall.jpg", UriKind.Absolute));
            }
        }

        public static void PlaceBets()
        {
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
                MessageDialog dialog = new MessageDialog("All bets not placed");
                await dialog.ShowAsync();
            }
        }

        private static async void RunTheRace()
        {
           
            int currentRacer = -1;
            bool winStatus = false;
            do
            {
                List<int> racerOrder = RandomiseRaceOrder();
                foreach (int racer in racerOrder)
                {
                    currentRacer = racer;
                    if(!winStatus){ winStatus = goldMiners[racer].Run();}
                    if (winStatus && winner == -1)
                    {
                        winner = currentRacer;}
                    UpdateRacePosition(racer);

                }
                await Task.Delay(200);
              
            } while (!winStatus);
            
            EndTheRace();
        }

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
            string raceWinnerName = goldMiners[winner].Name;
            string img = "img" + goldMiners[winner].Name;
            goldMiners[winner].MyImage.Source= new BitmapImage(new Uri(@"ms-appx:///Assets/GoldMinerWonSmall.jpg", UriKind.Absolute));
            string betWinnerNames = BetWinners();
            
            MessageDialog dialog = new MessageDialog(raceWinnerName+" won the race, "+betWinnerNames+" won their bet.");
            await dialog.ShowAsync();
            foreach (GoldDigger goldDigger in goldDiggers)
            {
                if (!goldDigger.Busted)
                {
                    goldDigger.Collect(winner);
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
            if (goldDiggers[0].Busted && goldDiggers[1].Busted && goldDiggers[2].Busted)
            {
                MessageDialog dialog = new MessageDialog("All golddiggers are bust. Play again?");
                
                dialog.Commands.Clear();
                dialog.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
                dialog.Commands.Add(new UICommand { Label = "No", Id = 1 });
               

                var res = await dialog.ShowAsync();
                if ((int)res.Id == 0)
                {
                    RestartGame();
                }

                if ((int)res.Id == 1)
                {
                    //Do nothing because programatically closing a windows app is bad
                    //https://msdn.microsoft.com/en-us/library/windows/apps/jj569366.aspx
                    //  Application.Current.Exit();
                }


            }
        }

        private static void RestartGame()
        {
            ResetRacersPosition();
            foreach (var goldDigger in goldDiggers)
            {
                goldDigger.Busted = false;
                goldDigger.MyRadioButton.IsEnabled=true;
                goldDigger.Cash = 50;
                goldDigger.MyLabel.Text = "";
                goldDigger.UpdateLabels("");

            }
        }

        private static string BetWinners()
        {
            string betWinners = "";
            List<string> betWinnerNames = new List<string>();
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
        private static List<int> RandomiseRaceOrder()
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
        private static bool CheckAllBetsIn()
        {
            foreach (GoldDigger goldDigger in goldDiggers)
            {
                if (goldDigger.MyBet == null &&!goldDigger.Busted)
                {
                    return false;
                }
            }
            return true;
        }
        public static void DispatcherTimerSetup()

        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick; //methods that runs
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(10);
            dispatcherTimer.Start();

        }



        static void dispatcherTimer_Tick(object sender, object e)

        {
            var myrandom = new Random();
            for (int j = 0; j < 4; j++)
            {
                goldMiners[j].movePlayer.X += myrandom.Next(0, 4);
                //if the dog has reached the end of the track
                if (goldMiners[j].movePlayer.X > 700)
                {
                    //stop timer
                    dispatcherTimer.Stop();
                    //winningdog holds the winning dog number j
                    //WinnerDetails(j);



                }

            }

        }
    }
}
