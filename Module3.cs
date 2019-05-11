using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Iveonik.Stemmers;
namespace IR_milestone
{
	class Module3
	{
		string connectionString = "Data Source=ABANOUB\\SQLEXPRESS;Initial Catalog=College;Integrated Security=True";
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
		char[] delimiters = new char[] { '\r', '\n', ' ', ',' };
		static HashSet<string> JackOutput = new HashSet<string>();

		public string[] StemWords(string query)
		{

			//Split by delimiters to get Tokens
			string[] words = query.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToArray();
			string[] words_Stop = new string[10000];
			for (int i = 0; i < 10000; i++)
				words_Stop[i] = "";
			//Remove StopWords
			int counter = 0;
			for (int i = 0; i < words.Length; i++)
			{
				if (!StopWords.ContainsKey(words[i]))
					words_Stop[i] = words[i];
				counter++;
			}

			//Stemming using StemmersNet snowballs port
			string[] Tokens_Stem = new string[counter];
			for (int i = 0; i < counter; i++)
				Tokens_Stem[i] = "";
			IStemmer stemmer = new EnglishStemmer();
			for (int i = 0; i < counter; i++)
			{
				var StemmedWord = new[] { stemmer.Stem(words_Stop[i]) };
				Tokens_Stem[i] = (StemmedWord[0]);
			}
			return Tokens_Stem;
		}
		public void exactSearch(string[] wordsList)
		{
			//assume that searchQuery = "search music"

			//select a.Position as A
			//from(select d.Term, p.ID, p.TermID, p.DocNum, p.Freq, p.Position from[Dictionary] d,[Posting] p where d.ID = p.TermID and d.Term = 'search')a , 
			// (select d.Term , p.ID ,p.TermID,p.DocNum,p.Freq,p.Position from[Dictionary] d ,[Posting] p where d.ID = p.TermID and d.Term='music') b
			//where a.DocNum=b.DocNum

			foreach (var b in wordsList)
			{


				//    List<> positionA = new List;
				SqlConnection connection = new SqlConnection(connectionString);
				string command = "select a.DocNum, a.Position AS A , b.Position AS B from(select d.Term, p.ID, p.TermID, p.DocNum, p.Freq, p.Position from[Dictionary] d,[Posting] p where d.ID = p.TermID and d.Term = 'search')a ,(select d.Term , p.ID ,p.TermID,p.DocNum,p.Freq,p.Position from[Dictionary] d ,[Posting] p where d.ID = p.TermID and d.Term='music') b where a.DocNum=b.DocNum";
				SqlCommand cmd = new SqlCommand(command, connection);
				connection.Open();

				string TempPositionA = "";
				string[] TempPositionAString = new string[1000];
				List<int> PositionAList = new List<int>();

				string TempPositionB = "";
				string[] TempPositionBString = new string[1000];
				List<int> PositionBList = new List<int>();
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						int documentNumber = Int32.Parse(reader["DocNum"].ToString());

						TempPositionA = reader["A"].ToString();
						TempPositionB = reader["B"].ToString();

						TempPositionAString = TempPositionA.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
						TempPositionBString = TempPositionB.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

						for (int i = 0; i < TempPositionAString.Length; i++)
						{
							try
							{
								PositionAList.Add(Int32.Parse(TempPositionAString[i]));
								PositionBList.Add(Int32.Parse(TempPositionBString[i]));
							}
							catch
							{ }
						}

						//Now you have list for each  word -- complete here 





					}
					int z = 2;
				}
			}



		}
		public Dictionary<string, List<Tuple<string, int>>> SoundexSearch(string[] words)
		{
			SqlConnection connection = new SqlConnection(connectionString);
            Dictionary<string, List<Tuple<string, int>>> Results = new Dictionary<string, List<Tuple<string, int>>>();
			foreach (var b in words)
			{
				Module2 module2 = new Module2();
				string hashcode = module2.Soundex(b);

				string command = "SELECT Term FROM Soundex where Soundex =@param1";
				SqlCommand cmd = new SqlCommand(command, connection);
				connection.Open();
				SqlParameter par1 = new SqlParameter("@param1", hashcode);
				cmd.Parameters.Add(par1);
				string soundexTerms = "";
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
                        soundexTerms = reader.GetString(0);
					}
				}
				string[] soundexTerm_Stem = StemWords(soundexTerms);
				foreach (var x in soundexTerm_Stem)
				{
					command = @"SELECT c.ID,c.URL,p.Freq  
						FROM   [Dictionary] d,[Posting] p,[Documents] c
						Where d.Term = @param1 and d.ID = p.TermID and p.DocNum = c.ID
                        Order by p.Freq Desc";
					cmd = new SqlCommand(command, connection);
					SqlParameter par2 = new SqlParameter("@param1", x);
					cmd.Parameters.Add(par2);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Tuple<string, int>> temp = new List<Tuple<string, int>>();
                        while (reader.Read())
                        {
                            temp.Add(new Tuple<string, int>(reader.GetString(1),reader.GetInt32(2)));
                        }
                        Results.Add(x, temp);
                    }
                }

			}
            connection.Close();
            return Results;
		}
		public SortedDictionary<int, List<string>> SpellCheck(string[] words)
		{
			SqlConnection connection = new SqlConnection(connectionString);
			HashSet<string> BigramIndex = new HashSet<string>();
			 HashSet<string> BigramValues = new HashSet<string>();
            SortedDictionary<int, List<string>> Results = new SortedDictionary<int, List<string>>();
			//Hanakhud kol kelma mn l query negblha l bigram beta3ha
			//Gets all Terms from InvertedIndex table(Dictionary), 
			//Splits each term into bigrams adding $ before and after the term, 
			//Adds to HashSet so all bigrams are unique
			foreach (var QueryWord in words)
			{
				BigramIndex = buildBigram(QueryWord);
				
				//W b3den hanru7 na5ud kol el kalemat elly bttl3 mn lbigrams da
				foreach (var x in BigramIndex)
				{
					BigramValues = retrieveBigram(BigramValues, x);
				}
				//W ne7sb lkol kelma mn el query m3 elklemat ely tel3et mn l sql l jacquard coefficient
				//BigramValues for the QuerWord Bigram
				JackQuardCoeff(QueryWord, BigramIndex, BigramValues);
				foreach (var a in JackOutput)
				{
					//W el a2l jacquard mn 0.45 aw equa byt7sblu el edit distance ben el kelma mn l jacquard w el query w ha sort bl a2l distance
					int x = Levenshtein.Compute(QueryWord, a);
                    if(Results.ContainsKey(x))
                    {
                        Results[x].Add(a);
                    }
                    else
                    {
                        List<string> temp = new List<string>();
                        temp.Add(a);
                        Results.Add(x, temp);
                    }
				}
			}

            return Results;
		}
		//bigram of the first word in the query = BigramValues
		public void JackQuardCoeff(string Query, HashSet<string> BigramIndex, HashSet<string> BigramValues)
		{
			HashSet<string> BigramIndex2 = new HashSet<string>();
			HashSet<string> bigramDictionary = new HashSet<string>();
			int commonCount = 0, query = BigramIndex.Count, dictionary = 0;
			double result =0;
			//x=earth
			foreach (var x in BigramValues)
			{

				bigramDictionary = buildBigram(x);				 
				dictionary = bigramDictionary.Count;
				HashSet<string> common = new HashSet<string>(BigramIndex);
				common.IntersectWith(bigramDictionary);
				commonCount = common.Count;
				result = (2 * commonCount);
				result/=(dictionary + query);
				if (result >= 0.45)
					JackOutput.Add(x);
			}
		}
		public HashSet<string> buildBigram(string QueryWord)
		{
			HashSet<string> BigramIndex = new HashSet<string>();
			string temp = "", bigram;
			//Gets all Terms from InvertedIndex table(Dictionary), 
			//Splits each term into bigrams adding $ before and after the term, 
			//Adds to HashSet so all bigrams are unique
			temp = "";
			temp += "$" + QueryWord + "$";
			for (int i = 0; i < temp.Length - 1; i++)
			{
				bigram = "";
				bigram += temp[i];
				bigram += temp[i + 1];
				//mest5dmen hashset 3shan mafehush duplicates
				if (!BigramIndex.Contains(bigram))
				{
					BigramIndex.Add(bigram);
				}

			}

			return BigramIndex;

		}
		public HashSet<string> retrieveBigram(HashSet<string> BigramValues,string x)
		{
			//HashSet<string> BigramValues = new HashSet<string>();

			SqlConnection connection = new SqlConnection(connectionString);
			string command = "SELECT Terms FROM Bigram  where [Bigram] =@param1 ";
			SqlCommand cmd = new SqlCommand(command, connection);
			connection.Open();
			SqlParameter par1 = new SqlParameter("@param1", x);
			cmd.Parameters.Add(par1);
			string temp = "";
			using (SqlDataReader reader = cmd.ExecuteReader())
			{
				while (reader.Read())
				{
					temp = reader.GetString(0);
					string[] tempA = temp.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToArray();
					foreach (var z in tempA)
					{
						if (!BigramValues.Contains(z))
						{
							BigramValues.Add(z);
						}
						temp = "";
					}
				}
			}



			return BigramValues;


		}
	}
}
