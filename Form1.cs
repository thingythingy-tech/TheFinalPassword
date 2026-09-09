using TheFinalPassword.Managers;

namespace TheFinalPassword;
using System.Drawing;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        // Initialise the managers used by the game and pass the required objects
        // between them so each class has responsibility for a specific part of the game.
        fileSystemManager = new FileSystemManager(player);
        networkManager = new NetworkManager();
        missionManager = new MissionManager();
        SetupUI();
        lblMissionDisplay.Text = missionManager.GetDisplayText();
        commandParser = new CommandParser(fileSystemManager, player,networkManager);
    }

    // Store references to the controls and managers so they can be updated
    // while the game is running.
    private RichTextBox txtTerminalOutput = null!;
    private TextBox txtCommandInput = null!;
    private Label lblMissionDisplay = null!;
    private Label lblStatusMessage = null!;
    private CommandParser commandParser = null!;
    private Player player = new Player();
    private FileSystemManager fileSystemManager = null!;
    private NetworkManager networkManager = null!;
    private MissionManager missionManager = null!;

    // Creates the terminal interface programmatically so the layout and appearance
    // of the game can be controlled from one method.
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

        // Add each control to the form so it becomes visible to the player.
        this.Controls.Add(txtTerminalOutput);
        this.Controls.Add(lblMissionDisplay);
        this.Controls.Add(lblStatusMessage);
        this.Controls.Add(txtCommandInput);
    }

    // Processes the player's command when Enter is pressed, sends it to the
    // CommandParser and displays the returned response in the terminal.
    private void TxtCommandInput_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            string input = txtCommandInput.Text;
            txtTerminalOutput.AppendText("> " + input + "\n");
            string response = commandParser.ProcessCommand(input);

            // Check whether the player's command has completed the current mission.
            // This keeps mission progression separate from the command processing itself.
            if (CompletesCurrentMission(input, response))
            {
                response += "\n" + missionManager.CompleteCurrentMission();
                lblMissionDisplay.Text = missionManager.GetDisplayText();
            }

            txtTerminalOutput.AppendText(response + "\n");
            txtCommandInput.Clear();
            e.SuppressKeyPress = true; // stops the "ding" sound
        }
    }

    private static bool CompletesCurrentMission(string input, string response)
    {
        string[] parts = input.Trim().Split(' ', 2);
        return parts.Length == 2
               && parts[0].Equals("open", StringComparison.OrdinalIgnoreCase)
               && parts[1].Equals("note1.txt", StringComparison.OrdinalIgnoreCase)
               && !response.StartsWith("File '", StringComparison.OrdinalIgnoreCase);
    }
}
