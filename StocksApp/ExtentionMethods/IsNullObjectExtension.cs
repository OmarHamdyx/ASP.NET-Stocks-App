using System.Reflection;

namespace StocksApp.ExtentionMethods
{
	public static class IsNullObjectExtension
	{

		public static bool AreAllPropertiesNotNull(this object obj)
        {
            if (obj != null)
            {
                // Get all properties of the object
                PropertyInfo[] properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo property in properties)
                {
                    // Get the value of the property
                    object? value = property.GetValue(obj);

                    // If any property is null, return false
                    if (value == null)
                    {
                        return false;
                    }
                }
                return true;
            }

            throw new ArgumentNullException(nameof(obj));
        }
    }
}

