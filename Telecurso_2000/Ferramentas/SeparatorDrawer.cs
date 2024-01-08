namespace Telecurso2000Programacao.Ferramentas
{
    /// <summary>
    /// Handles the work of drawing separator lines in the console to divide subjects written on it.
    /// </summary>
    public static class SeparatorDrawer
    {
        private static int EVEN_SEPARATOR_LENGTH = 154;
        private static int ODD_SEPARATOR_LENGTH = 155;
        private static char SEPARATOR_CHARACTER = '-';

        /// <summary>
        /// Draws an separator line in the console.
        /// </summary>
        /// <param name="separatorTitle">A word or phrase that will be in the middle of the separator.</param>
        public static void DrawSeparator(string separatorTitle = "")
        {
            int linesLength = separatorTitle.Length is var titleLength && isEven(titleLength) ?
                (EVEN_SEPARATOR_LENGTH - titleLength) / 2 : (ODD_SEPARATOR_LENGTH - titleLength) / 2;
            string separatorLine = new String(SEPARATOR_CHARACTER, linesLength);

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