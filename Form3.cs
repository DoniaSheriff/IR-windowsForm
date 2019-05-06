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
using System.Text.RegularExpressions;

namespace IR_milestone
{
	public partial class Form3 : Form
	{
		static string connectionString = "Data Source=DONIA\\SQLEXPRESS;Initial Catalog=College;Integrated Security=True";

		public Form3()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string searchQuery = textBox1.Text.TrimStart();
			Module3 module3 = new Module3();
			char[] delimiters = new char[] { '\r', '\n', ' ' , ',' };


			//exact search
			if (searchQuery != null && searchQuery.StartsWith("\"") && searchQuery.EndsWith("\""))
			{
                //Remove Punctuation and numbers
				string result = Regex.Replace(searchQuery, "[^a-zA-Z\\s]", "");
				result = Regex.Replace(result, @"(?<!^)(?=[A-Z])", " ");
				module3.exactSearch(module3.StemWords(result));

			}
			else if (radioButton1.Checked == true)
			{ 
				string[] words = searchQuery.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Where(x => x.Length > 1).ToArray();
				module3.SpellCheck(words);
			}

			else if (radioButton2.Checked == true)
			{
				string[] words = searchQuery.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Where(x => x.Length > 1).ToArray();
				 module3.SoundexSearch(words);
			}

             
		}

		private void groupBox1_Enter(object sender, EventArgs e)
		{

		}
	}
}
