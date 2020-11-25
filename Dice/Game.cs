using Dice.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public GameState CurrentState { get; set; }
        private TurnScoreManager TurnScoreControl { get; set; }
        public delegate void WinEventHandler(IPlayer player);
        public event WinEventHandler WinEvent;
        public delegate void NextTurnEventHandler(GameState newState);
        public event NextTurnEventHandler NextTurnEvent;
        public Game(IPlayer player1, IPlayer player2)
        {
            Player1 = player1;
            Player2 = player2;
            TurnControl = new TurnManager(this);
            TurnScoreControl = new TurnScoreManager(this);
        }
        public GameState ReturnGameState()
        {
            float notLooseChance = 5f / 6f;
            int player = TurnControl.CurrentPlayer == Player1 ? 1 : 2,
                rollsDone = 0,
                deltaPoints = 0;

            if (CurrentState != null && player == CurrentState.Turn.CurrentPlayer)
            {
                notLooseChance = CurrentState.Turn.NextRollNotLoseChance * 5f / 6f;
                rollsDone = CurrentState.Turn.RollsDone + 1;
            }

            PlayerTurn turn = new PlayerTurn(notLooseChance, player, rollsDone, deltaPoints);
            GameState state = new GameState(null, Player1.Score, Player2.Score, turn);
            CurrentState = state;
            return state;
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
                WinEvent(Player1);
            }
            else if(Player2.Score >= WinPointQuantity)
            {
                WinEvent(Player2);
            }
            NextTurnEvent(ReturnGameState());
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

                NextTurnEvent(ReturnGameState());
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
