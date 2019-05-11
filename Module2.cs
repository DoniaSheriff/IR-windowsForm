using Iveonik.Stemmers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace IR_milestone
{
    class Module2
    {
        static string connectionString = "Data Source=ABANOUB\\SQLEXPRESS;Initial Catalog=College;Integrated Security=True";

        //Term, List of DocumentIds
       static public Dictionary<string, List<int>> SpellCheckIndex = new Dictionary<string, List<int>>();
        //Term, Dictionary<DocumentId, List of positions>
        public Dictionary<string, Dictionary<int, List<int>>> InvertedIndex = new Dictionary<string, Dictionary<int, List<int>>>();
        static Dictionary<string, bool> StopWords = new Dictionary<string, bool>
    {
        { "a", true },
        { "about", true },
        { "above", true },
        { "across", true },
        { "after", true },
        { "afterwards", true },
        { "again", true },
        { "against", true },
        { "all", true },
        { "almost", true },
        { "alone", true },
        { "along", true },
        { "already", true },
        { "also", true },
        { "although", true },
        { "always", true },
        { "am", true },
        { "among", true },
        { "amongst", true },
        { "amount", true },
        { "an", true },
        { "and", true },
        { "another", true },
        { "any", true },
        { "anyhow", true },
        { "anyone", true },
        { "anything", true },
        { "anyway", true },
        { "anywhere", true },
        { "are", true },
        { "around", true },
        { "as", true },
        { "at", true },
        { "back", true },
        { "be", true },
        { "became", true },
        { "because", true },
        { "become", true },
        { "becomes", true },
        { "becoming", true },
        { "been", true },
        { "before", true },
        { "beforehand", true },
        { "behind", true },
        { "being", true },
        { "below", true },
        { "beside", true },
        { "besides", true },
        { "between", true },
        { "beyond", true },
        { "bill", true },
        { "both", true },
        { "bottom", true },
        { "but", true },
        { "by", true },
        { "call", true },
        { "can", true },
        { "cannot", true },
        { "cant", true },
        { "co", true },
        { "computer", true },
        { "con", true },
        { "could", true },
        { "couldnt", true },
        { "cry", true },
        { "de", true },
        { "describe", true },
        { "detail", true },
        { "do", true },
        { "done", true },
        { "down", true },
        { "due", true },
        { "during", true },
        { "each", true },
        { "eg", true },
        { "eight", true },
        { "either", true },
        { "eleven", true },
        { "else", true },
        { "elsewhere", true },
        { "empty", true },
        { "enough", true },
        { "etc", true },
        { "even", true },
        { "ever", true },
        { "every", true },
        { "everyone", true },
        { "everything", true },
        { "everywhere", true },
        { "except", true },
        { "few", true },
        { "fifteen", true },
        { "fify", true },
        { "fill", true },
        { "find", true },
        { "fire", true },
        { "first", true },
        { "five", true },
        { "for", true },
        { "former", true },
        { "formerly", true },
        { "forty", true },
        { "found", true },
        { "four", true },
        { "from", true },
        { "front", true },
        { "full", true },
        { "further", true },
        { "get", true },
        { "give", true },
        { "go", true },
        { "had", true },
        { "has", true },
        { "have", true },
        { "he", true },
        { "hence", true },
        { "her", true },
        { "here", true },
        { "hereafter", true },
        { "hereby", true },
        { "herein", true },
        { "hereupon", true },
        { "hers", true },
        { "herself", true },
        { "him", true },
        { "himself", true },
        { "his", true },
        { "how", true },
        { "however", true },
        { "hundred", true },
        { "i", true },
        { "ie", true },
        { "if", true },
        { "in", true },
        { "inc", true },
        { "indeed", true },
        { "interest", true },
        { "into", true },
        { "is", true },
        { "it", true },
        { "its", true },
        { "itself", true },
        { "keep", true },
        { "last", true },
        { "latter", true },
        { "latterly", true },
        { "least", true },
        { "less", true },
        { "ltd", true },
        { "made", true },
        { "many", true },
        { "may", true },
        { "me", true },
        { "meanwhile", true },
        { "might", true },
        { "mill", true },
        { "mine", true },
        { "more", true },
        { "moreover", true },
        { "most", true },
        { "mostly", true },
        { "move", true },
        { "much", true },
        { "must", true },
        { "my", true },
        { "myself", true },
        { "name", true },
        { "namely", true },
        { "neither", true },
        { "never", true },
        { "nevertheless", true },
        { "next", true },
        { "nine", true },
        { "no", true },
        { "nobody", true },
        { "none", true },
        { "nor", true },
        { "not", true },
        { "nothing", true },
        { "now", true },
        { "nowhere", true },
        { "of", true },
        { "off", true },
        { "often", true },
        { "on", true },
        { "once", true },
        { "one", true },
        { "only", true },
        { "onto", true },
        { "or", true },
        { "other", true },
        { "others", true },
        { "otherwise", true },
        { "our", true },
        { "ours", true },
        { "ourselves", true },
        { "out", true },
        { "over", true },
        { "own", true },
        { "part", true },
        { "per", true },
        { "perhaps", true },
        { "please", true },
        { "put", true },
        { "rather", true },
        { "re", true },
        { "same", true },
        { "see", true },
        { "seem", true },
        { "seemed", true },
        { "seeming", true },
        { "seems", true },
        { "serious", true },
        { "several", true },
        { "she", true },
        { "should", true },
        { "show", true },
        { "side", true },
        { "since", true },
        { "sincere", true },
        { "six", true },
        { "sixty", true },
        { "so", true },
        { "some", true },
        { "somehow", true },
        { "someone", true },
        { "something", true },
        { "sometime", true },
        { "sometimes", true },
        { "somewhere", true },
        { "still", true },
        { "such", true },
        { "system", true },
        { "take", true },
        { "ten", true },
        { "than", true },
        { "that", true },
        { "the", true },
        { "their", true },
        { "them", true },
        { "themselves", true },
        { "then", true },
        { "thence", true },
        { "there", true },
        { "thereafter", true },
        { "thereby", true },
        { "therefore", true },
        { "therein", true },
        { "thereupon", true },
        { "these", true },
        { "they", true },
        { "thick", true },
        { "thin", true },
        { "third", true },
        { "this", true },
        { "those", true },
        { "though", true },
        { "three", true },
        { "through", true },
        { "throughout", true },
        { "thru", true },
        { "thus", true },
        { "to", true },
        { "together", true },
        { "too", true },
        { "top", true },
        { "toward", true },
        { "towards", true },
        { "twelve", true },
        { "twenty", true },
        { "two", true },
        { "un", true },
        { "under", true },
        { "until", true },
        { "up", true },
        { "upon", true },
        { "us", true },
        { "very", true },
        { "via", true },
        { "was", true },
        { "we", true },
        { "well", true },
        { "were", true },
        { "what", true },
        { "whatever", true },
        { "when", true },
        { "whence", true },
        { "whenever", true },
        { "where", true },
        { "whereafter", true },
        { "whereas", true },
        { "whereby", true },
        { "wherein", true },
        { "whereupon", true },
        { "wherever", true },
        { "whether", true },
        { "which", true },
        { "while", true },
        { "whither", true },
        { "who", true },
        { "whoever", true },
        { "whole", true },
        { "whom", true },
        { "whose", true },
        { "why", true },
        { "will", true },
        { "with", true },
        { "within", true },
        { "without", true },
        { "would", true },
        { "yet", true },
        { "you", true },
        { "your", true },
        { "yours", true },
        { "yourself", true },
        { "yourselves", true }
    };
        public void Indexing(string Text, int docID)
        {
            //Remove Punctuation and numbers
            string result = Regex.Replace(Text, "[^a-zA-Z\\s]", "");
            result = Regex.Replace(result, @"(?<!^)(?=[A-Z])", " ");

            //Split by delimiters to get Tokens
            char[] delimiters = new char[] { '\r', '\n', ' ' };
            string[] tokens = result.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Where(x => x.Length > 1).ToArray();

            //Save position of each token before removing stop words
            List<Tuple<string, int>> Tokens = new List<Tuple<string, int>>();
            for (int i = 0; i < tokens.Length; i++)
            {
                Tokens.Add(new Tuple<string, int>(tokens[i].ToLower(), i));
            }

            //Remove StopWords
            List<Tuple<string, int>> Tokens_Stop = new List<Tuple<string, int>>();
            foreach (var x in Tokens)
            {
                if (!StopWords.ContainsKey(x.Item1))
                    Tokens_Stop.Add(x);
            }
            //Note: before stemming the terms, just save a copy of them (term, doc_id) to be used later on in the spell check module

            //Tokens_Stop contains : Term , Position after remmoving Stopwords
            foreach (var x in Tokens_Stop)
            {
                if (SpellCheckIndex.ContainsKey(x.Item1))
                {
                    if (!SpellCheckIndex[x.Item1].Contains(docID))
                    {
                        SpellCheckIndex[x.Item1].Add(docID);
                    }
                }
                else
                {
                    SpellCheckIndex.Add(x.Item1, new List<int>());
                    SpellCheckIndex[x.Item1].Add(docID);
                }
            }



            //Stemming using StemmersNet snowballs port
            List<Tuple<string, int>> Tokens_Stem = new List<Tuple<string, int>>();
            IStemmer stemmer = new EnglishStemmer();
            foreach (var x in Tokens_Stop)
            {
                var StemmedWord = new Tuple<string, int>(stemmer.Stem(x.Item1), x.Item2);
                Tokens_Stem.Add(StemmedWord);
            }

            //Create Inverted Index
            foreach (var x in Tokens_Stem)
            {
                if (InvertedIndex.ContainsKey(x.Item1))
                {
                    if (InvertedIndex[x.Item1].ContainsKey(docID))
                    {
                        InvertedIndex[x.Item1][docID].Add(x.Item2);
                    }
                    else
                    {
                        List<int> temp = new List<int>();
                        temp.Add(x.Item2);
                        InvertedIndex[x.Item1].Add(docID, temp);
                    }
                }
                else
                {
                    InvertedIndex.Add(x.Item1, new Dictionary<int, List<int>>());
                    List<int> temp = new List<int>();
                    temp.Add(x.Item2);
                    InvertedIndex[x.Item1].Add(docID, temp);
                }
            }
        }
        public string Soundex(string word)
        {
            //QUESTION
            const string soundexAlphabet = "0123012#02245501262301#202";
            string soundexString = "";
            char lastSoundexChar = '?';

            foreach (var c in from ch in word
                              where ch >= 'a' &&
                                    ch <= 'z' &&
                                    soundexString.Length < 4
                              select ch)
            {
                char thisSoundexChar = soundexAlphabet[c - 'a'];

                if (soundexString.Length == 0)
                    soundexString += c;
                else if (thisSoundexChar == '#')
                    continue;
                else if (thisSoundexChar != '0' &&
                         thisSoundexChar != lastSoundexChar)
                    soundexString += thisSoundexChar;

                lastSoundexChar = thisSoundexChar;
            }

            return soundexString.PadRight(4, '0');
        }


        public void spellcheckModule()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            foreach (var b in SpellCheckIndex)
            {
                try
                {
                    string insertStr = "insert into SpellCheckModule (Term,DocID)values(@par1,@par2)";
                    SqlCommand cmd = new SqlCommand(insertStr, connection);

                    string positions = "";
                    foreach (var pos in b.Value)
                    {
                        positions += pos.ToString() + ",";
                    }
                    SqlParameter par1 = new SqlParameter("@par1", b.Key);
                    SqlParameter par2 = new SqlParameter("@par2", positions);
                    cmd.Parameters.Add(par1);
                    cmd.Parameters.Add(par2);

                    cmd.ExecuteNonQuery();
                }

                catch
                { }
            }
        }
    }

}
