using Dice.AI;
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
        public PlayerTurn CurrentTurn { get; set; }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="previousState">null if initial state</param>
        /// <param name="player1Score"></param>
        /// <param name="player2Score"></param>
        /// <param name="currentTurn"></param>
        public GameState(GameState previousState ,int player1Score, int player2Score, PlayerTurn currentTurn)
        {
            AvaliableStates = new List<GameState>();
            PreviousState = previousState;
            Player1Score = player1Score;
            Player2Score = player2Score;
            CurrentTurn = currentTurn;
        }

        public void EraseStates()
        {
            AvaliableStates.Clear();
        }
    }
}
