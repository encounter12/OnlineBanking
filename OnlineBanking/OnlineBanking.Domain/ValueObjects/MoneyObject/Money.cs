using System;
using System.Text;

namespace OnlineBanking.Domain.ValueObjects.MoneyObject
{
    public readonly struct Money : IEquatable<Money>, IComparable<Money>, IComparable
    {
        public Money(decimal moneyValue, CurrencyCode currency)
        {
            if (moneyValue < 0M)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(moneyValue), moneyValue, "MoneyValue should be equal or greater than zero");
            }
            
            MoneyValue = moneyValue;
            Currency = currency;
        }

        public decimal MoneyValue { get; init; }

        public CurrencyCode Currency { get; init; }
        
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(nameof(Money));
            stringBuilder.Append(" { ");

            PrintMembers(stringBuilder);

            stringBuilder.Append(" }");
            return stringBuilder.ToString();
        }

        private void PrintMembers(StringBuilder builder)
        {
            builder.Append(nameof(MoneyValue));
            builder.Append(" = ");
            builder.Append(MoneyValue.ToString("F"));

            builder.Append(", ");

            builder.Append(nameof(Currency));
            builder.Append(" = ");
            builder.Append(Currency.ToString());
        }

        public string ToString(MoneyFormattingType formattingType)
        {
            var moneyString = formattingType switch
            {
                MoneyFormattingType.MoneyValueCurrencyCode => $"{MoneyValue} {Currency}",
                MoneyFormattingType.CurrencyCodeMoneyValue => $"{Currency} {MoneyValue}",
                _ => string.Empty
            };

            return moneyString;
        }

        public bool Equals(Money other)
            => Currency == other.Currency && MoneyValue == other.MoneyValue;

        public override bool Equals(object? other)
        {
            var otherMoney = other as Money?;
            return otherMoney.HasValue && Equals(otherMoney.Value);
        }

        public override int GetHashCode()
            => HashCode.Combine(MoneyValue, Currency);

        public int CompareTo(Money other)
        {
            if (Currency != other.Currency)
            {
                throw new InvalidOperationException("Cannot compare money of different currencies");
            }

            return Equals(other) ? 0 : MoneyValue.CompareTo(other.MoneyValue);
        }

        public int CompareTo(object? other)
        {
            if (!(other is Money))
            {
                throw new InvalidOperationException("CompareTo() argument is not Money");
            }

            return CompareTo((Money) other);
        }

        public static bool operator ==(Money? m1, Money? m2)
            => m1.HasValue && m2.HasValue ? m1.Equals(m2) : !m1.HasValue && !m2.HasValue;

        public static bool operator !=(Money? m1, Money? m2)
            => m1.HasValue && m2.HasValue ? !m1.Equals(m2) : m1.HasValue ^ m2.HasValue;
        
        public static bool operator ==(Money? m1, decimal m2Value)
            => m1?.MoneyValue.Equals(m2Value) ?? false;
        
        public static bool operator !=(Money? m1, decimal m2Value)
            => !m1?.MoneyValue.Equals(m2Value) ?? false;
        
        public static bool operator ==(decimal m1Value, Money? m2)
            => m2?.MoneyValue.Equals(m1Value) ?? false;
        
        public static bool operator !=(decimal m1Value, Money? m2)
            => !m2?.MoneyValue.Equals(m1Value) ?? false;

        public static bool operator <(Money m1, Money m2)
            => m1.CompareTo(m2) < 0;

        public static bool operator >(Money m1, Money m2)
            => m1.CompareTo(m2) > 0;
        
        public static bool operator <(Money m1, decimal m2Value)
            => m1.MoneyValue.CompareTo(m2Value) < 0;
        
        public static bool operator >(Money m1, decimal m2Value)
            => m1.MoneyValue.CompareTo(m2Value) > 0;
        
        public static bool operator <(decimal m1Value, Money m2)
            => m1Value.CompareTo(m2.MoneyValue) < 0;
        
        public static bool operator >(decimal m1Value, Money m2)
            => m1Value.CompareTo(m2.MoneyValue) > 0;

        public static bool operator <=(Money m1, Money m2)
            => m1 == m2 || m1 < m2;

        public static bool operator >=(Money m1, Money m2)
            => m1 == m2 || m1 > m2;
        
        public static bool operator <=(Money m1, decimal m2Value)
            => m1.MoneyValue == m2Value || m1.MoneyValue < m2Value;
        
        public static bool operator >=(Money m1, decimal m2Value)
            => m1.MoneyValue == m2Value || m1.MoneyValue > m2Value;
        
        public static bool operator <=(decimal m1Value, Money m2)
            => m1Value == m2.MoneyValue || m1Value < m2.MoneyValue;
        
        public static bool operator >=(decimal m1Value, Money m2)
            => m1Value == m2.MoneyValue || m1Value > m2.MoneyValue;

        public static Money operator +(Money m1, Money m2)
        {
            if (m1.Currency != m2.Currency)
            {
                throw new InvalidOperationException("Cannot add money having different currencies");
            }

            return new Money(m1.MoneyValue + m2.MoneyValue, m1.Currency);
        }

        public static Money operator +(Money m1, decimal m2Value) 
            => new(m1.MoneyValue + m2Value, m1.Currency);

        public static Money operator +(decimal m1Value, Money m2) 
            => new(m1Value + m2.MoneyValue, m2.Currency);

        public static Money operator -(Money m1, Money m2)
        {
            if (m1.Currency != m2.Currency)
            {
                throw new InvalidOperationException("Cannot subtract money having different currencies");
            }

            return new Money(m1.MoneyValue - m2.MoneyValue, m1.Currency);
        }

        public static Money operator -(Money m1, decimal m2Value)
            => new(m1.MoneyValue - m2Value, m1.Currency);

        public static Money operator -(decimal m1Value, Money m2)
            => new(m1Value - m2.MoneyValue, m2.Currency);

        public static Money operator *(Money m1, Money m2)
        {
            if (m1.Currency != m2.Currency)
            {
                throw new InvalidOperationException("Cannot multiply money having different currencies");
            }

            return new Money(m1.MoneyValue * m2.MoneyValue, m1.Currency);
        }

        public static Money operator *(Money m1, decimal m2Value)
            => new(m1.MoneyValue * m2Value, m1.Currency);

        public static Money operator *(decimal m1Value, Money m2)
            => new(m1Value * m2.MoneyValue, m2.Currency);

        public static Money operator /(Money m1, Money m2)
        {
            if (m1.Currency != m2.Currency)
            {
                throw new InvalidOperationException("Cannot divide money having different currencies");
            }

            return new Money(m1.MoneyValue / m2.MoneyValue, m1.Currency);
        }

        public static Money operator /(Money m1, decimal m2Value)
            => new(m1.MoneyValue / m2Value, m1.Currency);

        public static Money operator /(decimal m1Value, Money m2)
            => new(m1Value / m2.MoneyValue, m2.Currency);

        public static Money operator %(Money m, int divisor)
            => new(m.MoneyValue % divisor, m.Currency);
        
        public static Money operator +(Money m)
            => new(m.MoneyValue, m.Currency);
        
        public static Money operator -(Money m)
            => new(-m.MoneyValue, m.Currency);

        public static Money operator ++(Money m)
            => new(m.MoneyValue + 1M, m.Currency);

        public static Money operator --(Money m)
            => new(m.MoneyValue - 1M, m.Currency);

        public static explicit operator decimal(Money m) => m.MoneyValue;
    }
}