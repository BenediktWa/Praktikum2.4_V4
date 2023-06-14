/*
 * Praktikum 2.4
 * Author: Benedikt Walessa
 * Erstellt: 12.06.2023
 * C# Datei für das Gui
 * Startet die Instanz
 * enthält die Events
*/


using Praktikum_2._3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Praktikum2._4
{
    /// <summary>
    /// Interaktion mit MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //initialize of the system
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                read(); //Einlesen aus der Datei
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler beim Einlesen der Daten!");
            }
            BerechneZweipol();
        }


        //event handler
        /// <summary>
        /// Wenn der Wert geändert wird vom Slider wird neu berechnet und der COntent vom Lable ändert sich
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slFrequency_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lbFreq.Content = (slFrequency.Value) / 10;

            BerechneZweipol();
        }


        /// <summary>
        /// Event: Sichert Daten in der Datei bei Knopfdruck von speichern
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string bau = cbType.Text;

            bool saved = false;
            string path = @"..\..\..\txt.txt";
            FileStream fs;
            using (new FileStream(path, FileMode.OpenOrCreate));

            double fre;
            fre = slFrequency.Value / 10;
            string freq = string.Format("{0}", fre);

            fs = new FileStream(path, FileMode.Append, FileAccess.Write);
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine("{0};{1};{2};{3}", tbWid.Text, tbCap.Text, freq, bau);
                sw.Flush();
                cbPreset.Items.Add(string.Format("{0};{1};{2};{3}", tbWid.Text, tbCap.Text, slFrequency.Value, bau));
            }
            return;
        }

        /// <summary>
        /// Event: Neuberechnung bei Änderung des Widerstandes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbWid_TextChanged(object sender, TextChangedEventArgs e)
        {
            BerechneZweipol();
        }

        /// <summary>
        /// Event: Neuberechnung bei Änderung der Kapazität
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCap_TextChanged(object sender, TextChangedEventArgs e)
        {
            BerechneZweipol();
        }


        //funktion Code
        /// <summary>
        /// Berechnet Parallel Zweipol
        /// </summary>
        private void BerechneParallelZweiPol()
        {
            double r;
            double f;
            double c;
            string tmp;
            bool tmp1;
            RCZweipol zppObjekt;
            string bauform = "placeholder";


            double betrag;
            double im, re;


            if (tbKomplex == null)//only activates if window is not initialized
            {
                return;
            }

            tmp = tbWid.Text;
            tmp1 = double.TryParse(tmp, out r);

            if (r < 0)
            {
                tbBetrag.Text = "";
                tbKomplex.Text = "";
                MessageBox.Show("Fehler! Der Widerstand muss eine Zahl sein!");
                //throw new ArgumentOutOfRangeException("Fehler! Der Widerstand muss eine positive Zahl sein!");
            }

            if (!tmp1)
            {
                tbBetrag.Text = "";
                tbKomplex.Text = "";
                MessageBox.Show("Fehler! Der Widerstand muss eine Zahl sein!");
                //throw new ArgumentException("Fehler! Der Widerstand muss eine Zahl sein!");
            }

            tmp = tbCap.Text;
            tmp1 = double.TryParse(tmp, out c);

            if (c < 0)
            {
                tbBetrag.Text = "";
                tbKomplex.Text = "";
                MessageBox.Show("Fehler! Die Kapazität muss eine positive Zahl sein!");
                //throw new ArgumentOutOfRangeException("Fehler! Die Frequenz muss eine positive Zahl sein!");
            }

            if (!tmp1)
            {
                tbBetrag.Text = "";
                tbKomplex.Text = "";
                MessageBox.Show("Fehler! Die Kapazität muss eine Zahl sein!");
                //throw new ArgumentException("Fehler! Die Frequenz muss eine Zahl sein!");

            }


            f = (slFrequency.Value) / 10;

            c = c * 1E-6;

            if (f == 0 || c == 0 || r == 0)
            {
                //in diesem fall hat nur r einfluss c = 0
                tbBetrag.Text = r.ToString();
                tbKomplex.Text = r.ToString();
                return;
            }


            try
            {
                zppObjekt = new RCZweipolParallel(r, c, bauform, f);
                betrag = zppObjekt.GetZBetrag();
                re = zppObjekt.GetZReal();
                im = Math.Abs(zppObjekt.GetZImag());
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler");
                return;
            }

            string reStr = String.Format("{0:F2}", re);
            string betragStr = String.Format("{0:F2}", betrag);
            string imStr = String.Format("{0:F2}", im);

            tbBetrag.Text = string.Format("{0:F3}", betragStr);
            tbKomplex.Text = string.Format("{0:F3} -j{1:F3}", reStr, imStr);

            return;
        }

        /// <summary>
        /// Berechnet Reihenzweipol
        /// </summary>
        private void BerechneReihenZweiPol()
        {
            double r;
            double f;
            double c;
            string tmp;
            bool tmp1;
            RCZweipol zppObjekt;
            string bauform = "placeholder";


            double betrag;
            double im, re;


            if (tbKomplex == null)
            {
                return;
            }

            tmp = tbWid.Text;
            tmp1 = double.TryParse(tmp, out r);

            if (r < 0)
            {
                tbBetrag.Text = "";
                tbKomplex.Text = "";
                MessageBox.Show("Fehler! Der Widerstand muss eine positive Zahl sein!");
            }

            //Überprüfen ob double Zahl
            if (!tmp1)
            {
                tbBetrag.Text = "";
                tbKomplex.Text = "";
                MessageBox.Show("Fehler! Der Widerstand muss eine Zahl sein!");

            }

            tmp = tbCap.Text;
            tmp1 = double.TryParse(tmp, out c);

            //Überprüfen ob die Zahl größer Null ist
            if (c < 0)
            {
                tbBetrag.Text = "";
                tbKomplex.Text = "";
                MessageBox.Show("Fehler! Die Kapazität muss eine positive Zahl sein!");
            }

            //Überprüfen ob es eine double Zahl ist
            if (!tmp1)
            {
                tbBetrag.Text = "";
                tbKomplex.Text = "";
                MessageBox.Show("Fehler! Die Kapazität muss eine Zahl sein!");

            }

            f = slFrequency.Value; //Frequenz aus dem Slider auswerten
            c = c * 1E-6; //in SI Einheiten

            if (f == 0 || c == 0)
            {
                //in diesem fall hat nur r einfluss c = 0
                tbBetrag.Text = "∞";
                tbKomplex.Text = r.ToString() + " +j∞";
                return;
            }

            try
            {
                zppObjekt = new RCZweipolReihe(f, r, bauform, c);
                betrag = zppObjekt.GetZBetrag();
                re = zppObjekt.GetZReal();
                im = zppObjekt.GetZImag();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            //Einfügen der berechneten Werte in die Textboxen
            string reStr = String.Format("{0:F2}", re);
            string betragStr = String.Format("{0:F2}", betrag);
            string imStr = String.Format("{0:F2}", im);

            tbBetrag.Text = string.Format("{0:F3}", betragStr);
            tbKomplex.Text = string.Format("{0:F3} -j{1:F3}", reStr, imStr);

            return;
        }

        /// <summary>
        /// Entscheidet ob Parallel oder Reihe berechnet werden soll
        /// </summary>
        /// 
        private void BerechneZweipol()
        {
            if (cbType != null)
            {
                switch (cbType.Text)
                {
                    case "RC-Parallel-Zweipol":
                        BerechneParallelZweiPol();
                        break;

                    case "RC-Reihen-Zweipol":
                        BerechneReihenZweiPol();
                        break;
                }
            }

        }

        /// <summary>
        /// wählt aus bei Änderung der Combobox
        /// </summary>
        private void BerechneZweipolChanged()
        {
            switch (cbType.Text)
            {
                case "RC-Parallel-Zweipol":
                    BerechneReihenZweiPol();
                    break;

                case "RC-Reihen-Zweipol":
                    BerechneParallelZweiPol();
                    break;
            }

        }

        //file interaction methods
        /// <summary>
        /// Liest Daten ein
        /// </summary>
        private void read()
        {
            string zeile;
            string path = @"..\..\..\txt.txt";
            FileStream fs;
            fs = new FileStream(path, FileMode.OpenOrCreate);


            using (StreamReader sr = new StreamReader(fs))
            {
                while (!sr.EndOfStream)
                {
                    zeile = sr.ReadLine();
                    cbPreset.Items.Add(zeile);

                }

            }
        }

        /// <summary>
        /// Neue Methode bei Änderung, aufgrund verspäteter Änderung in der Box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BerechneZweipolChanged();
        }

        /// <summary>
        /// Auswahländerung der Beispiele
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbPreset_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double tmp;
            string selectedString = cbPreset.SelectedItem.ToString();
            if (selectedString != null && cbPreset.SelectedIndex-1 > 0)
            {
                string[] Elemente = selectedString.Split(';');
                tbWid.Text = Elemente[0];
                tbCap.Text = Elemente[1];
                slFrequency.Value = Convert.ToDouble(Elemente[2]);

            }
        }
    }
}