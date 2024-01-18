using System;

namespace Encapsulations
{
    /// <summary>
    /// Main program to demonstrate encapsulation with a BankAccount class.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Entry point of the program.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        static void Main(string[] args)
        {
            // Creating an instance of BankAccount
            BankAccount myAccount = new BankAccount("Dimple Mithiya", 1000);

            // Accessing data through getters
            Console.WriteLine($"Account Holder: {myAccount.GetAccountHolder()}");
            Console.WriteLine($"Balance: {myAccount.GetBalance()}");

            // Performing transactions using methods
            myAccount.Deposit(500);
            myAccount.Withdraw(200);

            // Accessing data after transactions
            Console.WriteLine($"New Balance: {myAccount.GetBalance()}");

            // Wait for a key press before closing the console window
            Console.ReadKey();
        }
    }

    /// <summary>
    /// Class representing a bank account with encapsulated data and methods.
    /// </summary>
    public class BankAccount
    {
        private string accountHolder; // Private data member
        private double balance; // Private data member

        /// <summary>
        /// Constructor to initialize a BankAccount.
        /// </summary>
        /// <param name="accountHolder">The name of the account holder.</param>
        /// <param name="initialBalance">The initial balance of the account.</param>
        public BankAccount(string accountHolder, double initialBalance)
        {
            this.accountHolder = accountHolder;
            this.balance = initialBalance;
        }

        /// <summary>
        /// Getter for accountHolder.
        /// </summary>
        /// <returns>The account holder's name.</returns>
        public string GetAccountHolder() => accountHolder;

        /// <summary>
        /// Getter for balance.
        /// </summary>
        /// <returns>The current balance of the account.</returns>
        public double GetBalance() => balance;

        /// <summary>
        /// Method for deposit transaction.
        /// </summary>
        /// <param name="amount">The amount to deposit.</param>
        public void Deposit(double amount)
        {
            if (amount > 0)
            {
                balance += amount;
                Console.WriteLine($"Deposited: {amount}. New balance: {balance}");
            }
            else
            {
                Console.WriteLine("Invalid deposit amount.");
            }
        }

        /// <summary>
        /// Method for withdrawal transaction.
        /// </summary>
        /// <param name="amount">The amount to withdraw.</param>
        public void Withdraw(double amount)
        {
            if (amount > 0 && amount <= balance)
            {
                balance -= amount;
                Console.WriteLine($"Withdrawn: {amount}. New balance: {balance}");
            }
            else
            {
                Console.WriteLine("Invalid withdrawal amount or insufficient funds.");
            }
        }
    }
}
