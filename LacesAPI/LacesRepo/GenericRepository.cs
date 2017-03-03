using System.Collections.Generic;
using LacesRepo.Mappers;

namespace LacesRepo
{
    public class GenericRepository<T>
    {
        public bool Create(T request)
        {
            Entity entity = new EntityMapper().MapToEntity(request);

            return new BaseRepository().Create(entity);
        }

        public List<T> Read(SearchEntity search)
        {
            List<T> result = new List<T>();

            List<Entity> readResult = new BaseRepository().Read(search);

            foreach (Entity entity in readResult)
            {
                result.Add(new ResponseMapper<T>().MapFromEntity(entity));
            }

            return result;
        }

        public bool Update(T request)
        {
            Entity entity = new EntityMapper().MapToEntity(request);

            return new BaseRepository().Update(entity);
        }

        public bool Delete(T request)
        {
            Entity entity = new EntityMapper().MapToEntity(request);

            return new BaseRepository().Delete(entity);
        }
    }
}
