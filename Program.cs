

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MJU23v_DTP_T1
{
    public class Language
    {
        public string family, group;
        public string language, area, link;
        public int pop;
        public Language(string line)
        {
            string[] field = line.Split("|");
            family = field[0];
            group = field[1];
            language = field[2];
            pop = (int)double.Parse(field[3], CultureInfo.InvariantCulture);
            area = field[4];
            link = field[5];
        }
        public void Print()
        {
            Console.WriteLine($"Language {language}:");
            Console.Write($"  family: {family}");
            if (group != "")
                Console.Write($">{group}");
            Console.WriteLine($"\n  population: {pop}");
            Console.WriteLine($"  area: {area}");
        }
    }
    public class Program
    {
        static string dir = @"..\..\..";
        static List<Language> eulangs = new List<Language>();
        static void Main(string[] arg)
        {
            // Load data from lang.txt file

            using (StreamReader sr = new StreamReader($"{dir}\\lang.txt"))
            {
                Language lang;
                string line = sr.ReadLine();
                while (line != null)
                {
                    lang = new Language(line);
                    eulangs.Add(lang);
                    line = sr.ReadLine();
                }
            }
            // Program loop
            bool exit = false;
            while (!exit)
            {
                Console.Write("Enter command (type 'help' for a list of commands): ");
                string input = Console.ReadLine().ToLower();

                switch (input)
                {
                    case "list group":

                        Console.WriteLine("Enter the name of the language group (leave blank to list all groups):");
                        string inputGroup = Console.ReadLine().Trim();

                        Console.WriteLine("==== Languages in Language Group ====");
                        foreach (var language in eulangs)
                        {
                            if (string.IsNullOrWhiteSpace(inputGroup) || language.group.IndexOf(inputGroup, StringComparison.OrdinalIgnoreCase) != -1)
                            {
                                Console.WriteLine(language.language);
                            }
                        }
                        break;

                    case "list country":

                        Console.WriteLine("Enter the name of the country (leave blank to list all countries):");
                        string inputCountry = Console.ReadLine().Trim();

                        Console.WriteLine("==== Languages in Country ====");
                        foreach (var language in eulangs)
                        {
                            if (string.IsNullOrWhiteSpace(inputCountry) || language.area.IndexOf(inputCountry, StringComparison.OrdinalIgnoreCase) != -1)
                            {
                                Console.WriteLine(language.language);
                            }
                        }
                        break;

                    case "list between":

                        Console.WriteLine("Enter the lower limit of the population range:");
                        if (int.TryParse(Console.ReadLine(), out int lowNumber))
                        {
                            Console.WriteLine("Enter the upper limit of the population range:");
                            if (int.TryParse(Console.ReadLine(), out int highNumber))
                            {
                                Console.WriteLine($"==== Languages with Population Between {lowNumber} and {highNumber} ====");
                                foreach (var language in eulangs)
                                {
                                    if (language.pop >= lowNumber && language.pop <= highNumber)
                                    {
                                        Console.WriteLine(language.language);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input for upper limit. Please enter a valid integer.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input for lower limit. Please enter a valid integer.");
                        }
                        break;

                    case "show language":

                        Console.Write("Enter language name: ");
                        string languageName = Console.ReadLine();

                        var languageToShow = eulangs.FirstOrDefault(lang => lang.language.Equals(languageName, StringComparison.OrdinalIgnoreCase));

                        if (languageToShow != null)
                        {
                            Console.WriteLine($"Language {languageToShow.language}:\n  family: {languageToShow.family}\n  population: {languageToShow.pop}\n  area: {languageToShow.area}");
                        }
                        else
                        {
                            Console.WriteLine($"Language '{languageName}' not found.");
                        }
                        break;

                    case "show group":

                        Console.Write("Enter group name: ");
                        inputGroup = Console.ReadLine().Trim();

                        var languagesInGroup = eulangs
                            .Where(lang => lang.group.Split('>').Any(group => group.Equals(inputGroup, StringComparison.OrdinalIgnoreCase)))
                            .ToList();

                        if (languagesInGroup.Any())
                        {
                            foreach (var language in languagesInGroup)
                            {
                                Console.WriteLine($"{language.language}:\nfamily: {language.family}\ngroup: {language.group}\npopulation: {language.pop}\narea: {language.area}\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Group '{inputGroup}' not found.");
                        }
                        break;

                    case "show country":

                        Console.Write("Enter country name: ");
                        inputCountry = Console.ReadLine();

                        var languagesInCountry = eulangs.Where(lang => lang.area.Split(',').Any(country => country.Equals(inputCountry, StringComparison.OrdinalIgnoreCase))).ToList();

                        if (languagesInCountry.Any())
                        {
                            foreach (var language in languagesInCountry)
                            {
                                Console.WriteLine($"{language.language}:\nfamily: {language.family}\ngroup: {language.group}\npopulation: {language.pop}\narea: {language.area}\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Country '{inputCountry}' not found.");
                        }
                        break;

                    case "show between":

                        Console.Write("Enter low population number: ");
                        if (int.TryParse(Console.ReadLine(), out int lowNum))
                        {
                            Console.Write("Enter high population number: ");
                            if (int.TryParse(Console.ReadLine(), out int highNum))
                            {
                                var languagesInRange = eulangs.Where(lang => lang.pop >= lowNum && lang.pop <= highNum).OrderBy(lang => lang.pop);

                                if (languagesInRange.Any())
                                {
                                    foreach (var language in languagesInRange)
                                    {
                                        Console.WriteLine($"Language {language.language}:\nfamily: {language.group}\npopulation: {language.pop}\narea: {language.area}\n");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"No languages found in the population range {lowNum} to {highNum}.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input for high population number. Please enter a valid integer.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input for low population number. Please enter a valid integer.");
                        }
                        break;

                    case "population group":

                        Console.Write("Enter group name: ");
                        string inputGroupName = Console.ReadLine();

                        var populationInGroup = eulangs.Where(lang => lang.group.Split('>').Any(group => group.Equals(inputGroupName, StringComparison.OrdinalIgnoreCase))).Sum(lang => lang.pop);

                        if (populationInGroup > 0)
                        {
                            Console.WriteLine($"Sum population {inputGroupName}: {populationInGroup}");
                        }
                        else
                        {
                            Console.WriteLine($"Group '{inputGroupName}' not found");
                        }
                        break;

                    case "help":
                            Console.WriteLine("Available commands: list group, list country, list between, show language, show group, show country, show between, population group, help, quit");
                        break;

                    case "quit":
                        Console.WriteLine("Exiting program...");
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid command. Type 'help' for a list of commands.");
                        break;
                }
            }
        }
    }
}

