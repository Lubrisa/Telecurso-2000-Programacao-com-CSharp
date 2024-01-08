namespace Telecurso2000Programacao.Ferramentas
{
    public static class TypeExtensions
    {
        private static Dictionary<Type, object> defaultValues = new();

        public static object? DefaultValue(this Type type) => type switch
        {
            var typeInfo when typeInfo.IsByRef => null,
            var typeInfo when defaultValues.ContainsKey(typeInfo) => defaultValues[typeInfo],
            var typeInfo =>
                defaultValues.TryAdd(typeInfo, GetDefault(typeInfo)) is var _ ?
                    defaultValues[typeInfo] : null
        };

        private static object GetDefault(Type type)
        {
            var defaultGetter = DefaultGetter<bool>;

            return defaultGetter
                .Method
                .GetGenericMethodDefinition()
                .MakeGenericMethod(type)
                .Invoke(null, null)!;
        }

        private static object DefaultGetter<T>() where T : struct
            => default(T);
    }
}