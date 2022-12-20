using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media.Animation;

namespace MAU_Csharp_lab3
{
    /// <summary>
    /// Handles the calculation for the BMR - results. Inherits from BMICalculator.
    /// Some of the variables are shared between the two classes. 
    /// /// to 
    /// </summary>
    class BMRCalculator : BMICalculator
    {
        const int BMR_FEMALE_ADD = -161;
        const int BMR_MALE_ADD = 5;
        private double activityLevelFactor;
        private bool isMale;
        private int age;
        private double bmr;
        private double maintainWeightBMRs;

        /// <summary>
        /// Set the private is instance variable that stores the person's age. The age
        /// is set by the user in a textbox.
        /// </summary>
        /// <param name="age">The persons age.</param>
        public void SetAge(int age)
        { 
            this.age = age;
        }

        /// <summary>
        /// Set the boolean for female or male. The calculations are slightly different
        /// depending on sex. The gender setting is a user input radio button.
        /// </summary>
        /// <param name="isMale">Boolean that decides sex.</param>
        public void SetIsMale(bool isMale)
        {
            this.isMale = isMale;
        }


        /// <summary>
        /// Set the activity level based on the user's choise of
        /// radio buttons in the UI.
        /// </summary>
        /// <param name="activityLevel">The activity level is set by the user using radio buttons
        /// in the UI.</param>
        public void SetActivityLevel(double activityLevel)
        {
            this.activityLevelFactor = activityLevel;
        }


        /// <summary>
        /// Calculate the BMR to maintain the weight.
        /// </summary>
        /// <param name="isMetric">Tells if bmr is based on metric or imperial units.</param>
        /// <returns></returns>
        private double GetMaintainWeightBMRs(bool isMetric)
        {
            return bmr * activityLevelFactor;
        }


        /// <summary>
        /// Calculates the bmr and sets the private instance variable.
        /// </summary>
        /// <param name="isMetric">Tells if the weight and height variables are set to
        /// metric or imperial units.</param>
        private void CalculateAndSetBMR(bool isMetric)
        {
            // Convert to kg and cm if input is imperial
            if (!isMetric)
            {
                PoundToKg();
                InchToCm();
            }
            double tempBMR = (10 * GetWeight()) + (6.25 * GetHeight()) - (5 * age);
            if (isMale)
                bmr = tempBMR + BMR_MALE_ADD;
            else
                bmr = tempBMR + BMR_FEMALE_ADD;
        }


        /// <summary>
        /// Set the output info for the BMI calculations.
        /// </summary>
        /// <param name="isMetric">Tells if the calculations are based on metric or imperial units.</param>
        /// <returns>A string array with one element representing the listbox content.</returns>
        public string[] GetBMRStringOutput(bool isMetric)
        {
            CalculateAndSetBMR(isMetric);
            double maintain = GetMaintainWeightBMRs(isMetric);
            string output = "";
            string line1 = $"BMR RESULTS FOR {Name}";
            string line2 = $"Your BMR(calories / day)";
            string line3 = $"Calories to maintain your weight";
            string line4 = $"Calories to lose 0.5 kg per week";
            string line5 = $"Calories to lose 1 kg per week";
            string line6 = $"Calories to gain 0.5 kg per week";
            string line7 = $"Calories to gain 1 kg per week";
            string line8 = $"Losing more than 1000 calories per day is to be avoided.";            

            output += $"{line1,-35}\n\n{line2,-35}{bmr,7:F1}\n{line3, -35}{maintain,7:F1}\n{line4,-35}{maintain-500,7:F1}\n{line5,-35}{maintain - 1000,7:F1}\n";
            output += $"{line6,-35}{maintain + 500,7:F1}\n{line7,-35}{maintain + 1000,7:F1}\n\n{line8}";

            string[] itemsSource = new string[] { output };
            return itemsSource;
        }
    }
}
