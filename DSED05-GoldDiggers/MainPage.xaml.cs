using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        }

        private void btnRace_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
/*
 
     
      <Slider
                x:Name="udDog"
                Height="40"
                Margin="9,0,0,0"
                VerticalAlignment="Bottom"
                LargeChange="1"
                Maximum="4"
                Minimum="1"
                TickPlacement="Outside"
                ToolTipService.ToolTip="Choose Monster"
                ValueChanged="Slider_ValueChanged" />
            <Slider
                x:Name="udBet"
                Height="40"
                Margin="9,0,0,54"
                VerticalAlignment="Bottom"
                Maximum="2"
                Minimum="1"
                TickPlacement="Outside"
                ToolTipService.ToolTip="Choose Bet"
                ValueChanged="Slider_ValueChanged" />
            <TextBlock
                x:Name="txtMonster"
                Width="34"
                Height="30"
                Margin="157,99,0,0"
                HorizontalAlignment="Left"
                VerticalAlignment="Top"
                RenderTransformOrigin="0.511,-0.35"
                Text="{Binding Value, ElementName=udDog, Mode=OneWay}"
                TextWrapping="Wrap" />
            <TextBlock
                x:Name="txtBet"
                Width="34"
                Height="30"
                Margin="157,43,0,0"
                HorizontalAlignment="Left"
                VerticalAlignment="Top"
                RenderTransformOrigin="0.511,-0.35"
                Text="{Binding Value, ElementName=udBet, Mode=OneWay}"
                TextWrapping="Wrap" />
            <TextBlock
                x:Name="lblName_Copy"
                Width="116"
                Height="32"
                Margin="41,97,0,0"
                HorizontalAlignment="Left"
                VerticalAlignment="Top"
                Text="Monster Chosen"
                TextWrapping="Wrap" />
     
     
    
    
    
    
    
    
    private void rb_CheckedChanged(object sender, RoutedEventArgs e)
        {
            //all rb clicks
            RadioButton rbfake = sender as RadioButton;

            if (rbfake.IsChecked.Value == true) {

                Factory.SetTheGuyNumber(rbfake.Name);

                udBet.Maximum = myguy[Factory.GuyNumber].Cash; //make the bet only go to maximum of the money the person has
                lblName.Text = myguy[Factory.GuyNumber].Name + " bets"; // shows on the label
                btnBet.Content = "Place Bet for " + myguy[Factory.GuyNumber].Name; //shows on the bet button
                lblBet.Text = "Max bet is $" + myguy[Factory.GuyNumber].Cash;  //shows the max he can bet
                 
                //set the initial dog and bet
                myguy[Factory.GuyNumber].Amount = Convert.ToInt32(udBet.Value);
                myguy[Factory.GuyNumber].Monster = Convert.ToInt32(udDog.Value);
            }
        }

        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            //https://msdn.microsoft.com/en-us/windows/uwp/controls-and-patterns/slider

            Slider slider = sender as Slider;

            if (slider.Name == "udBet" && Factory.GuyNumber != 99) //only run if a person is selected 99 is the default of no person
            {
                myguy[Factory.GuyNumber].Amount = Convert.ToInt32(slider.Value);
            }

            if (slider.Name == "udDog" && Factory.GuyNumber != 99)  //pass the amount betted to the class
            {
                myguy[Factory.GuyNumber].Monster = Convert.ToInt32(slider.Value);

            }




    TO SET SLIDER MAXIMUM

      udBet.Maximum = myguy[Factory.GuyNumber].Cash; //make the bet only go to maximum of the money the person has




     
     */
