using System.Reflection;
using System.Text;

namespace Telecurso2000Programacao.Ferramentas
{
    /// <summary>
    /// Reflects a type and draws an table with it's public fields and properties.
    /// </summary>
    public static class TypeDataTableDrawer
    {
        /// <summary>
        /// The character used in horizontal separators of the table.
        /// </summary>
        private const char HORIZONTAL_SEPARATOR_CHAR = '-';
        /// <summary>
        /// The character used in the column separator of the table. 
        /// </summary>
        private const char COLUM_SEPARATOR_CHAR = '|';
        /// <summary>
        /// The character used when the field or property don't have a value.
        /// </summary>
        private const string NO_VALUE_MESSAGE = "Sem valor";

        /// <summary>
        /// Iterates through a collection and draws a table entry for each value in the collection.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the collection.</typeparam>
        /// <param name="instances">The list of instances that will be used to retrieve the data.</param>
        /// <returns>An enumerable object of strings with the table entries.</returns>
        public static string DrawTable<T>(params T[] instances)
        {
            Type typeInfo = typeof(T);

            MemberInfo[] fields = typeInfo.GetFields();
            MemberInfo[] properties = typeInfo.GetProperties();
            MemberInfo[] members = fields.Concat(properties).ToArray();

            int[] namesLengths = (from member in members
                                  select member.Name.Length).ToArray();
            int[][] valuesLengths = (from instance in instances
                                     from member in members
                                     select GetMemberValue(member, instance) is object value ?
                                     value.ToString()!.Length : NO_VALUE_MESSAGE.Length)
                .Chunk(members.Length)
                .ToArray();

            int[] columnsSizes = GetHighestsInIndex(namesLengths, GetHighestsInIndex(valuesLengths));

            string[] membersNames = (from member in members
                                     select member.Name).ToArray();
            string[][] valuesStrings = (from instance in instances
                                        from member in members
                                        select GetMemberValue(member, instance) is object value ?
                                        value.ToString() : NO_VALUE_MESSAGE)
                .Chunk(members.Length)
                .ToArray();

            string[] lines = BuildTableLines(columnsSizes, membersNames)
                .Concat(BuildTableLines(columnsSizes, valuesStrings))
                .ToArray();

            int columnsQuantity = members.Length + 1;
            int tableSize = columnsSizes.Sum() + columnsQuantity;
            return BuildTable(tableSize, lines);
        }

        private static object? GetMemberValue<T>(MemberInfo member, T instance) => member switch
        {
            FieldInfo field => field.GetValue(instance),
            PropertyInfo property => property.GetIndexParameters().Length == 0 ?
                property.GetValue(instance) : property.GetMethod!.ReturnType,
            _ => null
        };

        private static int[] GetHighestsInIndex(params int[][] nums)
        {
            int[] highests = new int[nums[0].Length];

            foreach (var line in nums)
                for (int i = 0; i < highests.Length; i++)
                    highests[i] = line[i] > highests[i] ? line[i] : highests[i];

            return highests;
        }

        private static string[] BuildTableLines(int[] columnsSizes, params string[][] columnsContent)
        {
            string[] lines = new string[columnsContent.Length];

            var lineBuilder = new StringBuilder();

            for (int i = 0; i < columnsContent.Length; i++)
            {
                lineBuilder.Clear();
                for (int j = 0; j < columnsContent[i].Length; j++)
                    lineBuilder
                        .Append(COLUM_SEPARATOR_CHAR)
                        .Append(columnsContent[i][j])
                        .Append(new string(' ', columnsSizes[j] - columnsContent[i][j].Length));
                lineBuilder.Append(COLUM_SEPARATOR_CHAR);

                lines[i] = lineBuilder.ToString();
            }

            return lines;
        }

        private static string BuildTable(int tableSize, params string[] lines)
        {
            var tableBuilder = new StringBuilder();
            var separator = new string(HORIZONTAL_SEPARATOR_CHAR, tableSize);

            foreach (var line in lines)
                tableBuilder
                .AppendLine(separator)
                .AppendLine(line);
            tableBuilder.Append(separator);

            return tableBuilder.ToString();
        }
    }
}