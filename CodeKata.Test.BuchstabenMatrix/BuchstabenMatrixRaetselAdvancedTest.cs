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
        private readonly char[][] buchstabenMatrix = TestDatenProvider.ErzeugeBuchstabenMatrixWieInDerAnforderungen();

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
            Assert.That(listeVonMatches[1], Is.EqualTo(erwartetePositionenFuerDenZweitenMatch));
        }

        [Test]
        public void TesteDasSelbeZeichenFuerEinAnderesMatchWiederverwendetWerdenKann()
        {
            var zuMatchendesWort = "KATZE";

            var buchstabenMatrix = TestDatenProvider.ErzeugeBuchstabenMatrixSelbeZeichenFuerEinAnderesMatch();

            var buchstabenMatrixRaetsel = new BuchstabenMatrixRaetselAdvanced(buchstabenMatrix, zuMatchendesWort);
            buchstabenMatrixRaetsel.LoeseRaetsel();
            var anzahlMatches = buchstabenMatrixRaetsel.AnzahlMatches;
            var listeVonMatches = buchstabenMatrixRaetsel.HoleMatches();

            Assert.That(listeVonMatches.Count, Is.EqualTo(3));
            Assert.That(anzahlMatches, Is.EqualTo(3));
        }
    }
}
