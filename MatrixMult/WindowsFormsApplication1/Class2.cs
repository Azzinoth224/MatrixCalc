using System;

namespace MatrixMult
{
    class Matrix2
    {



        protected double[,] array;
        protected String name = null;

        public Matrix2(double[,] m)
        {
            array = m;
        }

        public Matrix2(double[,] m, String name)
        {
            array = m;
            this.name = name;
        }

        public String getName()
        {
            return name;
        }


        public double[,] getArray()
        {
            return array;
        }

        public static void print(double[,] array)
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
            Matrix2.print(array);
        }

        private static bool multPossible(double[,] A, double[,] B)
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

        public bool multPossible(Matrix2 B)
        {
            return Matrix2.multPossible(array, B.getArray());
        }

        private static double[,] mult(double[,] A, double[,] B)
        {
            //Lösungsarray initialisieren
            //Größe des Lösungsarrays bestimmen
            double[,] C = new double[A.GetLength(0), B.GetLength(1)];


            //Überprüfen, ob Multiplikation durchführbar
            if (!Matrix2.multPossible(A, B))
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

                    double summe = 0;

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

        public Matrix2 mult(Matrix2 m)
        {
            return new Matrix2(Matrix2.mult(array, m.getArray()));
        }

        public Matrix2 power(long n)
        {
            if (n >= 1)
            {
                Matrix2 C = this;
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



        private static bool addPossible(double[,] A, double[,] B)
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

        public bool addPossible(Matrix2 B)
        {
            return Matrix2.addPossible(array, B.getArray());
        }

        private static double[,] add(double[,] A, double[,] B)
        {
            //Lösungsarray initialisieren
            //Größe des Lösungsarrays bestimmen
            double[,] C = new double[A.GetLength(0), A.GetLength(1)];


            //Überprüfen, ob Addition durchführbar
            if (!Matrix2.addPossible(A, B))
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

        public Matrix2 add(Matrix2 m)
        {
            return new Matrix2(Matrix2.add(array, m.getArray()));
        }

        private static double[,] skalarMult(double[,] A, double s)
        {
            //Lösungsarray initialisieren
            //Größe des Lösungsarrays bestimmen

            double[,] C = new double[A.GetLength(0), A.GetLength(1)];



            //Berechnung für jeden Eintrag (c_i_k) des Lösungsarrays C
            checked
            {
                for (int i = 0; i < A.GetLength(0); i++)
                {
                    double[] B = new double[A.GetLength(1)];
                    for (int k = 0; k < A.GetLength(1); k++)
                    {
                        C[i, k] = A[i, k] * s;
                    }

                }
            }

            //Zurückgeben des Lösungsarrays

            return C;
        }

        public Matrix2 skalarMult(double s)
        {
            return new Matrix2(Matrix2.skalarMult(array, s));
        }

        public Matrix2 transp()
        {
            Matrix2 C = new Matrix2(new double[array.GetLength(1), array.GetLength(0)]);
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

        private static string ToString(double[,] array)
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
            return Matrix2.ToString(array);
        }


        public override bool Equals(Object obj)
        {

            try
            {
                Matrix2 m = (Matrix2)obj;
                if (!m.addPossible(new Matrix2(array)))
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
            double s = 0;
            for (int i = 0; i < array.GetLength(1); i++)
            {
                s = array[a, i];
                array[a, i] = array[b, i];
                array[b, i] = s;
            }
        }

        private void addzeile(int a, double d, int b)
        {
            for (int i = 0; i < array.GetLength(1); i++)
            {
                array[a, i] = array[a, i] + array[b, i] * d;
            }
        }


        public Matrix2 ZSF()
        {
            if (this.addPossible(this))
            {
                Matrix2 m = new Matrix2((double[,])this.array.Clone());
                double[,] array = m.getArray();

                int zeilen = array.GetLength(0);
                int spalten = array.GetLength(1);
                int z = 1;

                for (int k = 0; k < spalten; k++)
                {
                    for (int i = z; i < zeilen; i++)
                    {
                        if (array[i, k] != 0)
                        {
                            if (array[i - 1, k] != 0)
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
                    if (array[zeilen - 1, f] == 0) { nullen1++; }
                    if (array[zeilen - 2, f] == 0) { nullen2++; }
                }
                if (array[zeilen - 1, spalten - 2] != 0 && array[zeilen - 2, spalten - 2] != 0 && nullen1 == nullen2 && nullen1 != 0)
                {
                    m.addzeile(zeilen - 1, -array[zeilen - 1, spalten - 2] / array[zeilen - 2, spalten - 2], zeilen - 2);
                }
                m.round(10);
                return m;
            }

            else { throw new CalculationImpossibleException(); }
        }

        private void round(int r)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int k = 0; k < array.GetLength(1); k++)
                {
                    array[i, k] = Math.Round(array[i, k], r);
                }
            }

        }




        public Matrix2 LGSLöse()
        {
            Matrix2 m = ZSF();

            int freieVar = m.FreieVar();
            if (freieVar == -1)
            {
                throw new CalculationImpossibleException();
            }
            if (freieVar >= 0)
            {


                double[,] array = m.getArray();

                int zeilen = array.GetLength(0);
                int spalten = array.GetLength(1);


                for (int i = 0; i < zeilen - 1; i++)
                {
                    for (int p = 1; (p < zeilen - i) && (p + i < spalten); p++)
                    {
                        if (array[i + p, p + i] != 0)
                        {

                            m.addzeile(i, -array[i, p + i] / array[i + p, p + i], i + p);
                        }
                    }
                }


                for (int i = 0; i < zeilen && i < spalten; i++)
                {
                    double s = array[i, i];
                    if (array[i, i] != 0)
                    {
                        for (int k = 0; k < spalten; k++)
                        {
                            array[i, k] = array[i, k] / s;
                        }
                    }
                    else if (array[i, i] == 0 && i + 1 < zeilen) { m.tausch(i, i + 1); }

                }

                m.round(8);
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
                    if (array[i, k] != 0)
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
                    if (array[i, k] != 0)
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


        public Matrix2 ZSFInv(Matrix2 inv)
        {
            if (this.addPossible(this))
            {
                Matrix2 m = new Matrix2((double[,])this.array.Clone());
                double[,] array = m.getArray();

                int zeilen = array.GetLength(0);
                int spalten = array.GetLength(1);
                int z = 1;
                double br;


                for (int k = 0; k < spalten; k++)
                {
                    for (int i = z; i < zeilen; i++)
                    {
                        if (array[i, k] != 0)
                        {
                            if (array[i - 1, k] != 0)
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
                    if (array[zeilen - 1, f] == 0) { nullen1++; }
                    if (array[zeilen - 2, f] == 0) { nullen2++; }
                }
                if (array[zeilen - 1, spalten - 2] != 0 && array[zeilen - 2, spalten - 2] != 0 && nullen1 == nullen2 && nullen1 != 0)
                {
                    br = -array[zeilen - 1, spalten - 2] / array[zeilen - 2, spalten - 2];
                    m.addzeile(zeilen - 1, br, zeilen - 2);
                    inv.addzeile(zeilen - 1, br, zeilen - 2);
                }
                m.round(10);
                return m;
            }

            else { throw new CalculationImpossibleException(); }
        }






        public Matrix2 LGSLöseInv()
        {
            if (array.GetLength(0) != (array.GetLength(1) - 1)) { throw new CalculationImpossibleException(); }
            Matrix2 inv = new Matrix2(new double[array.GetLength(0), array.GetLength(0)]);
            for (int i = 0; i < inv.array.GetLength(0); i++)
            {
                inv.array[i, i] = 1;
                for (int j = 0; j < inv.array.GetLength(0); j++)
                {
                    if (i != j) { inv.array[i, j] = 0; }
                }
            }

            Matrix2 m = ZSFInv(inv);

            int freieVar = m.FreieVar();
            if (freieVar != 0)
            {
                throw new SingulaereMatrixException();
            }
            if (freieVar == 0)
            {


                double[,] array2 = m.getArray();
                double br;

                int zeilen = array2.GetLength(0);
                int spalten = array2.GetLength(1);


                for (int i = 0; i < zeilen - 1; i++)
                {
                    for (int p = 1; (p < zeilen - i) && (p + i < spalten); p++)
                    {
                        if (array2[i + p, p + i] != 0)
                        {
                            br = -array2[i, p + i] / array2[i + p, p + i];
                            m.addzeile(i, br, i + p);
                            inv.addzeile(i, br, i + p);
                        }
                    }
                }


                for (int i = 0; i < zeilen && i < spalten; i++)
                {
                    double s = array2[i, i];
                    if (array2[i, i] != 0)
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
                    else if (array2[i, i] == 0 && i + 1 < zeilen) { m.tausch(i, i + 1); inv.tausch(i, i + 1); }

                }

                m.round(8);
                return inv;

            }
            else { return inv; }
        }



        public static Matrix2 operator -(Matrix2 m)
        { return m.skalarMult(-1); }

        public static Matrix2 operator +(Matrix2 m1, Matrix2 m2)
        { return m1.add(m2); }

        public static Matrix2 operator -(Matrix2 m1, Matrix2 m2)
        { return m1.add(m2.skalarMult(-1)); }

        public static Matrix2 operator *(Matrix2 m1, Matrix2 m2)
        { return m1.mult(m2); }

        public static Matrix2 operator *(double n, Matrix2 m)
        { return m.skalarMult(n); }

        public static Matrix2 operator *(Matrix2 m, double n)
        { return m.skalarMult(n); }

    }



}

