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
        private int CalculatingDepth { get; set; } = 10;
        public BotOpponent()
        {
            float notGettingOneChance = 5f / 6f;
            var startPlayer = 1;
            var rollsDone = 0;
            var initialTurn = new PlayerTurn(notGettingOneChance, startPlayer, rollsDone);
            CurrentState = new GameState(null, 0, 0, initialTurn);
            CalculateNextGameStatesRecursive(CurrentState, CalculatingDepth);
        }

        /// <summary>
        /// Recursive method of calculating the game states tree for Mini-Max algorythm. 
        /// If it's already calculated - calculates till gets the needed depth.
        /// </summary>
        private void CalculateNextGameStatesRecursive(GameState state, int depth)
        {
            if(depth == 0)
            {
                return;
            }
            if(state.AvaliableStates.Count == 0)  //if it wasn't calculated
            {
                float notLoseChance = state.CurrentTurn.NextRollNotLoseChance * 5f / 6f;
                int player1Score,
                    player2Score,
                    rollsDone,
                    currentPlayerTurn = state.CurrentTurn.CurrentPlayer;

                for (int diceRoll = 6; diceRoll > 0; diceRoll--)    //picking every possible number
                {
                    player1Score = state.Player1Score;
                    player2Score = state.Player2Score;


                    if (diceRoll != 1)   //if not missed all points add score
                    {
                        if (state.CurrentTurn.CurrentPlayer == 1)
                        {
                            player1Score += diceRoll;
                        }
                        else
                        {
                            player2Score += diceRoll;
                        }
                        rollsDone = state.CurrentTurn.RollsDone + 1;
                    }
                    else
                    {
                        currentPlayerTurn = SwitchPlayer(state.CurrentTurn.CurrentPlayer);   //if getting 1 - going to next players turn
                        notLoseChance = 5f / 6f;  //and switching turn values to defaults
                        rollsDone = 0;
                    }
                    var newTurnInfo = new PlayerTurn(notLoseChance, currentPlayerTurn, rollsDone);
                    var newState = new GameState(state, player1Score, player2Score, newTurnInfo);

                    state.AvaliableStates.Add(newState);    //adding a current state to the tree
                }

                if (state.CurrentTurn.RollsDone > 0) //if alredy rolled a dice on this turn
                {
                    currentPlayerTurn = SwitchPlayer(state.CurrentTurn.CurrentPlayer); //creating a turn skip case
                    var turnInfo = new PlayerTurn(5f / 6f, currentPlayerTurn, 0);
                    var skipTurnState = new GameState(state, state.Player1Score, state.Player2Score, turnInfo);    //if player skips the turn

                    state.AvaliableStates.Add(skipTurnState);    //adding a new state to the tree
                }
            }

            CalculateFurtherStates(state.AvaliableStates, depth - 1);
        }
        /// <summary>
        /// Calculates further game states for each possible state of the given
        /// </summary>
        public void CalculateFurtherStates(List<GameState> states, int depth)
        {
            foreach (var gameState in states)     //for every found game state
            {
                if (gameState.Player1Score < 100 && gameState.Player2Score < 100)  //calcualting a solution tree branch
                {
                    CalculateNextGameStatesRecursive(gameState, depth);
                }
                else
                {
                    return;
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
