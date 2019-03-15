using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHunspell;

namespace CSharpConsoleSamples
{
    class Program
    {
        static void Main(string[] args)
        {

            // Important: Due to the fact Hunspell will use unmanaged memory you have to serve the IDisposable pattern
            // In this block of code this is be done by a using block. But you can also call hunspell.Dispose()

            Console.WriteLine("NHunspell functions demonstration");
            Console.WriteLine("¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");
            Console.WriteLine();
 
            using (Hunspell hunspell = new Hunspell("en_us.aff", "en_us.dic"))
            {
                Console.WriteLine("Hunspell - Spell Checking Functions");
                Console.WriteLine("¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");

                Console.WriteLine("Check if the word 'Recommendation' is spelled correct"); 
                bool correct = hunspell.Spell("Recommendation");
                Console.WriteLine("Recommendation is spelled " + (correct ? "correct" : "not correct"));

                Console.WriteLine("");
                Console.WriteLine("Make suggestions for the misspelled word 'Recommendatio'");
                List<string> suggestions = hunspell.Suggest("Recommendatio");
                Console.WriteLine("There are " + suggestions.Count.ToString() + " suggestions" );
                foreach (string suggestion in suggestions)
                {
                    Console.WriteLine("Suggestion is: " + suggestion );
                }


                Console.WriteLine("");
                Console.WriteLine("Find the word stem of the word 'decompressed'");
                List<string> stems = hunspell.Stem("decompressed");
                foreach (string stem in stems)
                {
                    Console.WriteLine("Word Stem is: " + stem);
                }

                Console.WriteLine("");
                Console.WriteLine("Generate the plural of 'girl' by providing sample 'boys'");
                List<string> generated = hunspell.Generate("girl","boys");
                foreach (string stem in generated)
                {
                    Console.WriteLine("Generated word is: " + stem);
                }

                Console.WriteLine("");
                Console.WriteLine("Analyze the word 'decompressed'");
                List<string> morphs = hunspell.Analyze("decompressed");
                foreach (string morph in morphs)
                {
                    Console.WriteLine("Morph is: " + morph);
                }

            }

            Console.WriteLine("");
            Console.WriteLine("Loading and adding a file with custom words into hunspell");
            using (var hunspell = new Hunspell("en_us.aff", "en_us.dic"))
            {

                string[] lines = System.IO.File.ReadAllLines("CustomWords-en_US.txt");
                foreach (var line in lines)
                {
                    hunspell.Add(line);
                }

                Console.WriteLine("Check if the added word 'MyTag' is spelled correct"); 
                bool correct = hunspell.Spell("MyTag");
                Console.WriteLine("'MyTag' is spelled " + (correct ? "correct" : "not correct"));
                Console.WriteLine("Make suggestions for the misspelled word 'MyTog'");
                List<string> suggestions = hunspell.Suggest("MyTog");
                Console.WriteLine("There are " + suggestions.Count + " suggestions");
                foreach (string suggestion in suggestions)
                {
                    Console.WriteLine("Suggestion is: " + suggestion);
                }
            }


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Hyph - Hyphenation Functions");
            Console.WriteLine("¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");

            // Important: Due to the fact Hyphen will use unmanaged memory you have to serve the IDisposable pattern
            // In this block of code this is be done by a using block. But you can also call hyphen.Dispose()
            using (Hyphen hyphen = new Hyphen("hyph_en_us.dic"))
            {
                Console.WriteLine("Get the hyphenation of the word 'Recommendation'"); 
                HyphenResult hyphenated = hyphen.Hyphenate("Recommendation");
                Console.WriteLine("'Recommendation' is hyphenated as: " + hyphenated.HyphenatedWord ); 
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("MyThes - Thesaurus/Synonym Functions");
            Console.WriteLine("¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");

            MyThes thes = new MyThes("th_en_us_new.dat");
            using (Hunspell hunspell = new Hunspell("en_us.aff", "en_us.dic"))
            {
                Console.WriteLine("Get the synonyms of the plural word 'cars'");
                Console.WriteLine("hunspell must be used to get the word stem 'car' via Stem().");
                Console.WriteLine("hunspell generates the plural forms of the synonyms via Generate()");
                ThesResult tr = thes.Lookup("cars", hunspell);
                if (tr != null)
                {
                    if (tr.IsGenerated)
                        Console.WriteLine("Generated over stem (The original word form wasn't in the thesaurus)");
                    foreach (ThesMeaning meaning in tr.Meanings)
                    {
                        Console.WriteLine();
                        Console.WriteLine("  Meaning: " + meaning.Description);

                        foreach (string synonym in meaning.Synonyms)
                        {
                            Console.WriteLine("    Synonym: " + synonym);

                        }
                    }
                }
            }

            // Using the spell engine for server applications
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("SpellEngine - Spell Check/Hyphenation/Thesaurus Engine");
            Console.WriteLine("¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");
            Console.WriteLine("High performance spell checking for servers and web servers");
            Console.WriteLine("All functions are tread safe. Implementaion uses multi core/multi processor");
            Console.WriteLine("Multiple Languages can be added via AddLanguage()");
            using (SpellEngine engine = new SpellEngine())
            {

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Adding a Language with all dictionaries for Hunspell, Hypen and MyThes");
                Console.WriteLine("¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");
                LanguageConfig enConfig = new LanguageConfig();
                enConfig.LanguageCode = "en";
                enConfig.HunspellAffFile = "en_us.aff";
                enConfig.HunspellDictFile = "en_us.dic";
                enConfig.HunspellKey = "";
                enConfig.HyphenDictFile = "hyph_en_us.dic";
                enConfig.MyThesDatFile = "th_en_us_new.dat";
                Console.WriteLine("Configuration will use " + engine.Processors.ToString() + " processors to serve concurrent requests");
                engine.AddLanguage(enConfig);

                Console.WriteLine();
                Console.WriteLine("Check if the word 'Recommendation' is spelled correct");
                bool correct = engine["en"].Spell("Recommendation");
                Console.WriteLine("Recommendation is spelled " + (correct ? "correct" : "not correct"));


                Console.WriteLine();
                Console.WriteLine("Make suggestions for the word 'Recommendatio'");
                List<string> suggestions = engine["en"].Suggest("Recommendatio");
                Console.WriteLine("There are " + suggestions.Count.ToString() + " suggestions");
                foreach (string suggestion in suggestions)
                {
                    Console.WriteLine("Suggestion is: " + suggestion);
                }

                Console.WriteLine("");
                Console.WriteLine("Analyze the word 'decompressed'");
                List<string> morphs = engine["en"].Analyze("decompressed");
                foreach (string morph in morphs)
                {
                    Console.WriteLine("Morph is: " + morph);
                }

                Console.WriteLine("");
                Console.WriteLine("Find the word stem of the word 'decompressed'");
                List<string> stems = engine["en"].Stem("decompressed");
                foreach (string stem in stems)
                {
                    Console.WriteLine("Word Stem is: " + stem);
                }

                Console.WriteLine();
                Console.WriteLine("Generate the plural of 'girl' by providing sample 'boys'");
                List<string> generated = engine["en"].Generate("girl", "boys");
                foreach (string stem in generated)
                {
                    Console.WriteLine("Generated word is: " + stem);
                }

                Console.WriteLine();
                Console.WriteLine("Get the hyphenation of the word 'Recommendation'");
                HyphenResult hyphenated = engine["en"].Hyphenate("Recommendation");
                Console.WriteLine("'Recommendation' is hyphenated as: " + hyphenated.HyphenatedWord);


                Console.WriteLine("Get the synonyms of the plural word 'cars'");
                Console.WriteLine("hunspell must be used to get the word stem 'car' via Stem().");
                Console.WriteLine("hunspell generates the plural forms of the synonyms via Generate()");
                ThesResult tr = engine["en"].LookupSynonyms("cars", true);
                if (tr != null)
                {
                    if (tr.IsGenerated)
                        Console.WriteLine("Generated over stem (The original word form wasn't in the thesaurus)");
                    foreach (ThesMeaning meaning in tr.Meanings)
                    {
                        Console.WriteLine();
                        Console.WriteLine("  Meaning: " + meaning.Description);

                        foreach (string synonym in meaning.Synonyms)
                        {
                            Console.WriteLine("    Synonym: " + synonym);

                        }
                    }
                }
            }

            Console.WriteLine("");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();


        }

    }
}
