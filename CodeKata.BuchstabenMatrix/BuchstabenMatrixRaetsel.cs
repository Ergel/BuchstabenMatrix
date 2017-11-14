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
    public class BuchstabenMatrixRaetsel : BuchstabenMatrixRaetselBase
    {
        public BuchstabenMatrixRaetsel(char[][] buchstabenMatrix, string zuMatchendesWort) :
            base(buchstabenMatrix, zuMatchendesWort)
        {
        }

        protected override void BestueckeListeVonMatches(Stack<char> buchstabenStack, Point startIndex,List<Point> matches)
        {
            var zusuchendeBuchstabe = buchstabenStack.Pop();
            for (var indexBuchstabenArray = startIndex.X; indexBuchstabenArray < buchstabenMatrix.Length; indexBuchstabenArray++)
            {
                var buchstabenArray = buchstabenMatrix[indexBuchstabenArray];
                for (var indexBuchstabe = startIndex.Y; indexBuchstabe < buchstabenArray.Length; indexBuchstabe++)
                {
                    var buchstabeInArray = buchstabenMatrix[indexBuchstabenArray][indexBuchstabe];
                    if (buchstabeInArray != zusuchendeBuchstabe)
                    {
                        continue;
                    }

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

        private static bool SindDieBuchstabenZusamenhaengend(Point vorherigeBuchstabenIndex, Point aktuelleBuchstabenIndex)
        {
            var liegtHorizontalNebeneinander = vorherigeBuchstabenIndex.X == aktuelleBuchstabenIndex.X
                                               && vorherigeBuchstabenIndex.Y < aktuelleBuchstabenIndex.Y;

            var liegtVertikalNebeEinander = vorherigeBuchstabenIndex.Y == aktuelleBuchstabenIndex.Y;

            return liegtHorizontalNebeneinander || liegtVertikalNebeEinander;
        }
    }
}