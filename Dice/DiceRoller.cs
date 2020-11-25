using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pcg;

namespace Dice
{
    static class DiceRoller
    {
        public static IDiceRollResult RollDice(int sides)
        {
            return new DiceRollResult(new PcgRandom().Next(1, sides));
        }
    }
}
