namespace MoneyExample;

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
}
