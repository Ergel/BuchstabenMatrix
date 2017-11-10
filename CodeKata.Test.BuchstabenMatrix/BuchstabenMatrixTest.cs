using System;
using System.Collections.Generic;
using System.Linq;
using CodeKata.BuchstabenMatrix;
using NUnit.Framework;

namespace CodeKata.Test.BuchstabenMatrix
{
    [TestFixture]
    public class BuchstabenMatrixTest
    {
        readonly char[][] buchstabenMatrix = ErzeugeZufaelligeBuchstabenMatrix();

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

            var erwartetePositionen = new List<Tuple<int, int>>();
            erwartetePositionen.Add(new Tuple<int, int>(0, 0));
            erwartetePositionen.Add(new Tuple<int, int>(1, 0));
            erwartetePositionen.Add(new Tuple<int, int>(1, 1));
            erwartetePositionen.Add(new Tuple<int, int>(2, 1));
            erwartetePositionen.Add(new Tuple<int, int>(2, 2));

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
