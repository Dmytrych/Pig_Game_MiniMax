using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice
{
    public class Player : IPlayer
    {
        public int Score { get; set; }
        public string Name { get;}

        public Player(string name)
        {
            Name = name;
        }
    }
}
