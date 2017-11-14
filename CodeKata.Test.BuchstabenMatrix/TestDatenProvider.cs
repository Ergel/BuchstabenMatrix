namespace CodeKata.Test.BuchstabenMatrix
{
    public class TestDatenProvider
    {
        public static char[][] ErzeugeBuchstabenMatrixWieInDerAnforderungen()
        {
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
        public static char[][] ErzeugeBuchstabenMatrixSelbeZeichenFuerEinAnderesMatch()
        {
            var buchstabenMatrix = new char[4][];

            var ersteZeile = new char[6] { 'K', 'L', 'P', 'Q', 'R', 'K' };
            var zweiteZeile = new char[6] { 'A', 'T', 'Z', 'D', 'A', 'I' };
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
