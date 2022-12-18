using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Windows;

namespace MAU_Csharp_lab3
{
    class BMICalculator
    {
        const double IMPERIAL_CONSTANT = 703.0;
        const double NORMAL_BMI_LOW = 18.5;
        const double NORMAL_BMI_HIGH = 24.9;
        const int CONVERT_TO_INCH = 12;

        protected double BMI { get; set; }
        public string Name { get; set; }        
        public double Height { get; set; }
        public double Weight { get; set; }
        protected bool isMetric { get; set; }


        /// <summary>
        /// Calculate the lowest recomended weight based on the lowest normal bmi
        /// </summary>
        /// <param name="isMetric">Defines whether the units are metric or imperial</param>
        /// <returns>double The lowest normal weight</returns>
        public double GetNormalWeights(bool isMetric, bool isLowestWeight)
        {
            // Set the BMI to lowest or highest normal.
            BMI = NORMAL_BMI_HIGH;
            if (isLowestWeight)
                BMI = NORMAL_BMI_LOW;

            if (isMetric)
                return BMI * (Math.Pow(Height / 100, 2));
            else
                return BMI * ((Height * Height) / IMPERIAL_CONSTANT);
        }

        public double CalculateBMI(bool isMetric)
        {
            if (isMetric)
                BMI = Weight / Math.Pow(Height / 100, 2);
            else
            {                
                Height *= CONVERT_TO_INCH;
                BMI = (IMPERIAL_CONSTANT * Weight) / Math.Pow(Height,2);
            }
            return BMI;
        }

        public string CalculateWeightClass()
        {
            if (BMI < 18.5)
                return "Underweight";
            if (BMI >= 18.5 && BMI <= 24.9)
                return "Normal weight";
            if (BMI >= 25.0 && BMI <= 29.9)
                return "Overweight (Pre-obesity)";
            if (BMI >= 30.0 && BMI <= 34.9)
                return "Overweight (obesity class I)";
            if (BMI >= 35.0 && BMI <= 39.9)
                return "Overweight (obesity class II)";
            if (BMI >= 40)
                return "Overweight (obesity class III)";
            return "";
        }

        public double WeightInKg(double weightInPounds)
        {
            return weightInPounds * 0.45359237;
        }
    }
}
