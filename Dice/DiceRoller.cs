using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice
{
    static class DiceRoller
    {
        public static IDiceRollResult RollDice(int sides)
        {
            return new DiceRollResult(new Random().Next(sides));
        }
    }
}
