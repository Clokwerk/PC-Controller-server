using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Server1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e) {
           
               
            this.webBrowser1.Navigate(Form1.url);
            var th = new Thread(() => {
                int amountreceived = 0;
                byte[] received = new byte[256];
                do
                {
                    Console.WriteLine("starting to receive");
                    do
                    {
                        amountreceived = Form1.client.Receive(received);
                    } while (amountreceived == 0);
                    Form1.url = Encoding.ASCII.GetString(received.Take(amountreceived - 1).ToArray());
                    Console.WriteLine("New browser command received");
                    try
                    {
                        if (Form1.url != "0")
                        {
                            webBrowser1.Url = new Uri("https://"+Form1.url.ToString());
                        }
                    }catch(System.Runtime.InteropServices.InvalidComObjectException e1)
                    {
                        continue;
                    }
                } while (Form1.url != "0");
                
                
                return;
            }
            );
            th.SetApartmentState(ApartmentState.STA);
            th.Start();




        }
        
     


        

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
