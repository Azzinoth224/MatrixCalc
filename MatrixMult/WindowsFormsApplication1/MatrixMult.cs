using System;

namespace MatrixMult
{
    class Matrix
    {



        protected Bruch[,] array;
        protected String name = null;

        public Matrix(Bruch[,] m)
        {
            array = m;
        }

        public Matrix(Bruch[,] m, String name)
        {
            array = m;
            this.name = name;
        }

        public String getName()
        {
            return name;
        }


        public Bruch[,] getArray()
        {
            return array;
        }

        public static void print(Bruch[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                Console.Write("{");
                int j;
                for (j = 0; j < array.GetLength(1) - 1; j++)
                {
                    Console.Write(array[(int)i, (int)j] + " , ");
                }
                Console.Write(array[(int)i, (int)j] + "");
                Console.WriteLine("}");
            }
        }

        public void print()
        {
            Matrix.print(array);
        }

        private static bool multPossible(Bruch[,] A, Bruch[,] B)
        {
            for (int i = 0; i < A.GetLength(0); i++)
            {
                if (A.GetLength(1) != B.GetLength(0))
                {
                    return false;
                }
            }
            return true;
        }

        public bool multPossible(Matrix B)
        {
            return Matrix.multPossible(array, B.getArray());
        }

        private static Bruch[,] mult(Bruch[,] A, Bruch[,] B)
        {
            //Lösungsarray initialisieren
            //Größe des Lösungsarrays bestimmen
            Bruch[,] C = new Bruch[A.GetLength(0), B.GetLength(1)];


            //Überprüfen, ob Multiplikation durchführbar
            if (!Matrix.multPossible(A, B))
            {
                //Wenn nicht, Exception werfen
                throw new CalculationImpossibleException();

            }


            //Wenn doch:
            //Berechnung für jeden Eintrag (c_i_k) der Lösungsarray C

            for (int i = 0; i < C.GetLength(0); i++)
            {
                for (int k = 0; k < C.GetLength(1); k++)
                {
                    //Anwenden der Formel c_i_k = summe (a_i_j * b_j_k) von j=0 bis m-1
                    //wobei c_i_k das Element in der i-ten Zeile und k-ten Spalte von C beschreibt und 
                    //m der Anzahl der Spalten in A entspricht

                    Bruch summe = new Bruch(0, 1);

                    for (int j = 0; j < A.GetLength(1); j++)
                    {
                        summe += A[i, j] * B[j, k];
                    }
                    C[i, k] = summe;
                }
            }

            //Zurückgeben des Lösungsarrays

            return C;
        }

        public Matrix mult(Matrix m)
        {
            return new Matrix(Matrix.mult(array, m.getArray()));
        }

        public Matrix power(long n)
        {
            if (n >= 1)
            {
                Matrix C = this;
                int i = 0;
                while (i < n - 1)
                {
                    C = C.mult(this);
                    i++;
                }
                return C;
            }
            else
            {
                throw new CalculationImpossibleException();
            }
        }



        private static bool addPossible(Bruch[,] A, Bruch[,] B)
        {
            if (A.GetLength(0) != B.GetLength(0))
            {
                return false;
            }
            for (int i = 0; i < A.GetLength(0); i++)
            {
                if (A.GetLength(1) != B.GetLength(1))
                {
                    return false;
                }
            }

            return true;
        }

        public bool addPossible(Matrix B)
        {
            return Matrix.addPossible(array, B.getArray());
        }

        private static Bruch[,] add(Bruch[,] A, Bruch[,] B)
        {
            //Lösungsarray initialisieren
            //Größe des Lösungsarrays bestimmen
            Bruch[,] C = new Bruch[A.GetLength(0), A.GetLength(1)];


            //Überprüfen, ob Addition durchführbar
            if (!Matrix.addPossible(A, B))
            {
                //Wenn nicht, Exception werfen
                throw new CalculationImpossibleException();
            }


            //Wenn doch:
            //Berechnung für jeden Eintrag (c_i_k) des Lösungsarrays C

            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int k = 0; k < A.GetLength(1); k++)
                {
                    C[i, k] = A[i, k] + B[i, k];
                }
            }

            //Zurückgeben des Lösungsarrays

            return C;
        }

        public Matrix add(Matrix m)
        {
            return new Matrix(Matrix.add(array, m.getArray()));
        }

        private static Bruch[,] skalarMult(Bruch[,] A, Bruch s)
        {
            //Lösungsarray initialisieren
            //Größe des Lösungsarrays bestimmen

            Bruch[,] C = new Bruch[A.GetLength(0), A.GetLength(1)];



            //Berechnung für jeden Eintrag (c_i_k) des Lösungsarrays C
            checked
            {
                for (int i = 0; i < A.GetLength(0); i++)
                {
                    Bruch[] B = new Bruch[A.GetLength(1)];
                    for (int k = 0; k < A.GetLength(1); k++)
                    {
                        C[i, k] = A[i, k] * s;
                    }

                }
            }

            //Zurückgeben des Lösungsarrays

            return C;
        }

        public Matrix skalarMult(Bruch s)
        {
            return new Matrix(Matrix.skalarMult(array, s));
        }

        public Matrix transp()
        {
            Matrix C = new Matrix(new Bruch[array.GetLength(1), array.GetLength(0)]);
            if (this.addPossible(this))
            {
                for (int i = 0; i < C.getArray().GetLength(0); i++)
                {
                    for (int k = 0; k < C.getArray().GetLength(1); k++)
                    {
                        C.getArray()[i, k] = array[k, i];
                    }
                }
                return C;
            }
            else
            {
                throw new CalculationImpossibleException();
            }
        }

        private static string ToString(Bruch[,] array)
        {
            string stringa = "{";
            for (int i = 0; i < array.GetLength(0); i++)
            {
                stringa = stringa + "{";
                int j;
                for (j = 0; j < array.GetLength(1) - 1; j++)
                {
                    stringa = stringa + array[(int)i, (int)j] + ",";
                }
                if (i < array.GetLength(0) - 1)
                {
                    stringa = stringa + array[(int)i, (int)j] + "},";
                }
                else
                {
                    stringa = stringa + array[(int)i, (int)j] + "}}";
                }
            }
            return stringa;
        }



        public override string ToString()
        {
            return Matrix.ToString(array);
        }


        public override bool Equals(Object obj)
        {

            try
            {
                Matrix m = (Matrix)obj;
                if (!m.addPossible(new Matrix(array)))
                {
                    return false;
                }
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    for (int k = 0; k < array.GetLength(1); k++)
                    {
                        if (m.getArray()[i, k] != array[i, k])
                        {
                            return false;
                        }
                    }
                }
            }
            catch (InvalidCastException e)
            {
                return false;
            }
            return true;
        }

        private void tausch(int a, int b)
        {
            if (a == array.GetLength(0) - 1 && b == a + 1)
            {
                b = 0;
            }
            Bruch s = new Bruch(0, 1);
            for (int i = 0; i < array.GetLength(1); i++)
            {
                s = array[a, i];
                array[a, i] = array[b, i];
                array[b, i] = s;
            }
        }

        private void addzeile(int a, Bruch d, int b)
        {
            for (int i = 0; i < array.GetLength(1); i++)
            {
                array[a, i] = array[a, i] + array[b, i] * d;
            }
        }


        public Matrix ZSF()
        {
            if (this.addPossible(this))
            {
                Matrix m = new Matrix((Bruch[,])this.array.Clone());
                Bruch[,] array = m.getArray();

                int zeilen = array.GetLength(0);
                int spalten = array.GetLength(1);
                int z = 1;

                for (int k = 0; k < spalten; k++)
                {
                    for (int i = z; i < zeilen; i++)
                    {
                        if (array[i, k].zaehler != 0)
                        {
                            if (array[i - 1, k].zaehler != 0)
                            {

                                m.addzeile(i, -(array[i, k] / array[i - 1, k]), i - 1);
                            }
                            else
                            {
                                m.tausch(i, i - 1);
                                i = z - 1;
                            }
                        }
                    }
                    z++;
                }

                int nullen1 = 0;
                int nullen2 = 0;
                for (int f = 0; f < spalten - 2; f++)
                {
                    if (array[zeilen - 1, f].zaehler == 0) { nullen1++; }
                    if (array[zeilen - 2, f].zaehler == 0) { nullen2++; }
                }
                if (array[zeilen - 1, spalten - 2].zaehler != 0 && array[zeilen - 2, spalten - 2].zaehler != 0 && nullen1 == nullen2 && nullen1 != 0)
                {
                    m.addzeile(zeilen - 1, -array[zeilen - 1, spalten - 2] / array[zeilen - 2, spalten - 2], zeilen - 2);
                }
                //m.round(10);
                return m;
            }

            else { throw new CalculationImpossibleException(); }
        }

        /*private void round(int r)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int k = 0; k < array.GetLength(1); k++)
                {
                    array[i, k] = Math.Round(array[i, k],r);
                }
            }

        }*/




        public Matrix LGSLöse()
        {
            Matrix m = ZSF();

            int freieVar = m.FreieVar();
            if (freieVar == -1)
            {
                throw new CalculationImpossibleException();
            }
            if (freieVar >= 0)
            {


                Bruch[,] array = m.getArray();

                int zeilen = array.GetLength(0);
                int spalten = array.GetLength(1);


                for (int i = 0; i < zeilen - 1; i++)
                {
                    for (int p = 1; (p < zeilen - i) && (p + i < spalten); p++)
                    {
                        if (array[i + p, p + i].zaehler != 0)
                        {

                            m.addzeile(i, -array[i, p + i] / array[i + p, p + i], i + p);
                        }
                    }
                }


                for (int i = 0; i < zeilen && i < spalten; i++)
                {
                    Bruch s = array[i, i];
                    if (array[i, i].zaehler != 0)
                    {
                        for (int k = 0; k < spalten; k++)
                        {
                            array[i, k] = array[i, k] / s;
                        }
                    }
                    else if (array[i, i].zaehler == 0 && i + 1 < zeilen) { m.tausch(i, i + 1); }

                }

                //m.round(8);
                return m;

            }
            else { return m; }
        }

        public int FreieVar()
        {
            int nullzeilenAB = 0;
            bool nichtnullAB;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                nichtnullAB = false;
                for (int k = 0; k < array.GetLength(1); k++)
                {
                    if (array[i, k].zaehler != 0)
                    {
                        nichtnullAB = true;
                        break;
                    }
                }
                if (nichtnullAB == false)
                {
                    nullzeilenAB++;
                }
            }

            int nullzeilenA = 0;
            bool nichtnullA = false;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                nichtnullA = false;
                for (int k = 0; k < array.GetLength(1) - 1; k++)
                {
                    if (array[i, k].zaehler != 0)
                    {
                        nichtnullA = true;
                        break;
                    }
                }
                if (nichtnullA == false)
                {
                    nullzeilenA++;
                }
            }
            int rangAB = array.GetLength(0) - nullzeilenAB;
            int rangA = array.GetLength(0) - nullzeilenA;
            if (rangAB != rangA) { return -1; } //nicht Lösbar
            else { return (array.GetLength(1) - 1) - rangA; }
        }


        public Matrix ZSFInv(Matrix inv)
        {
            if (this.addPossible(this))
            {
                Matrix m = new Matrix((Bruch[,])this.array.Clone());
                Bruch[,] array = m.getArray();

                int zeilen = array.GetLength(0);
                int spalten = array.GetLength(1);
                int z = 1;
                Bruch br;


                for (int k = 0; k < spalten; k++)
                {
                    for (int i = z; i < zeilen; i++)
                    {
                        if (array[i, k].zaehler != 0)
                        {
                            if (array[i - 1, k].zaehler != 0)
                            {
                                br = -(array[i, k] / array[i - 1, k]);
                                m.addzeile(i, br, i - 1);
                                inv.addzeile(i, br, i - 1);
                            }
                            else
                            {
                                m.tausch(i, i - 1);
                                inv.tausch(i, i - 1);
                                i = z - 1;
                            }
                        }
                    }
                    z++;
                }

                int nullen1 = 0;
                int nullen2 = 0;
                for (int f = 0; f < spalten - 2; f++)
                {
                    if (array[zeilen - 1, f].zaehler == 0) { nullen1++; }
                    if (array[zeilen - 2, f].zaehler == 0) { nullen2++; }
                }
                if (array[zeilen - 1, spalten - 2].zaehler != 0 && array[zeilen - 2, spalten - 2].zaehler != 0 && nullen1 == nullen2 && nullen1 != 0)
                {
                    br = -array[zeilen - 1, spalten - 2] / array[zeilen - 2, spalten - 2];
                    m.addzeile(zeilen - 1, br, zeilen - 2);
                    inv.addzeile(zeilen - 1, br, zeilen - 2);
                }
                //m.round(10);
                return m;
            }

            else { throw new CalculationImpossibleException(); }
        }






        public Matrix LGSLöseInv()
        {
            if (array.GetLength(0) != (array.GetLength(1) - 1)) { throw new CalculationImpossibleException(); }
            Matrix inv = new Matrix(new Bruch[array.GetLength(0), array.GetLength(0)]);
            for (int i = 0; i < inv.array.GetLength(0); i++)
            {
                inv.array[i, i] = new Bruch(1, 1);
                for (int j = 0; j < inv.array.GetLength(0); j++)
                {
                    if (i != j) { inv.array[i, j] = new Bruch(0, 1); }
                }
            }

            Matrix m = ZSFInv(inv);

            int freieVar = m.FreieVar();
            if (freieVar != 0)
            {
                throw new SingulaereMatrixException();
            }
            if (freieVar == 0)
            {


                Bruch[,] array2 = m.getArray();
                Bruch br;

                int zeilen = array2.GetLength(0);
                int spalten = array2.GetLength(1);


                for (int i = 0; i < zeilen - 1; i++)
                {
                    for (int p = 1; (p < zeilen - i) && (p + i < spalten); p++)
                    {
                        if (array2[i + p, p + i].zaehler != 0)
                        {
                            br = -array2[i, p + i] / array2[i + p, p + i];
                            m.addzeile(i, br, i + p);
                            inv.addzeile(i, br, i + p);
                        }
                    }
                }


                for (int i = 0; i < zeilen && i < spalten; i++)
                {
                    Bruch s = array2[i, i];
                    if (array2[i, i].zaehler != 0)
                    {
                        for (int k = 0; k < spalten; k++)
                        {
                            array2[i, k] = array2[i, k] / s;
                        }
                        for (int k = 0; k < inv.array.GetLength(1); k++)
                        {
                            inv.array[i, k] = inv.array[i, k] / s;
                        }
                    }
                    else if (array2[i, i].zaehler == 0 && i + 1 < zeilen) { m.tausch(i, i + 1); inv.tausch(i, i + 1); }

                }

                //m.round(8);
                return inv;

            }
            else { return inv; }
        }



        public static Matrix operator -(Matrix m)
        { return m.skalarMult(new Bruch(-1, 1)); }

        public static Matrix operator +(Matrix m1, Matrix m2)
        { return m1.add(m2); }

        public static Matrix operator -(Matrix m1, Matrix m2)
        { return m1.add(m2.skalarMult(new Bruch(-1, 1))); }

        public static Matrix operator *(Matrix m1, Matrix m2)
        { return m1.mult(m2); }

        public static Matrix operator *(Bruch n, Matrix m)
        { return m.skalarMult(n); }

        public static Matrix operator *(Matrix m, Bruch n)
        { return m.skalarMult(n); }

    }

    class CalculationImpossibleException : System.Exception
    {
    }


}

