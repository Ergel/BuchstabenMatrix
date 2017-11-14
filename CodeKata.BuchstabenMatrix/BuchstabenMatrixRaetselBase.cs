using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;

namespace CodeKata.BuchstabenMatrix
{
   
    public abstract class BuchstabenMatrixRaetselBase
    {
        protected readonly char[][] buchstabenMatrix;
        protected readonly string zuMatchendesWort;
        protected List<List<Point>> listeVonMatches = null;

        protected BuchstabenMatrixRaetselBase(char[][] buchstabenMatrix, string zuMatchendesWort)
        {
            this.buchstabenMatrix = buchstabenMatrix;
            this.zuMatchendesWort = zuMatchendesWort;
        }

        public List<List<Point>> HoleMatches()
        {
            CheckObDasRaetselGeloestWurde();
            return listeVonMatches;
        }

      public int AnzahlMatches
        {
            get
            {
                CheckObDasRaetselGeloestWurde();
                return listeVonMatches.Count;
            }
        }

        public List<List<Point>> LoeseRaetsel()
        {
            //todo: Überlegen, ob es besser ist, die folgende Assertion direkt im Konstruktor zu machen. 
            //Dann kann mann verhindern, dass das BuchstabenMatrixRaetsel Objekt
            //mit ungültigen Zustand instanziert werden kann.
            if (buchstabenMatrix == null
                || buchstabenMatrix.Length == 0)
            {
                throw new ArgumentException("Es wurde keinen Buchstabenmatrix bereitgestellt oder der Buchstabenmatrix ist leer.");
            }

            if (string.IsNullOrEmpty(zuMatchendesWort))
            {
                throw new ArgumentException("Es wurde kein zu findendes Wort definiert.");
            }

            listeVonMatches = new List<List<Point>>();

            var buchstabenStack = new Stack<char>(zuMatchendesWort.Reverse());
            var startIndex = new Point(0, 0);
            var matches = new List<Point>();

            BestueckeListeVonMatches(buchstabenStack, startIndex, matches);

            return listeVonMatches;
        }

        protected abstract void BestueckeListeVonMatches(Stack<char> buchstabenStack, Point startIndex,
            List<Point> matches);

        private void CheckObDasRaetselGeloestWurde()
        {
            if (listeVonMatches == null)
            {
                throw new TargetInvocationException("Sie haben das Rätsel noch nicht gelöst.", null);
            }
        }
    }
}