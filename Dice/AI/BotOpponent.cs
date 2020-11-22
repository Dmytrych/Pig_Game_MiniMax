using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Dice.AI
{
    class BotOpponent
    {
        private GameState CurrentState { get; set; }
        /// <summary>
        /// Recursive method of calculating the game states tree for Mini-Max algorythm
        /// </summary>
        public void CalculateNextGameStates(GameState state)
        {
            float notLoseChance = state.NextStepNotLoseChance * 5 / 6;
            int player1Score,
                player2Score,
                currentPlayerTurn = state.CurrentPlayer;

            for (int diceRoll = 6; diceRoll > 0; diceRoll--)          //picking every possible number
            {
                player1Score = state.Player1Score;
                player2Score = state.Player2Score;
                
                
                if(diceRoll != 1)   //if not missed all points add score
                {
                    if (state.CurrentPlayer == 1)
                    {
                        player1Score += diceRoll;
                    }
                    else
                    {
                        player2Score += diceRoll;
                    }
                }
                else
                {
                    currentPlayerTurn = state.CurrentPlayer == 1 ? 2 : 1;   //if getting 1 - to next players turn
                }

                var newState = new GameState(state, player1Score, player2Score, notLoseChance, currentPlayerTurn);

                state.AvaliableStates.Add(newState);    //adding a current state to the tree
                if(newState.Player1Score < 100 && newState.Player2Score < 100)  //calcualting tree branch for new state
                {
                    CalculateNextGameStates(newState);
                }
            }
        }
        /// <summary>
        /// Evaluates the game state.
        /// </summary>
        /// <param name="gameState">Evaluated state</param>
        private void EvaluateState(GameState gameState)
        {
            throw new NotImplementedException();
        }
    }
}
