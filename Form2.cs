using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IR_milestone
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        static string connectionString = "Data Source=DONIA\\SQLEXPRESS;Initial Catalog=College;Integrated Security=True";
        private void InvertedIndexButton_Click(object sender, EventArgs e)
        {
            //Fetch 1500 document from DB
            Module2 module2 = new Module2();
            SqlConnection connection = new SqlConnection(connectionString);
            string command = "SELECT TOP 1500 * FROM Documents;";
            SqlCommand cmd = new SqlCommand(command, connection);
            connection.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    module2.Indexing(reader.GetString(2), reader.GetInt32(0));
                }
            }
            connection.Close();
            SaveInvertedIndex(module2.InvertedIndex);
           
            MessageBox.Show("Done");
        }

        private void SaveInvertedIndex(Dictionary<string, Dictionary<int, List<int>>> invertedIndex)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            int TermID = 0;
            foreach(var Term in invertedIndex)
            {
                //If Term low frequency or Length of the Term is more than 50
                if (Term.Value.Values.Count < 5 || Term.Key.Length>50)
                    continue;
                //Calculate Total Frequency of Term across all documents
                int CollFreq = 0;
                foreach(var x in Term.Value)
                {
                    CollFreq += x.Value.Capacity;
                }
                string insertStr = "insert into Dictionary (Term,NumDocs,CollFreq)values(@par1,@par2,@par3)";
                SqlCommand cmd = new SqlCommand(insertStr, connection);
                SqlParameter par1 = new SqlParameter("@par1", Term.Key);
                SqlParameter par2 = new SqlParameter("@par2", Term.Value.Count);
                SqlParameter par3 = new SqlParameter("@par3", CollFreq);
                cmd.Parameters.Add(par1);
                cmd.Parameters.Add(par2);
                cmd.Parameters.Add(par3);
                cmd.ExecuteNonQuery();

                string insertStr2 = "insert into Posting (TermID,DocNum,Freq,Position)values(@par4,@par5,@par6,@par7)";
                TermID++;
                foreach (var doc in Term.Value)
                {
                    SqlCommand cmd2 = new SqlCommand(insertStr2, connection);
                    SqlParameter par4 = new SqlParameter("@par4", TermID);
                    SqlParameter par5 = new SqlParameter("@par5", doc.Key);
                    SqlParameter par6 = new SqlParameter("@par6", doc.Value.Count);
                    //Puts the list of positions into one comma sperated string
                    string positions = "";
                    foreach(var pos in Term.Value[doc.Key])
                    {                       
                            positions += pos.ToString() + ",";
                    }
                    SqlParameter par7 = new SqlParameter("@par7", positions);
                    cmd2.Parameters.Add(par4);
                    cmd2.Parameters.Add(par5);
                    cmd2.Parameters.Add(par6);
                    cmd2.Parameters.Add(par7);
                    cmd2.ExecuteNonQuery();
                }               
            }
            connection.Close();

        }

        private void SoundexButton_Click(object sender, EventArgs e)
        {
            Module2 module2 = new Module2();
            Dictionary<string, List<string>> SoundexIndex = new Dictionary<string, List<string>>();

            SqlConnection connection = new SqlConnection(connectionString);
            string command = "SELECT Term FROM SpellcheckModule;";
            SqlCommand cmd = new SqlCommand(command, connection);
            connection.Open();
            //Fetches all Terms from InvertedIndex (Dictionary)
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    //Gets the soundex of the term using the module2.soundex function
                    string so = module2.Soundex(reader.GetString(0));
                    //Adds the soundex to SoundexIndex if it deoesn't exist, if It exists Adds the term to the list of terms that matches the soundex
                    if(SoundexIndex.ContainsKey(so))
                    {
                        SoundexIndex[so].Add(reader.GetString(0));
                    }
                    else
                    {
                        SoundexIndex.Add(so, new List<string>());
                        SoundexIndex[so].Add(reader.GetString(0));
                    }
                }
            }
            Dictionary<string, string> SoundexIndex2 = new Dictionary<string, string>();
            foreach (var x in SoundexIndex)
            {
                string temp = "";
                foreach(var y in x.Value)
                {
                    temp += y + ",";
                }
                SoundexIndex2.Add(x.Key, temp);
            }
            string insertStr = "insert into Soundex (Soundex,Term)values(@par1,@par2)";
            foreach(var x in SoundexIndex2)
            {
                SqlCommand cmd2 = new SqlCommand(insertStr, connection);
                SqlParameter par1 = new SqlParameter("@par1", x.Key);
                SqlParameter par2 = new SqlParameter("@par2", x.Value);
                cmd2.Parameters.Add(par1);
                cmd2.Parameters.Add(par2);
                cmd2.ExecuteNonQuery();
            }

            connection.Close();
            MessageBox.Show("done");
        }

        private void Bigram_Click(object sender, EventArgs e)
        {
            HashSet<string> BigramIndex = new HashSet<string>();
            Dictionary<string, string> BigramTable = new Dictionary<string, string>();
            SqlConnection connection = new SqlConnection(connectionString);
            string command = "SELECT Term FROM SpellcheckModule;";
            SqlCommand cmd = new SqlCommand(command, connection);
            connection.Open();
            string temp="",bigram;
            //Gets all Terms from InvertedIndex table(Dictionary), 
            //Splits each term into bigrams adding $ before and after the term, 
            //Adds to HashSet so all bigrams are unique
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    temp = "";
                    temp += "$"+reader.GetString(0)+"$";
                    for(int i=0; i<temp.Length-1;i++)
                    {
                        bigram = "";                        
                        bigram += temp[i];
                        bigram += temp[i+1];
                        if (!BigramIndex.Contains(bigram))
                        {
                            BigramIndex.Add(bigram);
                        }

                    }
                }
            }
            //Takes each bigram and Queries the DB using a 'Like' statment to get all Terms that match the bigram
            //Adds to BigramTable dictionary<Bigram,Listofterms that match>
            foreach(var b in BigramIndex)
            {
                string Temp = "";
                if(b[0]=='$')
                {
                    Temp += b[1];
                    Temp += "%";
                }
                else if(b[1] == '$')
                {
                    Temp += "%";
                    Temp += b[0];
                }
                else
                {
                    Temp += "%";
                    Temp += b[0];
                    Temp += b[1];
                    Temp += "%";
                }
                command = "select Term from Dictionary where Term Like '"+Temp+"'";
                SqlCommand cmd2 = new SqlCommand(command, connection);
                string ListOfTerms = "";
                using (SqlDataReader reader = cmd2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ListOfTerms+=reader.GetString(0)+",";
                    }
                }
                BigramTable.Add(b, ListOfTerms);
            }
            //Inserts into BigramTable in DB
            foreach(var b in BigramTable)
            {
                command = "insert into Bigram (Bigram,Terms)values(@par1,@par2)";
                SqlCommand cmd3 = new SqlCommand(command, connection);
                cmd3.Parameters.AddWithValue("@par1", b.Key);
                cmd3.Parameters.AddWithValue("@par2", b.Value);
                cmd3.ExecuteNonQuery();
            }
            connection.Close();
            MessageBox.Show("Done");
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }        
    }
}
;