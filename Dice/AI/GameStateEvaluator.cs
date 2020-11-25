using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice.AI
{
    static class GameStateEvaluator
    {
        public static int Evaluate(GameState state)
        {
            return EvaluateNext(state);
        }

        private static int EvaluateNext(GameState state)
        {
            int sumPoints = 0,
                deltaPoints = 0;

            foreach (var nextState in state.AvaliableStates)
            {
                sumPoints += EvaluateNext(nextState);
            }
            
            if (state.Turn.CurrentPlayer == 1 && state.PreviousState.Turn.CurrentPlayer == 1)
            {
                deltaPoints -= (int)((state.Player1Score - state.PreviousState.Player1Score) * state.Turn.NextRollNotLoseChance / 1.1f);
            }
            else if (state.Turn.CurrentPlayer == 2 && state.PreviousState.Turn.CurrentPlayer == 2)
            {
                deltaPoints += (int)((state.Player2Score - state.PreviousState.Player2Score) * state.Turn.NextRollNotLoseChance);
            }
            
            if(deltaPoints == 0) // if it is a turn skip
            {
                deltaPoints += (int)((1 - state.Turn.NextRollNotLoseChance) * 50); // making it more attractive for bot
            }
            sumPoints += deltaPoints;

            if(state.Player1Score >= 100)
            {
                sumPoints -= 100;
            }
            else if(state.Player2Score >= 100)
            {
                sumPoints += 100;
            }
            state.Value = sumPoints;
            return sumPoints;
        }
    }
}
