using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CodeKata.BuchstabenMatrix;
using NUnit.Framework;

namespace CodeKata.Test.BuchstabenMatrix
{
    [TestFixture]
    public class BuchstabenMatrixTest
    {
        private readonly char[][] buchstabenMatrix = TestDatenProvider.ErzeugeBuchstabenMatrixWieInDerAnforderungen();

        [Test]
        public void TestDasFindenVonEinemWortInDerBuchstabenMatrixWennDasWortVorhandenIst()
        {
            var zuMatchendesWort = "KATZE";

            var buchstabenMatrixRaetsel = new BuchstabenMatrixRaetsel(buchstabenMatrix, zuMatchendesWort);
            buchstabenMatrixRaetsel.LoeseRaetsel();
            var anzahlMatches = buchstabenMatrixRaetsel.AnzahlMatches;
            var listeVonMatches = buchstabenMatrixRaetsel.HoleMatches();

            Assert.That(listeVonMatches.Count, Is.EqualTo(1));
            Assert.That(anzahlMatches, Is.EqualTo(1));

            var erwartetePositionen = new List<Point>();
            erwartetePositionen.Add(new Point(0, 0));
            erwartetePositionen.Add(new Point(1, 0));
            erwartetePositionen.Add(new Point(1, 1));
            erwartetePositionen.Add(new Point(2, 1));
            erwartetePositionen.Add(new Point(2, 2));

            Assert.That(listeVonMatches[0], Is.EqualTo(erwartetePositionen));
        }

        [Test]
        public void TestDasFindenVonEinemWortInDerBuchstabenMatrixWennDasWortMerhmalsVorhandenIst()
        {
            var zuMatchendesWort = "KAT";

            var buchstabenMatrixRaetsel = new BuchstabenMatrixRaetsel(buchstabenMatrix, zuMatchendesWort);
            buchstabenMatrixRaetsel.LoeseRaetsel();
            var anzahlMatches = buchstabenMatrixRaetsel.AnzahlMatches;

            Assert.That(buchstabenMatrixRaetsel.HoleMatches().Count, Is.EqualTo(2));
            Assert.That(anzahlMatches, Is.EqualTo(2));
        }

        [Test]
        public void TestDasNichtFindenVonEinemWortInDerBuchstabenMatrixWennDasWortNichtVorhandenIst()
        {
            var zuMatchendesWort = "NICHT";

            var buchstabenMatrixRaetsel = new BuchstabenMatrixRaetsel(buchstabenMatrix, zuMatchendesWort);
            buchstabenMatrixRaetsel.LoeseRaetsel();
            var anzahlMatches = buchstabenMatrixRaetsel.AnzahlMatches;

            Assert.That(buchstabenMatrixRaetsel.HoleMatches().Count, Is.EqualTo(0));
            Assert.That(anzahlMatches, Is.EqualTo(0));
        }

        [Test]
        public void TestDasNichtFindenVonEinemWortDerLaengerAlsBuchstabenMatrixGroesseIst()
        {
            var zuMatchendesWort = "KATZE IN DEM SACK";
            var buchstabenMatrixRaetsel = new BuchstabenMatrixRaetsel(buchstabenMatrix, zuMatchendesWort);
            buchstabenMatrixRaetsel.LoeseRaetsel();
            var anzahlMatches = buchstabenMatrixRaetsel.AnzahlMatches;

            Assert.That(buchstabenMatrixRaetsel.HoleMatches().Count, Is.EqualTo(0));
            Assert.That(anzahlMatches, Is.EqualTo(0));
        }

        [Test]
        public void TestDasListeVonMatchesUndAnzahlErstNachDemRaetselLoesenVerfuegbarSind()
        {
            var zuMatchendesWort = "WHITE-BOX Test";

            var buchstabenMatrixRaetsel = new BuchstabenMatrixRaetsel(buchstabenMatrix, zuMatchendesWort);

            Assert.That(() => buchstabenMatrixRaetsel.AnzahlMatches, Throws.TargetInvocationException, "Es muss erst das Rätsel gelöset werden.");
            Assert.That(() => buchstabenMatrixRaetsel.HoleMatches(), Throws.TargetInvocationException, "Es muss erst das Rätsel gelöset werden.");

            buchstabenMatrixRaetsel.LoeseRaetsel();

            Assert.That(buchstabenMatrixRaetsel.HoleMatches().Count, Is.EqualTo(0));
            Assert.That(buchstabenMatrixRaetsel.AnzahlMatches, Is.EqualTo(0));
        }

        [Test]
        public void TestDasFindenVonEinemWortWennKeineMatrixDaIst()
        {
            var buchstabenMatrixRaetsel = new BuchstabenMatrixRaetsel(null, "Keine Matrix");
            Assert.That(() => buchstabenMatrixRaetsel.LoeseRaetsel(), Throws.ArgumentException);

            var leererBuchstabenMatrix = new char[0][];

            buchstabenMatrixRaetsel = new BuchstabenMatrixRaetsel(leererBuchstabenMatrix, "Keine Matrix");
            Assert.That(() => buchstabenMatrixRaetsel.LoeseRaetsel(), Throws.ArgumentException);

        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void TestDasFindenVonEinemWortWennKeinZuSuchendesWortSpezifiziertIst(string zuMatchendesWort)
        {
            var buchstabenMatrixRaetsel = new BuchstabenMatrixRaetsel(buchstabenMatrix, zuMatchendesWort);
            Assert.That(() => buchstabenMatrixRaetsel.LoeseRaetsel(), Throws.ArgumentException);
        }
    }
}
