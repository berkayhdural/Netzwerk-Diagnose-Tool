using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;


namespace NetzwerkDiagnoseTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtHost_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void btnPing_Click(object sender, EventArgs e)
        {
            // Get the hostname/IP from the text box.
            string host = txtHost.Text;

            // Validate Check if the text box is empty or whitespace.
            if (string.IsNullOrWhiteSpace(host))
            {
                //Show error message in the output box.
                rtbOutput.AppendText("Error: Please enter a valid address (e.g. google.com)\n");
                return; //Stop execution. 
            }

            //Inform user that the process has started.
            rtbOutput.AppendText($"Pinging {host}...\n");

            // Create a new Ping object instance.
            Ping myPingSender = new Ping();

            try
            {
                //Send ping asynchronously and await the reply. 1000ms timeout.
                PingReply reply = await myPingSender.SendPingAsync(host, 1000);

                // Check if the ping was successful.
                if (reply.Status == IPStatus.Success)
                {
                 
                    rtbOutput.AppendText($"Success! IP: {reply.Address}\n");

                    rtbOutput.AppendText($"Time: {reply.RoundtripTime} ms\n");

                    rtbOutput.AppendText("--------------------\n");
                }
                else
                {
                    rtbOutput.AppendText($"Failed. Status: {reply.Status}\n");
                }
            }
            catch (Exception ex)
            {
                rtbOutput.AppendText($"System Error: {ex.Message}\n");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
