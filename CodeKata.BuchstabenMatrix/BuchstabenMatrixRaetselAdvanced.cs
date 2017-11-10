using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;

namespace CodeKata.BuchstabenMatrix
{
    /// <summary>
    /// Hier wird ein BuchstabenMatrix Rätsel abgebildet, indem man ein Wort suchen muss.
    /// Beim Lesen ist von einem Buchstaben zu einem benachbarten Buchstaben (nach rechts, nach unten und wieder nach oben und nach links) fortzuschreiten.
    /// </summary>
    /// <example>
    /// //TODO: Ein besiepiel wäre hier nicht schlecht, um die Regeln klar zu machen.
    /// </example>
    public class BuchstabenMatrixRaetselAdvanced
    {
        private readonly char[][] buchstabenMatrix;
        private readonly string zuMatchendesWort;

        private List<List<Point>> listeVonMatches = null;

        public BuchstabenMatrixRaetselAdvanced(char[][] buchstabenMatrix, string zuMatchendesWort)
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

            throw new NotImplementedException("Diese muss noch implementiert werden!");
        }

        private void CheckObDasRaetselGeloestWurde()
        {
            if (listeVonMatches == null)
            {
                throw new TargetInvocationException("Sie haben das Rätsel noch nicht gelöst.", null);
            }
        }
    }
}