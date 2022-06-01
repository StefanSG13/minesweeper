using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraT
{
    public class Tabla
    {
        public int Size { get; set; }
        public Cell[,] Grid { get; set; }

        public Tabla(int size)
        {
            Size = size;
            Grid = new Cell[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Grid[i, j] = new Cell(i, j);
                }
            }
        }

        public void PlaceBombs(int nrbombe)
        {
            int ct = 0, x, y;
            Random r = new Random();
            while (ct < nrbombe)
            {
                x = r.Next(0,Size);
                y = r.Next(0,Size);
                if (Grid[x, y].Bomba == false)
                {
                    ct++;
                    Grid[x, y].Bomba = true;
                    for(int i = -1;i<2;i++)
                        for(int j=-1;j<2;j++)
                            if (j != 0 || i != 0)
                                if(x+i>=0&&x+i<Size)
                                    if(y+j>=0&&y+j<Size)
                            {
                                Grid[x+i, y+j].vecin++;
                            }
                }
            }
        }

       /* public void GenerareTabla()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (j - 1 >= 0 && j - 1 < Size)
                        if (Grid[i, j - 1].Bomba == true)
                            Grid[i, j].vecin++;
                    if (j + 1 >= 0 && j + 1 < Size)
                        if (Grid[i, j + 1].Bomba == true)
                            Grid[i, j].vecin++;
                    if (i - 1 >= 0 && i - 1 < Size)
                        if (Grid[i - 1, j].Bomba == true)
                            Grid[i, j].vecin++;
                    if (i + 1 >= 0 && i + 1 < Size)
                        if (Grid[i + 1, j].Bomba == true)
                            Grid[i, j].vecin++;
                    if (i + 1 >= 0 && i + 1 < Size && j + 1 >= 0 && j + 1 < Size)
                        if (Grid[i + 1, j + 1].Bomba == true)
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
        }*/

        public void wipe()
        {
            for(int i=0; i < Size; i++)
            {
                for(int j=0; j < Size; j++)
                {
                    Grid[i,j].Bomba = false;
                    Grid[i, j].vecin = 0;
                }
            }
        }
    }
}
