using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace LacesRepo
{
    public class BaseRepository
    {
        public bool Create(Entity entity)
        {
            bool result = false;

            try
            {
                SqlCommand command = new SqlCommand();

                CommandBuilder builder = new CommandBuilder();

                command.CommandText = builder.BuildCommandTextForCreate(entity);
                command.Connection = new SqlConnection(entity.ConnectionString);
                command.Parameters.AddRange(builder.BuildSqlParametersForModify(entity));

                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                command.Connection.Close();

                result = true;
            }
            catch { }

            return result;
        }

        public List<Entity> Read(SearchEntity search)
        {
            List<Entity> result = new List<Entity>();

            try
            {
                SetConditionIndexes(ref search);

                SqlCommand command = new SqlCommand();

                CommandBuilder builder = new CommandBuilder();

                command.CommandText = builder.BuildCommandTextForRead(search);
                command.Connection = new SqlConnection(search.ConnectionString);
                command.Parameters.AddRange(builder.BuildSqlParametersForRead(search));

                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Entity entity = new Entity();

                    for (int colIndex = 0; colIndex < reader.FieldCount; colIndex++)
                    {
                        entity.SetValue(reader.GetName(colIndex), reader[colIndex]);
                    }

                    result.Add(entity);
                }

                command.Connection.Close();
            }
            catch { }

            return result;
        }

        public bool Update(Entity entity)
        {
            bool result = false;

            try
            {
                SqlCommand command = new SqlCommand();

                CommandBuilder builder = new CommandBuilder();

                command.CommandText = builder.BuildCommandTextForUpdate(entity);
                command.Connection = new SqlConnection(entity.ConnectionString);
                command.Parameters.AddRange(builder.BuildSqlParametersForModify(entity));

                SqlDataReader reader = command.ExecuteReader();

                command.Connection.Close();

                result = true;
            }
            catch { }

            return result;
        }

        public bool Delete(Entity entity)
        {
            bool result = false;

            try
            {
                SqlCommand command = new SqlCommand();

                CommandBuilder builder = new CommandBuilder();

                command.CommandText = builder.BuildCommandTextForDelete(entity);
                command.Connection = new SqlConnection(entity.ConnectionString);
                command.Parameters.Add(new SqlParameter(entity.PrimaryKeyName, entity.GetValue(entity.PrimaryKeyName)));

                SqlDataReader reader = command.ExecuteReader();

                command.Connection.Close();

                result = true;
            }
            catch { }

            return result;
        }

        private void SetConditionIndexes(ref SearchEntity search)
        {
            int index = 0;

            foreach (Condition cond in search.Conditions)
            {
                cond.Index = index;
            }
        }
    }
}
