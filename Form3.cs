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

          
            //exact search
            if (searchQuery != null && searchQuery.StartsWith("\"")&& searchQuery.EndsWith("\""))
            {
                
                  module3.exactSearch(module3.StemWords(searchQuery));

            }


        }
    }
}
