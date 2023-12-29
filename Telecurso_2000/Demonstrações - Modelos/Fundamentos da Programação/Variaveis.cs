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
#pragma warning disable CS8500 // Isso pega o endereço, obtém o tamanho ou declara um ponteiro para um tipo gerenciado
            DrawAddressTableEntry(&y, nameof(y));
#pragma warning restore CS8500 // Isso pega o endereço, obtém o tamanho ou declara um ponteiro para um tipo gerenciado
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

        /// <summary>
        /// Demonstra como as variáveis são declaradas e atribuídas.
        /// </summary>
        public void DeclaracaoEAtribuicao()
        {
            SeparatorDrawer.DrawSeparator("Declaração e Atribuição");

            int x;
            string y = "declarado";
            bool z = false;
            object w;

            Console.WriteLine(DrawVariableDeclaration(&x, nameof(x)));
#pragma warning disable CS8500 // Isso pega o endereço, obtém o tamanho ou declara um ponteiro para um tipo gerenciado
            Console.WriteLine(DrawVariableDeclaration(&y, nameof(y)));
#pragma warning restore CS8500 // Isso pega o endereço, obtém o tamanho ou declara um ponteiro para um tipo gerenciado
            Console.WriteLine(DrawVariableDeclaration(&z, nameof(z)));
#pragma warning disable CS8500 // Isso pega o endereço, obtém o tamanho ou declara um ponteiro para um tipo gerenciado
            Console.WriteLine(DrawVariableDeclaration(&w, nameof(w)));
#pragma warning restore CS8500 // Isso pega o endereço, obtém o tamanho ou declara um ponteiro para um tipo gerenciado

            SeparatorDrawer.DrawSeparator();
        }

        #region Helpers
        /// <summary>
        /// Converte a referência de ponteiro em um endereço de memória.
        /// </summary>
        /// <typeparam name="T">Tipo da variável referenciada pelo ponteiro.</typeparam>
        /// <param name="pointer">O ponteiro que terá sua referência convertida.</param>
        /// <returns>Um endereço de memória.</returns>
#pragma warning disable CS8500 // Isso pega o endereço, obtém o tamanho ou declara um ponteiro para um tipo gerenciado
        private string StringfyAddress<T>(T* pointer)
#pragma warning restore CS8500 // Isso pega o endereço, obtém o tamanho ou declara um ponteiro para um tipo gerenciado
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
#pragma warning disable CS8500 // Isso pega o endereço, obtém o tamanho ou declara um ponteiro para um tipo gerenciado
        private void DrawAddressTableEntry<T>(T* pointer, string variableName)
#pragma warning restore CS8500 // Isso pega o endereço, obtém o tamanho ou declara um ponteiro para um tipo gerenciado
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

        /// <summary>
        /// Verifica se uma variável foi inicializada.
        /// </summary>
        /// <typeparam name="T">O tipo da variável que será verificada.</typeparam>
        /// <param name="pointer">Um ponteiro com o endereço da variável que será verificada.</param>
        /// <returns>True se a variável foi inicializada, false caso ela provavelmente não tenha sido.</returns>
#pragma warning disable CS8500 // Isso pega o endereço, obtém o tamanho ou declara um ponteiro para um tipo gerenciado
        private bool WasInitialized<T>(T* pointer) where T : notnull
#pragma warning restore CS8500 // Isso pega o endereço, obtém o tamanho ou declara um ponteiro para um tipo gerenciado
        {
            return !Equals(*pointer, default(T));
        }

        /// <summary>
        /// Escreve no console como uma variável foi declarada.
        /// </summary>
        /// <typeparam name="T">O tipo da variável que será escrita.</typeparam>
        /// <param name="pointer">Um ponteiro para o endereço da variável que será escrita.</param>
        /// <param name="variableName">O nome da variável escrita.</param>
        /// <returns>Uma mensagem que corresponde a como a variável deve ter sido declarada de acordo com seu valor atual.</returns>
#pragma warning disable CS8500 // Isso pega o endereço, obtém o tamanho ou declara um ponteiro para um tipo gerenciado
        private string DrawVariableDeclaration<T>(T* pointer, string variableName)
#pragma warning restore CS8500 // Isso pega o endereço, obtém o tamanho ou declara um ponteiro para um tipo gerenciado
        {
            const string UNINITIALIZED_MESSAGE = "// A variável provavelmente não foi inicializada";
            const string INITIALIZED_MESSAGE = "// A variável foi inicializada";

            return $"{typeof(T)} {variableName} "
                + (WasInitialized(pointer)
                    ? $"= {*pointer} " + INITIALIZED_MESSAGE
                    : UNINITIALIZED_MESSAGE)
                + ";";
        }
        #endregion
    }
}