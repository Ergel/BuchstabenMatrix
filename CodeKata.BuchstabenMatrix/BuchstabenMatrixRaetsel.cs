using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CodeKata.BuchstabenMatrix
{
    public class BuchstabenMatrixRaetsel
    {
        private readonly char[][] buchstabenMatrix;
        private readonly string zuMatchendesWort;

        //todo: Nutze Position anstatt Tupel<int,int>
        private List<List<Tuple<int, int>>> listeVonMatches = null;

        public BuchstabenMatrixRaetsel(char[][] buchstabenMatrix, string zuMatchendesWort)
        {
            this.buchstabenMatrix = buchstabenMatrix;
            this.zuMatchendesWort = zuMatchendesWort;
        }

        public List<List<Tuple<int, int>>> HoleMatches()
        {
            if (listeVonMatches == null)
            {
                throw new TargetInvocationException("Sie haben das Rätsel noch nicht gelöst.", null);
            }

            return listeVonMatches;
        }

        public int AnzahlMatches
        {
            get
            {
                if (listeVonMatches == null)
                {
                    throw new TargetInvocationException("Sie haben das Rätsel noch nicht gelöst.", null);
                }

                return listeVonMatches.Count;
            }
        }


        public List<List<Tuple<int, int>>> LoeseRaetsel()
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

            listeVonMatches = new List<List<Tuple<int, int>>>();

            var buchstabenStack = new Stack<char>(zuMatchendesWort.Reverse());
            var startIndex = new Tuple<int, int>(0, 0);
            var matches = new List<Tuple<int, int>>();

            BestueckeListeVonMatches(buchstabenStack, startIndex, matches);

            return listeVonMatches;
        }

        private void BestueckeListeVonMatches(Stack<char> buchstabenStack, Tuple<int, int> startIndex,
            List<Tuple<int, int>> matches)
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
            for (var indexBuchstabenArray = startIndex.Item1;
                indexBuchstabenArray < buchstabenMatrix.Length;
                indexBuchstabenArray++)
            {
                var buchstabenArray = buchstabenMatrix[indexBuchstabenArray];
                for (var indexBuchstabe = startIndex.Item2; indexBuchstabe < buchstabenArray.Length; indexBuchstabe++)
                {
                    var buchstabeInArray = buchstabenMatrix[indexBuchstabenArray][indexBuchstabe];
                    if (buchstabeInArray == zusuchendeBuchstabe)
                    {
                        if (matches.Count == 0)
                        {
                            //Erstes Wort
                            matches.Add(new Tuple<int, int>(indexBuchstabenArray, indexBuchstabe));
                            var neuerStartIndex = new Tuple<int, int>(matches.Last().Item1, matches.Last().Item2);

                            var buchstabenStackIntern = new Stack<char>(zuMatchendesWort.Reverse());
                            buchstabenStackIntern.Pop();
                            BestueckeListeVonMatches(buchstabenStackIntern, neuerStartIndex, matches);
                            matches = new List<Tuple<int, int>>();
                            continue;
                        }

                        var vorherigeBuchstabenIndex = matches.Last();
                        var aktuelleBuchstabenIndex = new Tuple<int, int>(indexBuchstabenArray, indexBuchstabe);
                        if (SindDieBuchstabenZusamenhaengend(vorherigeBuchstabenIndex, aktuelleBuchstabenIndex))
                        {
                            matches.Add(new Tuple<int, int>(indexBuchstabenArray, indexBuchstabe));
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

        private bool SindDieBuchstabenZusamenhaengend(Tuple<int, int> vorherigeBuchstabenIndex, Tuple<int, int> aktuelleBuchstabenIndex)
        {
            var liegtHorizontalNebeneinander = vorherigeBuchstabenIndex.Item1 == aktuelleBuchstabenIndex.Item1
                                               && vorherigeBuchstabenIndex.Item2 < aktuelleBuchstabenIndex.Item2;

            var liegtVertikalNebeEinander = vorherigeBuchstabenIndex.Item2 == aktuelleBuchstabenIndex.Item2;

            return liegtHorizontalNebeneinander || liegtVertikalNebeEinander;
        }
    }
}