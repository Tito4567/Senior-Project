using System;
using LacesRepo;
using LacesRepo.Mappers;

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

        public bool Update()
        {
            Entity entity = new EntityMapper().MapToEntity(this);

            return new BaseRepository().Update(entity);
        }

        public bool Delete()
        {
            Entity entity = new EntityMapper().MapToEntity(this);

            return new BaseRepository().Delete(entity);
        }
    }
}
