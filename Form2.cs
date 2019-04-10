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
        static string connectionString = "Data Source=ABANOUB\\SQLEXPRESS;Initial Catalog=College;Integrated Security=True";
        private void button1_Click(object sender, EventArgs e)
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
    }
}
