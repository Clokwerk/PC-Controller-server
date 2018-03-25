using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Sockets;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
         string message;
        private void Form3_Load(object sender, EventArgs e)
        {
            //write message sending mechanism here
            byte[] received = new byte[256];
            int amountreceived = 0;
            do
            {
                do
                {
                   amountreceived= Form1.client.Receive(received);
                } while (amountreceived == 0);
                message = Encoding.ASCII.GetString(received.Take(amountreceived - 1).ToArray());
                richTextBox1.Text = Environment.NewLine + message;
            } while (message != "terminate");
        }
        
        
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
