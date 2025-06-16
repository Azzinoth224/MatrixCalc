using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MatrixMult
{

    public partial class MatrixMult : Form
    {
        List<String> namen = new List<String>();
        List<Matrix> matrizen = new List<Matrix>();
        BindingSource bs = new BindingSource();

        public MatrixMult()
        {

            InitializeComponent();
            bs.DataSource = namen;
            comboBox6.DataSource = bs;
            comboBox3.DataSource = bs;


        }


        private void saveMatrix(RichTextBox t, String name)
        {
            if (bs.Contains(name) == false)
            {
                matrizen.Add(new Matrix(BoxToMatrix(t).getArray(), name));
                bs.Add(name);
            }
            else
            {
                matrizen.RemoveAll(delegate (Matrix bk)
                {
                    if (bk.getName() == name) { return true; }
                    else { return false; }
                });

                bs.Remove(name);

                matrizen.Add(new Matrix(BoxToMatrix(t).getArray(), name));
                bs.Add(name);


            }

        }

        private void loadMatrix(String name, RichTextBox t)
        {
            Matrix m = matrizen.Find(delegate (Matrix bk)
            {
                if (bk.getName() == name) { return true; }
                else { return false; }
            });

            pasteMatrixToBox(m, t);
        }

        private string ExtractNumbers(string strValue)
        {
            string strNummer = string.Empty;
            if (strValue.Length == 0) return "";

            foreach (char numChar in strValue.ToCharArray())
            {
                if (Char.IsNumber(numChar)) strNummer += numChar.ToString();
            }

            if (strNummer == string.Empty) return "";
            return strNummer;
        }

        private String[] splitter(String text, String sp)
        {
            string[] trenner1 = new string[1] { sp };
            string[] getrennt1 = text.Split(trenner1, StringSplitOptions.None);
            return getrennt1;
        }

        private String[][] splitter(String text, String sp, String sp2)
        {
            string[] trenner1 = new string[1] { sp };
            string[] getrennt1 = text.Split(trenner1, StringSplitOptions.None);

            string[] trenner2 = new string[1] { sp2 };
            string[][] erg = new string[getrennt1.Length][];
            for (int i = 0; i < getrennt1.Length; i++)
            {
                erg[i] = getrennt1[i].Split(trenner2, StringSplitOptions.None);
            }
            return erg;
        }

        private Matrix BoxToMatrix(RichTextBox box)
        {

            String[][] erg = splitter(box.Text, "\n", " ");
            Bruch[,] matrix = new Bruch[erg.GetLength(0), erg[0].GetLength(0)];

            try
            {
                for (int i = 0; i < erg.GetLength(0); i++)
                {
                    for (int k = 0; k < erg[0].GetLength(0); k++)
                    {
                        string[] split = splitter(erg[i][k], "/");
                        if (split.GetLength(0) <= 1)
                        {
                            string zaehler1 = split[0];
                            matrix[i, k] = new Bruch(Convert.ToInt64(zaehler1), 1);
                        }
                        else
                        {
                            string zaehler1 = split[0];
                            string nenner1 = split[1];
                            matrix[i, k] = new Bruch(Convert.ToInt64(zaehler1), Convert.ToInt64(nenner1));
                        }

                    }
                }
            }
            catch (System.FormatException)
            {
                throw new WrongFormatException();
            }
            catch (System.IndexOutOfRangeException)
            {
                throw new WrongFormatException();
            }
            return new Matrix(matrix);

        }


        private void pasteMatrixToBox(Matrix m, RichTextBox box)
        {
            Bruch[,] array = m.getArray();
            box.Text = "";
            for (int i = 0; i < array.GetLength(0); i++)
            {


                int j;
                for (j = 0; j < array.GetLength(1) - 1; j++)
                {
                    box.AppendText(array[(int)i, (int)j] + " ");
                }
                box.AppendText(array[(int)i, (int)j] + "");
                if (i < array.GetLength(0) - 1)
                {
                    box.AppendText("\n");
                }
            }

        }


        private Bruch getWert()
        {
            try
            {
                String[] erg = splitter(textBox1.Text, "/");
                Bruch n;
                if (erg.GetLength(0) == 1)
                {
                    string zaehler1 = erg[0];
                    n = new Bruch(Convert.ToInt64(erg[0]), 1);
                }
                else
                {
                    string zaehler1 = erg[0];
                    string nenner1 = erg[1];
                    n = new Bruch(Convert.ToInt64(erg[0]), Convert.ToInt64(erg[1]));
                }

                return n;
            }
            catch (Exception)
            {
                throw new NoBruchException();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 2 || comboBox1.SelectedIndex == 3)
            {
                label4.Visible = true;
                textBox1.Visible = true;
            }
            else
            {
                label4.Visible = false;
                textBox1.Visible = false;
            }
        }



        private void Weiter_Click(object sender, EventArgs e)
        {
            try
            {
                Matrix a = BoxToMatrix(BoxA);

                switch (comboBox1.SelectedIndex)
                {

                    case 0:
                        {
                            pasteMatrixToBox(a.mult(BoxToMatrix(BoxB)), BoxC); break;
                        }
                    case 1:
                        {
                            pasteMatrixToBox(a.add(BoxToMatrix(BoxB)), BoxC); break;
                        }
                    case 2:
                        {
                            pasteMatrixToBox(a.skalarMult(getWert()), BoxC); break;
                        }
                    case 3:
                        {
                            Bruch wert = getWert();
                            if (wert.nenner != 1) { throw new NoBruchException(); }
                            pasteMatrixToBox(a.power(wert.zaehler), BoxC); break;
                        }
                    case 4:
                        {
                            pasteMatrixToBox(a.transp(), BoxC); break;
                        }
                    case 5:
                        {
                            pasteMatrixToBox(a.ZSF(), BoxC); break;
                        }

                    case 6:
                        {

                            try
                            {
                                Matrix l = a.LGSLöse(); int free = l.FreieVar();
                                pasteMatrixToBox(l, BoxC); BoxC.AppendText("\nFreie Variablen: " + free);
                            }
                            catch (CalculationImpossibleException) { BoxC.Text = "Keine Lösung."; };

                            break;
                        }
                    case 7:
                        {


                            Bruch[,] arr = a.getArray();
                            Bruch[,] arrnew = new Bruch[arr.GetLength(0), arr.GetLength(1) + 1];
                            for (int i = 0; i < arr.GetLength(0); i++)
                            {
                                for (int j = 0; j < arr.GetLength(1); j++)
                                {
                                    arrnew[i, j] = arr[i, j];
                                }
                            }
                            for (int i = 0; i < arrnew.GetLength(0); i++)
                            {
                                arrnew[i, arrnew.GetLength(1) - 1] = new Bruch(0, 1);
                            }
                            Matrix inv = new Matrix(arrnew);
                            pasteMatrixToBox(inv.LGSLöseInv(), BoxC);
                            break;
                        }


                }

            }

            catch (CalculationImpossibleException)
            {
                BoxC.Text = "Fehler - Diese Berechnung ist nicht möglich. Matrix-Dimension überprüfen.";
            }
            catch (WrongFormatException)
            {
                BoxC.Text = "Fehlerhaftes Matrix-Format - Bitte nur Matrizen folgender Form eingeben:\n\nZahl Zahl Zahl\nZahl Zahl Zahl\nBsp.:\n2 5 6\n3 4 5";
            }
            catch (NoBruchException)
            {
                BoxC.Text = "Bitte als Wert einen Bruch angeben. (beim Potenzieren eine natürliche Zahl)\nBsp.: 42/4";
            }
            catch (SingulaereMatrixException)
            {
                BoxC.Text = "Singuläre Matrix";
            }
            catch (OverflowException)
            {
                BoxC.Text = "Überlauf";
            }
            catch (Exception)
            {
                BoxC.Text = "InternalError";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox2.Text == "Eingabe 1")
                {
                    saveMatrix(BoxA, comboBox3.Text);
                }
                if (comboBox2.Text == "Eingabe 2")
                {
                    saveMatrix(BoxB, comboBox3.Text);
                }
                if (comboBox2.Text == "Ausgabe")
                {
                    saveMatrix(BoxC, comboBox3.Text);
                }
            }
            catch (CalculationImpossibleException)
            {
                BoxC.Text = "Fehler - Diese Berechnung ist nicht möglich. Matrix-Dimension überprüfen.";
            }
            catch (WrongFormatException)
            {
                BoxC.Text = "Fehlerhaftes Matrix-Format - Bitte nur Matrizen folgender Form eingeben:\n\nZahl Zahl Zahl\nZahl Zahl Zahl\nBsp.:\n2 5 6\n3 4 5";
            }
            catch (NoBruchException)
            {
                BoxC.Text = "Bitte als Wert einen Bruch angeben. (beim Potenzieren eine natürliche Zahl)\nBsp.: 42/4";
            }
            catch (Exception)
            {
                BoxC.Text = "InternalError";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox5.Text == "Eingabe 1")
                {
                    loadMatrix(comboBox6.Text, BoxA);
                }
                if (comboBox5.Text == "Eingabe 2")
                {
                    loadMatrix(comboBox6.Text, BoxB);
                }
                if (comboBox5.Text == "Ausgabe")
                {
                    loadMatrix(comboBox6.Text, BoxC);
                }

            }
            catch (CalculationImpossibleException)
            {
                BoxC.Text = "Fehler - Diese Berechnung ist nicht möglich. Matrix-Dimension überprüfen.";
            }
            catch (WrongFormatException)
            {
                BoxC.Text = "Fehlerhaftes Matrix-Format - Bitte nur Matrizen folgender Form eingeben:\n\nZahl Zahl Zahl\nZahl Zahl Zahl\nBsp.:\n2 5 6\n3 4 5";
            }
            catch (NoBruchException)
            {
                BoxC.Text = "Bitte als Wert einen Bruch angeben. (beim Potenzieren eine natürliche Zahl)\nBsp.: 42/4";
            }
            catch (NullReferenceException)
            {
                BoxC.Text = "Diese Matrix existiert nicht.";
            }
            catch (Exception)
            {
                BoxC.Text = "InternalError";
            }
        }

        class WrongFormatException : System.Exception
        {
        }
        class NoDoubleException : System.Exception
        {
        }












        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




    }

}
