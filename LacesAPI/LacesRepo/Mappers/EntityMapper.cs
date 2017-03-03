using System;
using System.Reflection;
using LacesRepo.Attributes;

namespace LacesRepo.Mappers
{
    public class EntityMapper
    {
        public Entity MapToEntity(object source)
        {
            Entity result = new Entity();

            result.ConnectionString = ReadAttributeName<ConnectionString>(source);
            result.PrimaryKeyName = ReadAttributeName<PrimaryKeyName>(source);
            result.SchemaName = ReadAttributeName<SchemaName>(source);
            result.TableName = ReadAttributeName<TableName>(source);

            foreach (PropertyInfo propertyInfo in source.GetType().GetProperties())
            {
                if (propertyInfo.CanRead && propertyInfo.GetValue(source) != null)
                {
                    string name = ReadAttributeName<ColumnName>(propertyInfo);

                    if (string.IsNullOrEmpty(name))
                    {
                        name = propertyInfo.Name;
                    }

                    result.SetValue(name, propertyInfo.GetValue(source));
                }
            }

            foreach (FieldInfo fieldInfo in source.GetType().GetFields())
            {
                if (fieldInfo.GetValue(source) != null)
                {
                    string name = ReadAttributeName<ColumnName>(fieldInfo);

                    if (string.IsNullOrEmpty(name))
                    {
                        name = fieldInfo.Name;
                    }

                    result.SetValue(fieldInfo.Name, fieldInfo.GetValue(source));
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
