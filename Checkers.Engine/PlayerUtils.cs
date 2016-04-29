using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Engine
{
    public static class PlayerUtils
    {
        public static PlayerColor StartingPlayer()
        {
            return PlayerColor.White;
        }

        public static PlayerColor NextPlayer(PlayerColor currentPlayer)
        {
            return currentPlayer == PlayerColor.White ? PlayerColor.Black : PlayerColor.White;
        }
    }
}
