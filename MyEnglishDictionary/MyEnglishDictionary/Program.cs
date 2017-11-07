using System;
using System.Collections.Generic;

namespace MyEnglishDictionary
{
    class Program
    {
        static void Main(string[] args)
        { 
            Dict<string, string> myDictionary = new Dict<string, string>();
            myDictionary.Add("мама", "mother");
            myDictionary.Add("тато", "father");
            myDictionary.Add("сестра", "sister");
            myDictionary.Add("брат", "brother");
            myDictionary.Add("тітка", "aunt");
            myDictionary.Add("дядько", "uncle");

            //You can not Add element with the same key
            //myDictionary.Add("мама", "mom");

            Console.WriteLine("All words in my dictionary");

            foreach (KeyValuePair<string,string> item in myDictionary)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();

            //or

            foreach (var item in myDictionary)
            {
                Console.WriteLine("{0}, {1}",item.Key,item.Value);
            }

            Console.WriteLine();

            Console.WriteLine("Can you translate - мама?");
            if (myDictionary.ContainsKey("мама"))
            {
                Console.WriteLine("Yes");
            }
            else
            {
                Console.WriteLine("No");
            }

            Console.ReadKey();
        }
    }
}
