using System;
using System.Drawing; // Required for Colors and UI styling
using System.Net; // Required for DNS and IP operations
using System.Net.NetworkInformation; // Required for Ping
using System.Net.Sockets; // Required for AddressFamily
using System.Windows.Forms;

namespace NetzwerkDiagnoseTool
{
    public partial class Form1 : Form
    {
        // --- COLOR PALETTE (Modern Dark Theme) ---
        private readonly Color colorBackground = Color.FromArgb(32, 33, 36); // Dark Gray Background
        private readonly Color colorPanel = Color.FromArgb(45, 45, 48);      // Lighter Gray for Panels
        private readonly Color colorText = Color.White;                  // White Text
        private Color colorAccent = Color.FromArgb(0, 122, 204);     // Visual Studio Blue (Accent)
        private readonly Color colorSuccess = Color.FromArgb(92, 184, 92);   // Green (Success)
        private readonly Color colorError = Color.FromArgb(217, 83, 79);     // Red (Error)

        public Form1()
        {
            InitializeComponent();
            ApplyModernDesign(); // Apply the custom dark theme on startup
        }

        // --- UI DESIGN METHOD ---
        // This method overrides the default Windows look with our custom style.
        private void ApplyModernDesign()
        {
            // 1. Main Form Settings
            this.BackColor = colorBackground;
            this.ForeColor = colorText;
            this.Text = "Network Diagnostic Tool 2026"; // Window Title
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Prevent resizing for a cleaner look
            this.MaximizeBox = false;

            // 2. Header Panel Styling
            if (pnlHeader != null)
            {
                pnlHeader.BackColor = colorAccent;
                pnlHeader.Dock = DockStyle.Top;
                pnlHeader.Height = 60;
            }

            // 3. Title Label Styling
            if (lblTitle != null)
            {
                lblTitle.ForeColor = Color.White;
                lblTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                // Center the label vertically in the panel (approximate)
                lblTitle.Location = new Point(20, 15);
                lblTitle.Text = "NETWORK DIAGNOSTIC TOOL";
            }

            // 4. Output Box (RichTextBox) Styling - "Terminal Look"
            if (rtbOutput != null)
            {
                rtbOutput.BackColor = colorPanel;
                rtbOutput.ForeColor = Color.LightGreen; // Hacker-style green text
                rtbOutput.Font = new Font("Consolas", 10); // Monospaced font for code readability
                rtbOutput.BorderStyle = BorderStyle.None;
            }

            // 5. Button Styling
            StyleButton(btnPing);
            StyleButton(btnSysInfo);
        }

        // Helper method to apply consistent style to buttons
        private void StyleButton(Button btn)
        {
            if (btn == null) return;

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = colorPanel;
            btn.ForeColor = colorText;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.Cursor = Cursors.Hand; // Change cursor to hand on hover

            // Add Hover Effects (Mouse Enter/Leave events)
            btn.MouseEnter += (s, e) => btn.BackColor = colorAccent;
            btn.MouseLeave += (s, e) => btn.BackColor = colorPanel;
        }

        // --- LOGIC: PING FUNCTION ---
        private async void btnPing_Click(object sender, EventArgs e)
        {
            string host = txtHost.Text;

            // Validation: Check if input is empty
            if (string.IsNullOrWhiteSpace(host))
            {
                Log("Error: Please enter a valid host address!", colorError);
                return;
            }

            Log($"Pinging target: {host}...", colorAccent);
            Ping pinger = new Ping();

            try
            {
                // Send Ping asynchronously with a 2000ms timeout
                PingReply reply = await pinger.SendPingAsync(host, 2000);

                if (reply.Status == IPStatus.Success)
                {
                    Log($"✔ SUCCESS: {host}", colorSuccess);
                    Log($"   IP Address: {reply.Address}", colorText);
                    Log($"   Roundtrip Time: {reply.RoundtripTime} ms", colorText);
                    Log($"   TTL: {reply.Options?.Ttl}", colorText);
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

        // --- LOGIC: SYSTEM INFO FUNCTION ---
        private void btnSysInfo_Click(object sender, EventArgs e)
        {
            Log("Scanning local system information...", colorAccent);
            try
            {
                // Get Hostname
                string hostName = Dns.GetHostName();
                Log($"💻 Hostname: {hostName}", colorText);

                // Get IP Addresses
                var ipEntry = Dns.GetHostEntry(hostName);
                foreach (var ip in ipEntry.AddressList)
                {
                    // Filter for IPv4 addresses only (Standard format)
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        Log($"🌐 Local IP (v4): {ip}", colorSuccess);
                    }
                }

                // Get OS Version
                Log($"⚙️ OS Version: {Environment.OSVersion}", colorText);
            }
            catch (Exception ex)
            {
                Log($"ERROR: {ex.Message}", colorError);
            }
            Log("------------------------------------------------", colorPanel);
        }

        // --- HELPER: LOGGING TO SCREEN ---
        // Appends colored text to the output box
        private void Log(string text, Color color)
        {
            rtbOutput.SelectionStart = rtbOutput.TextLength;
            rtbOutput.SelectionLength = 0;
            rtbOutput.SelectionColor = color;
            rtbOutput.AppendText(text + "\n");
            rtbOutput.ScrollToCaret(); // Auto-scroll to bottom
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}