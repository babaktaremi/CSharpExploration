using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpExploration
{
    class BookPriceCalculator
    {
      internal  static decimal Calculate(BookRecord book) =>
            book switch
            {
                BookRecord b when (b.NumberOfPages >= 300 && b.NumberOfPages < 400) => 50.0m,
                BookRecord b when (b.NumberOfPages >= 400 && b.NumberOfPages < 500) => 60.0m,
                BookRecord b when (b.NumberOfPages >= 500 && b.NumberOfPages < 600) => 70.0m,
                { } => 100.0m
            };
    }
}
