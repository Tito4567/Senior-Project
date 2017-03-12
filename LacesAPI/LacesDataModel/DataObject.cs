using System;
using LacesRepo;
using LacesRepo.Mappers;
using System.Collections.Generic;

namespace LacesDataModel
{
    public abstract class DataObject
    {
        public DataObject() {  }

        public DataObject(int id)
        {
            Load(id);
        }

        public virtual void Load(int id) { }

        public virtual bool Add()
        {
            Entity entity = new EntityMapper().MapToEntity(this);

            return new BaseRepository().Create(entity);
        }

        public virtual bool Update()
        {
            Entity entity = new EntityMapper().MapToEntity(this);

            return new BaseRepository().Update(entity);
        }

        public bool Delete()
        {
            Entity entity = new EntityMapper().MapToEntity(this);

            return new BaseRepository().Delete(entity);
        }

        // This may need revisiting in the future. Currently it does a SELECT * rather than naming specific columns,
        // which is not best practice.
        // Additionally, using generics in the DataObject class is not ideal. This should probably be added to the
        // GenericRepository class.
        public T GetByValue<T>(string column, string value, string tableName, string schemaName)
        {
            T result = default(T);

            SearchEntity search = new SearchEntity();

            search.ConnectionString = Constants.CONNECTION_STRING;

            Condition searchCond = new Condition();
            searchCond.Column = column;
            searchCond.Operator = Condition.Operators.EqualTo;
            searchCond.Value = value;

            search.Conditions.Add(searchCond);

            search.PageSizeLimit = 1;
            search.SchemaName = schemaName;
            search.TableName = tableName;

            List<T> response = new GenericRepository<T>().Read(search);

            if (response.Count > 0)
            {
                result = response[0];
            }

            return result;
        }
    }
}
