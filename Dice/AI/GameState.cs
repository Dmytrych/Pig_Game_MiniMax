using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice
{
    class GameState
    {
        public GameState PreviousState { get; set; }
        public List<GameState> AvaliableStates { get; private set; }
        private int currentPlayer = 1;
        /// <summary>
        /// Chance of not loosing in the next game always is pow(5/6, n),
        /// where n is the count od wins in the current turn.
        /// </summary>
        public float NextStepNotLoseChance { get; set; }
        public int CurrentPlayer
        {
            get
            {
                return currentPlayer;
            }
            set
            {
                if(value == 1 || value == 2)
                {
                    currentPlayer = value;
                }
                else
                {
                    throw new Exception("This property only takes two numbers: 1 or 2");
                }
            }
        }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="previousState">null if initial state</param>
        /// <param name="player1Score"></param>
        /// <param name="player2Score"></param>
        /// <param name="chanceOfGetting">chance of having this state</param>
        /// <param name="currentPlayerTurn">Takes only numbers 1 or 2, otherwise throws an exception</param>
        public GameState(GameState previousState ,int player1Score, int player2Score, float nextStepNotLoseChance, int currentPlayerTurn)
        {
            PreviousState = previousState;
            Player1Score = player1Score;
            Player2Score = player2Score;
            CurrentPlayer = currentPlayerTurn;
            NextStepNotLoseChance = nextStepNotLoseChance;
        }
    }
}
