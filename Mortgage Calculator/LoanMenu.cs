using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mortgage_Calculator
{
    internal class LoanMenu
    {
        #region Private & Public Variables
        private string _fullname; //used
        private string _firstName; //used
        private double _income; //used
        private double _downPayment; //used
        private double _loanAmount; //used
        private double _years15or30; //used
        private double _marketValue; //used
        private double _purchasePrice; //used
        private double _loanTotal; //used
        private double _monthlyPayment; //used

        //These can be changed to reflect any updates to bank policy
        private const double downPaymentRate = .10; //used
        private const double closingCosts = 2500; //used
        private double originFee = 0.01; //used
        private const double propertyTax = .125;
        private double insuranceRate = .01; //used
        private const double HOAFee = 1; //used
        private double escrow = .02; //used
        private double moInterestRate = .01; //used
        #endregion

        #region Public Setters
        public string FullName { get => _fullname; set => _fullname = value; }
        public double Income { get => _income; set => _income = value; }
        public double DownPayment { get => _downPayment; set => _downPayment = value; }
        public double LoanAmount { get => _loanAmount; set => _loanAmount = value; }
        public double Years15or30 { get => _years15or30; set => _years15or30 = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public double MarketValue { get => _marketValue; set => _marketValue = value; }
        public double PurchasePrice { get => _purchasePrice; set => _purchasePrice = value; }
        public double LoanTotal { get => _loanTotal; set => _loanTotal = value; }
        public double OriginFee { get => originFee; set => originFee = value; }
        public double MonthlyPayment { get => _monthlyPayment; set => _monthlyPayment = value; }
        #endregion

        public LoanMenu()
        {
            PrettyLines(2);
            LoanPromptTabs(3, "Welcome to the AI Loan Menu");
            PrettyLines(2);
            Trish("My name is Trish and I will be helping you today get a loan for your Forever Home!");
            Trish("To get started I need some information from you.");
            Trish("What is your first and last name?");
            FullName = Console.ReadLine();
            FirstName = FullName.Split(" ")[0];
            Trish($"Great to meet you {FirstName}!");
            Trish("What is the purchase price of your home?");
            User();
            PurchasePrice = double.Parse(Console.ReadLine());
            Trish("What size downpayment are you making today?");
            User();
            DownPayment = double.Parse(Console.ReadLine());
            if (DownPayment / PurchasePrice > downPaymentRate)
            {
                InitiateLoan();
            }
            else
            {
                Trish($"I'm sorry {FirstName} but your downpayment is not greater than 10% of the asking amount so I cannot approve your loan.");
                //Denied();
            }
        }

        #region UI Management
        public void PrettyLines(int n)
        {
            for (int i = 0; i < n; i++)
                Console.WriteLine("************************************************************************************");

        }
        public void LoanPromptTabs(int tabs, string s)
        {
            string formatted = "";
            for (int i = 0; i < tabs; i++)
            {
                formatted += "\t";
            }
            Console.WriteLine(formatted + s);

        }
        public async void Proccessing(int speed)
        {
            if (speed == 0) //slow
            {

                Trish("Let me do some calculations for you.");
                Thread.Sleep(TimeSpan.FromSeconds(1));
                Console.Write(" .");
                Thread.Sleep(TimeSpan.FromSeconds(1));
                Console.Write(" .");
                Thread.Sleep(TimeSpan.FromSeconds(1));
                Console.Write(" .\n");
            }
            else if (speed == 1) //fast no prompt
            {

                Thread.Sleep(TimeSpan.FromSeconds(1));

            }

        }
        public void Trish(string s)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"Trish: {s}");
            Console.ResetColor();
            Proccessing(1);
        }
        public void User()
        {
            Console.Write($"{FirstName}: ");
        }
        #endregion

        #region Loan Management
        public void InitiateLoan()
        {
            Trish("How much money do you need from the bank?");
            User();
            LoanAmount = double.Parse(Console.ReadLine());
            Trish("What is the market value of your Forever Home?");
            User();
            MarketValue = double.Parse(Console.ReadLine());
            Trish("What the purchase price?");
            User();
            PurchasePrice = double.Parse(Console.ReadLine());
            Trish("What is your yearly income?");
            User();
            Income = double.Parse(Console.ReadLine());
            Proccessing(0);
            PrettyLines(2);
            DetermineLoanValue();
            //LoanDetermination();
        }

        public void DetermineLoanValue()
        {
            LoanTotal = PurchasePrice - DownPayment;
            originFee = LoanTotal * originFee;
            LoanTotal = LoanTotal + originFee + closingCosts;
            Trish($"Your total loan amount including our origin fee and closing costs is: {LoanTotal}.");
            Trish("Congratulations we are running a special where your loan will be set at a fixed rate!");
            Trish("Are you looking for a 15 or 30 year loan?");
            User();
            Years15or30 = double.Parse(Console.ReadLine());
            Trish($"Thank you so much {FirstName}. Let us see if your loan will require insurance.");
            Proccessing(0);
            PrettyLines(2);
            LoanInsurance();
        }
        public void LoanEscrow()
        {
            PrettyLines(2);
            Trish("We're almost done!");
            Trish("Let me determine your escrow amount.");
            escrow = MarketValue * escrow;
            Trish($"Your annual escrow amount is: {escrow}");
            GenerateMonthlyPayment();
        }
        public void ApproveDeny()
        {
            PrettyLines(5);
            double monthlyincome = Income / 12;
            double percentageDifference = MonthlyPayment / monthlyincome;
            if (percentageDifference <= .25)
            {
                Trish("Congratulations your loan has been approved!");
            }
            else
            {
                Trish("Loan has been denied");
                //Denied(1);
            }
        }
        public void LoanInsurance()
        {
            double MarketDifference = PurchasePrice - MarketValue;
            double InsuranceCheck = (PurchasePrice / 10) + MarketDifference;
            insuranceRate = LoanTotal * insuranceRate;
            if (DownPayment < InsuranceCheck)
            {
                Trish("Loan insurance is going to be required for this application.");
                Trish($"Your annual insurance amount will be: {insuranceRate}");
                LoanEscrow();
            }
            else
            {
                Trish("Loan insurance is not required for this application.");
                LoanEscrow();
            }

        }
        public void GenerateMonthlyPayment()
        {
            PrettyLines(2);
            Trish("Now that we have gotten all of that out of the way let's get your monthly payment.");
            double MonthlyInterest = moInterestRate / 12;
            double MonthsOfLoan = Years15or30 * 12;
            double baseMonthlyPayment = LoanTotal * ((MonthlyInterest * Math.Pow((1 + MonthlyInterest), MonthsOfLoan)) / (Math.Pow(1 + MonthlyInterest, MonthsOfLoan) - 1));
            MonthlyPayment = baseMonthlyPayment + (escrow / 12) + (insuranceRate / 12) + HOAFee;
            Proccessing(0);
            Trish($"Your monthly payment is {MonthlyPayment}");
            ApproveDeny();
        }
        public void Denied(int stage)  //TODO: Make a deny result that cycles user back to beginning of loan
        {
            if (stage == 0)
            {

            }
            else if (stage == 1)
            {

            }
        }
        #endregion

    }
}