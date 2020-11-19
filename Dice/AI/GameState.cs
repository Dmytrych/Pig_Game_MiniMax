﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice
{
    class GameState
    {
        private int currentPlayer = 1;
        public int CurrentPlayer
        {
            get
            {
                return currentPlayer;
            }
            set
            {
                if(value == 1 || value == 2)
                {
                    currentPlayer = value;
                }
                else
                {
                    throw new Exception("This property only takes two numbers: 1 or 2");
                }
            }
        }
        public int Player1_Score { get; set; }
        public int Player2_Score { get; set; }
    }
}