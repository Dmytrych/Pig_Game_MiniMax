using Dice.AI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dice
{
    public partial class Form1 : Form
    {
        public Game GameInstance { get; private set; }
        public Form1()
        {
            InitializeComponent();
            startButton_Click(null, null);
            BotOpponent bot = new BotOpponent();
        }
        //Updates info screen
        public void UpdateGameInfo()
        {
            if(GameInstance != null)
            {
                gameInfoBox.Text = GameInstance.GetGameState();
            }
        }
        //Starts the new game
        private void startButton_Click(object sender, EventArgs e)
        {
            var player1 = new Player("Player1");
            var player2 = new Player("Player2");
            GameInstance = new Game(player1, player2);
            GameInstance.Win += WinAlert;
            UpdateGameInfo();
        }
        //Rolls a dice
        private void diceRollButton_Click(object sender, EventArgs e)
        {
            var diceResult = GameInstance.Roll();
            SetDicePicture(diceResult);
            UpdateGameInfo();
        }
        //Skips a turn
        private void nextTurnButton_Click(object sender, EventArgs e)
        {
            GameInstance.EndTurn();
            UpdateGameInfo();
        }

        //Dice picture change
        private void SetDicePicture(IDiceRollResult number)
        {
            Image image;
            switch (number.Score)
            {
                case 2:
                    image = Image.FromFile("pictures/two.png");
                    break;
                case 3:
                    image = Image.FromFile("pictures/three.png");
                    break;
                case 4:
                    image = Image.FromFile("pictures/four.png");
                    break;
                case 5:
                    image = Image.FromFile("pictures/five.png");
                    break;
                case 6:
                    image = Image.FromFile("pictures/six.png");
                    break;
                default:
                    image = Image.FromFile("pictures/one.png");
                    break;
            }
            pictureBox1.Image = image;
        }
        //Creates a pop-up window with congratulations
        private void WinAlert(IPlayer winPlayer)
        {
            var form = new Form();
            var congratulationLabel = new Label() { Text = winPlayer.Name + " Wins!!!!!" };
            form.Controls.Add(congratulationLabel);
            form.Show();
            startButton_Click(null, null);
        }
    }
}
