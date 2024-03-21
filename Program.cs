using MoneyExample;

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
