using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice
{
    class DiceRollResult : IDiceRollResult
    {
        public int Score { get; }

        public DiceRollResult(int score)
        {
            Score = score;
        }
    }
}
