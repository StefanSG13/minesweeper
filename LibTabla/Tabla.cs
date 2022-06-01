using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibTabla
{
    public class Tabla
    {
        public int Size { get; set; }
        public Cell[,] Grid { get; set; }

        public Tabla(int size)
        {
            Size = size;
            Grid = new Cell[Size, Size];
            for(int i = 0; i < Size; i++)
            {
                for(int j = 0; j < Size;j++)
                {
                    Grid[i, j] = new Cell(i,j);
                }
            }
        }

        public void PlaceBombs (int nrbombe)
        {
            int ct = 0,x,y;
            Random r = new Random();
            Random r2 = new Random();
            while (ct < nrbombe)
            {
                x=r.Next();
                y=r2.Next();
                if(Grid[x, y].Bomba == false)
                {
                    ct++;
                    Grid[x,y].Bomba = true;
                }
            }
        }

        public void GenerareTabla()
        {
            for(int i=0; i<Size; i++)
            {
                for(int j=0; j<Size; j++)
                {
                    if(j-1>=0&&j-1<Size)
                        if (Grid[i, j - 1].Bomba == true)
                            Grid[i, j].vecin++;
                    if (j + 1 >= 0 && j + 1 < Size)
                        if (Grid[i, j + 1].Bomba == true)
                            Grid[i, j].vecin++;
                    if (i - 1 >= 0 && i - 1 < Size)
                        if (Grid[i-1, j].Bomba == true)
                            Grid[i, j].vecin++;
                    if (i + 1 >= 0 && i + 1 < Size)
                        if (Grid[i+1, j].Bomba == true)
                            Grid[i, j].vecin++;
                    if (i + 1 >= 0 && i + 1 < Size && j + 1 >= 0 && j + 1 < Size)
                        if (Grid[i + 1, j+1].Bomba == true)
                            Grid[i, j].vecin++;
                    if (i + 1 >= 0 && i + 1 < Size && j - 1 >= 0 && j - 1 < Size)
                        if (Grid[i + 1, j - 1].Bomba == true)
                            Grid[i, j].vecin++;
                    if (i - 1 >= 0 && i - 1 < Size && j + 1 >= 0 && j + 1 < Size)
                        if (Grid[i - 1, j + 1].Bomba == true)
                            Grid[i, j].vecin++;
                    if (i - 1 >= 0 && i - 1 < Size && j - 1 >= 0 && j - 1 < Size)
                        if (Grid[i - 1, j - 1].Bomba == true)
                            Grid[i, j].vecin++;
                }
            }
        }
    }
}
