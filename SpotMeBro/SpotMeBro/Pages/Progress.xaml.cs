using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SpotMeBro.Database;
using SpotMeBro.HelperFiles;

namespace SpotMeBro.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Progress : ContentPage
    {
        string selectedWorkout;
        protected override void OnAppearing()
        {
            base.OnAppearing();
            exerciseProgress.ItemsSource = DatabaseClass.getListOfPastExercises();
        }

        public Progress()
        {
            InitializeComponent();
        }
       
        private async void showExerciseProgress_Clicked(object sender, EventArgs e)
        {
            var layout = (BindableObject)sender;
            var selectedWorkout = (WorkoutModel)layout.BindingContext;
            await Navigation.PushAsync(new ShowExerciseProgress(selectedWorkout));

        }

        private async void backToMain_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Main());
        }
    }
}