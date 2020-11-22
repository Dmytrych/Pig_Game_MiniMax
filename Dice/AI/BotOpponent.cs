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

        public BotOpponent()
        {
            CurrentState = new GameState(null, 0, 0, 5 / 6, 1);
            CalculateNextGameStates(CurrentState);
        }
        /// <summary>
        /// Recursive method of calculating the game states tree for Mini-Max algorythm
        /// </summary>
        public void CalculateNextGameStates(GameState state)
        {
            float notLoseChance = state.NextStepNotLoseChance * 5 / 6;
            int player1Score,
                player2Score,
                currentPlayerTurn = state.CurrentPlayer;

            for (int diceRoll = 6; diceRoll > 0; diceRoll--)    //picking every possible number
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
                    currentPlayerTurn = SwitchPlayer(state.CurrentPlayer);   //if getting 1 - going to next players turn
                    notLoseChance = 5 / 6;
                }

                var newState = new GameState(state, player1Score, player2Score, notLoseChance, currentPlayerTurn);

                state.AvaliableStates.Add(newState);    //adding a current state to the tree
            }

            currentPlayerTurn = SwitchPlayer(state.CurrentPlayer);
            var skipTurnState = new GameState(state, state.Player1Score, state.Player2Score, 5 / 6, currentPlayerTurn);    //if player skips the turn
            state.AvaliableStates.Add(skipTurnState);    //adding a new state to the tree

            foreach(var gameState in state.AvaliableStates)     //for every found game state
            {
                if (gameState.Player1Score < 100 && gameState.Player2Score < 100)  //calcualting a solution tree branch
                {
                    CalculateNextGameStates(gameState);
                }
            }
        }

        private static int SwitchPlayer(int currentPlayer)
        {
            return currentPlayer == 1 ? 2 : 1;
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
