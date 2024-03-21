namespace MoneyExample;

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
