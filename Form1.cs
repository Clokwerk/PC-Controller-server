using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using WMPLib;
namespace Server1
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
           
        }
       
WMPLib.WindowsMediaPlayer Player= new WMPLib.WindowsMediaPlayer();

        private void Form1_Load(object sender, EventArgs e)
        {
           
            textBox2.DragEnter += new DragEventHandler(textBox2_DragEnter);
            textBox2.DragDrop += new DragEventHandler(textBox2_DragDrop);
            textBox3.DragEnter += new DragEventHandler(textBox3_DragEnter);
            textBox3.DragDrop += new DragEventHandler(textBox3_DragDrop);
            textBox4.DragEnter += new DragEventHandler(textBox4_DragEnter);
            textBox4.DragDrop += new DragEventHandler(textBox4_DragDrop);
            textBox5.DragEnter += new DragEventHandler(textBox5_DragEnter);
            textBox5.DragDrop += new DragEventHandler(textBox5_DragDrop);
            textBox6.DragEnter += new DragEventHandler(textBox6_DragEnter);
            textBox6.DragDrop += new DragEventHandler(textBox6_DragDrop);
            textBox7.DragEnter += new DragEventHandler(textBox7_DragEnter);
            textBox7.DragDrop += new DragEventHandler(textBox7_DragDrop);
            textBox8.DragEnter += new DragEventHandler(textBox8_DragEnter);
            textBox8.DragDrop += new DragEventHandler(textBox8_DragDrop);
            textBox9.DragEnter += new DragEventHandler(textBox9_DragEnter);
            textBox9.DragDrop += new DragEventHandler(textBox9_DragDrop);
            textBox10.DragEnter += new DragEventHandler(textBox10_DragEnter);
            textBox10.DragDrop += new DragEventHandler(textBox10_DragDrop);
 





        }
    
        int port;
        public static string url;
        public static int active;
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                port = Convert.ToInt32(textBox1.Text);
                IPAddress ipaddress = IPAddress.Any;
                label3.Text = "Listening";
                label3.ForeColor = Color.Green;
                Socket listener = new Socket(ipaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(new IPEndPoint(ipaddress, port));
                listener.Listen(10);
                listener.BeginAccept(new AsyncCallback(OnConnectRequest), listener);


               
            



            }
            catch (Exception ex)
            {
                Console.WriteLine("Exceotion caught: {0} ", ex.Message);
            }
}
      public static volatile  Socket client;
        public void OnConnectRequest(IAsyncResult ar)
        {
            string command;
            int receivedbuf=0;
            Socket listener = (Socket)ar.AsyncState;
            client = listener.EndAccept(ar);
            Console.WriteLine("Client {0}, joined", client.RemoteEndPoint);
            label3.Invoke((MethodInvoker)(() =>
            {
                label3.Text = String.Format("Client: {0} connected",client.RemoteEndPoint);
            }));

            byte[] receivbuf = new byte[100];
            byte[] sendbuf = new byte[100];
            Console.WriteLine("Now receiving");
            do
            {
                do
                {
                    receivedbuf = client.Receive(receivbuf);
                } while (receivedbuf == 0);
                Console.WriteLine("Something received");

                command = Encoding.ASCII.GetString(receivbuf.Take(receivedbuf-1).ToArray());
                Console.WriteLine(command);


                String FileName;

           switch (command)
                {
                    case "1":
                 
                        Console.WriteLine("Sound 1");
                         FileName = textBox2.Text;

                        Player.URL = FileName;
                        Player.controls.play();
                        break;
                    case "2":
                        //play sound 2
                        Console.WriteLine("Sound 2");
                        FileName = textBox3.Text;
                        Player.URL = FileName;
                        Player.controls.play();
                        break;
                    case "3":
                        //play sound 3
                        Console.WriteLine("Sound 3");
                        FileName = textBox4.Text;
                        Player.URL = FileName; 
                        Player.controls.play();
                        break;
                    case "4":
                        //play sound 4
                        Console.WriteLine("Sound 4");
                        FileName = textBox5.Text;
                        Player.URL = FileName;
                        Player.controls.play();
                        break;
                    case "5":
                        //play sound 5
                        Console.WriteLine("Sound 5");
                        FileName = textBox6.Text;
                        Player.URL = FileName;
                        Player.controls.play();
                        break;
                    case "6":
                        //play sound 6
                        Console.WriteLine("Sound 6");
                        FileName = textBox7.Text;
                        Player.URL = FileName;
                        Player.controls.play();
                        break;
                    case "7":
                        //play sound 7
                        Console.WriteLine("Sound 7");
                        FileName = textBox8.Text;
                        Player.URL = FileName;
                        Player.controls.play();
                        break;
                    case "8":
                        //play sound 8
                        Console.WriteLine("Sound 8");
                        FileName = textBox9.Text;
                        Player.URL = FileName;
                        Player.controls.play();
                        break;
                    case "9":
                        //play sound 9
                        Console.WriteLine("Sound 9");
                        FileName = textBox10.Text;
                        Player.URL = FileName;
                        Player.controls.play();
                        break;
                    case "10":
                        //experimental
                       int amountreceived = 0;
                        byte[] received = new byte[256];
                       
                       
                       
                            do
                            {
                                amountreceived = client.Receive(received);
                            } while (amountreceived == 0);
                            url = Encoding.ASCII.GetString(received.Take(amountreceived - 1).ToArray());
                        
                        var th = new Thread(() => {
                            Form f2 = new Form2();
                        f2.Show();
                            
                            Application.Run();
                            
                           
                        });
                       
                            th.SetApartmentState(ApartmentState.STA);
                            th.Start();
                        while (url != "0") {
                            continue;
                        }
                        th.Abort();
                        Console.WriteLine("Browser terminated");
                        break;

                    

                    default:
                        Console.WriteLine("Invalid command: {0}", command);
                        Console.WriteLine(command.GetType());
                        break;
                }
                
            } while (command != "0");
            client.Close();
            label3.Invoke((MethodInvoker)(() =>
            {
                label3.Text = String.Format("Client disconnected manually");
                label3.ForeColor = Color.Red;
            }));
            return;

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string hostname = Dns.GetHostName();
            label2.Text = Dns.GetHostByName(hostname).AddressList[2].ToString();

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }
        bool islocked = false;
        private void button3_Click(object sender, EventArgs e)
        {
            if (islocked == false)
            {
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = true;
                textBox6.ReadOnly = true;
                textBox7.ReadOnly = true;
                textBox8.ReadOnly = true;
                textBox9.ReadOnly = true;
                textBox10.ReadOnly = true;
                islocked = true;
                button3.Text = "Unlock sources";
            }
            else
            {
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
                textBox4.ReadOnly = false;
                textBox5.ReadOnly = false;
                textBox6.ReadOnly = false;
                textBox7.ReadOnly = false;
                textBox8.ReadOnly = false;
                textBox9.ReadOnly = false;
                textBox10.ReadOnly = false;
                islocked = false;
                button3.Text = "Lock sources";
            }

        }

       

        private void textBox2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void textBox2_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop);
            textBox2.Text = FileList[0];

        }

        private void textBox3_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop);
            textBox3.Text = FileList[0];
        }

        private void textBox3_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void textBox4_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void textBox4_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop);
            textBox4.Text = FileList[0];
        }

        private void textBox5_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop);
            textBox5.Text = FileList[0];
        }

        private void textBox5_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void textBox6_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop);
            textBox6.Text = FileList[0];
        }

        private void textBox6_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void textBox7_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop);
            textBox7.Text = FileList[0];
        }

        private void textBox7_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void textBox8_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop);
            textBox8.Text = FileList[0];
        }

        private void textBox8_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void textBox9_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop);
            textBox9.Text = FileList[0];
        }

        private void textBox9_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void textBox10_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop);
            textBox10.Text = FileList[0];
        }

        private void textBox10_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
