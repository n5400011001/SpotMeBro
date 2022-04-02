using System;
using SpotMeBro.HelperFiles;
using SpotMeBro.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace SpotMeBro.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodaysWorkout : ContentPage
    {
        DateTime thisWorkout;
        public TodaysWorkout(DateTime date)
        {
            //Helper.setWorkouts();
            InitializeComponent();
            thisWorkout = date;
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (await DisplayAlert("Warning!", "Leaving this page ends your workout. Click 'Pick your next exercise' to continue with this workout.", "LEAVE", "CONTINUE"))
                {
                    base.OnBackButtonPressed();
                    await Navigation.PushAsync(new Main());
                }
            });
            return true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            exercisesDone.ItemsSource = DatabaseClass.getExercisesDone(thisWorkout);
            //exercisesDone.ItemsSource = DatabaseClass.getAllExercises();

        }
        private async void PickNextExercise_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PickNextExercise(thisWorkout));
        }


        private async void Exercise_Clicked(object sender, EventArgs e)
        {
            var layout = (BindableObject)sender;
            var exercises = (WorkoutModel)layout.BindingContext;
            string selectedExercisename = exercises.ExerciseName;
            await Navigation.PushAsync(new ExerciseReview(thisWorkout, selectedExercisename));
        }
        
        private async void backToMain_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Warning!", "Leaving this page ends your workout. Click 'Pick your next exercise' to continue with this workout.", "LEAVE", "CONTINUE");
            if(answer == true)
            {
                await Navigation.PushAsync(new Main());
            }    
        }
    }
}