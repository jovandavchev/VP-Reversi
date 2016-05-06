using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace VP_Reversi
{
    [Serializable]
    public class RankedPlayers : IComparable<RankedPlayers>
    {
        public string name;
        public string name2;
        public string result;
        public int winnerpoints;
        public int loserpoints;

        public RankedPlayers(string s,string s2, int a, int b)
        {
            name = s;
            name2 = s2;
            winnerpoints = a;
            loserpoints = b;
            result = a + " - " + b;
        }

        public int CompareTo(RankedPlayers other)
        {
            if ( (loserpoints == other.loserpoints) && loserpoints==0)
            {
                if (winnerpoints > other.winnerpoints) return 1;
                return -1;
            }
            if (loserpoints == 0) return -1;
            if (other.loserpoints == 0) return 1;
            return winnerpoints.CompareTo(other.winnerpoints);
        }

        public override string ToString()
        {
            return string.Format("{0} vs {1}   ( {2} )", name, name2, result);
        }
    }
}
