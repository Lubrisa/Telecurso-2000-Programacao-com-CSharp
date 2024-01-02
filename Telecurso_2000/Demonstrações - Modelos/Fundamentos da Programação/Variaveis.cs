using System.Runtime.InteropServices;
using Telecurso2000Programacao.Ferramentas;

namespace Telecurso2000Programacao.Demonstracoes.Modelos.FundamentosDaProgramacao
{
    /// <summary>
    /// Modelo para a demonstração prática da aula sobre variáveis.
    /// </summary>
    public static unsafe class Variaveis
    {
#pragma warning disable CS8500 // Isso pega o endereço, obtém o tamanho ou declara um ponteiro para um tipo gerenciado
        /// <summary>
        /// Demonstra como as variáveis funcionam internamente (expõe o endereço subjacente).
        /// </summary>
        public static void Definicao()
        {
            SeparatorDrawer.DrawSeparator("Definição de variável");
            Console.WriteLine();

            int x = 0;
            DrawAddressTableEntry(&x, nameof(x));
            string y = "str";
            DrawAddressTableEntry(&y, nameof(y));
            bool z = false;
            DrawAddressTableEntry(&z, nameof(z));

            Console.WriteLine();
            SeparatorDrawer.DrawSeparator();
        }

        /// <summary>
        /// Demonstra quais são os tipos primitivos do C#.
        /// </summary>
        public static void TiposPrimitivos()
        {
            SeparatorDrawer.DrawSeparator("Tipos Primitivos");
            Console.WriteLine();

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

            Console.WriteLine();
            SeparatorDrawer.DrawSeparator();
        }

        /// <summary>
        /// Demonstra como as variáveis são declaradas e atribuídas.
        /// </summary>
        public static void DeclaracaoEAtribuicao()
        {
            SeparatorDrawer.DrawSeparator("Declaração e Atribuição");
            Console.WriteLine();

            int x;
            string y = "declarado";
            bool z = false;
            object w;

            Console.WriteLine(GetVariableDeclaration(&x, nameof(x)));
            Console.WriteLine(GetVariableDeclaration(&y, nameof(y)));
            Console.WriteLine(GetVariableDeclaration(&z, nameof(z)));
            Console.WriteLine(GetVariableDeclaration(&w, nameof(w)));

            Console.WriteLine();
            SeparatorDrawer.DrawSeparator();
        }

        /// <summary>
        /// Demonstra como funciona a conversão de tipo, tanto implícita quanto explicita.
        /// </summary>
        public static void TypeCasting()
        {
            SeparatorDrawer.DrawSeparator("Conversão de Tipo");
            Console.WriteLine();
            SeparatorDrawer.DrawSeparator("Conversão Implícita");
            Console.WriteLine();

            char a = 'a';
            int b = a;
            long c = b;
            float d = c;
            double e = d;
            Console.WriteLine(GetIsOfTypeMessage(a));
            Console.WriteLine(GetIsOfTypeMessage(b));
            Console.WriteLine(GetIsOfTypeMessage(c));
            Console.WriteLine(GetIsOfTypeMessage(d));
            Console.WriteLine(GetIsOfTypeMessage(e));

            Console.WriteLine();
            SeparatorDrawer.DrawSeparator("Conversão Explicita");
            Console.WriteLine();

            double v = 173233333252.763363337d;
            float w = (float)v;
            long x = (long)w;
            int y = (int)x;
            char z = (char)y;
            Console.WriteLine(GetIsOfTypeMessage(v));
            Console.WriteLine(GetIsOfTypeMessage(w));
            Console.WriteLine(GetIsOfTypeMessage(x));
            Console.WriteLine(GetIsOfTypeMessage(y));
            Console.WriteLine(GetIsOfTypeMessage(z));

            Console.WriteLine();
            SeparatorDrawer.DrawSeparator();
        }

        /// <summary>
        /// Demonstra como os tipos de valor e referência funcionam.
        /// </summary>
        public static void TiposDeValorEReferencia()
        {
            SeparatorDrawer.DrawSeparator("Tipos de Valor e Referência");
            Console.WriteLine();
            SeparatorDrawer.DrawSeparator("Atribuição por Valor");
            Console.WriteLine();

            int w = 0;
            int x = w;
            DrawAddressTableEntry(&w, nameof(w));
            DrawAddressTableEntry(&x, nameof(x));

            Console.WriteLine();
            SeparatorDrawer.DrawSeparator("Atribuição por Referência - Endereço da Variável");
            Console.WriteLine();

            object y = new();
            object z = y;
            DrawAddressTableEntry(&y, nameof(y));
            DrawAddressTableEntry(&z, nameof(z));

            Console.WriteLine();
            SeparatorDrawer.DrawSeparator("Atribuição por Referência - Endereço do Valor");
            Console.WriteLine();

            DrawAddressTableEntry(y, nameof(y));
            DrawAddressTableEntry(z, nameof(z));

            Console.WriteLine();
            SeparatorDrawer.DrawSeparator();
        }

        #region Helpers
        /// <summary>
        /// Converte a referência de ponteiro em um endereço de memória.
        /// </summary>
        /// <typeparam name="T">Tipo da variável referenciada pelo ponteiro.</typeparam>
        /// <param name="pointer">O ponteiro que terá sua referência convertida.</param>
        /// <returns>Um endereço de memória em string.</returns>
        private static string StringfyAddress<T>(T* pointer)
        {
            string addressToHexadecimal = ((nint)pointer).ToString("X");
            return $"0x{addressToHexadecimal}";
        }

        /// <summary>
        /// Converte a referência de uma instância de um tipo de referência em um endereço de memória.
        /// </summary>
        /// <typeparam name="T">O tipo da referência.</typeparam>
        /// <param name="obj">O objeto que terá seu endereço retornado.</param>
        /// <returns>Um endereço de memória em string.</returns>
        private static string StringfyAddress<T>(T obj) where T : class
        {
            var handle = GCHandle.Alloc(obj, GCHandleType.Pinned);
            string handleToHexadecimal = handle.AddrOfPinnedObject().ToString("X");
            handle.Free();

            return $"0x{handleToHexadecimal}";
        }

        /// <summary>
        /// Escreve no console o nome, valor e endereço de uma variável em uma estrutura de tabela.
        /// </summary>
        /// <typeparam name="T">O tipo da variável referenciada pelo ponteiro.</typeparam>
        /// <param name="pointer">Um ponteiro com uma referência a um endereço de memória.</param>
        /// <param name="variableName">O nome da variável referenciada pelo ponteiro.</param>
        private static void DrawAddressTableEntry<T>(T* pointer, string variableName)
        {
            string variableColum = $"Variável: {variableName}".PadRight(25);
            string valueColum = $"Valor: {*pointer}".PadRight(25);
            string addressColum = $"Endereço: {StringfyAddress(pointer)}";

            Console.WriteLine(variableColum + valueColum + addressColum);
        }

        private static void DrawAddressTableEntry<T>(T obj, string variableName) where T : class
        {
            string variableColum = $"Variável: {variableName}".PadRight(25);
            string valueColum = $"Valor: {obj}".PadRight(25);
            string addressColum = $"Endereço: {StringfyAddress(obj)}";

            Console.WriteLine(variableColum + valueColum + addressColum);
        }

        /// <summary>
        /// Escreve no console o nome, valores importantes e algumas notas sobre um tipo.
        /// </summary>
        /// <typeparam name="T">O tipo que será descrito.</typeparam>
        /// <param name="notes">Notas sobre o tipo descrito.</param>
        /// <param name="values">Valores importantes do tipo descrito.</param>
        private static void DrawTypeTableEntry<T>(string notes = "", params T[] values) where T : struct
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
        private static bool WasInitialized<T>(T* pointer) => !Equals(*pointer, default(T));

        /// <summary>
        /// Cria uma mensagem que corresponde a como uma variável foi declarada de acordo com seu estado atual.
        /// </summary>
        /// <typeparam name="T">O tipo da variável que será escrita.</typeparam>
        /// <param name="pointer">Um ponteiro para o endereço da variável que será escrita.</param>
        /// <param name="variableName">O nome da variável escrita.</param>
        /// <returns>Uma mensagem que corresponde a como a variável deve ter sido declarada de acordo com seu valor atual.</returns>
        private static string GetVariableDeclaration<T>(T* pointer, string variableName)
        {
            const string UNINITIALIZED_MESSAGE = "// A variável provavelmente não foi inicializada";
            const string INITIALIZED_MESSAGE = "// A variável foi inicializada";

            return $"{typeof(T)} {variableName} "
                + (WasInitialized(pointer)
                    ? $"= {*pointer} " + INITIALIZED_MESSAGE
                    : UNINITIALIZED_MESSAGE)
                + ";";
        }

        /// <summary>
        /// Cria uma mensagem que diz qual o tipo de um valor.
        /// </summary>
        /// <typeparam name="T">O tipo do valor que será descrito na mensagem.</typeparam>
        /// <param name="value">O valor que terá seu tipo descrito.</param>
        /// <returns>Uma mensagem dizendo qual é o tipo do valor passado.</returns>
        private static string GetIsOfTypeMessage<T>(T value)
        {
            const string IS_OF_TYPE_MESSAGE = "é um valor do tipo: ";

            return $"{value} {IS_OF_TYPE_MESSAGE} {typeof(T)}";
        }
        #endregion
#pragma warning restore CS8500 // Isso pega o endereço, obtém o tamanho ou declara um ponteiro para um tipo gerenciado
    }
}