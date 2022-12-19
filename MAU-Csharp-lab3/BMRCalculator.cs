using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media.Animation;

namespace MAU_Csharp_lab3
{
    class BMRCalculator : BMICalculator
    {
        const int BMR_FEMALE_ADD = -161;
        const int BMR_MALE_ADD = 5;
        private double activityLevelFactor;
        private bool isMale;
        private int age;
        private double bmr;
        private double maintainWeightBMRs;

        public void SetAge(int age)
        { 
            this.age = age;
        }

        public void SetIsMale(bool isMale)
        {
            this.isMale = isMale;
        }

        public void SetActivityLevel(double activityLevel)
        {
            this.activityLevelFactor = activityLevel;
        }

        private double GetMaintainWeightBMRs(bool isMetric)
        {
            return bmr * activityLevelFactor;
        }

        private void CalculateAndSetBMR(bool isMetric)
        {
            // Convert to kg and cm if input is imperial
            if (!isMetric)
            {
                Weight *= 0.453592;
                Height *= 2.54;
            }
            double tempBMR = (10 * Weight) + (6.25 * Height) - (5 * age);
            if (isMale)
                bmr = tempBMR + BMR_MALE_ADD;
            else
                bmr = tempBMR + BMR_FEMALE_ADD;
        }

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

        /*Formula

        Females: (10*weight[kg]) + (6.25*height[cm]) – (5*age[years]) – 161
        Males: (10*weight[kg]) + (6.25*height[cm]) – (5*age[years]) + 5

        Multiply by scale factor for activity level:
        Sedentary*1.2
        Lightly active *1.375
        Moderately active *1.55
        Active*1.725
        Very active *1.9 */

    }
}
