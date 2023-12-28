using Telecurso2000Programacao.Drawers;

namespace Telecurso2000Programacao.FundamentosDaProgramação.Demonstrações.Modelo
{
    /// <summary>
    /// Modelo para a demonstração prática da aula sobre variáveis.
    /// </summary>
    public unsafe class Variaveis
    {
        public Variaveis()
        {
            Definicao();
        }

        /// <summary>
        /// Demonstra como as variáveis funcionam internamente (expõe o endereço subjacente).
        /// </summary>
        public void Definicao()
        {
            SeparatorDrawer.DrawSeparator("Definição de variável");

            int x = 0;
            DrawUnderlyingAddress(&x, nameof(x));
            string y = "str";
            DrawUnderlyingAddress(&y, nameof(y));
            bool z = false;
            DrawUnderlyingAddress(&z, nameof(z));

            SeparatorDrawer.DrawSeparator();
        }

        /// <summary>
        /// Turns a pointer into an address in hexadecimal.
        /// </summary>
        /// <typeparam name="T">The variable tipe.</typeparam>
        /// <param name="pointer">The pointer that will be converted.</param>
        /// <returns>A memory address in hexadecimal notation.</returns>
        private string StringfyAddress<T>(T* pointer)
        {
            string addressToHexadecimal = ((int)pointer).ToString("X");
            return $"0x{addressToHexadecimal}";
        }

        /// <summary>
        /// Logs in the console a variable's name, value and underlying address in a table-formatted way.
        /// </summary>
        /// <typeparam name="T">The type of the variable.</typeparam>
        /// <param name="pointer">A pointer holding the address of a variable.</param>
        /// <param name="variableName">The name of the variable that the pointer is referencing.</param>
        private void DrawUnderlyingAddress<T>(T* pointer, string variableName)
        {
            string variableColum = $"Variável: {variableName}".PadRight(25);
            string valueColum = $"Valor: {*pointer}".PadRight(25);
            string addressColum = $"Endereço: {StringfyAddress(pointer)}";

            Console.WriteLine(variableColum + valueColum + addressColum);
        }
    }
}