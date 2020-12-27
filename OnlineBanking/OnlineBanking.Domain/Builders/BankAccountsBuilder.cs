using System.Collections.Generic;
using OnlineBanking.Domain.Aggregates;
using OnlineBanking.Domain.Enums;
using OnlineBanking.Domain.ValueObjects.MoneyObject;

namespace OnlineBanking.Domain.Builders
{
    public static class BankAccountsBuilder
    {
        public static List<BankAccount> GetBankAccounts()
        {
            var bankAccounts = new List<BankAccount>()
            {
                new BankAccount(1, AccountType.CashingAccount, new Money(250M, CurrencyCode.USD)),
                new BankAccount(2, AccountType.CashingAccount, new Money(2400M, CurrencyCode.USD)),
                new BankAccount(3, AccountType.CashingAccount, CurrencyCode.USD)
            };

            return bankAccounts;
        }
    }
}