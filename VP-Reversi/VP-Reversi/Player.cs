using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace VP_Reversi
{

    public enum Type
    {
        Human,
        Easy,
        Hard
    }

    public class Player
    {
        public string name { get; set; }
        public Rvs rvs { get; set; }
        public Type type { get; set; }


        public Player(string n)
        {
            name = n;
            rvs = new Rvs();
        }


        public void move()
        {
            if (type==Type.Easy )
            {
                Point p = rvs.generateRandom();
                rvs.changeValue(p.X, p.Y);
                //panel2.Invalidate(true);
                rvs.changeTurn();
            }
        }

    }
}
