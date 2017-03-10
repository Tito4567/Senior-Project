using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace LacesRepo
{
    internal class CommandBuilder
    {
        #region Create

        internal string BuildCommandTextForCreate(Entity entity)
        {
            StringBuilder command = new StringBuilder();

            command.AppendLine("INSERT INTO " + entity.SchemaName + "." + entity.TableName);
            command.AppendLine("(");

            List<string> columns = entity.GetKeys();

            command.AppendLine(columns[0]);

            for (int i = 1; i < columns.Count; i++)
            {
                command.AppendLine(", " + columns[i]);
            }

            command.AppendLine(")");
            command.AppendLine("VALUES");
            command.AppendLine("(");

            command.AppendLine("@" + columns[0]);

            for (int i = 1; i < columns.Count; i++)
            {
                command.AppendLine(", @" + columns[i]);
            }

            command.AppendLine(")");

            return command.ToString();
        }

        #endregion

        #region Read

        internal string BuildCommandTextForRead(SearchEntity search)
        {
            StringBuilder command = new StringBuilder();

            if (search.PageSizeLimit > 0)
            {
                command.AppendLine("SELECT TOP(" + search.PageSizeLimit + ")");
            }
            else
            {
                command.AppendLine("SELECT");
            }

            if (search.ColumnsToReturn == null || search.ColumnsToReturn.Count == 0)
            {
                command.AppendLine("*");
            }
            else
            {
                command.AppendLine(search.ColumnsToReturn[0]);

                for (int i = 1; i < search.ColumnsToReturn.Count; i++)
                {
                    command.AppendLine(", " + search.ColumnsToReturn[i]);
                }
            }

            command.AppendLine("FROM " + search.SchemaName + "." + search.TableName);

            if (search.Conditions != null && search.Conditions.Count > 0)
            {
                command.AppendLine("WHERE");

                command.AppendLine(search.Conditions[0].ToString());

                for (int i = 1; i < search.Conditions.Count; i++)
                {
                    command.AppendLine("AND " + search.Conditions[i].ToString());
                }
            }

            if (search.OrderBy != null)
            {
                command.AppendLine("ORDER BY " + search.OrderBy.Column + " " + Enum.GetName(typeof(OrderByDirection), search.OrderBy.Direction));
            }

            return command.ToString();
        }

        internal SqlParameter[] BuildSqlParametersForRead(SearchEntity search)
        {
            List<SqlParameter> result = new List<SqlParameter>();

            foreach (Condition cond in search.Conditions)
            {
                result.Add(new SqlParameter(cond.GetParameterName(), cond.Value));
            }

            return result.ToArray();
        }

        #endregion

        #region Update

        internal string BuildCommandTextForUpdate(Entity entity)
        {
            StringBuilder command = new StringBuilder();

            command.AppendLine("UPDATE " + entity.SchemaName + "." + entity.TableName);
            command.AppendLine("SET");

            List<string> columns = entity.GetKeys();

            command.AppendLine(columns[0] + " = @" + columns[0]);

            for (int i = 1; i < columns.Count; i++)
            {
                command.AppendLine(", " + columns[i] + " = @" + columns[i]);
            }

            command.AppendLine("WHERE " + entity.PrimaryKeyName + " = @" + entity.PrimaryKeyName);

            return command.ToString();
        }

        #endregion

        #region Delete

        internal string BuildCommandTextForDelete(Entity entity)
        {
            StringBuilder command = new StringBuilder();

            command.AppendLine("DELETE FROM " + entity.SchemaName + "." + entity.TableName);
            command.AppendLine("SET");
            command.AppendLine("WHERE " + entity.PrimaryKeyName + " = @" + entity.PrimaryKeyName);

            return command.ToString();
        }

        #endregion

        #region Shared

        internal SqlParameter[] BuildSqlParametersForModify(Entity entity)
        {
            List<SqlParameter> result = new List<SqlParameter>();

            List<string> keys = entity.GetKeys();

            foreach (string key in keys)
            {
                result.Add(new SqlParameter(key, entity.GetValue(key)));
            }

            return result.ToArray();
        }

        #endregion
    }
}
