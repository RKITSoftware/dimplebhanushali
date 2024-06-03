namespace MiddlewareWithFilters.Helpers
{
    /// <summary>
    /// Extension methods for object mapping.
    /// </summary>
    public static class MapperExtension
    {
        #region Public Methods

        /// <summary>
        /// Maps properties from the source object to the destination object.
        /// </summary>
        /// <typeparam name="TSource">The type of the source object.</typeparam>
        /// <typeparam name="TDestination">The type of the destination object.</typeparam>
        /// <param name="source">The source object.</param>
        /// <param name="destination">The destination object.</param>
        /// <returns>The destination object with mapped properties.</returns>
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

        #endregion
    }
}
