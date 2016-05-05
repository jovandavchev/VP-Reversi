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
    public class HighScore
    {
        public List<RankedPlayers> list;

        public HighScore()
        {
            list = new List<RankedPlayers>();
        }

        public void addItem(RankedPlayers r)
        {
            list.Add(r);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (RankedPlayers rp in list)
            {
                sb.Append(rp.ToString());
                sb.Append("\n");
            }
            return sb.ToString();
        }
    }
}
