using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;

namespace CodeKata.BuchstabenMatrix
{
    /// <summary>
    /// Hier wird ein BuchstabenMatrix Rätsel abgebildet, indem man ein Wort suchen muss.
    /// Beim Lesen ist stets von einem Buchstaben zu einem benachbarten Buchstaben (nach rechts oder nach unten) fortzuschreiten.
    /// </summary>
    /// <example>
    /// //TODO: Ein besiepiel wäre hier nicht schlecht, um die Regeln klar zu machen.
    /// </example>
    public class BuchstabenMatrixRaetsel
    {
        private readonly char[][] buchstabenMatrix;
        private readonly string zuMatchendesWort;

        private List<List<Point>> listeVonMatches = null;

        public BuchstabenMatrixRaetsel(char[][] buchstabenMatrix, string zuMatchendesWort)
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

        private void BestueckeListeVonMatches(Stack<char> buchstabenStack, Point startIndex,
            List<Point> matches)
        {
            if (buchstabenStack.Count == 0)
            {
                return;
            }

            if (matches.Count == zuMatchendesWort.Length)
            {
                return;
            }

            var zusuchendeBuchstabe = buchstabenStack.Pop();
            for (var indexBuchstabenArray = startIndex.X;
                indexBuchstabenArray < buchstabenMatrix.Length;
                indexBuchstabenArray++)
            {
                var buchstabenArray = buchstabenMatrix[indexBuchstabenArray];
                for (var indexBuchstabe = startIndex.Y; indexBuchstabe < buchstabenArray.Length; indexBuchstabe++)
                {
                    var buchstabeInArray = buchstabenMatrix[indexBuchstabenArray][indexBuchstabe];
                    if (buchstabeInArray == zusuchendeBuchstabe)
                    {
                        if (matches.Count == 0)
                        {
                            //Erstes Wort
                            matches.Add(new Point(indexBuchstabenArray, indexBuchstabe));
                            var neuerStartIndex = new Point(matches.Last().X, matches.Last().Y);

                            var buchstabenStackIntern = new Stack<char>(zuMatchendesWort.Reverse());
                            buchstabenStackIntern.Pop();
                            BestueckeListeVonMatches(buchstabenStackIntern, neuerStartIndex, matches);
                            matches = new List<Point>();
                            continue;
                        }

                        var vorherigeBuchstabenIndex = matches.Last();
                        var aktuelleBuchstabenIndex = new Point(indexBuchstabenArray, indexBuchstabe);
                        if (SindDieBuchstabenZusamenhaengend(vorherigeBuchstabenIndex, aktuelleBuchstabenIndex))
                        {
                            matches.Add(new Point(indexBuchstabenArray, indexBuchstabe));
                            if (matches.Count == zuMatchendesWort.Length)
                            {
                                listeVonMatches.Add(matches);
                                return;
                            }


                            zusuchendeBuchstabe = buchstabenStack.Pop();
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
        }

        private static bool SindDieBuchstabenZusamenhaengend(Point vorherigeBuchstabenIndex, Point aktuelleBuchstabenIndex)
        {
            var liegtHorizontalNebeneinander = vorherigeBuchstabenIndex.X == aktuelleBuchstabenIndex.X
                                               && vorherigeBuchstabenIndex.Y < aktuelleBuchstabenIndex.Y;

            var liegtVertikalNebeEinander = vorherigeBuchstabenIndex.Y == aktuelleBuchstabenIndex.Y;

            return liegtHorizontalNebeneinander || liegtVertikalNebeEinander;
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