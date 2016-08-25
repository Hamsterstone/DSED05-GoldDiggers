using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DSED05_GoldDiggers
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        
        
        public MainPage()
        {
            this.InitializeComponent();
            Race.LoadData();
            ConnectMainPageElements();
            Race.SetImages();
        }

        // Connects Mainpage XAML components to class components
        public void ConnectMainPageElements()
        {
            Race.goldMiners[0].movePlayer = MoveJim;
            Race.goldMiners[1].movePlayer = MoveKelly;
            Race.goldMiners[2].movePlayer = MoveLee;
            Race.goldMiners[3].movePlayer = MoveGeorge;
            Race.goldDiggers[0].MyRadioButton = rbtCandi;
            Race.goldDiggers[1].MyRadioButton = rbtMandi;
            Race.goldDiggers[2].MyRadioButton = rbtSandi;
            Race.goldDiggers[0].MyLabel = txtCandi;
            Race.goldDiggers[1].MyLabel = txtMandi;
            Race.goldDiggers[2].MyLabel = txtSandi;
            Race.goldMiners[0].MyImage = imgJim;
            Race.goldMiners[1].MyImage = imgKelly;
            Race.goldMiners[2].MyImage = imgLee;
            Race.goldMiners[3].MyImage = imgGeorge;
        }


        private async void btnPlaceBet_Click(object sender, RoutedEventArgs e)
        {
            // Check that the racers are at the starting line
            if (Race.goldMiners[1].Location != 0)
            {
                //Create a dialog box
                MessageDialog dialog = new MessageDialog("Reset the racers before betting");
                //Show the dialog box
                await dialog.ShowAsync();
            }
            else
            {
                // Place bet
                Race.PlaceBets();
                // Update Bet descriptions on the form
                switch (Race.CurrentDigger)
                {
                    case 0:
                        txtCandi.Text = Race.goldDiggers[Race.CurrentDigger].MyBet.GetDescription();
                        break;
                    case 1:
                        txtMandi.Text = Race.goldDiggers[Race.CurrentDigger].MyBet.GetDescription();
                        break;
                    case 2:
                        txtSandi.Text = Race.goldDiggers[Race.CurrentDigger].MyBet.GetDescription();
                        break;
                }
            }


        }

        private void btnRace_Click(object sender, RoutedEventArgs e)
        {
            Race.StartTheRace();
           

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            Race.ResetRacersPosition();
            
            foreach (GoldDigger goldDigger in Race.goldDiggers)
            {
                goldDigger.MyBet = null;
            }
            foreach (GoldDigger goldDigger in Race.goldDiggers)
            {
                if (goldDigger.MyRadioButton.IsEnabled)
                {
                    Race.CurrentDigger = Convert.ToInt32(goldDigger.MyRadioButton.Tag);
                    sldBetAmount.Maximum = Race.goldDiggers[Race.CurrentDigger].Cash;


                    //Factory.SetTheGuyNumber(rbfake.Name);

                    //  sldBetAmount.Maximum = myguy[Factory.GuyNumber].Cash;
                    //make the bet only go to maximum of the money the person has
                    //lblName.Text = myguy[Factory.GuyNumber].Name + " bets"; // shows on the label
                    btnPlaceBet.Content = "Place Bet for " + Race.goldDiggers[Race.CurrentDigger].Name;
                    //shows on the bet button
                    lblBet.Text = "Max bet is $" + Race.goldDiggers[Race.CurrentDigger].Cash;
                    //shows the max he can bet

                    //set the initial dog and bet}
                }

            }
        }

        private void rb_CheckedChanged(object sender, RoutedEventArgs e)
        {
            //all rb clicks
            RadioButton rbfake = sender as RadioButton;
            

            switch (rbfake.GroupName)
            {
                case "GoldDiggers":
                    if (rbfake.IsChecked.Value == true)
                    {
                        Race.CurrentDigger = Convert.ToInt32(rbfake.Tag);
                        sldBetAmount.Maximum = Race.goldDiggers[Race.CurrentDigger].Cash;
                       

                        //Factory.SetTheGuyNumber(rbfake.Name);

                        //  sldBetAmount.Maximum = myguy[Factory.GuyNumber].Cash;
                        //make the bet only go to maximum of the money the person has
                        //lblName.Text = myguy[Factory.GuyNumber].Name + " bets"; // shows on the label
                        btnPlaceBet.Content = "Place Bet for " + Race.goldDiggers[Race.CurrentDigger].Name;
                        //shows on the bet button
                        lblBet.Text = "Max bet is $" + Race.goldDiggers[Race.CurrentDigger].Cash;
                        //shows the max he can bet

                        //set the initial dog and bet
                       
                        

                    }
                    break;
                case "GoldMiners":

                   
                  

                    if (rbfake.IsChecked.Value == true)
                    {
                        Race.CurrentMiner = Convert.ToInt32(rbfake.Tag);
                        
                    }

                    break;

            }
        }

        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            //https://msdn.microsoft.com/en-us/windows/uwp/controls-and-patterns/slider

            Slider slider = sender as Slider;

            if (Race.CurrentDigger!=null)
                //only run if a person is selected
            {
                Race.currentBet = Convert.ToInt32(slider.Value);
            }

            
        }

       
    }
}
