using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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

            object inteiro = 0;
            object real = 6.9f;
            object caractere = '!';
            object texto = "str";
            object confirmacao = true;

            Console.WriteLine(
                TypeDataTableDrawer.DrawTable([
                new Variavel<object>(nameof(inteiro), inteiro, &inteiro),
                new Variavel<object>(nameof(real), real, &real),
                new Variavel<object>(nameof(caractere), caractere, &caractere),
                new Variavel<object>(nameof(texto), texto, &texto),
                new Variavel<object>(nameof(confirmacao), confirmacao, &confirmacao)
                ])
            );

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

            Console.WriteLine();
            SeparatorDrawer.DrawSeparator("Conversão Explicita");
            Console.WriteLine();

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

            Console.WriteLine();
            SeparatorDrawer.DrawSeparator("Atribuição por Referência - Endereço da Variável");
            Console.WriteLine();

            Console.WriteLine();
            SeparatorDrawer.DrawSeparator("Atribuição por Referência - Endereço do Valor");
            Console.WriteLine();

            Console.WriteLine();
            SeparatorDrawer.DrawSeparator();
        }

        /// <summary>
        /// Helper class that make strings that the <c>Variaveis</c> class needs.
        /// </summary>
        private static class VariaveisDrawer
        {
            /// <summary>
            /// Stringifies the address pointed by a pointer.
            /// </summary>
            /// <param name="pointer">The pointer that holds the address that will be stringified.</param>
            /// <returns>A string with a memory address.</returns>
            public static string StringifyAddress<T>(T* pointer)
            {
                string ADDRESS_PREFIX = "0x";
                nint Address = (nint)pointer;

                return ADDRESS_PREFIX + Address.ToString("X");
            }

            /// <summary>
            /// Builds a string representing a entry in a table that contains a variable's name, memory address and value.
            /// </summary>
            /// <param name="variable">The variable that will be used to build the table entry.</param>
            /// <returns>A string formatted like a table entry containing the info about the given variable.</returns>
            public static string GetAddressTableEntry<T>(Variavel<T> variable)
            {
                const string NAME_LABEL = $"Nome";
                const string ADDRESS_LABEL = $"Endereço";
                const string VALUE_LABEL = $"Valor";
                const string NULL_VALUE_LABEL = "NULL";

                int tableColumSize = GetLongestStringSize(NAME_LABEL,
                    variable.Nome,
                    ADDRESS_LABEL,
                    variable.Endereco,
                    VALUE_LABEL,
                    variable.Valor?.ToString() ?? NULL_VALUE_LABEL);


                string entryHeader = BuildTableEntryLine(tableColumSize, NAME_LABEL, ADDRESS_LABEL, VALUE_LABEL);
                string entryBody = BuildTableEntryLine(tableColumSize,
                    variable.Nome,
                    variable.Endereco,
                    variable.Valor?.ToString() ?? NULL_VALUE_LABEL);

                return BuildTableEntry(entryHeader, entryBody);
            }

            /// <summary>
            /// Builds a string representing a entry in a table that contains a primitive type name, and default value.
            /// </summary>
            /// <param name="T">The primitive type that will be have it's info listed in the table.</param>
            /// <returns>A string formatted like a table entry containing the info about the given primitive.</returns>
            public static string GetPrimitiveInfoTableEntry<T>() where T : unmanaged
            {
                const string NAME_LABEL = "Nome";
                const string DEFAULT_LABEL = "Valor Padrão";

                string typeName = typeof(T).Name;
                string typeDefaultValue = typeof(T) == typeof(char) ? "\"\"" : default(T).ToString()!;

                int tableColumSize = GetLongestStringSize(NAME_LABEL,
                    typeName,
                    DEFAULT_LABEL,
                    typeDefaultValue);

                string entryHeader = BuildTableEntryLine(tableColumSize, NAME_LABEL, DEFAULT_LABEL);
                string entryBody = BuildTableEntryLine(tableColumSize, typeName, typeDefaultValue);

                return BuildTableEntry(entryHeader, entryBody);
            }

            /// <summary>
            /// Find and return the size of the longest string between the given strings.
            /// </summary>
            /// <param name="strings">The strings that will have their lengths compared.</param>
            /// <returns>The length of the longest string. 0 if the given array is empty.</returns>
            /// <exception cref="ArgumentException"></exception>
            private static int GetLongestStringSize(params string[] strings)
            {
                if (strings is null)
                    throw new ArgumentException("The argument is null");

                if (strings.Length == 0)
                    return 0;

                return strings
                    .MaxBy(texto => texto?.Length ?? 0)?
                    .Length ?? 0;
            }

            /// <summary>
            /// Builds a line of a table entry body with the columns values.
            /// </summary>
            /// <param name="columnsSize">The size of each column.</param>
            /// <param name="columnsValues">The text contained in each column.</param>
            /// <returns>A string with the table entry body.</returns>
            /// <exception cref="ArgumentException"></exception>
            private static string BuildTableEntryLine(int columnsSize, params string[] columnsValues)
            {
                if (columnsValues is null)
                    throw new ArgumentException("The \"columnsValues\" argument is null");

                const char SEPARATOR_CHAR = '|';

                var sb = new StringBuilder(SEPARATOR_CHAR.ToString());
                foreach (var valueString in from columnValue in columnsValues
                                            select columnValue?.ToString())
                {
                    sb.Append(valueString)
                        .Append(string.Empty.PadRight(columnsSize - valueString.Length))
                        .Append(SEPARATOR_CHAR);
                }

                return sb.ToString();
            }

            /// <summary>
            /// Builds the entry of a table with the given entry body (content).
            /// </summary>
            /// <param name="entryBody"></param>
            /// <returns></returns>
            private static string BuildTableEntry(params string[] entryBody)
            {
                int tableLength = GetLongestStringSize(entryBody);

                string tableSeparator = string.Empty.PadRight(tableLength, '-');

                var sb = new StringBuilder();
                foreach (var entryLayer in entryBody)
                    sb.AppendLine(tableSeparator)
                        .AppendLine(entryLayer);
                sb.Append(tableSeparator);

                return sb.ToString();
            }
        }

        /// <summary>
        /// Uma estrutura para representar uma variável.
        /// </summary>
        /// <typeparam name="T">O tipo da variável.</typeparam>
        /// <param name="Nome">O nome da variável.</param>
        /// <param name="Valor">O valor da variável.</param>
        private readonly record struct Variavel<T>(string Nome, T Valor)
        {
            /// <summary>
            /// O endereço de memória da variável.
            /// </summary>
            public readonly string Endereco { get; init; }

            /// <param name="nome">O nome da variável.</param>
            /// <param name="valor">O valor da variável.</param>
            /// <param name="endereco">O endereço de memória da variável.</param>
            public Variavel(string nome, T valor, T* endereco) : this(nome, valor) =>
                Endereco = VariaveisDrawer.StringifyAddress(endereco);
        }
#pragma warning restore CS8500 // Isso pega o endereço, obtém o tamanho ou declara um ponteiro para um tipo gerenciado
    }
}