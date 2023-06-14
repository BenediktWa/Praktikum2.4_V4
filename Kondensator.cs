/*
  Praktikum 2.3
  Author: Benedikt Walessa
  Erstellt: 22.05.2023
  Dimensionierung eines Kondensators

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktikum2._4
{
    internal class Kondensator
    {
        /// <summary>
        /// konstructor 
        /// </summary>
        /// <param name="bauformC">build style</param>
        /// <param name="materialDielektrikumC">material dieelektrrikum</param>
        /// <param name="kapazitaetC">capacity</param>
        /// <param name="relDielektrizKonstC">relative dieelectric const</param>
        public Kondensator(string bauformC, string materialDielektrikumC, double kapazitaetC, double relDielektrizKonstC)
        {
            if (bauformC != null) bauform = bauformC;
            else throw new ArgumentNullException("Fehler! Die Bauform muss definiert sein!");
            if (materialDielektrikumC != null) materialDielektrikum = materialDielektrikumC;
            else throw new ArgumentNullException("Fehler! Das Material des Dieelektikums muss definiert sein!");

            if (kapazitaetC == null)
            {
                throw new ArgumentNullException("Fehler! Die Kapazität muss existieren!");
            }
            else if (kapazitaetC <= 0)
            {
                throw new ArgumentOutOfRangeException("Fehler! Die Kapazität muss positiv sein!");
            }
            else
            {
                Kapazitaet = kapazitaetC;
            }

            if (relDielektrizKonstC == null)
            {
                throw new ArgumentNullException("Fehler! Die relative Dielektrizitätskonstante muss existieren!");
            }
            else if (relDielektrizKonstC <= 0)
            {
                throw new ArgumentOutOfRangeException("Fehler! Die relative Dielektrizitätskonstante muss positiv sein!");
            }

            else
            {
                RelDielektrizKonst = relDielektrizKonstC;
            }
        }

        public Kondensator()
        {
        }

        public Kondensator(Kondensator koObjekt)
        {
            Bauform = koObjekt.bauform;
            MaterialDielektrikum = koObjekt.materialDielektrikum;
            Kapazitaet = koObjekt.kapazitaet;
            RelDielektrizKonst = koObjekt.relDielektrizKonst;
        }

        /// <summary>
        /// konstructor 
        /// </summary>
        /// <param name="bauformC">build style</param>
        /// <param name="kapazitaetC">capacity</param>
        public Kondensator(string bauformC, double kapazitaetC)
        {
            if (bauformC != null) bauform = bauformC;
            else throw new ArgumentNullException("Fehler! Die Bauform muss definiert sein!");
            materialDielektrikum = "default";
            if (kapazitaetC <= 0)
            {
                throw new ArgumentOutOfRangeException("Fehler! Die Kapazität muss positiv sein!");
            }
            else if (kapazitaetC == null)
            {
                throw new ArgumentNullException("Fehler! Die Kapazität muss existieren!");
            }
            else
            {
                kapazitaet = kapazitaetC;
            }
            relDielektrizKonst = 1;
        }

        private string bauform;
        private string materialDielektrikum;
        private double kapazitaet;
        private double relDielektrizKonst;

        //Getter/Setter
        public string Bauform
        {
            get => bauform;
            set
            {
                if (value != null || value.Length > 0) bauform = value;
                else throw new ArgumentNullException("Fehler! Die Bauform muss definiert sein!");
            }
        }

        public string MaterialDielektrikum
        {
            get => materialDielektrikum;
            set
            {
                if (value != null || value.Length > 0) materialDielektrikum = value;
                else throw new ArgumentNullException("Fehler! Das Material des Dieelektikums muss definiert sein!");
            }
        }

        public double Kapazitaet
        {
            get => kapazitaet;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Fehler! Die Kapazität muss positiv sein!");
                }
                else if (value == null)
                {
                    throw new ArgumentNullException("Fehler! Die Kapazität muss existieren!");
                }
                else
                {
                    kapazitaet = value;
                }
            }
        }

        public double RelDielektrizKonst
        {
            get => relDielektrizKonst;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Fehler! Die relative Dielektrizitätskonstante muss positiv sein!");
                }
                else if (value == null)
                {
                    throw new ArgumentNullException("Fehler! Die relative Dielektrizitätskonstante muss existieren!");
                }
                else
                {
                    relDielektrizKonst = value;
                }
            }
        }
    }
}