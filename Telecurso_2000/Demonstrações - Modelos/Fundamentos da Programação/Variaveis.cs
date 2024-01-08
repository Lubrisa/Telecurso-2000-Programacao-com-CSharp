using System.Reflection;
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

            Console.WriteLine(
                TypeDataTableDrawer.DrawTable([
                new Primitivo(typeof(bool)),
                new Primitivo(typeof(byte)),
                new Primitivo(typeof(char)),
                new Primitivo(typeof(double)),
                new Primitivo(typeof(float)),
                new Primitivo(typeof(int)),
                new Primitivo(typeof(long)),
                new Primitivo(typeof(short)),
                new Primitivo(typeof(sbyte)),
                new Primitivo(typeof(uint)),
                new Primitivo(typeof(ulong)),
                new Primitivo(typeof(ushort)),
                new Primitivo(typeof(nint)),
                new Primitivo(typeof(nuint)),
                ])
            );

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

            char c = 'L';
            int i = c;
            long l = i;
            float f = l;
            double d = f;

            Console.WriteLine(
                TypeDataTableDrawer.DrawTable([
                    new ParTipoValor(c.GetType(), c),
                    new ParTipoValor(i.GetType(), i),
                    new ParTipoValor(l.GetType(), l),
                    new ParTipoValor(f.GetType(), f),
                    new ParTipoValor(d.GetType(), d),
                ])
            );

            Console.WriteLine();
            SeparatorDrawer.DrawSeparator("Conversão Explicita");
            Console.WriteLine();

            double d2 = double.Pi;
            float f2 = (float)d2;
            long l2 = (long)f2;
            int i2 = (int)l2;
            char c2 = (char)i2;

            Console.WriteLine(
                TypeDataTableDrawer.DrawTable([
                    new ParTipoValor(d2.GetType(), d2),
                    new ParTipoValor(f2.GetType(), f2),
                    new ParTipoValor(l2.GetType(), l2),
                    new ParTipoValor(i2.GetType(), i2),
                    new ParTipoValor(c2.GetType(), c2),
                ])
            );

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

        private readonly record struct Primitivo()
        {
            public readonly string Nome { get; init; }
            public readonly string Default { get; init; }
            public readonly string ValoresImportantes { get; init; }

            public Primitivo(Type tipoInfo) : this()
            {
                if (!tipoInfo.IsPrimitive)
                    throw new ArgumentException(
                        $"O tipo {tipoInfo.Name} não é primitivo"
                    );

                Nome = tipoInfo.Name;

                object valorDefault = tipoInfo.DefaultValue()!;
                Default = valorDefault.ToString()!;

                var campos =
                    from campo in tipoInfo.GetFields()
                    select campo.GetValue(valorDefault)!.ToString();

                ValoresImportantes = campos
                    .ToArray()
                    .Stringify();
            }
        };

        private readonly record struct ParTipoValor(Type Tipo, object Valor);
#pragma warning restore CS8500 // Isso pega o endereço, obtém o tamanho ou declara um ponteiro para um tipo gerenciado
    }
}