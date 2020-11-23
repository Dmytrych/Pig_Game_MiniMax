using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice.AI
{
    class PlayerTurn
    {
        /// <summary>
        /// Chance of not loosing in the next game always is pow(5/6, n),
        /// where n is the count od wins in the current turn.
        /// </summary>
        public float NextRollNotLoseChance { get; set; }
        public int RollsDone { get; set; }
        private int currentPlayer = 1;
        public int CurrentPlayer
        {
            get
            {
                return currentPlayer;
            }
            set
            {
                if (value == 1 || value == 2)
                {
                    currentPlayer = value;
                }
                else
                {
                    throw new Exception("Player property only takes two numbers: 1 or 2");
                }
            }
        }

        public PlayerTurn(float notLoseChance, int currentPlayer, int rollsDone)
        {
            CurrentPlayer = currentPlayer;
            NextRollNotLoseChance = notLoseChance;
            RollsDone = rollsDone;
        }
    }
}
