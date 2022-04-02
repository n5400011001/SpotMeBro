using System;
using SpotMeBro.Database;
using SpotMeBro.HelperFiles;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace SpotMeBro.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditThisExercise : ContentPage
    {
        int ID;
        string oldExerciseName;
        string newExerciseName;
        public EditThisExercise(ExerciseModel exercise)
        {
            InitializeComponent( );
            ID = exercise.Id;
            oldExerciseName = exercise.ExerciseName;
        }

        private async void SaveEdit_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Warning!",
                "You are about to change the name " 
                + oldExerciseName + " to " + nameEntry.Text.ToUpper() + 
                ". This will also change all previous records with this name to the new one.",
                "Edit Exercise Name",
                "Cancel");
            if (answer == true)
            {
                if (!string.IsNullOrWhiteSpace(nameEntry.Text))
                {
                    newExerciseName = nameEntry.Text.ToUpper();
                    //check to see if the exercise already exists in the sqlite exerciseTable
                    if (DatabaseClass.duplicateExercise(newExerciseName) == false && BodyPartPicker.SelectedIndex != -1)
                    {
                        DatabaseClass.saveCurrentExercise(new ExerciseModel { 
                                                                            ExerciseName = nameEntry.Text.ToUpper(), 
                                                                            ExerciseBodyPart = BodyPartPicker.SelectedItem.ToString().ToUpper() });
                        //Updates previous records that might have the old workout name in it with the new one
                        DatabaseClass.updateExerciseNameInWorkoutTable(oldExerciseName.ToUpper(), 
                                                                       nameEntry.Text.ToUpper());//Updates previous records that might have the old workout name in it with the new one
                        DatabaseClass.deleteRowInExerciseModel(oldExerciseName);

                        await Navigation.PushAsync(new EditExercise());

                    }
                    else
                    {
                        await DisplayAlert("Error", nameEntry.Text + " is already listed.", "OK");
                    }
                }
            }
      
            
        }
    }
}