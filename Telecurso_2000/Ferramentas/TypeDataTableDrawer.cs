using System.Reflection;
using System.Text;

namespace Telecurso2000Programacao.Ferramentas
{
    /// <summary>
    /// Draws an table with the data of a type.
    /// </summary>    
    /// <remarks>
    /// This class uses reflection to access the public fields and properties of the type.
    /// </remarks>
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
        public static IEnumerable<string> GetTableDrawing<T>(IEnumerable<T> instances)
        {
            var enumerator = instances.GetEnumerator();
            for (bool hasNext = enumerator.MoveNext(); hasNext; hasNext = enumerator.MoveNext())
                yield return GetTableEntryDrawing(enumerator.Current);
        }

        /// <summary>
        /// Receives an value and draw an table entry listing the type fields and properties,
        /// showing the instance values of these members and the current value of static fields. 
        /// </summary>
        /// <typeparam name="T">The type from which the table entry will be built.</typeparam>
        /// <param name="instance">The instance from which the instance members will have their values taken.</param>
        /// <param name="isNext">Set to true if the entry that needs to be drawn is coming right after another entry.</param>
        /// <returns>A string containing a table-like structure with the type name in the first row, the fields and properties of the type named in the second row and those members values in the third row.</returns>
        public static string GetTableEntryDrawing<T>(T instance, bool isNext = false)
        {
            var sb = new StringBuilder();

            // Getting the type info.
            var typeInfo = typeof(T);
            string typeName = typeInfo.Name;

            // Getting the type fields and properties.
            var fields = typeInfo.GetFields();
            var properties = typeInfo.GetProperties();

            // Getting the quantity of columns and separators between them.
            int columnsQuantity = fields.Length + properties.Length;
            int separatorsQuantity = columnsQuantity + 1;

            // Getting a list of the members that we are dealing with.
            var members = ((MemberInfo[])fields).Concat((MemberInfo[])properties);

            // Getting the horizontal quantity of characters of each colum.
            // The callback method is iterating through the list of members and
            // checking what is the longest string between their names and values. 
            int columnsSize = GetLongestStringOfMemberInfo(members, instance, typeName.Length);

            // Getting the horizontal quantity of characters of the table.
            int tableSize = columnsSize * columnsQuantity + separatorsQuantity;

            // Getting the line separator.
            string entrySeparator = string.Empty.PadRight(tableSize, HORIZONTAL_SEPARATOR_CHAR);

            // Getting the top of the table entry (empty if isNext equals true, the type name otherwise).
            sb.Append(isNext ?
                string.Empty :
                entrySeparator + Environment.NewLine +
                COLUM_SEPARATOR_CHAR + typeName.PadRight(tableSize - 2) + COLUM_SEPARATOR_CHAR + Environment.NewLine +
                entrySeparator + Environment.NewLine);

            // Getting the part of the table containing the variables names.
            sb.Append(COLUM_SEPARATOR_CHAR);
            foreach (var member in members)
                sb.Append(member.Name).Append(new string(' ', columnsSize - member.Name.Length)).Append(COLUM_SEPARATOR_CHAR);
            sb.Append(Environment.NewLine);

            // Getting the part of the table containing the variables values.
            sb.Append(COLUM_SEPARATOR_CHAR);
            foreach (var member in members)
                sb.Append(GetMemberValueString(member, instance).PadRight(columnsSize)).Append(COLUM_SEPARATOR_CHAR);
            sb.Append(Environment.NewLine).Append(entrySeparator);

            return sb.ToString();
        }

        /// <summary>
        /// Get the length of the longest string between the names and values of a list of type members.
        /// </summary>
        /// <typeparam name="T">The type of the instance.</typeparam>
        /// <param name="members">The type members that will have the length of their names and values compared.</param>
        /// <param name="instance">The instance that will be used to get instance members value.</param>
        /// <param name="seed">The initial for the accumulator.</param>
        /// <returns>An integer representing the size of the longest string found.</returns>
        /// <remarks>
        /// The accumulator is an intern variable that holds the longest string value until the return.
        /// </remarks>
        private static int GetLongestStringOfMemberInfo<T>(IEnumerable<MemberInfo> members, T instance, int seed = 0)
        {
            int accumulator = seed;

            var getValueLength = (object value) =>
                value is not null &&
                value.ToString() is string valueString ?
                    valueString.Length :
                    NO_VALUE_MESSAGE.Length;

            foreach (var member in members)
            {
                // Getting the length of the name of the member.
                int nameLength = member.Name.Length;
                int valueLength = 0;

                // Getting the length of the value of the member if it is a FieldInfo or PropertyInfo.
                if (member is FieldInfo field)
                    valueLength = getValueLength(field);
                else if (member is PropertyInfo property)
                    // Checking if the property is an indexer.
                    valueLength = property.GetIndexParameters().Length == 0 ?
                        getValueLength(property) :
                        property.GetMethod!.ReturnType.ToString().Length;

                // Getting the longest string between the name and the value.
                int compareValue = nameLength > valueLength ? nameLength : valueLength;

                // Save the biggest value between the compared value and the current biggest.
                accumulator = compareValue > accumulator ? compareValue : accumulator;
            }

            return accumulator;
        }

        /// <summary>
        /// Returns the value of a type member as a string.
        /// </summary>
        /// <typeparam name="T">The type of the instance.</typeparam>
        /// <param name="member">The member that will have it's value returned as a string.</param>
        /// <param name="instance">The instance that will be used to get instance members value.</param>
        /// <returns>The value of the member as a string.</returns>
        private static string GetMemberValueString<T>(MemberInfo member, T instance)
        {
            var getValueString = (object value) =>
                value is not null &&
                value.ToString() is string valueString ?
                    valueString :
                    NO_VALUE_MESSAGE;

            // Getting the string of the member if it is a field.
            if (member is FieldInfo field)
                return getValueString(field);
            // Getting the string of the member if it is a property.
            else if (member is PropertyInfo property)
                // Checking if the property is a indexer.
                return property.GetIndexParameters().Length == 0 ?
                    getValueString(property) :
                    property.GetMethod!.ReturnType.ToString();
            // Return a special message if the member isn't a field or property.
            else
                return NO_VALUE_MESSAGE;
        }
    }
}