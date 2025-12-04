using System;
using System.Drawing; // Required for UI colors and styling
using System.Net; // Required for DNS and IP operations
using System.Net.NetworkInformation; // Required for Ping and Traceroute
using System.Net.Sockets; // Required for AddressFamily (IPv4 filtering)
using System.Windows.Forms;

namespace NetzwerkDiagnoseTool
{
    public partial class Form1 : Form
    {
        // --- COLOR PALETTE (Modern Dark Theme) ---
        // Professional dark colors similar to VS Code or Discord
        private readonly Color colorBackground = Color.FromArgb(32, 33, 36);
        private readonly Color colorPanel = Color.FromArgb(45, 45, 48);
        private readonly Color colorText = Color.White;
        private Color colorAccent = Color.FromArgb(0, 122, 204); // Visual Studio Blue
        private readonly Color colorSuccess = Color.FromArgb(92, 184, 92); // Green
        private readonly Color colorError = Color.FromArgb(217, 83, 79);   // Red

        public Form1()
        {
            InitializeComponent();
            ApplyModernDesign(); // Initialize the custom UI theme
        }

        // --- UI DESIGN METHOD ---
        // Overrides default Windows Forms styles for a flat, modern look
        private void ApplyModernDesign()
        {
            // Main Window Settings
            this.BackColor = colorBackground;
            this.ForeColor = colorText;
            this.Text = "Network Diagnostic Tool 2026";
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Fixed size
            this.MaximizeBox = false;

            // Header Panel Styling
            if (pnlHeader != null)
            {
                pnlHeader.BackColor = colorAccent;
                pnlHeader.Dock = DockStyle.Top;
            }

            // Title Label Styling
            if (lblTitle != null)
            {
                lblTitle.ForeColor = Color.White;
                lblTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                lblTitle.Text = "NETWORK DIAGNOSTIC TOOL";
            }

            // Output Box Styling (Terminal Look)
            if (rtbOutput != null)
            {
                rtbOutput.BackColor = colorPanel;
                rtbOutput.ForeColor = Color.LightGreen; // Matrix/Terminal style
                rtbOutput.Font = new Font("Consolas", 10); // Monospaced font
                rtbOutput.BorderStyle = BorderStyle.None;
            }

            // Apply styles to all buttons
            StyleButton(btnPing);
            StyleButton(btnSysInfo);
            StyleButton(btnTrace);
        }

        // Helper to style buttons consistently
        private void StyleButton(Button btn)
        {
            if (btn == null) return;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = colorPanel;
            btn.ForeColor = colorText;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;

            // Hover Effects
            btn.MouseEnter += (s, e) => btn.BackColor = colorAccent;
            btn.MouseLeave += (s, e) => btn.BackColor = colorPanel;
        }

        // --- FEATURE 1: PING FUNCTION ---
        private async void btnPing_Click(object sender, EventArgs e)
        {
            string host = txtHost.Text;

            // Validation
            if (string.IsNullOrWhiteSpace(host))
            {
                Log("Error: Please enter a valid host!", colorError);
                return;
            }

            Log($"Pinging target: {host}...", colorAccent);
            Ping pinger = new Ping();

            try
            {
                // Async Ping (Does not freeze UI)
                PingReply reply = await pinger.SendPingAsync(host, 2000);

                if (reply.Status == IPStatus.Success)
                {
                    Log($"✔ SUCCESS: {host}", colorSuccess);
                    Log($"   IP Address: {reply.Address}", colorText);
                    Log($"   Roundtrip Time: {reply.RoundtripTime} ms", colorText);
                }
                else
                {
                    Log($"❌ FAILED: {reply.Status}", colorError);
                }
            }
            catch (Exception ex)
            {
                Log($"CRITICAL ERROR: {ex.Message}", colorError);
            }
            Log("------------------------------------------------", colorPanel);
        }

        // --- FEATURE 2: SYSTEM INFO ---
        private void btnSysInfo_Click(object sender, EventArgs e)
        {
            Log("Scanning local system information...", colorAccent);
            try
            {
                // Get Hostname
                string hostName = Dns.GetHostName();
                Log($"💻 Hostname: {hostName}", colorText);

                // Get Local IP Addresses
                var ipEntry = Dns.GetHostEntry(hostName);
                foreach (var ip in ipEntry.AddressList)
                {
                    // Filter for IPv4 only (Standard format like 192.168.x.x)
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        Log($"🌐 Local IP (v4): {ip}", colorSuccess);
                    }
                }
                Log($"⚙️ OS Version: {Environment.OSVersion}", colorText);
            }
            catch (Exception ex)
            {
                Log($"ERROR: {ex.Message}", colorError);
            }
            Log("------------------------------------------------", colorPanel);
        }

        // --- FEATURE 3: TRACEROUTE ---
        private async void btnTrace_Click(object sender, EventArgs e)
        {
            string host = txtHost.Text;

            if (string.IsNullOrWhiteSpace(host))
            {
                Log("Error: Please enter a host!", colorError);
                return;
            }

            Log($"Starting Traceroute to: {host}...", colorAccent);
            Log("Hop\tTime\tIP Address", colorText);
            Log("------------------------------------------------", colorPanel);

            Ping pinger = new Ping();
            PingOptions options = new PingOptions(1, true); // TTL control
            int maxHops = 30;
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            byte[] buffer = new byte[32];

            try
            {
                // Loop increasing TTL from 1 to 30
                for (int ttl = 1; ttl <= maxHops; ttl++)
                {
                    options.Ttl = ttl;
                    timer.Restart();

                    // Send ping with specific TTL
                    PingReply reply = await pinger.SendPingAsync(host, 5000, buffer, options);

                    timer.Stop();

                    string hopInfo = $"{ttl}\t{timer.ElapsedMilliseconds}ms\t{reply.Address}";

                    if (reply.Status == IPStatus.Success)
                    {
                        Log(hopInfo + " [Target Reached]", colorSuccess);
                        break; // Destination found
                    }
                    else if (reply.Status == IPStatus.TtlExpired)
                    {
                        Log(hopInfo, colorText); // Router found
                    }
                    else if (reply.Status == IPStatus.TimedOut)
                    {
                        Log($"{ttl}\t*\tRequest Timed Out", colorError); // Firewall block
                    }
                    else
                    {
                        Log($"{ttl}\t\t{reply.Status}", colorError);
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"Trace Error: {ex.Message}", colorError);
            }
            Log("------------------------------------------------", colorPanel);
            Log("Traceroute Complete.", colorAccent);
        }

        // --- HELPER: LOGGING TO SCREEN ---
        // Appends colored text to the RichTextBox and auto-scrolls
        private void Log(string text, Color color)
        {
            rtbOutput.SelectionStart = rtbOutput.TextLength;
            rtbOutput.SelectionLength = 0;
            rtbOutput.SelectionColor = color;
            rtbOutput.AppendText(text + "\n");
            rtbOutput.ScrollToCaret();
        }
    }
}