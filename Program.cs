using System;

namespace Money;

public class Program
{
    public static void Main()
    {
        var m1 = Currency.RUR.Amount(100);
        var m2 = Currency.USD.Amount(200);
        var x = 1000;

        // converts number to money
        Console.WriteLine($"{m1} + {x} = {m1 + x} (expected: 1100 RUR)");

        // converts money to number
        Console.WriteLine($"{x} + {m1} = {x + m1} (expected: 1100)");

        // converts RUB to USD
        Console.WriteLine($"{m2} + {m1} = {m2 + m1} (expected: 201.11 USD)");

        // everything is converted to the first currency
        // converts $200 (m2) into RUR, adds 100 RUR, after that adds another 1000
        Console.WriteLine($"{m1} + {m2} + {x} = {m1 + m2 + x} (expected: 19152 RUR)");

        // everything is treated as numbers
        Console.WriteLine($"{x} + {m1} + {m2} = {x + m1 + m2} (expected: 1300)");

        // converts USD to RUR, then adds RUR to 1000 and gives a number
        Console.WriteLine($"{x} + ({m1} + {m2}) = {x + (m1 + m2)} (expected: 19152)");
    }
}

public enum Currency
{
    RUR,
    USD,
    EUR
}

public class Money
{
    public decimal Amount { get; }
    public Currency Currency { get; }

    public Money(decimal amount, Currency currency)
    {
        Amount = Math.Round(amount, 2);
        Currency = currency;
    }

    public Money ConvertTo(Currency newCurrency)
    {
        var newAmount = Amount.Convert(Currency, newCurrency);

        return new Money(newAmount, newCurrency);
    }

    public static implicit operator decimal(Money m) => m.Amount;

    public static implicit operator int(Money m) => (int)m.Amount;

    public static Money operator +(Money money, int amount) =>
        money + (decimal)amount;

    public static Money operator +(Money money, decimal amount) =>
        money.Currency.Amount(money.Amount + amount);

    public static Money operator +(Money m1, Money m2)
    {
        var conv = m2.ConvertTo(m1.Currency);
        return new Money(m1.Amount + conv.Amount, m1.Currency);
    }

    public override string ToString() => $"{Amount} {Currency}";
}

public static class Extensions
{
    public static decimal Convert(this decimal amount, Currency from, Currency to) =>
        (decimal)((double)amount * from.GetExchangeRateTo(to));

    public static double GetExchangeRateTo(this Currency from, Currency to)
    {
        if (from == to)
        {
            return 1;
        }

        // placeholder
        if (from == Currency.USD && to == Currency.RUR)
        {
            return 90.26;
        }

        if (from == Currency.EUR && to == Currency.USD)
        {
            return 1.09;
        }

        try
        {
            var reverseRate = to.GetExchangeRateTo(from);

            if (reverseRate != 0)
            {
                return 1 / reverseRate;
            }
        }
        catch
        {
        }

        throw new Exception($"No exchange rate from {from} to {to}.");
    }

    public static Money Amount(this Currency currency, decimal amount) =>
        new Money(amount, currency);
}
