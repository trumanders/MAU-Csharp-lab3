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
        const double POUND_TO_WEIGHT = 0.453592;
        const double INCH_TO_CM = 2.54;

        private double bmi;
        private string name;
        private double height;
        private double weight;

        double BMI
        {
            get { return bmi; }
            set { this.bmi = value; }
        }

        public string Name
        {
            get { return name; }
            set { this.name = value; }
        }
        
        public double Height
        {
            get { return height; }
            set { this.height = value; }
        }

        public double Weight
        {
            get { return weight; }
            set { this.weight = value; }
        }

        public void PoundToKg()
        {
            this.weight *= POUND_TO_WEIGHT;
        }

        public void InchToCm()
        {
            this.height *= INCH_TO_CM;
        }

        public double GetWeight()
        {
            return this.weight;
        }

        public double GetHeight()
        {
            return this.height;
        }

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
                return BMI * (Math.Pow(height / 100, 2));
            else
                return BMI * ((height * height) / IMPERIAL_CONSTANT);
        }

        public double CalculateBMI(bool isMetric)
        {
            if (isMetric)
                BMI = weight / Math.Pow(height / 100, 2);
            else
            {
                BMI = (IMPERIAL_CONSTANT * weight) / Math.Pow(height,2);
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
