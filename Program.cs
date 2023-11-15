/* 
DELUPPGIFT 1: Ladda filen lang.txt vid programstart
1.1 Implementera kod för att läsa innehållet från lang.txt (Redan gjort av läraren)
1.2 Testa och säkerställ att filen laddas korrekt  (körde en test körning och den listar infon)

DELUPPGIFT 2: Kommandoradsloop och Basfunktionalitet
2.1 Implementera en enkel kommandoradsloop
2.2 Skapa kommandon för list group, list country, list between, show language, show group,show country countryname, show between lownum and hinum och population group groupname
2.3 Skapa help och quit kommandon
2.4 Gör stage/commit/push för varje implementerat kommando

DELUPPGIFT 3: Implementera Fyra Frågekommandon
3.1 Implementera kod för varje kommando i 2.2
3.2 Gör stage/commit/push för varje implementerat kommando

DELUPPGIFT 4: Hjälputskrift och Avsluta Kommandon
4.1 Implementera help kommandot för att visa tillgängliga kommandon
4.2 Implementera quit kommandot för att avsluta programmet
4.3 Gör stage/commit/push för varje implementerat kommando

DELUPPGIFT 5: Kommentera Kod
5.1 Kommentera koden för förståelse och läsbarhet
5.2 Lägg till //NYI-kommentarer för de kommandon som inte har implementerats
5.3 Lägg till //TBD-funktioner om det finns möjlighet till refaktorering i framtiden
5.4 Gör stage/commit/push för detta steg

DELUPPGIFT 6: Validering och Testning
6.1 Testa varje kommando och se till att de ger förväntade resultat
6.2 Validera användarens inmatning och hantera felaktiga kommandon med //FIXME-kommentarer
6.3 Gör stage/commit/push för detta steg
*/


using System.Globalization;

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
            using (StreamReader sr = new StreamReader($"{dir}\\lang.txt"))
            {
                Language lang;
                string line = sr.ReadLine();
                while (line != null)
                {
                    // Console.WriteLine(line);
                    lang = new Language(line);
                    eulangs.Add(lang);
                    line = sr.ReadLine();
                }
            }
            Console.WriteLine("==== Languages in Spain ====");
            foreach (Language L in eulangs)
            {
                int index = L.area.IndexOf("Spain");
                if (index != -1)
                    L.Print();
            }
            Console.WriteLine("==== Baltic Languages ====");
            foreach (Language L in eulangs)
            {
                int index = L.group.IndexOf("Baltic");
                if (index != -1)
                    L.Print();
            }
            Console.WriteLine("==== Population larger than 50 millions ====");
            foreach (Language L in eulangs)
            {
                if (L.pop >= 50_000_000)
                    L.Print();
            }
            Console.WriteLine("==== Number of Germanics ====");
            int sumgerm = 0;
            foreach (Language L in eulangs)
            {
                int index = L.group.IndexOf("Germanic");
                if (index != -1)
                    sumgerm += L.pop;
            }
            Console.WriteLine($"Germanic speaking population: {sumgerm}");
            Console.WriteLine("==== Number of Romance ====");
            int sumromance = 0;
            foreach (Language L in eulangs)
            {
                int index = L.group.IndexOf("Romance");
                if (index != -1)
                    sumromance += L.pop;
            }
            Console.WriteLine($"Romance speaking population: {sumromance}");
        }
    }
}
