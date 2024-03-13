namespace IMGCloud.Infrastructure.Extensions
{
    public static class EntityMapper
    {
        public static TEntity MapFor<TEntity>(this TEntity source, object dest)
        {
            var fromProperties = dest.GetType().GetProperties();
            var toProperties = source.GetType().GetProperties();

            foreach (var fromProperty in fromProperties)
            {
                foreach (var toProperty in toProperties)
                {
                    if (fromProperty.Name.ToUpper() == toProperty.Name.ToUpper() && fromProperty.PropertyType == toProperty.PropertyType)
                    {
                        if (fromProperty != null && fromProperty.CanWrite)
                        {
                            toProperty.SetValue(source, fromProperty.GetValue(dest));
                            break;
                        }
                    }
                }
            }
            return source;
        }
    }
}
