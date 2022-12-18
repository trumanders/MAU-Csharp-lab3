using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MAU_Csharp_lab3
{
    class SavingsCalculator
    {
        const int MONTHS_IN_A_YEAR = 12;
        private decimal initialDeposit;
        private decimal monthlyDeposit;
        private int periodInYears;
        private decimal monthlyInterest;
        private decimal totalFee;
        private decimal fee;

        private decimal finalBalance = 0;

        public void SetAllInputVariables(string initial, string monthly, string period, string interest, string fees)
        {
            this.initialDeposit = Convert.ToDecimal(initial);
            this.monthlyDeposit = Convert.ToDecimal(monthly);
            this.periodInYears = Convert.ToInt32(period);
            this.monthlyInterest = Convert.ToDecimal(interest) / 100m / 12;
            this.fee = Convert.ToDecimal(fees) / 100m;
        }

        public decimal GetFinalBalance()
        {
            finalBalance = initialDeposit;
            decimal mDeposit;
  
            for (int i = 0; i <= periodInYears * 12; i++)
            {
                if (i == 0) mDeposit = initialDeposit;
                else mDeposit = monthlyDeposit;

                if (i % MONTHS_IN_A_YEAR == 0)
                {
                    decimal expense = finalBalance * fee;
                    finalBalance -= expense;
                    totalFee += expense;
                }
                decimal interestEarned = monthlyInterest * finalBalance;
                finalBalance += interestEarned + mDeposit;
            }      
            return finalBalance;
        }

        public decimal GetAmountEarned()
        {
            return finalBalance - GetAmountPaid();
        }     

        public decimal GetAmountPaid()
        {
            return monthlyDeposit * periodInYears * MONTHS_IN_A_YEAR;
        }

        public decimal GetTotalFees()
        {
            return totalFee;
        }
    }
}
