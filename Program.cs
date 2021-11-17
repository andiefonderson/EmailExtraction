using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace EmailExtraction
{
    internal class Program
    {
        static string path = @"C:\Users\Andrea.Fonderson\OneDrive - IRIS Software Group\Corndel Bootcamp\C Sharp Bootcamp\1 - Email Extraction\ExtractionText.txt";
        static string textFile = File.ReadAllText(path);

        static void Main(string[] args)
        {
            DomainCountLookup();
        }

        static void EmailLookup()
        {
            string emailPattern = @"([\w\.\-])+(@)([\w\.\-]+)";

            Regex emailRG = new Regex(emailPattern);
            MatchCollection emailMatches = emailRG.Matches(textFile);

            foreach (Match match in emailMatches)
            {
                Console.WriteLine(match);
            }
        }

        static void DomainCountLookup()
        {
            string domainPattern = @"(@)([\w\.\-]+)";

            Regex domainRG = new Regex(domainPattern);
            MatchCollection domainMatches = domainRG.Matches(textFile);

            Dictionary<string, int> domainDictionary = new Dictionary<string, int>();

            foreach (Match match in domainMatches)
            {
                if (!domainDictionary.ContainsKey(match.ToString()))
                {
                    domainDictionary.Add(match.ToString(), 1);
                }

                else
                {
                    domainDictionary[match.ToString()]++;
                }
            }

            ReturnTopDomainResults(domainDictionary, 10);

            Console.WriteLine("\nEnter a frequency (e.g. 63) to returns the domains that occur more than that number of times");
            ReturnInputNumberOfDomainsOrMore(domainDictionary,int.Parse(Console.ReadLine()));

            Console.WriteLine($"\nNumber of Softwire.com domain email addresses: {domainDictionary["@softwire.com"]}");
        }

        private static void ReturnTopDomainResults(Dictionary<string, int> domainDictionary, int filterNumber)
        {
            foreach (var domain in domainDictionary.OrderByDescending(key => key.Value).Take(filterNumber))
            {
                Console.WriteLine($"{domain.Key.Substring(1)}: {domain.Value}");
            }
        }

        private static void ReturnInputNumberOfDomainsOrMore(Dictionary<string, int> domainDictionary, int filterNumber)
        {
            foreach (var domain in domainDictionary.OrderByDescending(key => key.Value))
            {
                if(domain.Value >= filterNumber)
                {
                    Console.WriteLine($"{domain.Key.Substring(1)}: {domain.Value}");
                }
                else
                {
                    break;
                }
                
            }
        }
    }
}
