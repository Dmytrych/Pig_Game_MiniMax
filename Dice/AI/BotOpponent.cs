using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Dice.AI
{
    public class BotOpponent
    {
        public GameState CurrentState { get; set; }
        private int CalculatingDepth { get; set; } = 8;
        private event Action OnRollDecision;
        private event Action OnSkipTurnDecision;
        public BotOpponent(Action roll, Action skipTurn)
        {
            OnRollDecision += roll;
            OnSkipTurnDecision += skipTurn;

            float notGettingOneChance = 5f / 6f;
            var startPlayer = 1;
            var rollsDone = 0;
            var initialTurn = new PlayerTurn(notGettingOneChance, startPlayer, rollsDone, 0);
            CurrentState = new GameState(null, 0, 0, initialTurn);

            CalculateNextGameStatesRecursive(CurrentState, CalculatingDepth);
        }

        public void PlayTurn(GameState state)
        {
            CurrentState = state;
            if (CurrentState.Turn.CurrentPlayer == 2)
            {
                CalculateNextGameStatesRecursive(CurrentState, CalculatingDepth);
                foreach (var nextState in state.AvaliableStates)
                {
                    EvaluateState(nextState);
                }

                MakeDecision()();
            }
        }

        private Action MakeDecision()
        {
            Action desision;
            int pointsForRolling = 0,
                pointsForSkipping = 0;
            foreach(var state in CurrentState.AvaliableStates)
            {
                if(state.Turn.CurrentPlayer == CurrentState.Turn.CurrentPlayer)
                {
                    pointsForRolling += state.Value;
                }
                else
                {
                    pointsForSkipping += state.Value;
                }
            }
            // calculating average number points for each decision
            pointsForRolling /= 10;

            var randomDesision = new Random().Next(0, pointsForSkipping + pointsForRolling);
            if(randomDesision < pointsForRolling || CurrentState.AvaliableStates.Count == 6)
            {
                desision = OnRollDecision;
            }
            else
            {
                desision = OnSkipTurnDecision;
            }

            return desision;
        }
        /// <summary>
        /// Evaluates the game state recursively. The bigger value returns - the better
        /// </summary>
        /// <param name="gameState">Evaluated state</param>
        private int EvaluateState(GameState state)
        {
            return GameStateEvaluator.Evaluate(state);
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
                float notLoseChance = state.Turn.NextRollNotLoseChance * 5f / 6f;
                int player1Score,
                    player2Score,
                    rollsDone,
                    deltaPoints,
                    currentPlayerTurn = state.Turn.CurrentPlayer;

                for (int diceRoll = 6; diceRoll > 0; diceRoll--)    //picking every possible number
                {
                    player1Score = state.Player1Score;
                    player2Score = state.Player2Score;
                    deltaPoints = state.Turn.DeltaPoints;

                    if (diceRoll != 1)   //if not missed all points add score
                    {
                        if (state.Turn.CurrentPlayer == 1)
                        {
                            player1Score += diceRoll;
                        }
                        else
                        {
                            player2Score += diceRoll;
                        }
                        rollsDone = state.Turn.RollsDone + 1;
                        deltaPoints += diceRoll;
                    }
                    else
                    {
                        if (state.Turn.CurrentPlayer == 1)
                        {
                            player1Score -= deltaPoints;
                        }
                        else
                        {
                            player2Score -= deltaPoints;
                        }
                        deltaPoints = 0;
                        currentPlayerTurn = SwitchPlayer(state.Turn.CurrentPlayer);   //if getting 1 - going to next players turn
                        notLoseChance = 5f / 6f;  //and switching turn values to defaults
                        rollsDone = 0;
                    }
                    var newTurnInfo = new PlayerTurn(notLoseChance, currentPlayerTurn, rollsDone, deltaPoints);
                    var newState = new GameState(state, player1Score, player2Score, newTurnInfo);

                    state.AvaliableStates.Add(newState);    //adding a current state to the tree
                }

                if (state.Turn.RollsDone > 0) //if alredy rolled a dice on this turn
                {
                    currentPlayerTurn = SwitchPlayer(state.Turn.CurrentPlayer); //creating a turn skip case
                    var turnInfo = new PlayerTurn(5f / 6f, currentPlayerTurn, 0, 0);
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
    }
}
