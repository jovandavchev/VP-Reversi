using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace VP_Reversi
{
     [Serializable]
    public enum Type
    {
        Human,
        Easy,
        Hard
    }

    public class Player
    {
        public string name { get; set; }
        public Type type { get; set; }
        public Color color { get; set; }
        public bool canMove { get; set; }

        public Player(string n)
        {
            name = n;
            canMove = true;
        }

    }
}
