/*
  Praktikum 2.4
  Author: Benedikt Walesa
  Erstellt: 13.06.23
  enthält die Frequenz und mehoden zur berechnung des komplexen Widerstandes
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktikum2._4
{
    internal class RCZweipolReihe : RCZweipol
    {
        private Kondensator ko;
        private double f;
        private double r;
        private string bauform;
        private double c;




        /// <summary>
        /// Konstuktor
        /// </summary>
        /// <param name="fC">frequency</param>
        /// <param name="rC">resistance</param>
        /// <param name="bau">build style</param>
        /// <param name="c">capacity</param>
        public RCZweipolReihe(double fC, double r, string bauForm, double c) : base(r, c, bauForm)
        {
            f = fC;

        }


        //Getter/Setter
        public double F
        {
            get => f; set
            {
                if (value < 0)
                {
                    throw new ArgumentNullException("Fehler! Die Frequenz muss positiv sein!");
                }
                /*
                else if (value == 0)
                {
                    f = 1;
                }
                */
                else
                {
                    f = value;
                }
            }
        }


        public double R
        {
            get => r; set

            {
                if (value <0)
                {
                    throw new ArgumentOutOfRangeException("Fehler! Widerstand muss positiv sein!");
                }

                /*
                else if (value == 0)
                {
                    r = 1;
                }
                */
                else
                {
                    r = value;
                }
            }
        }
        internal Kondensator Ko { get => ko; set => ko = value; }

        //methods


        /// <summary>
        /// berechnet den realen Widerstandsanteil des Objektes
        /// </summary>
        /// <returns>der berechente reale Wiederstandsanteil</returns>
        public override double GetZReal()
        {
            double ZReal;

            ZReal = r;

            return ZReal;
        }


        /// <summary>
        /// berechnet den imaginären Widerstandsanteil des Objektes
        /// </summary>
        /// <returns>der berechente imaginäre Wiederstandsanteil</returns>
        override public double GetZImag()
        {
            double ZImag;
            double c = base.Ko.Kapazitaet;

            double tmp = (2 * (Math.PI) * f * base.Ko.Kapazitaet);

            if (tmp == 0)
            {
                ZImag = 1;//placeholder div durch 0 not defined
            }

            ZImag = 1 / tmp;

            return ZImag;
        }


        /// <summary>
        /// Berechent den Betrag der beiden Widerstandsanteile
        /// </summary>
        /// <returns>den berechenten Betrag</returns>
        public override double GetZBetrag()
        {
            double tmp, ZBetrag;

            tmp = ((GetZImag() * GetZImag()) + (GetZReal() * GetZReal()));

            ZBetrag = Math.Sqrt(tmp);

            return ZBetrag;
        }


        /// <summary>
        /// berechent die Kreisfrequenz aus der Frequenz
        /// </summary>
        /// <returns>die berechente Kreisfrequenz</returns>
        public override double GetKreisFrequenz()
        {
            double omega;

            omega = (2 * (Math.PI) * f);

            return omega;
        }
    }
}