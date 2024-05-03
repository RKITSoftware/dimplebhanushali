namespace Certificate_Generator.Helpers
{
    public static class AutoMapperExtensions
    {
        public static TDestination Map<TSource, TDestination>(this TSource source, TDestination destination)
        {
            if (destination == null)
            {
                destination = Activator.CreateInstance<TDestination>();
            }

            var sourceProperties = typeof(TSource).GetProperties();
            var destinationProperties = typeof(TDestination).GetProperties();

            foreach (var sourceProp in sourceProperties)
            {
                var destProp = destinationProperties.FirstOrDefault(x => x.Name == sourceProp.Name);

                if (destProp != null && destProp.PropertyType == sourceProp.PropertyType)
                {
                    var value = sourceProp.GetValue(source, null);
                    destProp.SetValue(destination, value);
                }
            }
            return destination;
        }
    }
}
