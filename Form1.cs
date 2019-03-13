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

namespace IR_milestone
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //1.Put a set of known sites on a queue(Seeds).
            string URL = "http://www.cnn.com";
            //Fetch the document at the URL.
            Fetch(URL);
            //Parse the URL – HTML parser
            //Extract links from it to other docs(URLs)
   

        }
        //•	Sample Code for fetching the page (C#)
        void Fetch(string URL)
        {
            // Create a new 'WebRequest' object to the mentioned URL.
            WebRequest myWebRequest = WebRequest.Create(URL);
            // The response object of 'WebRequest' is assigned to a WebResponse' variable.
            WebResponse myWebResponse = myWebRequest.GetResponse();
            Stream streamResponse = myWebResponse.GetResponseStream();
            StreamReader sReader = new StreamReader(streamResponse);
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

               //5.For each extracted URL
               //Ensure it passes certain URL filter tests
               //Check if it is already exists (duplicate URL elimination)
               //lazm yebd2 b http 
               //domain backslach page 
            }
            streamResponse.Close();
            sReader.Close();
            myWebResponse.Close();

         

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
