using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MAU_Csharp_lab3
{
    /// <summary>
    /// This class handles the savings calculation based on the
    /// user input from MainWindow.
    /// </summary>
    class SavingsCalculator
    {
        const int MONTHS_IN_A_YEAR = 12;
        private decimal initialDeposit;     // Existing money (deposit on the first month)
        private decimal monthlyDeposit;
        private int periodInYears;
        private decimal monthlyInterest;
        private decimal totalFee = 0;
        private decimal fee;

        private decimal finalBalance = 0;

        /// <summary>
        /// Set the instance variables sent from the UI.
        /// </summary>
        /// <param name="initial">The initial savings amount (deposit)</param>
        /// <param name="monthly">The monthly deposit amount</param>
        /// <param name="period">The amount of years to calculate savings on</param>
        /// <param name="interest">The yearly interest in percent</param>
        /// <param name="fees">The fee yearly interest</param>
        public void SetAllInputVariables(string initial, string monthly, string period, string interest, string fees)
        {
            this.initialDeposit = Convert.ToDecimal(initial);
            this.monthlyDeposit = Convert.ToDecimal(monthly);
            this.periodInYears = Convert.ToInt32(period);
            this.monthlyInterest = Convert.ToDecimal(interest) / 100m / MONTHS_IN_A_YEAR;
            this.fee = Convert.ToDecimal(fees) / 100m;
        }


        /// <summary>
        /// Calculate the final balance based on initialDeposit, monthlyDeposit, 
        /// fees, and amount of years.
        /// </summary>
        /// <returns>The final balance after the given amount of years.</returns>
        public decimal GetFinalBalance()
        {
            finalBalance = initialDeposit;
            decimal mDeposit;

            for (int i = 0; i <= periodInYears * 12; i++)
            {
                if (i == 0) mDeposit = initialDeposit;
                else mDeposit = monthlyDeposit;

                try
                {
                    if (i % MONTHS_IN_A_YEAR == 0)
                    {
                        decimal expense = finalBalance * fee;
                        finalBalance -= expense;
                        totalFee += expense;
                    }

                    decimal interestEarned = monthlyInterest * finalBalance;
                    finalBalance += interestEarned + mDeposit;
                }
                catch (OverflowException ex)
                {
                    MessageBox.Show("" + ex);
                    finalBalance = 0;
                    totalFee = 0;
                    monthlyDeposit = 0;
                    monthlyInterest = 0;
                    return 0;
                }

            }
            return finalBalance;
        }


        /// <summary>
        /// Takes final balance and subtracts the amount deposited over the savings
        /// period.
        /// </summary>
        /// <returns>The amount earned from the savings.</returns>
        public decimal GetAmountEarned()
        {
            return finalBalance - GetAmountPaid();
        }


        /// <summary>
        /// Takes the monthly deposit and multiplies with the amount of months of the period.
        /// </summary>
        /// <returns>The total amount deposited over the period of years.</returns>
        public decimal GetAmountPaid()
        {
            return monthlyDeposit * periodInYears * MONTHS_IN_A_YEAR;
        }


        /// <summary>
        /// Return the total amount of fees calculated in the GetFinalBalance method.
        /// </summary>
        /// <returns>The total amount of fees.</returns>
        public decimal GetTotalFees()
        {
            return totalFee;
        }
    }
}
