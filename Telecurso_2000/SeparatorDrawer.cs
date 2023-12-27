namespace Telecurso2000Programacao.Drawers
{
    public static class SeparatorDrawer
    {
        private static int EVEN_SEPARATOR_LENGTH = 14;
        private static int ODD_SEPARATOR_LENGTH = 15;

        public static void DrawSeparator(string separatorTitle = "Fim")
        {
            int linesLength = separatorTitle.Length is var titleLength && isEven(titleLength) ?
                (EVEN_SEPARATOR_LENGTH - titleLength) / 2 : (ODD_SEPARATOR_LENGTH - titleLength) / 2;
            string separatorLine = new String("-", linesLength);

            Console.WriteLine(separatorLine + separatorTitle + separatorLine);
        }

        /// <summary>
        /// Checks if a number is even.
        /// </summary>
        /// <param name="num">The number checked.</param>
        /// <returns>true if the number is even and false otherwise.</returns>
        private static bool isEven(int num)
        {
            return num % 2 == 0;
        }
    }
}