using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice
{
    public class Game
    {
        private int MaxLooseNumber { get; } = 1;
        private int CubeSides { get; } = 6;
        private int WinPointQuantity { get; } = 100;
        public IPlayer Player1 { get; private set; }
        public IPlayer Player2 { get; private set; }
        private TurnManager TurnControl { get; set; }
        private TurnScoreManager TurnScoreControl { get; set; }
        public delegate void WinEventHandler(IPlayer player);
        public event WinEventHandler Win;
        public Game(IPlayer player1, IPlayer player2)
        {
            Player1 = player1;
            Player2 = player2;
            TurnControl = new TurnManager(this);
            TurnScoreControl = new TurnScoreManager(this);
        }
        /// <summary>
        /// Rolls a dice
        /// </summary>
        /// <returns></returns>
        public IDiceRollResult Roll()
        {
            var result = DiceRoller.RollDice(CubeSides);

            if (!IsLoseNumber(result.Score, MaxLooseNumber))
            {
                TurnScoreControl.AddTurnPoints(result.Score);
                TurnControl.CurrentPlayer.Score += result.Score;
            }
            else
            {
                TurnControl.CurrentPlayer.Score -= TurnScoreControl.CurrentTurnPoints;
                TurnScoreControl.ClearTurnPoints();
                TurnControl.Next();
            }

            if(Player1.Score >= WinPointQuantity)
            {
                Win(Player1);
            }
            else if(Player2.Score >= WinPointQuantity)
            {
                Win(Player2);
            }
            return result;
        }
        private static bool IsLoseNumber(int result, int maxLooseNumber)
        {
            return result <= maxLooseNumber;
        }
        /// <summary>
        /// Ends the players turn.
        /// </summary>
        public void EndTurn()
        {
            if(TurnScoreControl.CurrentTurnPoints != 0)
            {
                TurnScoreControl.ClearTurnPoints();
                TurnControl.Next();
            }
        }
        ///<summary>
        ///Returns a string with all the actual game scores
        ///</summary>
        public string GetGameState()
        {
            var gameStatus = "";

            gameStatus += "Current player: " + TurnControl.CurrentPlayer.Name + "\n";
            gameStatus += "Turn points: " + TurnScoreControl.CurrentTurnPoints + "\n";
            gameStatus += Player1.Name + " Score: " + Player1.Score + "\n";
            gameStatus += Player2.Name + " Score: " + Player2.Score + "\n";

            return gameStatus;
        }

        class TurnScoreManager
        {
            public Game Game { get; private set; }
            public int CurrentTurnPoints { get; private set; }
            public TurnScoreManager(Game game)
            {
                Game = game;
            }

            public void ClearTurnPoints()
            {
                CurrentTurnPoints = 0;
            }

            public void AddTurnPoints(int points)
            {
                CurrentTurnPoints += points;
            }
        }
        class TurnManager
        {
            public Game Game { get; private set; }
            public IPlayer CurrentPlayer { get; private set; }
            public TurnManager(Game game)
            {
                Game = game;
                CurrentPlayer = Game.Player1;
            }
            public IPlayer Next()
            {
                CurrentPlayer = CurrentPlayer == Game.Player2 ? Game.Player1 : Game.Player2;
                return CurrentPlayer;
            }
        }
    }
}
