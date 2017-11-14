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
    public class BuchstabenMatrixRaetselAdvanced : BuchstabenMatrixRaetselBase
    {
        public BuchstabenMatrixRaetselAdvanced(char[][] buchstabenMatrix, string zuMatchendesWort)
            : base(buchstabenMatrix, zuMatchendesWort)
        {

        }

        protected override void BestueckeListeVonMatches(Stack<char> buchstabenStack, Point startIndex, List<Point> matches)
        {
            var zuSuchendeBuchstabe = buchstabenStack.Pop();
            for (var indexBuchstabenArray = startIndex.X; indexBuchstabenArray < buchstabenMatrix.Length; indexBuchstabenArray++)
            {
                var buchstabenArray = buchstabenMatrix[indexBuchstabenArray];
                for (var indexBuchstabe = startIndex.Y; indexBuchstabe < buchstabenArray.Length; indexBuchstabe++)
                {
                    var buchstabeInArray = buchstabenMatrix[indexBuchstabenArray][indexBuchstabe];
                    if (buchstabeInArray == zuSuchendeBuchstabe)
                    {
                        matches.Add(new Point(indexBuchstabenArray, indexBuchstabe));
                        if (matches.Count == zuMatchendesWort.Length
                            || buchstabenStack.Count == 0)
                        {
                            return;
                        }

                        zuSuchendeBuchstabe = buchstabenStack.Pop();
                    }

                    if (matches.Count == 0)
                    {
                        continue;
                    }

                    var indexLastMatch = matches.LastOrDefault();
                    var indexNextMatch = new Point(-1, -1);

                    if (indexLastMatch.X < buchstabenMatrix.Length - 1)
                    {
                        var buchstabeUnten = buchstabenMatrix[indexLastMatch.X + 1][indexLastMatch.Y];
                        if (buchstabeUnten == zuSuchendeBuchstabe)
                        {
                            indexNextMatch = new Point(indexLastMatch.X + 1, indexLastMatch.Y);
                        }

                    }

                    if (indexNextMatch.X == -1
                        && indexLastMatch.X > 0)
                    {
                        var buchstabeOben = buchstabenMatrix[indexLastMatch.X - 1][indexLastMatch.Y];
                        if (buchstabeOben == zuSuchendeBuchstabe)
                        {
                            indexNextMatch = new Point(indexLastMatch.X - 1, indexLastMatch.Y);
                        }
                    }

                    if (indexNextMatch.X == -1)
                    {
                        if (indexLastMatch.Y == buchstabenArray.Length - 1)
                        {
                            matches = new List<Point>();
                            buchstabenStack = new Stack<char>(zuMatchendesWort.Reverse());
                            zuSuchendeBuchstabe = buchstabenStack.Pop();
                        }

                        continue;
                    }

                    matches.Add(indexNextMatch);
                    if (matches.Count == zuMatchendesWort.Length
                        || buchstabenStack.Count == 0)
                    {
                        listeVonMatches.Add(matches);
                        return;
                    }

                    var matchedTeil = zuMatchendesWort.Substring(0, matches.Count);
                    var restDesWortes = zuMatchendesWort.Replace(matchedTeil, string.Empty);

                    var indexNeueSuchPosition = new Point(indexNextMatch.X, indexNextMatch.Y + 1);

                    var buchstabenStackIntern = new Stack<char>(restDesWortes.Reverse());
                    BestueckeListeVonMatches(buchstabenStackIntern, indexNeueSuchPosition, matches);

                    if (matches.Count == zuMatchendesWort.Length
                        && restDesWortes.Length == 1)
                    {
                        listeVonMatches.Add(matches);
                        return;
                    }

                    matches = new List<Point>();
                    buchstabenStack = new Stack<char>(zuMatchendesWort.Reverse());
                    zuSuchendeBuchstabe = buchstabenStack.Pop();
                }
            }
        }
    }
}