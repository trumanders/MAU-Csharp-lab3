using System;
using System.Collections.Generic;
using System.Text;

namespace MAU_Csharp_lab3
{
    class BMRCalculator : BMICalculator
    {
        const int BMR_FEMALE_ADD = -161;
        const int BMR_MALE_ADD = 5;
        private double activityLevelFactor;
        private bool isMale;
        private int age;
        private double BMR;
        private double maintainWeightBMR;
        public void SetIsMale(bool isMale)
        {
            this.isMale = isMale;
        }

        public void SetActivityLevel(double activityLevel)
        {
            this.activityLevelFactor = activityLevel;
        }
        public double CalculateBMR()
        {
            BMR = (10 * Weight) + (6.25 * Height) - (5 * age);
            if (isMale) BMR += BMR_MALE_ADD;
            else BMR += BMR_FEMALE_ADD;

            maintainWeightBMR = BMR * activityLevelFactor;
            return maintainWeightBMR;
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
