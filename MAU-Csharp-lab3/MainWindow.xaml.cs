using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MAU_Csharp_lab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private BMICalculator bmiCalculator = new BMICalculator();
        private SavingsCalculator savingsCalculator = new SavingsCalculator();
        private BMRCalculator bmrCalculator = new BMRCalculator();


        public MainWindow()
        {
            InitializeComponent();
            SetUI();
        }

        private void SetUI()
        {
            btn_calculateBMI.IsEnabled = false;
            rbtn_metric.IsChecked = true;
            btn_calculateSavings.IsEnabled = false;
            SetGrpbx_BMI_results_header("Results for: Noname");
            //rbtn_sedentary.IsChecked = true;
            //rbtn_female.IsChecked = true;
           
        }

        // Set the GroupBox header for BMI results
        private void SetGrpbx_BMI_results_header(string header)
        {
            grpbx_BMIResults.Header = header;
        }


        // Calculate BMI button
        public void btn_CalculateBMI_Click(object sender, RoutedEventArgs e)
        {
            SetName();
            SetHeight();
            SetWeight();

            // Set the name to the results group box
            SetGrpbx_BMI_results_header($"Results for: {bmiCalculator.Name}");

            // Check if metric is enabled    
            bool isMetric = rbtn_metric.IsChecked == true;

            // Call BMI calculator and output bmi
            lbl_BMI.Content = $"{bmiCalculator.CalculateBMI(isMetric),2:F}";

            // Set the weight class output
            lbl_weightCategory.Content = bmiCalculator.CalculateWeightClass();

            // Set "normal weights" text output
            lbl_normalWeight.Content = $"Normal weight should be between {bmiCalculator.GetNormalWeights(isMetric, true),2:F} and {bmiCalculator.GetNormalWeights(isMetric, false),2:F} kg.";
        }


        // Listen for name tbx change
        private void NameTbxChanged(object sender, TextChangedEventArgs e)
        {
            bool isAllBmiInfoEntered = IsAllBmiInfoEntered();
            if (isAllBmiInfoEntered)
                btn_calculateBMI.IsEnabled = true;
            else btn_calculateBMI.IsEnabled = false;
        }


        // Listen for height change
        private void HeightTbxChanged(object sender, TextChangedEventArgs e)
        {
            bool isAllBmiInfoEntered = IsAllBmiInfoEntered();
            if (isAllBmiInfoEntered)
                btn_calculateBMI.IsEnabled = true;
            else btn_calculateBMI.IsEnabled = false;
            if (tbx_height.Text == "")
            {
                lbl_BMI.Content = "";
                lbl_weightCategory.Content = "";
            }
        }


        // Listen for weight change
        private void WeightTbxChanged(object sender, TextChangedEventArgs e)
        {
            bool isAllBmiInfoEntered = IsAllBmiInfoEntered();
            if (isAllBmiInfoEntered)
                btn_calculateBMI.IsEnabled = true;
            else btn_calculateBMI.IsEnabled = false;
            if (tbx_weight.Text == "")
            {
                lbl_BMI.Content = "";
                lbl_weightCategory.Content = "";
            }
        }


        private void rbtn_Imperial_Click(object sender, RoutedEventArgs e)
        {
            if (rbtn_imperial.IsChecked == true)
            {
                lbl_height.Content = "Height (ft)";
                lbl_weight.Content = "Weight (lbs)";
            }
            tbx_height.Text = "";
            tbx_weight.Text = "";
        }


        private void rbtn_Metric_Click(object sender, RoutedEventArgs e)
        {
            if (rbtn_metric.IsChecked == true)
            {
                lbl_height.Content = "Height (cm)";
                lbl_weight.Content = "Weight (kg)";
            }
        }

        private void SetName()
        {
            bmiCalculator.Name = tbx_name.Text;
        }


        private void SetHeight()
        {
            bmiCalculator.Height = Convert.ToDouble(tbx_height.Text);
        }


        private void SetWeight()
        {
            bmiCalculator.Weight = Convert.ToDouble(tbx_weight.Text);
        }


        private bool IsAllBmiInfoEntered()
        {
            if (tbx_name.Text == "")
                return false;
            if (tbx_weight.Text == "" || !double.TryParse(tbx_weight.Text, out double weight))
                return false;
            if (tbx_height.Text == "" || !double.TryParse(tbx_height.Text, out double height))
                return false;
            return true;
        }


        // Savings
        private void btn_CalculateSavings_Click(object sender, RoutedEventArgs e)
        {
            // Send user input and set instance variables
            savingsCalculator.SetAllInputVariables(tbx_initialDeposit.Text, tbx_monthlyDeposit.Text, tbx_period.Text, tbx_growth.Text, tbx_fees.Text);
           
            // Output amount paid
            lbl_amountPaid.Content = $"{savingsCalculator.GetAmountPaid(),2:F}";

            // Output final balance
            lbl_finalBalance.Content = $"{savingsCalculator.GetFinalBalance(),2:F}";

            // Output amount earned
            lbl_amountEarned.Content = $"{savingsCalculator.GetAmountEarned(),2:F}";            

            // Output total fees
            lbl_totalFees.Content = $"{savingsCalculator.GetTotalFees(), 2:F}";
            
        }
      
        private void InitialChanged(object sender, TextChangedEventArgs e)
        {
            ToggleCalculateSavingsButton();
        }

        private void MonthlyChanged(object sender, TextChangedEventArgs e)
        {
            ToggleCalculateSavingsButton();
        }

        private void PeriodChanged(object sender, TextChangedEventArgs e)
        {
            ToggleCalculateSavingsButton();
        }

        private void GrowthChanged(object sender, TextChangedEventArgs e)
        {
            ToggleCalculateSavingsButton();
        }

        private void FeesChanged(object sender, TextChangedEventArgs e)
        {
            ToggleCalculateSavingsButton();
        }


        private bool IsAllInputValid()
        {
            if (!decimal.TryParse(tbx_initialDeposit.Text, out decimal initial))
                return false;
            if (!decimal.TryParse(tbx_monthlyDeposit.Text, out decimal monthly) || monthly <= 0)
                return false;
            if (!decimal.TryParse(tbx_period.Text, out decimal period) || period <= 0)
                return false;
            if (!decimal.TryParse(tbx_growth.Text, out decimal growth) || growth < 0)
                return false;
            if (!decimal.TryParse(tbx_fees.Text, out decimal fees) || fees < 0)
                return false;
            return true;
        }


        private void ToggleCalculateSavingsButton()
        {
            if (IsAllInputValid())
                btn_calculateSavings.IsEnabled = true;
            else btn_calculateSavings.IsEnabled = false;
        }

        // BMR

        private void btn_CalculateBMR_Click(object sender, RoutedEventArgs e)
        {
            lbl_BMR_results.Content = bmrCalculator.CalculateBMR();
        }

        private void rbtn_female_Click(object sender, RoutedEventArgs e)
        {
            if (rbtn_female.IsChecked == true)
                bmrCalculator.SetIsMale(false);
        }

        private void rbtn_male_Click(object sender, RoutedEventArgs e)
        {
            if (rbtn_male.IsChecked == true)
                bmrCalculator.SetIsMale(true);
        }


        private void AgeChanged(object sender, TextChangedEventArgs e)
        {
            if (!Int32.TryParse(tbx_age.Text, out int age) || age <= 0)
                btn_calculateBMR.IsEnabled = false;
            else btn_calculateBMR.IsEnabled = true;
        }

        private void rbtn_sedentary_Click(object sender, RoutedEventArgs e)
        {
            bmrCalculator.SetActivityLevel(1.2);
        }
        private void rbtn_lightly_Click(object sender, RoutedEventArgs e)
        {
            bmrCalculator.SetActivityLevel(1.375);
        }
        private void rbtn_moderately_Click(object sender, RoutedEventArgs e)
        {
            bmrCalculator.SetActivityLevel(1.550);
        }
        private void rbtn_very_Click(object sender, RoutedEventArgs e)
        {
            bmrCalculator.SetActivityLevel(1.725);
        }
        private void rbtn_extra_Click(object sender, RoutedEventArgs e)
        {
            bmrCalculator.SetActivityLevel(1.9);
        }
    }
}
