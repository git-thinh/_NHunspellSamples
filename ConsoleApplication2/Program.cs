using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //create object
            int initialCapacity = 82765;
            int maxEditDistanceDictionary = 2; //maximum edit distance per dictionary precalculation
            SymSpell symSpell = new SymSpell(initialCapacity, maxEditDistanceDictionary);

            //load dictionary
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string dictionaryPath = baseDirectory + "/frequency_dictionary_en_82_765.txt";
            int termIndex = 0; //column of the term in the dictionary text file
            int countIndex = 1; //column of the term frequency in the dictionary text file
            if (!symSpell.LoadDictionary(dictionaryPath, termIndex, countIndex))
            {
                Console.WriteLine("File not found!");
                //press any key to exit program
                Console.ReadKey();
                return;
            }

            //lookup suggestions for single-word input strings
            string inputTerm = "hous";
            int maxEditDistanceLookup = 1; //max edit distance per lookup (maxEditDistanceLookup<=maxEditDistanceDictionary)
            var suggestionVerbosity = SymSpell.Verbosity.Closest; //Top, Closest, All
            var suggestions = symSpell.Lookup(inputTerm, suggestionVerbosity, maxEditDistanceLookup);

            //display suggestions, edit distance and term frequency
            foreach (var suggestion in suggestions)
            {
                Console.WriteLine(suggestion.term + " " + suggestion.distance.ToString() + " " + suggestion.count.ToString("N0"));
            }

            //lookup suggestions for multi-word input strings (supports compound splitting & merging)
            inputTerm = "whereis th elove hehad dated forImuch of thepast who couqdn'tread in sixtgrade and ins pired him";
            maxEditDistanceLookup = 2; //max edit distance per lookup (per single word, not per whole input string)
            suggestions = symSpell.LookupCompound(inputTerm, maxEditDistanceLookup);

            //display suggestions, edit distance and term frequency
            foreach (var suggestion in suggestions)
            {
                Console.WriteLine(suggestion.term + " " + suggestion.distance.ToString() + " " + suggestion.count.ToString("N0"));
            }

            //press any key to exit program
            Console.ReadKey();
        }
    }
}
