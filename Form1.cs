using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using mshtml;
using System.Threading;
using System.Data;
using System.Data.SqlClient;

namespace IR_milestone
{
    public partial class Form1 : Form
    {
        Queue<string> templinks = new Queue<string>();
        WebRequest myWebRequest;
        WebResponse myWebResponse;
        Stream streamResponse;
        StreamReader sReader;
        List<string> links = new List<string>();
        Thread thread;
        SqlConnection con;
        string connectionString = "Data Source=ABANOUB\\SQLEXPRESS;Initial Catalog=College;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();

            //1.Put a set of known sites on a queue(Seeds).
            
            templinks.Enqueue("http://www.wikipedia.org");
            templinks.Enqueue("http://www.bbc.com");
            templinks.Enqueue("http://www.cnn.com");
            //Fetch the document at the URL.
            //Fetch();
            //Parse the URL – HTML parser
            //Extract links from it to other docs(URLs)
            //thread = new Thread(Fetch);
            //thread.Start();


        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //•	Sample Code for fetching the page (C#)
        void Fetch()
        {
            int counter = 0;
            while(templinks.Count!=0 ||counter<3020)
            {
                try
                {
                    string tempurl = templinks.Dequeue();
                    // Create a new 'WebRequest' object to the mentioned URL.
                    myWebRequest = WebRequest.Create(tempurl);
                    // The response object of 'WebRequest' is assigned to a WebResponse' variable.
                    myWebResponse = myWebRequest.GetResponse();
                    streamResponse = myWebResponse.GetResponseStream();
                    sReader = new StreamReader(streamResponse);
                    string rString = sReader.ReadToEnd();

                    //Parse the URL – HTML parser
                    //Extract links from it to other docs(URLs)
                    //Sample code for parsing the page and extracting link from it
                    //at first add reference to mshtml from solution explorer

                    HTMLDocument y = new HTMLDocument();
                    IHTMLDocument2 doc = (IHTMLDocument2)y;
                    doc.write(rString);
                    
                    //doc.links:haya5ud kol el tags 
                    IHTMLElementCollection elements = doc.links;

                    //IHTMLElement : hatgebli kol element fl html 
                    foreach (IHTMLElement el in elements)
                    {
                        string link = (string)el.getAttribute("href", 0);
                        string document = el.document.body.innertext;
                        if (el.lang!=null && ((el.lang).ToLower()!= "en-us" || (el.lang).ToLower() != "ar"))
                            continue;
                        //5.For each extracted URL
                        //Ensure it passes certain URL filter tests
                        //////Check if it is already exists (duplicate URL elimination)
                        //lazm yebd2 b http 
                        //domain backslach page 
                        if (link.StartsWith("https://l."))
                            continue;
                        else if (link.StartsWith("http"))
                        {
                            if (!(links.Contains(link)))
                            {
                                try {
                                    insert(link, document);
                                    links.Add(link);
                                    templinks.Enqueue(link);
                                    counter++;
                                    }
                                
                                catch (Exception e)
                                    {
                                         System.Diagnostics.Debug.WriteLine("------------\n" + e.ToString() + "\n------------");
                                    }
                            }
                        }
                        else if (link.StartsWith("/"))
                        {
                            string temp = tempurl + link;
                            if (!(links.Contains(temp)))
                            {
                                try
                                {
                                    insert(link, document);
                                    links.Add(link);
                                    templinks.Enqueue(link);
                                    counter++;
                                }

                                catch (Exception e)
                                {
                                    System.Diagnostics.Debug.WriteLine("------------\n" + e.ToString() + "\n------------");
                                }
                            }
                        }
                    }
                }
                catch(Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("------------\n"+e.ToString()+ "\n------------" + counter.ToString());
                }
            }
            streamResponse.Close();
            sReader.Close();
            myWebResponse.Close();
            listBox1.DataSource = null;
            listBox1.DataSource = links;
            Start.Enabled = true;

        }

        private void insert (string url,string html)
        {
            
            string insertStr = "insert into Documents (URL,Content)values(@url,@html)";
            SqlCommand cmd = new SqlCommand(insertStr,con);
            
            SqlParameter parURL = new SqlParameter("@url",url);
            SqlParameter parcontent = new SqlParameter("@html", html);
            cmd.Parameters.Add(parURL);
            cmd.Parameters.Add(parcontent);
            cmd.ExecuteNonQuery();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            Start.Enabled = false;
            //thread.Start();
            con = new SqlConnection(connectionString);
            con.Open();
            Fetch();
            con.Close();
            MessageBox.Show("done");
        }
    }
} 