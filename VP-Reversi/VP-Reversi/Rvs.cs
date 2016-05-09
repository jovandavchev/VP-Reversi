using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace VP_Reversi
{
    [Serializable]
    public class Rvs
    {
        public int[][] matrix;
        public int turn;
        public Player p1 { get; set; }
        public Player p2 { get; set; }
        public List<Point> possibleMoves;
        public Point hardPoint;
        public static readonly int[,] evalTable=   {
            {0,99,  -8,  8,  6,  6,  8,  -8, 99},
            {0,99,  -8,  8,  6,  6,  8,  -8, 99},
            {0,-8, -24, -4, -3, -3, -4, -24, -8},
            { 0,8,  -4,  7,  4,  4,  7,  -4,  8},
            { 0,6,  -3,  4,  0,  0,  4,  -3,  6},
            { 0,6,  -3,  4,  0,  0,  4,  -3,  6},
            { 0,8,  -4,  7,  4,  4,  7,  -4,  8},
            {0,-8, -24, -4, -3, -3, -4, -24, -8},
            {0,99,  -8,  8,  6,  6,  8,  -8, 99}
    };

        public void move()
        {
            if (turn==1)
            {
                if (p1.canMove==false)
                {
                    changeTurn();
                    move();
                }
            }


            if (turn==2 && p2.type!=Type.Human )
            {
                if (p2.type==Type.Easy)
                {
                    Point p = generateRandom();
                    if (p.X==0 && p.Y==0)
                    {
                        changeTurn();
                        move();
                    }
                    else
                    {
                        changeValue(p.X, p.Y);
                        changeTurn();
                        move();
                    }
                }
            }
        }


        public void findPossibleMoves()
        {
            possibleMoves = new List<Point>();
            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    if (matrix[i][j] == 3) possibleMoves.Add(new Point(i, j));
                }
            }
        }

        public Point generateRandom()
        {
            findPossibleMoves();
            if (possibleMoves.Count == 0) return new Point(0, 0);
            Random r = new Random();
            int a = r.Next(0, possibleMoves.Count);
            return possibleMoves[a];
        }

        public Point bestMove()
        {
            findPossibleMoves();
            Point p = possibleMoves[0];
            int result = evalTable[possibleMoves[0].X, possibleMoves[0].Y];
            foreach (Point temp in possibleMoves )
            {
                if (evalTable[temp.X,temp.Y]>result)
                {
                    p = temp;
                    result = evalTable[temp.X, temp.Y];
                }
            }
            return p;
        }

        public Point generateBestMove()
        {
            findPossibleMoves();
            hardPoint = possibleMoves[0];
             minimax(0, turn);
            return hardPoint;
        }

        public int minimax(int depth, int turn)
        {
            if (p1.canMove == false && p2.canMove == false)
            {
                if (getSecond() > getFirst()) return 1;
                else if (getSecond() < getFirst()) return -1;
                else return 0;
            }
            findPossibleMoves();
            int min = 100000; int max = -100000;

            for (int i = 0; i < possibleMoves.Count; i++)
            {
                Point point = possibleMoves[i];
                if (turn == 2)
                {
                    changeValue(point.X, point.Y);
                    findPossibleMoves();
                    int currentScore = minimax(depth + 1, 1);
                    max = Math.Max(currentScore, max);

                    if (currentScore >= 0)
                    {
                        if (depth == 0)
                        {
                            hardPoint = point;
                        }
                    }
                    if (currentScore == 1)
                    {
                        matrix[point.X][point.Y] = 3; break;
                    }
                    if (i == possibleMoves.Count - 1 && max < 0)
                    {
                        if (depth == 0)
                        {
                            hardPoint = point;
                        }
                    }
                }

                else if (turn == 1)
                {
                    changeValue(point.X, point.Y);
                    findPossibleMoves();
                    int currentScore = minimax(depth + 1, 2);
                    min = Math.Min(currentScore, min);
                    if (min == -1)
                    {
                        matrix[point.X][point.Y] = 3;
                        break;
                    }
                }

                matrix[point.X][point.Y] = 3;

            }
            return turn == 2 ? max : min;


        }

        public Rvs(int t)
        {
            matrix = new int[9][];
            for (int i = 1; i <= 8; i++)
            {
                matrix[i] = new int[9];
                for (int j = 1; j <= 8; j++)
                {
                    if (i == j && (i == 4 || i == 5)) matrix[i][j] = 1;
                    else if ((i == 4 && j == 5) || (i == 5 && j == 4)) matrix[i][j] = 2;
                    else
                        matrix[i][j] = 0;
                }
            }
            hardPoint = Point.Empty;
            p1 = null;
            p2 = null;
            turn = t;
            addPossibleMoves();


        }
        public int getFirst()
        {
            int disks = 0;
            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    if (matrix[i][j] == 1)
                    {
                        disks++;
                    }
                }
            }
            return disks;
        }
        public int getSecond()
        {
            int disks = 0;
            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    if (matrix[i][j] == 2)
                    {
                        disks++;
                    }
                }
            }
            return disks;
        }

        public bool isValid(int i, int j)
        {
            return matrix[i][j] == 3;
        }
        

        public bool isFinished()
        {
            bool bl = false;
            int counter1 = 0;
            int counter2 = 0;
            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    if (matrix[i][j] == 0)
                    {
                        bl = true;
                    }
                    if (matrix[i][j] == 1) counter1++;
                    if (matrix[i][j] == 2) counter2++;
                }

            }

            if (bl == false) return true;
            if (counter1 == 0 || counter2 == 0) return true;

            return false;
        }

        public bool noPossibleMoves()
        {
            //addPossibleMoves();
            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    if (matrix[i][j] == 3) return false;
                }

            }
            return true;
        }

        public void changeValue(int a, int b)
        {
            matrix[a][b] = turn;
            changeOtherValues(a, b);
        }

        public void changeOtherValues(int x, int y)
        {
            if (findLeft(x, y)) changeLeft(x, y);
            if (findRight(x, y)) changeRight(x, y);
            if (findTop(x, y)) changeTop(x, y);
            if (findDown(x, y)) changeDown(x, y);
            if (findTopLeft(x, y)) changeTopLeft(x, y);
            if (findDownRight(x, y)) changeDownRight(x, y);
            if (findTopRight(x, y)) changeTopRight(x, y);
            if (findDownLeft(x, y)) changeDownLeft(x, y);
        }

        public void changeLeft(int ind1, int ind2)
        {
            ind2--;
            while (matrix[ind1][ind2] != turn)
            {
                matrix[ind1][ind2] = turn;
                ind2--;
            }
        }

        public void changeRight(int ind1, int ind2)
        {
            ind2++;
            while (matrix[ind1][ind2] != turn)
            {
                matrix[ind1][ind2] = turn;
                ind2++;
            }
        }

        public void changeTop(int ind1, int ind2)
        {
            ind1--;
            while (matrix[ind1][ind2] != turn)
            {
                matrix[ind1][ind2] = turn;
                ind1--;
            }
        }

        public void changeDown(int ind1, int ind2)
        {
            ind1++;
            while (matrix[ind1][ind2] != turn)
            {
                matrix[ind1][ind2] = turn;
                ind1++;
            }
        }

        public void changeTopLeft(int ind1, int ind2)
        {
            ind1--; ind2--;
            while (matrix[ind1][ind2] != turn)
            {
                matrix[ind1][ind2] = turn;
                ind1--; ind2--;
            }
        }

        public void draw(Graphics g, Color c1, Color c2)
        {
            //g.Clear(Color.White);
            Pen pen = new Pen(Color.Black);
            Brush br = new SolidBrush(Color.Beige);
            Brush br1 = new SolidBrush(c1);
            Brush br2 = new SolidBrush(c2);
            Pen br3 = new Pen(c1, 2);
            if (turn==2)
            {
                br3 = new Pen(c2, 2);
            }
                
            for (int i=1;i<=8;i++)
            {
                for (int j=1;j<=8;j++)
                {
                    g.FillRectangle(br, i * 35 - 15, j * 35 - 15, 35, 35);
                    g.DrawRectangle(pen, i * 35 - 15, j * 35 - 15, 35, 35);
                    if (matrix[i][j]==1)
                    {
                        g.FillEllipse(br1, i * 35 - 14, j * 35 - 14, 33, 33);
                    }
                    if (matrix[i][j] == 2)
                    {
                        g.FillEllipse(br2, i * 35 - 14, j * 35 - 14, 33, 33);
                    }
                    if (matrix[i][j]==3)
                    {
                        g.DrawEllipse(br3, i * 35 - 14, j * 35 - 14, 33, 33);
                    }
                }
            } 
            pen.Dispose();
            br1.Dispose(); br2.Dispose();
            br.Dispose(); br3.Dispose();
          //  if (getFirst() !=2 || getSecond() != 2)
        //    if (turn == 2 && p2.type != Type.Human ) Thread.Sleep(1000);
        }

        public void changeDownLeft(int ind1, int ind2)
        {
            ind1++; ind2--;
            while (matrix[ind1][ind2] != turn)
            {
                matrix[ind1][ind2] = turn;
                ind1++; ind2--;
            }
        }

        public void changeTopRight(int ind1, int ind2)
        {
            ind1--; ind2++;
            while (matrix[ind1][ind2] != turn)
            {
                matrix[ind1][ind2] = turn;
                ind1--; ind2++;
            }
        }

        public void changeDownRight(int ind1, int ind2)
        {
            ind1++; ind2++;
            while (matrix[ind1][ind2] != turn)
            {
                matrix[ind1][ind2] = turn;
                ind1++; ind2++;
            }
        }

        public void changeTurn()
        {
            if (turn == 1) turn = 2;
            else turn = 1;
            addPossibleMoves();
        }


        public void clearPossibleMoves()
        {
            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    if (matrix[i][j] == 3) matrix[i][j] = 0;
                }
            }
        }

        public void addPossibleMoves()
        {
            clearPossibleMoves();

            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    if (findPossible(i, j))
                    {
                        matrix[i][j] = 3;
                    }
                }
            }
        }

        public bool findPossible(int ind1, int ind2)
        {
            if (matrix[ind1][ind2] == 1 || matrix[ind1][ind2] == 2 || matrix[ind1][ind2] == 3) return false;

            if (findLeft(ind1, ind2)) return true;
            if (findRight(ind1, ind2)) return true;
            if (findTop(ind1, ind2)) return true;
            if (findDown(ind1, ind2)) return true;
            if (findTopLeft(ind1, ind2)) return true;
            if (findDownRight(ind1, ind2)) return true;
            if (findTopRight(ind1, ind2)) return true;
            if (findDownLeft(ind1, ind2)) return true;
            return false;
        }

        public bool findTopRight(int ind1, int ind2)
        {
            bool result = false;
            bool result2 = false;
            ind1--;
            ind2++;
            while (ind1 > 0 && ind2 < 9)
            {
                if (matrix[ind1][ind2] == 0 || matrix[ind1][ind2] == 3) return false;
                if (result && result2) return true;
                if (turn == 1)
                {
                    if (matrix[ind1][ind2] == 2)
                    {
                        result = true;
                    }
                    if (matrix[ind1][ind2] == 1)
                    {
                        result2 = true;
                        break;
                    }
                }
                if (turn == 2)
                {
                    if (matrix[ind1][ind2] == 1)
                    {
                        result = true;
                    }
                    if (matrix[ind1][ind2] == 2)
                    {
                        result2 = true;
                        break;
                    }
                }
                ind1--; ind2++;
            }
            if (result && result2) return true;
            return false;
        }

        public bool findDownLeft(int ind1, int ind2)
        {
            bool result = false;
            bool result2 = false;
            ind2--;
            ind1++;
            while (ind2 > 0 && ind1 < 9)
            {
                if (matrix[ind1][ind2] == 0 || matrix[ind1][ind2] == 3) return false;
                if (result && result2) return true;
                if (turn == 1)
                {
                    if (matrix[ind1][ind2] == 2)
                    {
                        result = true;
                    }
                    if (matrix[ind1][ind2] == 1)
                    {
                        result2 = true;
                        break;
                    }
                }
                if (turn == 2)
                {
                    if (matrix[ind1][ind2] == 1)
                    {
                        result = true;
                    }
                    if (matrix[ind1][ind2] == 2)
                    {
                        result2 = true;
                        break;
                    }
                }
                ind1++; ind2--;
            }
            if (result && result2) return true;
            return false;
        }

       

        public bool findDownRight(int ind1, int ind2)
        {
            bool result = false;
            bool result2 = false;
            ind1++;
            ind2++;
            while (ind1 < 9 && ind2 < 9)
            {
                if (matrix[ind1][ind2] == 0 || matrix[ind1][ind2] == 3) return false;
                if (result && result2) return true;
                if (turn == 1)
                {
                    if (matrix[ind1][ind2] == 2)
                    {
                        result = true;
                    }
                    if (matrix[ind1][ind2] == 1)
                    {
                        result2 = true;
                        break;
                    }
                }
                if (turn == 2)
                {
                    if (matrix[ind1][ind2] == 1)
                    {
                        result = true;
                    }
                    if (matrix[ind1][ind2] == 2)
                    {
                        result2 = true;
                        break;
                    }
                }
                ind1++; ind2++;
            }
            if (result && result2) return true;
            return false;
        }

        public bool findTopLeft(int ind1, int ind2)
        {
            bool result = false;
            bool result2 = false;
            ind1--;
            ind2--;
            while (ind1 > 0 && ind2 > 0)
            {
                if (matrix[ind1][ind2] == 0 || matrix[ind1][ind2] == 3) return false;
                if (result && result2) return true;
                if (turn == 1)
                {
                    if (matrix[ind1][ind2] == 2)
                    {
                        result = true;
                    }
                    if (matrix[ind1][ind2] == 1)
                    {
                        result2 = true;
                        break;
                    }
                }
                if (turn == 2)
                {
                    if (matrix[ind1][ind2] == 1)
                    {
                        result = true;
                    }
                    if (matrix[ind1][ind2] == 2)
                    {
                        result2 = true;
                        break;
                    }
                }
                ind1--; ind2--;
            }
            if (result && result2) return true;
            return false;
        }

        public bool findTop(int ind1, int ind2)
        {
            bool result = false;
            bool result2 = false;
            for (int i = ind1 - 1; i >= 1; i--)
            {
                if (matrix[i][ind2] == 0 || matrix[i][ind2] == 3) return false;
                if (result && result2) return true;
                if (turn == 1)
                {
                    if (matrix[i][ind2] == 2)
                    {
                        result = true;
                    }
                    if (matrix[i][ind2] == 1)
                    {
                        result2 = true;
                        break;
                    }
                }
                if (turn == 2)
                {
                    if (matrix[i][ind2] == 1)
                    {
                        result = true;
                    }
                    if (matrix[i][ind2] == 2)
                    {
                        result2 = true;
                        break;
                    }
                }
            }
            if (result && result2) return true;
            return false;
        }

        public bool findDown(int ind1, int ind2)
        {
            bool result = false;
            bool result2 = false;
            for (int i = ind1 + 1; i <= 8; i++)
            {
                if (matrix[i][ind2] == 0 || matrix[i][ind2] == 3) return false;
                if (result && result2) return true;
                if (turn == 1)
                {
                    if (matrix[i][ind2] == 2)
                    {
                        result = true;
                    }
                    if (matrix[i][ind2] == 1)
                    {
                        result2 = true;
                        break;
                    }
                }
                if (turn == 2)
                {
                    if (matrix[i][ind2] == 1)
                    {
                        result = true;
                    }
                    if (matrix[i][ind2] == 2)
                    {
                        result2 = true;
                        break;
                    }
                }
            }
            if (result && result2) return true;
            return false;
        }

        public bool findRight(int ind1, int ind2)
        {
            bool result = false;
            bool result2 = false;
            for (int i = ind2 + 1; i <= 8; i++)
            {
                if (matrix[ind1][i] == 0 || matrix[ind1][i] == 3) return false;
                if (result && result2) return true;
                if (turn == 1)
                {
                    if (matrix[ind1][i] == 2)
                    {
                        result = true;
                    }
                    if (matrix[ind1][i] == 1)
                    {
                        result2 = true;
                        break;
                    }
                }
                if (turn == 2)
                {
                    if (matrix[ind1][i] == 1)
                    {
                        result = true;
                    }
                    if (matrix[ind1][i] == 2)
                    {
                        result2 = true;
                        break;
                    }
                }
            }
            if (result && result2) return true;
            return false;
        }

        public bool findLeft(int ind1, int ind2)
        {
            bool result = false;
            bool result2 = false;
            for (int i = ind2 - 1; i >= 1; i--)
            {
                if (matrix[ind1][i] == 0 || matrix[ind1][i] == 3) return false;
                if (result && result2) return true;
                if (turn == 1)
                {
                    if (matrix[ind1][i] == 2)
                    {
                        result = true;
                    }
                    if (matrix[ind1][i] == 1)
                    {
                        result2 = true;
                        break;
                    }
                }
                if (turn == 2)
                {
                    if (matrix[ind1][i] == 1)
                    {
                        result = true;
                    }
                    if (matrix[ind1][i] == 2)
                    {
                        result2 = true;
                        break;
                    }
                }
            }
            if (result && result2) return true;
            return false;

        }

        public Point checkPoint(Point p)
        {
            for (int i=1;i<=8;i++)
            {
                for (int j=1;j<=8;j++)
                {
                    int left = i * 35 - 15;
                    int top = j * 35 - 15;
                    int right = left + 35;
                    int bottom = top + 35;
                    if (p.X>=left && p.X<=right && p.Y <= bottom && p.Y>=top    )
                    {
                        return new Point(i, j);
                    }
                }
            }

            return new Point(0,0);
        }

    }
}
