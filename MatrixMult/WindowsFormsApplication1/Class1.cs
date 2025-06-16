namespace MatrixMult
{
    using System;

    public class Bruch
    {
        public long zaehler = 0;                // wird automatisch mit 0 initialisiert
        public long nenner = 1;
        string etikett = "";    // die Ref.typ-Init. auf null wird ersetzt, siehe Text

        public Bruch(long zpar, long npar, string epar)
        {
            zaehler = zpar;
            nenner = npar;
            etikett = epar;
        }
        public Bruch(long zpar, long npar)
        {
            zaehler = zpar;
            nenner = npar;
        }

        public Bruch() { }

        public long Zaehler
        {
            get
            {
                return zaehler;
            }
            set
            {
                zaehler = value;
            }
        }

        public long Nenner
        {
            get
            {
                return nenner;
            }
            set
            {
                if (value != 0)
                    nenner = value;
            }
        }

        public string Etikett
        {
            get
            {
                return etikett;
            }
            set
            {
                if (value.Length <= 40)
                    etikett = value;
                else
                    etikett = value.Substring(0, 40);
            }
        }

        public void Kuerze()
        {
            checked
            {
                // größten gemeinsamen Teiler mit dem Euklids Algorithmus bestimmen
                // (performante Variante mit Modulo-Operator)
                if (zaehler == 0)
                {
                    nenner = 1;
                }
                if (zaehler < 0 && nenner < 0)
                {
                    nenner = -nenner;
                    zaehler = -zaehler;
                }
                if (nenner < 0)
                {
                    nenner = -nenner;
                    zaehler = -zaehler;
                }
                if (zaehler != 0)
                {
                    long rest;
                    long ggt = Math.Abs(zaehler);
                    long divisor = Math.Abs(nenner);
                    do
                    {
                        rest = ggt % divisor;
                        ggt = divisor;
                        divisor = rest;
                    } while (rest > 0);
                    zaehler /= ggt;
                    nenner /= ggt;
                }
            }
        }

        public Bruch add(Bruch b)
        {
            checked
            {

                Bruch erg = new Bruch(zaehler * b.nenner + b.zaehler * nenner, nenner * b.nenner);
                erg.Kuerze();
                return erg;


            }
        }

        public Bruch mult(Bruch b)
        {
            checked
            {

                Bruch erg = new Bruch(zaehler * b.zaehler, nenner * b.nenner);
                erg.Kuerze();
                return erg;


            }
        }

        public override string ToString()
        {
            if (nenner < 0 && zaehler < 0)
            {
                zaehler = -zaehler;
                nenner = -nenner;
            }
            if (nenner == 1 || zaehler == 0)
            {
                return "" + zaehler;
            }
            else
            {
                return "" + zaehler + "/" + nenner;
            }
        }

        public override bool Equals(Object obj)
        {
            Bruch b2 = obj as Bruch;
            if (b2.zaehler == zaehler && b2.nenner == nenner) { return true; }
            return false;
        }


        public static Bruch operator -(Bruch b)
        { return new Bruch(-1 * b.zaehler, b.nenner); }

        public static Bruch operator +(Bruch b1, Bruch b2)
        { return b1.add(b2); }

        public static Bruch operator -(Bruch b1, Bruch b2)
        {
            return b1.add(new Bruch(b2.zaehler * (-1), b2.nenner));
        }

        public static Bruch operator *(Bruch b1, Bruch b2)
        { return b1.mult(b2); }

        public static Bruch operator /(Bruch b1, Bruch b2)
        {

            Bruch b3 = new Bruch(b2.nenner, b2.zaehler);

            return b1.mult(b3);
        }

        public static bool operator ==(Bruch b1, Bruch b2)
        {
            return b1.Equals(b2);
        }

        public static bool operator !=(Bruch b1, Bruch b2)
        {
            return !b1.Equals(b2);
        }




    }


}
