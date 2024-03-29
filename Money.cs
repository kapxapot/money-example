namespace MoneyExample;

public class Money(Currency currency, decimal amount)
{
    public Currency Currency { get; } = currency;
    public decimal Amount { get; } = Math.Round(amount, 2, MidpointRounding.AwayFromZero);

    public static implicit operator decimal(Money money) => money.Amount;

    public static implicit operator int(Money money) => (int)money.Amount;

    public static Money operator +(Money money1, Money money2)
    {
        var converted = money2.ConvertTo(money1.Currency);
        return money1 + converted.Amount;
    }

    public static Money operator +(Money money, int amount) => money + (decimal)amount;

    public static Money operator +(Money money, decimal amount) =>
        new(money.Currency, money.Amount + amount);

    public Money ConvertTo(Currency newCurrency)
    {
        var newAmount = Amount.Convert(Currency, newCurrency);
        return new(newCurrency, newAmount);
    }

    public override string ToString() => $"{Amount} {Currency}";
}
