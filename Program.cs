using MoneyExample;

Money m1 = new(Currency.RUR, 100);
Money m2 = new(Currency.USD, 200);
int x = 1000;

// converts number to money
Console.WriteLine($"{m1} + {x} = {m1 + x} (expected: 1100 RUR)");

// converts money to number
Console.WriteLine($"{x} + {m1} = {x + m1} (expected: 1100)");

// converts RUB to USD
Console.WriteLine($"{m2} + {m1} = {m2 + m1} (expected: 201.11 USD)");

// everything is converted to the first currency
// converts $200 (m2) to RUR, adds 100 RUR, after that adds another 1000
Console.WriteLine($"{m1} + {m2} + {x} = {m1 + m2 + x} (expected: 19152 RUR)");

// everything is treated as numbers
Console.WriteLine($"{x} + {m1} + {m2} = {x + m1 + m2} (expected: 1300)");

// converts USD to RUR, then adds RUR to 1000 and gives a number
Console.WriteLine($"{x} + ({m1} + {m2}) = {x + (m1 + m2)} (expected: 19152)");

// converts RUB to USD, then adds USD to 1000 and gives a number
// note that 201.11 USD is converted to int and becomes 201 (!)
Console.WriteLine($"{x} + ({m2} + {m1}) = {x + (m2 + m1)} (expected: 1201)");

// additional test 1

// Money m = new(Currency.RUR, 100);
// int y = 200;
// var sumMoney = m + y; // gives `300 RUR`
// var sumInt = y + m; // gives (int)300
// Console.WriteLine($"money: {sumMoney}, int: {sumInt}");

// additional test 2

// int TotalSum(int partOne, int partTwo) => partOne + partTwo;

// Money m = new(Currency.USD, 100);
// var total1 = TotalSum(m, 1000); // (int)1100
// var total2 = TotalSum(1000, m); // (int)1100

// Console.WriteLine($"total 1: {total1}, total 2: {total2}");
