using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CodeKata.Test.BuchstabenMatrix
{
    [TestFixture]
    public class BuchstabenMatrixTest
    {
        readonly char[][] buchstabenMatrix = ErzeugeZufaelligeBuchstabenMatrix();
        private List<List<Tuple<int, int>>> listeVonMatches;
        private string _zuMatchendesWort;

        [Test]
        public void TestDasFindenVonEinemWortInDerBuchstabenMatrixWennDasWortVorhandenIst()
        {
            _zuMatchendesWort = "KATZE";

            var buchstabenStack = new Stack<char>(_zuMatchendesWort.Reverse());
            var startIndex = new Tuple<int, int>(0, 0);

            // TODO: Feste struktur für Position nund Buchstabe
            listeVonMatches = new List<List<Tuple<int, int>>>();
            var matches = new List<Tuple<int, int>>();

            BestueckeListeVonMatches(buchstabenStack, startIndex, matches);
            Assert.That(listeVonMatches.Count, Is.EqualTo(1));

            var erwartetePositionen = new List<Tuple<int, int>>();
            erwartetePositionen.Add(new Tuple<int, int>(0, 0));
            erwartetePositionen.Add(new Tuple<int, int>(1, 0));
            erwartetePositionen.Add(new Tuple<int, int>(1, 1));
            erwartetePositionen.Add(new Tuple<int, int>(2, 1));
            erwartetePositionen.Add(new Tuple<int, int>(2, 2));

            Assert.That(listeVonMatches[0], Is.EqualTo(erwartetePositionen));
        }

        private void BestueckeListeVonMatches(Stack<char> buchstabenStack, Tuple<int, int> startIndex,
            List<Tuple<int, int>> matches)
        {
            if (buchstabenStack.Count == 0)
            {
                return;
            }

            if (matches.Count == 5)
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

                            var buchstabenStackIntern = new Stack<char>(_zuMatchendesWort.Reverse());
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
                            if (matches.Count == 5)
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

        private static char[][] ErzeugeZufaelligeBuchstabenMatrix()
        {
            //todo: Matrix flexible halten
            var buchstabenMatrix = new char[4][];

            var ersteZeile = new char[6] { 'K', 'L', 'P', 'Q', 'R', 'K' };
            var zweiteZeile = new char[6] { 'A', 'T', 'L', 'D', 'A', 'I' };
            var dritteZeile = new char[6] { 'M', 'Z', 'E', 'A', 'T', 'E' };
            var vierteZeile = new char[6] { 'T', 'A', 'K', 'A', 'T', 'Z' };

            buchstabenMatrix[0] = ersteZeile;
            buchstabenMatrix[1] = zweiteZeile;
            buchstabenMatrix[2] = dritteZeile;
            buchstabenMatrix[3] = vierteZeile;

            return buchstabenMatrix;
        }
    }
}
