using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CodeKata.BuchstabenMatrix;
using NUnit.Framework;

namespace CodeKata.Test.BuchstabenMatrix
{
    [TestFixture]
    public class BuchstabenMatrixRaetselAdvancedTest
    {
        readonly char[][] buchstabenMatrix = ErzeugeZufaelligeBuchstabenMatrix();

        [Test]
        public void TestDasFindenVonEinemWortInDerBuchstabenMatrixWennDasWortVorhandenIst()
        {
            var zuMatchendesWort = "KATZE";

            var buchstabenMatrixRaetsel = new BuchstabenMatrixRaetselAdvanced(buchstabenMatrix, zuMatchendesWort);
            buchstabenMatrixRaetsel.LoeseRaetsel();
            var anzahlMatches = buchstabenMatrixRaetsel.AnzahlMatches;
            var listeVonMatches = buchstabenMatrixRaetsel.HoleMatches();

            Assert.That(listeVonMatches.Count, Is.EqualTo(2));
            Assert.That(anzahlMatches, Is.EqualTo(2));

            var erwartetePositionenFuerDenErstenMatch = new List<Point>();
            erwartetePositionenFuerDenErstenMatch.Add(new Point(0, 0));
            erwartetePositionenFuerDenErstenMatch.Add(new Point(1, 0));
            erwartetePositionenFuerDenErstenMatch.Add(new Point(1, 1));
            erwartetePositionenFuerDenErstenMatch.Add(new Point(2, 1));
            erwartetePositionenFuerDenErstenMatch.Add(new Point(2, 2));

            var erwartetePositionenFuerDenZweitenMatch = new List<Point>();
            erwartetePositionenFuerDenZweitenMatch.Add(new Point(3, 2));
            erwartetePositionenFuerDenZweitenMatch.Add(new Point(3, 3));
            erwartetePositionenFuerDenZweitenMatch.Add(new Point(3, 4));
            erwartetePositionenFuerDenZweitenMatch.Add(new Point(3, 5));
            erwartetePositionenFuerDenZweitenMatch.Add(new Point(2, 5));

            Assert.That(listeVonMatches[0], Is.EqualTo(erwartetePositionenFuerDenErstenMatch));
            Assert.That(listeVonMatches[0], Is.EqualTo(erwartetePositionenFuerDenZweitenMatch));
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
