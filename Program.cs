using System;
using System.Security.Principal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace test
{
    class BankAccount
    {
        public int accountNumber { get; set; }
        public string ownerName { get; set; }
        public decimal Balance { get; set; }

        public BankAccount(int AccountNumber, string OwnerName, decimal balance)
        {
            accountNumber = AccountNumber;
            ownerName = OwnerName;
            Balance = balance;
        }
        public void MakeDeposit(decimal amount)
        {
            if (amount > 0)
            {
                Balance += amount;
            }

            Console.WriteLine("Deposited {amount} , New balance: { Balance}");
        }

        public void WithdrawAmount(decimal amount)
        {
            if (amount > 0 && amount < Balance)
            {
                Balance -= amount;
            }

            if (amount > Balance)
            {
                Console.WriteLine("cannot withdraw amount more than your Balance");
            }

            Console.WriteLine("withdraw {amount}, New Balance: {Balance}");
        }

    }

    class SavingsAccount : BankAccount
    {
        public decimal InterestRate { get; set; }

        public SavingsAccount(int accountNumber, string owner, decimal balance,decimal interestRate)
            : base(accountNumber, owner, balance)
        {
            InterestRate = interestRate;
        }
        public void ApplyInterest()
        {
            decimal Interest = Balance * (InterestRate/100);
            Balance += Interest;
            Console.WriteLine("Apply Interest with {InterestRate} rate, NEW BALANCE {Balance}");
        }
        
        
    }

    class Bank
    {
        public List<BankAccount> Accounts { get; set; }

        public Bank()
        {
            Accounts = new List<BankAccount>();
        }
        
        public void AddAccount (BankAccount accountToAdd)
        {
            if (accountToAdd != null)
            {
                Accounts.Add(accountToAdd);
            }
        }


        public BankAccount FindAccount (int accountNumber) 
        { 
            foreach (var account in Accounts)
            {
                if (account.accountNumber == accountNumber)
                {
                    return account;
                }
                    
            }
            return null;
        }

        public bool Transfer(BankAccount fromAccount, BankAccount toAccount, decimal amount)
        {
            if (!Accounts.Contains(fromAccount) || !Accounts.Contains(toAccount))
            {
                return false;
            }

            if(fromAccount.Balance < amount)
            {
                return false;
            }

            fromAccount.Balance -= amount;
            toAccount.Balance += amount;
            return true;
        }
        
        public void PrintAllAccounts()
        {
            foreach( var account in Accounts)
            {
                Console.WriteLine(account);
                
            }
        }


    }

    class Program
    {
        static void Main(string[] args)
        {
            BankAccount firstAccount = new BankAccount(1, "Bayan Zamil", 25000);
            BankAccount secondAccount = new BankAccount(2, "Karam Kabha", 50000);
            SavingsAccount firstSAccount = new SavingsAccount(3, "Braah Atamne", 10000, 2);
            Bank myBank = new Bank();

            myBank.AddAccount(firstAccount);
            myBank.AddAccount(secondAccount);
            myBank.AddAccount(firstSAccount);
            
            Console.WriteLine("Initial Accounts:");
            myBank.PrintAllAccounts();

            firstAccount.MakeDeposit(5000);
            firstSAccount.WithdrawAmount(2000);

            myBank.Transfer(secondAccount, firstAccount, 5000);
            firstSAccount.ApplyInterest();

            Console.WriteLine("Updated Accounts:");
            myBank.PrintAllAccounts();
        }
    }
}