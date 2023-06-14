/*
  Praktikum 2.3
  Author: Benedikt Walessa
  Erstellt: 22.05.2023
  erbt von RCZweipol
  enthält die Frequenz und mehoden zur berechnung des komplexen widerstandes

*/

using Praktikum2._4;
using System;

namespace Praktikum_2._3
{
    internal class RCZweipolParallel : RCZweipol
    {
        private double f;


        //getter/setter
        public double F
        {
            get { return f; }
            set
            {
                if (value == null || value <= 0)
                {
                    throw new ArgumentNullException("Fehler! Die Frequenz muss positiv sein!");
                }
                else { f = value; }

            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <param name="bauForm"></param>
        /// <param name="fC"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public RCZweipolParallel(double r, double c, string bauForm, double fC) : base(r, c, bauForm)
        {
            Ko = new Kondensator(bauForm, c);

            if (fC <= 0)
            {
                throw new ArgumentOutOfRangeException("Fehler! Die Frequenz muss positiv sein;");
            }
            else if (fC == null)
            {
                throw new ArgumentNullException("Fehler! Die Frequenz muss existieren;");
            }
            else
            {
                f = fC;
            }
        }

        //methods

        /// <summary>
        /// Berechnet die Kreisfrequenz
        /// </summary>
        /// <returns>double, die berechnete Kreisfrequenz</returns>
        public override double GetKreisFrequenz()
        {
            double omega;

            omega = 2 * (Math.PI) * f;

            return omega;
        }


        /// <summary>
        /// berechnet den realen Widerstandsanteil
        /// </summary>
        /// <returns>double, der berechnete Widerstandsanteil</returns>
        /// <exception cref="ArgumentNullException">thrown if nenner equals 0, illegal op</exception>

        public override double GetZReal()
        {
            double zReal;

            double omega = GetKreisFrequenz();
            double r = base.R;
            double c = base.Ko.Kapazitaet;

            double nenner = (1 + (Math.Pow(omega, 2) * Math.Pow(r, 2) * Math.Pow(c, 2)));

            if (nenner == 0)
            {
                throw new ArgumentNullException("Fehler! Nenner des Realteils darf nicht Null sein!");
            }
            else { zReal = (r / nenner); }

            return zReal;
        }

        /// <summary>
        /// berechnet den imaginären Widerstandsanteil
        /// </summary>
        /// <returns>double, der berechnete Widerstandsanteil</returns>
        /// <exception cref="ArgumentNullException">thrown if nenner equals 0, illegal op</exception>

        public override double GetZImag()
        {
            double zImag;

            double omega = GetKreisFrequenz();
            double r = base.R;
            double c = base.Ko.Kapazitaet;

            double nenner = (1 + (Math.Pow(omega, 2) * Math.Pow(r, 2) * Math.Pow(c, 2)));
            double zaeler = (-1 * (omega * Math.Pow(r, 2) * c));

            if (nenner == 0)
            {
                throw new ArgumentNullException("Fehler! Nenner des Imaginärteils darf nicht Null sein!");
            }
            else
            {

                zImag = zaeler / nenner;
            }

            return zImag;

        }

        /// <summary>
        /// berechnet den Betrag der Widerstandsanteile
        /// </summary>
        /// <returns>double, der berechnete Berag</returns>
        public override double GetZBetrag()
        {
            double zBetrag;

            double zImag = GetZImag();
            double zReal = GetZReal();

            double tmp = (Math.Pow(zImag, 2)) + (Math.Pow(zReal, 2));

            zBetrag = Math.Sqrt(tmp);

            return zBetrag;
        }
    }
}