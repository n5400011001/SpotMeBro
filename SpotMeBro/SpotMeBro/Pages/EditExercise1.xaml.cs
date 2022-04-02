using System;
using SpotMeBro.Database;
using SpotMeBro.HelperFiles;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpotMeBro.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditExercise1 : ContentPage
    {
        public EditExercise1()
        {
            InitializeComponent();
        }

        void AddExerciseToDatabase_Clicked(object sender, EventArgs e)
        {
            //var nameEntry = new Entry();
            //var BodyPartPicker = new Picker();
            if (!string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                //bool IsItADuplicate;
                if (DatabaseClass.duplicateExercise(nameEntry.Text) == false && BodyPartPicker.SelectedIndex != -1)//check to see if the exercise already exists in the sqlite exerciseTable
                {
                    DatabaseClass.saveCurrentExercise(new ExerciseModel { ExerciseName = nameEntry.Text.ToUpper(), ExerciseBodyPart = BodyPartPicker.SelectedItem.ToString().ToUpper() });
                    Navigation.PushAsync(new EditExercise());
                    
                }
                else
                {
                    DisplayAlert("Error", nameEntry.Text + " is already listed.", "OK");
                }
            }
        }
    }
}