using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SpotMeBro.Database;

namespace SpotMeBro.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Main : ContentPage
    {
        public Main()
        {
            InitializeComponent();
        }

        private async void recordWorkoutClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TodaysWorkout(System.DateTime.Now));
        }

        private async void addEditWorkoutsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditExercise());
        }

        private async void seeYourProgressClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Progress());
        }

        private async void optionsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Options());
        }
        private async void logOutClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}

