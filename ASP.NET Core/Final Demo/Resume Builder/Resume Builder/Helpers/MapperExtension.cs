namespace Resume_Builder.Helpers
{
    public static class MapperExtension
    {
        public static TDestination Map<TSource, TDestination>(this TSource source, TDestination destination)
        {
            if (destination == null)
            {
                destination = Activator.CreateInstance<TDestination>();
            }

            var sourceProperties = source.GetType().GetProperties();
            var destinationProperties = typeof(TDestination).GetProperties();

            if (sourceProperties != null && destinationProperties != null)
            {
                foreach (var sourceProp in sourceProperties)
                {
                    var destProp = destinationProperties.FirstOrDefault(x => x.Name == sourceProp.Name);

                    if (destProp != null && destProp.PropertyType == sourceProp.PropertyType)
                    {
                        var value = sourceProp.GetValue(source, null);
                        destProp.SetValue(destination, value);
                    }
                }
            }
            else
            {
                // Handle the case where sourceProperties or destinationProperties is null
                throw new InvalidOperationException("Source or destination properties are null.");
            }

            return destination;
        }
    }
}
