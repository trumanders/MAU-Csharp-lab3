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
        private SavingsCalculator savingsCalculator = new SavingsCalculator();
        private BMRCalculator calculator = new BMRCalculator();
        private bool isMetric;
        private const int FOOT_TO_INCH = 12;
        private bool allowBMR = false;


        public MainWindow()
        {
            InitializeComponent();
            SetUI();
        }


        private void SetUI()
        {
            btn_calculateBMI.IsEnabled = false;
            btn_calculateBMR.IsEnabled = false;
            rbtn_metric.IsChecked = true;
            rbtn_female.IsChecked = true;
            rbtn_moderate.IsChecked = true; 
            isMetric = true;
            tbx_height.IsEnabled = false;
            btn_calculateSavings.IsEnabled = false;
            SetGrpbx_BMI_results_header("Results for: Noname");
            //rbtn_sedentary.IsChecked = true;
            //rbtn_female.IsChecked = true;
           
        }


        /* BMI */

        // Set the GroupBox header for BMI results
        private void SetGrpbx_BMI_results_header(string header)
        {
            grpbx_BMIResults.Header = header;
        }


        // Calculate BMI - button
        public void btn_CalculateBMI_Click(object sender, RoutedEventArgs e)
        {
            SetName();
            SetHeight();
            SetWeight();

            // Set the name to the results group box
            SetGrpbx_BMI_results_header($"Results for: {calculator.Name}");
                        
            // Call BMI calculator and output bmi
            lbl_BMI.Content = $"{calculator.CalculateBMI(isMetric),2:F}";

            // Set the weight class output
            lbl_weightCategory.Content = calculator.CalculateWeightClass();

            // Set "normal weights" text output
            string weightUnit;
            if (isMetric) { weightUnit = "kg"; }
            else { weightUnit = "lb"; }
            lbl_normalWeight.Content = $"Normal weight should be between {calculator.GetNormalWeights(isMetric, true),2:F} and {calculator.GetNormalWeights(isMetric, false),2:F} {weightUnit}.";

            // Run Toggle method for BMR-button, to enable it if all BMR-info are entered.
            allowBMR = true;
            ToggleBMRButton();
            lbx_BMR_results.ItemsSource = null;
        }


        /// <summary>
        /// Listen for name change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NameTbxChanged(object sender, TextChangedEventArgs e)
        {
            allowBMR = false;
            ToggleBMIButton();

            // When new info is entered, don't allow BMR-calculations until BMI-button is clicked.
            btn_calculateBMR.IsEnabled = false;
        }


        /// <summary>
        /// Listen for height change (feet).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HeightTbxChanged(object sender, TextChangedEventArgs e)
        {
            allowBMR = false;
            ToggleBMIButton();
            if (tbx_height.Text == "")
            {
                lbl_BMI.Content = "";
                lbl_weightCategory.Content = "";
            }

            // When new info is entered, don't allow BMR-calculations until BMI-button is clicked.
            btn_calculateBMR.IsEnabled = false;
        }

        /// <summary>
        ///  Listen for height change (inch).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Height2TbxChanged(object sender, TextChangedEventArgs e)
        {
            allowBMR = false;
            ToggleBMIButton();
            if (tbx_height2.Text == "")
            {
                lbl_BMI.Content = "";
                lbl_weightCategory.Content = "";
            }

            // When new info is entered, don't allow BMR-calculations until BMI-button is clicked.
            btn_calculateBMR.IsEnabled = false;
        }


        /// <summary>
        /// Listen for weight change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeightTbxChanged(object sender, TextChangedEventArgs e)
        {
            allowBMR = false;
            ToggleBMIButton();
            if (tbx_weight.Text == "")
            {
                lbl_BMI.Content = "";
                lbl_weightCategory.Content = "";
            }

            // When new info is entered, don't allow BMR-calculations until BMI-button is clicked.
            btn_calculateBMR.IsEnabled = false;
        }


        /// <summary>
        /// Listen for unit change (imperial)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtn_Imperial_Click(object sender, RoutedEventArgs e)
        {
            isMetric = false;
            tbx_height.IsEnabled = true;
            if (rbtn_imperial.IsChecked == true)
            {
                lbl_height.Content = "Height (ft and in)";
                lbl_weight.Content = "Weight (lbs)";
            }
            tbx_height.Text = "";
            tbx_weight.Text = "";

            // When new info is entered, don't allow BMR-calculations until BMI-button is clicked.
            btn_calculateBMR.IsEnabled = false;
        }


        /// <summary>
        /// Listen for unit change (metric)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtn_Metric_Click(object sender, RoutedEventArgs e)
        {
            isMetric = true;
            tbx_height.IsEnabled = false;
            if (rbtn_metric.IsChecked == true)
            {
                lbl_height.Content = "Height (cm)";
                lbl_weight.Content = "Weight (kg)";
            }

            // When new info is entered, don't allow BMR-calculations until BMI-button is clicked.
            btn_calculateBMR.IsEnabled = false;
        }


        /// <summary>
        /// Send name textbox content to BMI class
        /// </summary>
        private void SetName()
        {
            calculator.Name = tbx_name.Text;
        }


        /// <summary>
        /// Send height textbox content to BMI class
        /// </summary>
        private void SetHeight()
        {
            if (isMetric)
                calculator.Height = Convert.ToDouble(tbx_height2.Text);
            else
                calculator.Height = (FOOT_TO_INCH * Convert.ToDouble(tbx_height.Text)) + Convert.ToDouble(tbx_height2.Text);            
        }


        /// <summary>
        /// Send weight textbox content to BMI class
        /// </summary>
        private void SetWeight()
        {
            calculator.Weight = Convert.ToDouble(tbx_weight.Text);
        }


        /// <summary>
        /// Check if all required BMI info is entered
        /// </summary>
        /// <returns></returns>
        private bool IsAllBmiInfoEntered()
        {
            if (tbx_name.Text == "")
                return false;
            if (tbx_weight.Text == "" || !double.TryParse(tbx_weight.Text, out double weight))
                return false;
            if (!isMetric && (tbx_height.Text == "" || !double.TryParse(tbx_height.Text, out double height)))
                return false;
            if (tbx_height2.Text == "" || !double.TryParse(tbx_height2.Text, out double height2))
                return false;
            return true;
        }

        private void ToggleBMIButton()
        {
            if (IsAllBmiInfoEntered())
                btn_calculateBMI.IsEnabled = true;
            else btn_calculateBMI.IsEnabled = false;
        }

        
        /* BMR calculator */

        /// <summary>
        /// Defines actions for click in Calculate BMR -button.
        /// Call the calculator method and set the output listbox content.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_CalculateBMR_Click(object sender, RoutedEventArgs e)
        {
            // Set age
            calculator.SetAge(Convert.ToInt32(tbx_age.Text));
            lbx_BMR_results.ItemsSource = null;
            lbx_BMR_results.ItemsSource = calculator.GetBMRStringOutput(isMetric); 
        }


        /// <summary>
        /// Listen for gender - female selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtn_female_Click(object sender, RoutedEventArgs e)
        {
            if (rbtn_female.IsChecked == true)
                calculator.SetIsMale(false);
        }


        /// <summary>
        /// Listen for gender - male selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtn_male_Click(object sender, RoutedEventArgs e)
        {
            if (rbtn_male.IsChecked == true)
                calculator.SetIsMale(true);
        }


        /// <summary>
        /// Listen for age textbox change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AgeChanged(object sender, TextChangedEventArgs e)
        {
            ToggleBMRButton();
        }


        /// <summary>
        /// Listen for activity selection (sedetary).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtn_sedentary_Click(object sender, RoutedEventArgs e)
        {
            calculator.SetActivityLevel(1.2);
        }


        /// <summary>
        /// Listen for activity selection (lightly).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtn_lightly_Click(object sender, RoutedEventArgs e)
        {
            calculator.SetActivityLevel(1.375);
        }


        /// <summary>
        /// Listen for activity selection (moderately).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtn_moderately_Click(object sender, RoutedEventArgs e)
        {
            calculator.SetActivityLevel(1.550);
        }


        /// <summary>
        /// Listen for activity selection (very).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtn_very_Click(object sender, RoutedEventArgs e)
        {
            calculator.SetActivityLevel(1.725);
        }


        /// <summary>
        /// Listen for activity selection (extra).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtn_extra_Click(object sender, RoutedEventArgs e)
        {
            calculator.SetActivityLevel(1.9);
        }


        private void ToggleBMRButton()
        {
            if (tbx_age.Text == "" || !Int32.TryParse(tbx_age.Text, out int age) || age <= 0)
                btn_calculateBMR.IsEnabled = false;
            else if (allowBMR)
                btn_calculateBMR.IsEnabled = true;
        }
        


        /* SAVINGS CALCULATOR */
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
            lbl_totalFees.Content = $"{savingsCalculator.GetTotalFees(),2:F}";

        }


        /// <summary>
        /// Listen for initial deposit textbox change and toggle calculate button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InitialChanged(object sender, TextChangedEventArgs e)
        {
            ToggleCalculateSavingsButton();
        }


        /// <summary>
        /// Listen for monthly deposit textbox change and toggle calculate button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MonthlyChanged(object sender, TextChangedEventArgs e)
        {
            ToggleCalculateSavingsButton();
        }


        /// <summary>
        /// Listen for period textbox change and toggle calculate button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PeriodChanged(object sender, TextChangedEventArgs e)
        {
            ToggleCalculateSavingsButton();
        }


        /// <summary>
        /// Listen for growth (interest) textbox change and toggle calculate button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrowthChanged(object sender, TextChangedEventArgs e)
        {
            ToggleCalculateSavingsButton();
        }


        /// <summary>
        /// Listen for fees textbox change and toggle calculate button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FeesChanged(object sender, TextChangedEventArgs e)
        {
            ToggleCalculateSavingsButton();
        }


        /// <summary>
        /// Check if all required BMI input info is entered and valid.
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Toggle the BMI-calculate button by calling the method to check for valid input.
        /// </summary>
        private void ToggleCalculateSavingsButton()
        {
            if (IsAllInputValid())
                btn_calculateSavings.IsEnabled = true;
            else btn_calculateSavings.IsEnabled = false;
        }
    }
}
