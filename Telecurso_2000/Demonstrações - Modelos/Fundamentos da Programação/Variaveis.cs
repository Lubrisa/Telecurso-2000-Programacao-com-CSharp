using Telecurso2000Programacao.Drawers;

namespace Telecurso2000Programacao.FundamentosDaProgramação.Demonstrações.Modelo
{
    /// <summary>
    /// Modelo para a demonstração prática da aula sobre variáveis.
    /// </summary>
    public unsafe class Variaveis
    {
        /// <summary>
        /// Demonstra como as variáveis funcionam internamente (expõe o endereço subjacente).
        /// </summary>
        public void Definicao()
        {
            SeparatorDrawer.DrawSeparator("Definição de variável");

            int x = 0;
            DrawAddressTableEntry(&x, nameof(x));
            string y = "str";
            DrawAddressTableEntry(&y, nameof(y));
            bool z = false;
            DrawAddressTableEntry(&z, nameof(z));

            SeparatorDrawer.DrawSeparator();
        }

        /// <summary>
        /// Demonstra quais são os tipos primitivos do C#.
        /// </summary>
        public void TiposPrimitivos()
        {
            SeparatorDrawer.DrawSeparator("Tipos Primitivos");

            DrawTypeTableEntry(values: [true, false], notes: "internamente é um bit");
            DrawTypeTableEntry(values: [byte.MinValue, byte.MaxValue]);
            DrawTypeTableEntry(values: [sbyte.MinValue, sbyte.MaxValue]);
            DrawTypeTableEntry(values: ['0', 'a', 'Z', '!'], notes: "inclui números, minúsculas, maiúsculas e sinais");
            DrawTypeTableEntry(values: [decimal.MinValue, decimal.MaxValue], notes: "inclui decimais");
            DrawTypeTableEntry(values: [double.MinValue, double.MaxValue], notes: "inclui decimais");
            DrawTypeTableEntry(values: [float.MinValue, float.MaxValue], notes: "inclui decimais");
            DrawTypeTableEntry(values: [int.MinValue, int.MaxValue]);
            DrawTypeTableEntry(values: [uint.MinValue, uint.MaxValue]);
            DrawTypeTableEntry(values: [nint.MinValue, nint.MaxValue], notes: "tamanho varia entre 32 e 64 bits");
            DrawTypeTableEntry(values: [nuint.MinValue, nuint.MaxValue], notes: "tamanho varia entre 32 e 64 bits");
            DrawTypeTableEntry(values: [long.MinValue, long.MaxValue]);
            DrawTypeTableEntry(values: [ulong.MinValue, ulong.MaxValue]);
            DrawTypeTableEntry(values: [short.MinValue, short.MaxValue]);
            DrawTypeTableEntry(values: [ushort.MinValue, ushort.MaxValue]);

            SeparatorDrawer.DrawSeparator();
        }

        #region Helpers
        /// <summary>
        /// Converte a referência de ponteiro em um endereço de memória.
        /// </summary>
        /// <typeparam name="T">Tipo da variável referenciada pelo ponteiro.</typeparam>
        /// <param name="pointer">O ponteiro que terá sua referência convertida.</param>
        /// <returns>Um endereço de memória.</returns>
        private string StringfyAddress<T>(T* pointer)
        {
            string addressToHexadecimal = ((int)pointer).ToString("X");
            return $"0x{addressToHexadecimal}";
        }

        /// <summary>
        /// Escreve no console o nome, valor e endereço de uma variável em uma estrutura de tabela.
        /// </summary>
        /// <typeparam name="T">O tipo da variável referenciada pelo ponteiro.</typeparam>
        /// <param name="pointer">Um ponteiro com uma referência a um endereço de memória.</param>
        /// <param name="variableName">O nome da variável referenciada pelo ponteiro.</param>
        private void DrawAddressTableEntry<T>(T* pointer, string variableName)
        {
            string variableColum = $"Variável: {variableName}".PadRight(25);
            string valueColum = $"Valor: {*pointer}".PadRight(25);
            string addressColum = $"Endereço: {StringfyAddress(pointer)}";

            Console.WriteLine(variableColum + valueColum + addressColum);
        }

        /// <summary>
        /// Escreve no console o nome, valores importantes e algumas notas sobre um tipo.
        /// </summary>
        /// <typeparam name="T">O tipo que será descrito.</typeparam>
        /// <param name="notes">Notas sobre o tipo descrito.</param>
        /// <param name="values">Valores importantes do tipo descrito.</param>
        private void DrawTypeTableEntry<T>(string notes = "", params T[] values) where T : struct
        {
            string typeColum = $"Tipo: {typeof(T).Name}".PadRight(25);

            string valuesList = values
                .Select(value => value.ToString())
                .Aggregate("", (accumulator, value) => accumulator + $"{value}, ")[..^2];
            string valuesColum = $@"Valores: {valuesList}"
                .PadRight(75);

            string notesColum = $"Notas: {notes}";

            Console.WriteLine(typeColum + valuesColum + notesColum);
        }
        #endregion
    }
}