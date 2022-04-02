using System;
using SpotMeBro.HelperFiles;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SpotMeBro.Database;

namespace SpotMeBro.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentExercise : ContentPage
    {
        DateTime thisWorkoutMarker;
        string exerciseName;
        public CurrentExercise(DateTime date, ExerciseModel exercise)
        {
            InitializeComponent();
            thisWorkoutMarker = date;
            exerciseName = exercise.ExerciseName;
            BindingContext = exerciseName;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            exerciseTitlex.Text = exerciseName;
        }
        private async void PickNextExercise_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PickNextExercise(thisWorkoutMarker));
        }

        private async void backToTodaysWorkout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TodaysWorkout(thisWorkoutMarker));
        }

        private async void RecordSet_Clicked(object sender, EventArgs e)
        {
            int weight;
            int reps;
            if (string.IsNullOrEmpty(weightsEntered.Text) || string.IsNullOrEmpty(repsEntered.Text))
            {
                DisplayAlert("Error", "Blank or empty values are not valid.", "OK");
            }
            if(int.TryParse(weightsEntered.Text, out weight) == false)
            {
                DisplayAlert("Error", "Invalid weight entered, integers only.", "OK");
            }
            if(int.TryParse(repsEntered.Text, out reps) == false)
            {
                DisplayAlert("Error", "Invalid reps entered, integers only", "OK");
            }
                if (int.TryParse(weightsEntered.Text, out weight) == true && int.TryParse(repsEntered.Text, out reps) == true)
                {
                    DatabaseClass.saveCurrentWorkoutSet(new WorkoutModel { Date = thisWorkoutMarker.ToString(), ExerciseName = exerciseName.ToUpper(), Weight = weight, Reps = reps });
                    DisplayAlert("Recorded", "That set has been recorded. Keep it up!", "OK");
                    weightsEntered.Text = "";
                    repsEntered.Text = "";
                    weightsEntered.Placeholder = "Enter weight";
                    repsEntered.Placeholder = "Enter reps";


                }
        }
        
    }
}