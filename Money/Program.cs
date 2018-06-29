using System;
using System.Linq;

namespace Money
{
	public class Program
	{
		public static void Main()
		{
			// test 1
			var m = new Money(100, Currencies.RUR);
			var x = 200;

			Console.WriteLine($"{m} + {x} = {m + x}"); // 300

			//test 2
			m = new Money(100, Currencies.USD);
			x = 1000;

			Console.WriteLine($"{m} + {x} = {Sum(m, x)}"); // 1100

			// test 3
			var m1 = new Money(100, Currencies.RUR);
			var m2 = new Money(200, Currencies.USD);
			x = 1000;

			Console.WriteLine($"{m1} + {m2} + {x} = {m1 + m2 + x}");
			// переведет $200 (m2) в рубли (допустим, по курсу 60 получим 12000 р), сложит их со 100 рублями, после чего прибавит к ним 1000
			// результат: 13100

			Console.WriteLine($"{x} + {m1} + {m2} = {x + m1 + m2}");
			// сложит все в виде чисел
			// результат: 1300

			Console.WriteLine($"{x} + ({m1} + {m2}) = {x + (m1 + m2)}");
			// результат: 13100

			Console.ReadLine();
		}

		private static decimal Sum(params decimal[] x) => x.Sum();
	}

	public enum Currencies
	{
		RUR,
		USD,
		EUR
	}

	public class Money
	{
		public decimal Amount { get; }
		public Currencies Currency { get; }

		public Money(decimal amount, Currencies currency)
		{
			Amount = amount;
			Currency = currency;
		}

		public Money ConvertTo(Currencies newCurrency)
		{
			var newAmount = (Currency == newCurrency)
				? Amount
				: Amount.Convert(Currency, newCurrency);

			return new Money(newAmount, newCurrency);
		}

		public static implicit operator decimal(Money m) => m.Amount;

		public static implicit operator int(Money m) => (int)m.Amount;

		public static Money operator +(Money m1, Money m2)
		{
			var conv = m2.ConvertTo(m1.Currency);
			return new Money(m1.Amount + conv.Amount, m1.Currency);
		}

		public override string ToString() => $"{Amount} {Currency}";
	}

	public static class Extensions
	{
		public static decimal Convert(this decimal amount, Currencies from, Currencies to) =>
			(decimal)((double)amount * from.GetCourseTo(to));

		public static double GetCourseTo(this Currencies from, Currencies to)
		{
			if (from == to) return 1;

			// placeholder
			if (from == Currencies.USD && to == Currencies.RUR)
			{
				return 60;
			}

			throw new Exception($"No course set for conversion from {from} to {to}.");
		}
	}
}
