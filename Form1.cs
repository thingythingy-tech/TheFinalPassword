namespace TheFinalPassword;
using System.Drawing;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        SetupUI();
    }
    private RichTextBox txtTerminalOutput;
    private TextBox txtCommandInput;
    private Label lblMissionDisplay;
    private Label lblStatusMessage;

    private void SetupUI()
    {
        this.Text = "Terminal Puzzle Game";
        this.Width = 900;
        this.Height = 600;
        this.BackColor = Color.Black;

        txtTerminalOutput = new RichTextBox
        {
            Left = 10, Top = 10, Width = 860, Height = 420,
            BackColor = Color.Black,
            ForeColor = Color.LightGreen,
            Font = new Font("Consolas", 11),
            ReadOnly = true,
            BorderStyle = BorderStyle.FixedSingle
        };

        lblMissionDisplay = new Label
        {
            Left = 10, Top = 440, Width = 860, Height = 40,
            ForeColor = Color.Yellow,
            Font = new Font("Consolas", 10),
            Text = "Mission: (none yet)"
        };

        lblStatusMessage = new Label
        {
            Left = 10, Top = 480, Width = 860, Height = 20,
            ForeColor = Color.OrangeRed,
            Font = new Font("Consolas", 9),
            Text = ""
        };

        txtCommandInput = new TextBox
        {
            Left = 10, Top = 510, Width = 860,
            BackColor = Color.Black,
            ForeColor = Color.White,
            Font = new Font("Consolas", 11),
            BorderStyle = BorderStyle.FixedSingle
        };
        txtCommandInput.KeyDown += TxtCommandInput_KeyDown;

        this.Controls.Add(txtTerminalOutput);
        this.Controls.Add(lblMissionDisplay);
        this.Controls.Add(lblStatusMessage);
        this.Controls.Add(txtCommandInput);
    }

    private void TxtCommandInput_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            string input = txtCommandInput.Text;
            txtTerminalOutput.AppendText("> " + input + "\n");
            txtCommandInput.Clear();
            e.SuppressKeyPress = true; // stops the "ding" sound
        }
    }
}