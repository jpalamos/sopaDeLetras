using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SopaDeLetras
{
    class Program
    {
        static void Main(string[] args)
        {
            int cPalabras,nFil,nCol;
            
            Console.WriteLine("Ingrese la cantidad de palabras");
            cPalabras = Convert.ToInt32(Console.ReadLine());
            
            string[] palabras = new string[cPalabras];

            for (int i = 0; i < cPalabras; i++)
            {
                Console.WriteLine("Ingrese la palabra: " + (i + 1));
                palabras[i] = Console.ReadLine();
            }

            Console.WriteLine("\nIngrese número de filas");
            nFil = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Ingrese número de columnas");
            nCol = Convert.ToInt32(Console.ReadLine());

            matrix m = new matrix(nFil,nCol, palabras);
            m.construir();
            m.show();
            m.outputFile();



        }
       
        
    }

    class matrix
    {
        private int fil;
        private int col;
        private string[] st;
        private char[,] m;

        public matrix(int fil, int col, string[] st)
        {
            this.fil = fil;
            this.col = col;
            this.st = st;
        }


        private int pushRandom(string st)
        {
            var guid = Guid.NewGuid();
            var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(justNumbers.Substring(0, 4));

            var rnd = new Random(seed);
            int dirHorVerDiag, posIniX, posIniY;
            int dirSentido;
            char[,] mTemp = new char[20,20];


            if (st.Length > this.fil || st.Length > this.col)
            {
                return -1;
            }
            else 
            {
                if(m != null)
                {
                    mTemp = (char[,])m.Clone();
                }
                dirHorVerDiag = rnd.Next(2);
                posIniX = rnd.Next(this.fil);
                posIniY = rnd.Next(this.col);
                if (dirHorVerDiag == 0)
                {
                    if ((posIniX + st.Length) < this.col)
                    {
                        dirSentido = 1;
                    }
                    else if ((posIniX - st.Length) >= 0)
                    {
                        dirSentido = -1;
                    }
                    else
                    {
                        return 0;
                    }
                    for (int i = 0; i < st.Length; i++)
                    {
                        if (dirSentido == 1)
                        {
                            if (mTemp[posIniX + i,posIniY].ToString() == "\0") mTemp[posIniX + i,posIniY] = st[i];
                            else return 0;

                        }
                        else
                        {
                            if (mTemp[posIniX - i,posIniY].ToString() == "\0") mTemp[posIniX - i,posIniY] = st[i];
                            else return 0;
                        }
                    }
                }
                else if (dirHorVerDiag == 1)
                {
                    if ((posIniY + st.Length) < this.fil)
                    {
                        dirSentido = 1;
                    }
                    else if (posIniY - st.Length >= 0)
                    {
                        dirSentido = -1;
                    }
                    else
                    {
                        return 0;
                    }
                    for (int i = 0; i < st.Length; i++)
                    {
                        if (dirSentido == 1)
                        {
                            if (mTemp[posIniX,posIniY + i].ToString() == "\0") mTemp[posIniX,posIniY + i] = st[i];
                            else return 0;
                        }
                        else
                        {
                            if (mTemp[posIniX,posIniY - i].ToString() == "\0") mTemp[posIniX,posIniY - i] = st[i];
                            else return 0;
                        }
                    }
                }
            }
            this.m=(char[,])mTemp.Clone();
            return 1;
        }

        private void rellenarVacios()
        {
            var guid = Guid.NewGuid();
            var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(justNumbers.Substring(0, 4));

            var rnd = new Random(seed);

            string st = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < this.fil; i++)
            {
                for(int j = 0; j < this.col; j++)
                {
                    if (this.m[i,j].ToString() == "\0") this.m[i,j] = st[rnd.Next(0, st.Length)];
                }
            }
            
        }

        public void outputFile()
        {
            string st = "";
            for (int i = 0; i < this.fil; i++)
            {
                for (int j = 0; j < this.col; j++)
                {
                    st += this.m[i, j];
                }
                st += System.Environment.NewLine;
        }

            System.IO.File.WriteAllText(@"C:\\Users\\DELL\\source\\repos\\SopaDeLetras\\resultado.txt", st);
            
            
        }

        public void show()
        {
            string line;
            
            Console.WriteLine("\nLa sopa de letra quedó así\n");
        
            for (int i = 0; i < this.fil; i++)
            {
                line = "";
                for (int j = 0; j < this.col; j++)
                {
                    line += this.m[i,j];
                }
                Console.WriteLine(line);
            }
            Console.ReadLine();

        }

        public bool construir()
        {
            int intentos = 0;
            int salida = 0;
            foreach (string stw in st)
            {
                intentos = 0;
                salida = 0;
                while (salida == 0 && intentos<10) {
                    salida = pushRandom(stw);
                    if (salida == 0){
                        intentos++;
                    }
                    else if(salida == -1)
                    {
                        Console.WriteLine("la palabra " + stw + " es muy grande");
                    }
                    if (intentos > 9 && salida!=1)
                    {
                        Console.WriteLine("No se encuentra posición para la palabra " + stw);
                    }
                }   
            }
            rellenarVacios();
            return true;
        }
    }
}
