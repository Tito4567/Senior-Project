using System;
using System.Reflection;
using LacesRepo.Attributes;

namespace LacesRepo.Mappers
{
    public class ResponseMapper<T>
    {
        public T MapFromEntity(Entity source)
        {
            T result = default(T);

            foreach (PropertyInfo propertyInfo in result.GetType().GetProperties())
            {
                if (propertyInfo.CanWrite)
                {
                    string name = ReadAttributeName<ColumnName>(propertyInfo);

                    if (string.IsNullOrEmpty(name))
                    {
                        name = propertyInfo.Name;
                    }

                    if (source.GetValue(name) != null)
                    {
                        propertyInfo.SetValue(result, source.GetValue(name));
                    }
                }
            }

            foreach (FieldInfo fieldInfo in result.GetType().GetFields())
            {
                string name = ReadAttributeName<ColumnName>(fieldInfo);

                if (string.IsNullOrEmpty(name))
                {
                    name = fieldInfo.Name;
                }

                if (source.GetValue(name) != null)
                {
                    fieldInfo.SetValue(result, source.GetValue(name));
                }
            }

            return result;
        }

        private string ReadAttributeName<T>(object any) where T : Attribute
        {
            string attributeName = string.Empty;

            dynamic attributeWithNameProperty = (T)Attribute.GetCustomAttribute(any.GetType(), typeof(T));

            if (attributeWithNameProperty != null)
            {
                attributeName = attributeWithNameProperty.Name;
            }

            return attributeName;
        }
    }
}
