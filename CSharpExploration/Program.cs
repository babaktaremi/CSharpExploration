using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CSharpExploration
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Class Approach
            BookClass b1 = new BookClass("Test Book", "Bob",200);
            BookClass b2 = new BookClass("Test Book 2", "Bob",300);

            var json = JsonSerializer.Serialize(b1);

            var equals = b1 == b2;

            Console.WriteLine($"is Equal?{equals}");
            Console.WriteLine(json);

            var b3 = JsonSerializer.Deserialize<BookClass>(json);

            var secondEquals = b3 == b1;

            Console.WriteLine($"is second Equal?{secondEquals}");

            (string bookName, string authorName) = b1;

            #endregion

            #region Record Approach

            var b4 = new BookRecord("Test Book 3", "Bob",300);
            var b5 = new BookRecord("Test Book 4", "Bob",100);

            var json2 = JsonSerializer.Serialize(b4);

            Console.WriteLine(json2);

            var b6 = JsonSerializer.Deserialize<BookRecord>(json2);

            var seprateRecordEquality = b4 == b5; //False

            var recordEquality = b4 == b6; //True

            var b7 = b4;

            b4.AuthorName = "Peter";

            var sameRecordChangeCheck = b7 == b4; //True
            

            b4 = b4 with { AuthorName = "Peter" }; //Record immutability ! A New Record is 

            (string authorNameTuple, string bookNameTuple,int numberOfPages) = b4;  // Test Book 3 , Peter, 300

            var immutabilityEquality = b6 == b4; //False

            Console.WriteLine(b4.ToString()); // Test Book 3 , Peter

            Console.WriteLine($"The Price of b4 is {BookPriceCalculator.Calculate(b4)}"); // 50.0m
            Console.WriteLine($"The Price of b4 is {BookPriceCalculator.Calculate(b5)}"); // 100.0m

            #endregion

        }
    }


    public class BookClass : IEquatable<BookClass> // Ordinary Way Without Records
    {
       
        
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public int NumberOfPages { get; set; }

        [JsonConstructor]
        public BookClass(string bookName, string authorName,int numberOfPages)
        {
            BookName = bookName;
            AuthorName = authorName;
            NumberOfPages = numberOfPages;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BookName, AuthorName);
        }

        public override bool Equals(object? obj)
        {
            return obj is BookClass b && Equals(b);
        }

        public static bool operator ==(BookClass left, BookClass right) => left is object && left.Equals(right);

        public static bool operator !=(BookClass left, BookClass right) => !(left == right);

        public bool Equals(BookClass other) => other is BookClass && BookName.Equals(other.BookName) &&
                                               AuthorName.Equals(other.AuthorName) && NumberOfPages.Equals(other.NumberOfPages);

        public void Deconstruct(out string bookName, out string authorName)
        {

            bookName = this.BookName;
            authorName = this.AuthorName;
        }
    }

    public record BookRecord(string BookName, string AuthorName,int NumberOfPages) //Records... New C# 9 Feature
    {
        public string BookName { get; set; } = BookName;
        public string AuthorName { get; set; } = AuthorName;
        public int NumberOfPages { get; set; } = NumberOfPages;

        public override string ToString() => $"{BookName} , {AuthorName}";

    }
}
