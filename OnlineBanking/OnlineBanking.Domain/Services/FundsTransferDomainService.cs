using System;
using System.Collections.Generic;
using System.Linq;
using OnlineBanking.Domain.Aggregates;
using OnlineBanking.Domain.Builders;
using OnlineBanking.Domain.ValueObjects.MoneyObject;
using OnlineBanking.Infrastructure;

namespace OnlineBanking.Domain.Services
{
    public class FundsTransferDomainService
    {
        private readonly List<BankAccount> _bankAccounts;

        public FundsTransferDomainService()
        {
            _bankAccounts = BankAccountsBuilder.GetBankAccounts();
        }
        
        public void TransferFunds(
            int sourceBankAccountId,
            int destinationBankAccountId,
            decimal amount)
        {
            BankAccount sourceBankAccount = _bankAccounts.Single(ba => ba.Id == sourceBankAccountId);
            BankAccount destBankAccount = _bankAccounts.Single(ba => ba.Id == destinationBankAccountId);

            var money = new Money(amount, sourceBankAccount.Currency);
            
            Notification note = sourceBankAccount.Withdraw(money);

            if (note.HasErrors)
            {
                return;
            }
            
            destBankAccount.Deposit(money);
        }
    }
}