using System;
using System.Collections.ObjectModel;

namespace OnlineBanking.Domain.MoneyObject
{
    public class MoneyCollection : Collection<Money>
    {
        private readonly CurrencyCode _currencyCode;

        public MoneyCollection(CurrencyCode currencyCode)
        {
            _currencyCode = currencyCode;
        }
        
        protected override void InsertItem (int index, Money item)
        {
            Validate(item);
            base.InsertItem (index, item);
        }
        
        protected override void SetItem (int index, Money item)
        {
            Validate(item);
            base.SetItem (index, item);
        }
        
        private void Validate(Money item)
        {
            if (item.Currency != _currencyCode)
            {
                throw new InvalidOperationException(
                    $"Money with currency different than the default: {_currencyCode} can't be added to MoneyCollection");
            }
        }
    }
}