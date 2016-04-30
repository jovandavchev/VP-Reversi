﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VP_Reversi
{
    public class Rvs
    {
        public int[][] matrix;
        public int turn;
        public List<Point> possibleMoves;

        public Rvs(Rvs r)
        {
            matrix = r.matrix;
            turn = r.turn;
            possibleMoves = new List<Point>();
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
            Random r = new Random();
            int a = r.Next(0, possibleMoves.Count);
            return possibleMoves[a];
        }

        public Rvs()
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
                    //   Console.Write(matrix[i][j] + " ");
                }
                // Console.WriteLine();
            }
            //Console.Read();
            turn = 1;
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

        public void printMatrix()
        {
            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    Console.Write(matrix[i][j] + "\t");
                }
                Console.WriteLine();
            }

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
