using System.Text;

namespace Telecurso2000Programacao.Ferramentas
{
    public static class ArrayExtensions
    {
        public static string Stringify(this Array arr)
        {
            var builder = new StringBuilder("[ ");
            const string NULL_LABEL = "Null";

            foreach (var element in arr)
            {
                if (element is Array elements)
                    builder
                        .Append(elements?.Stringify() ?? NULL_LABEL);
                else
                    builder
                        .Append(element?.ToString() ?? NULL_LABEL);

                builder
                    .Append(", ");
            }

            if (builder.Length > 2)
                builder.Remove(builder.Length - 2, 1);
            builder.Append("]");

            return builder.ToString();
        }
    }
}