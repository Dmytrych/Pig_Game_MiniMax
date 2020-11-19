using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice
{
    public interface IPlayer
    {
        int Score { get; set; }
        string Name { get;}
    }
}
