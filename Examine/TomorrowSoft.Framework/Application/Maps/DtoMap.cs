using System.Linq;
using AutoMapper;
using TomorrowSoft.Framework.Domain.Bases;

namespace TomorrowSoft.Framework.Application.Maps
{
    public class DtoMap<TEntity, TDto>
        where TEntity : Entity<TEntity>
    {
        protected IMappingExpression<TEntity, TDto> exp;

        public DtoMap()
        {
            exp = Mapper.CreateMap<TEntity, TDto>();
        }

        public TDto To(TEntity entity)
        {
            var dto = Mapper.Map<TEntity, TDto>(entity);
            ConvertNullToEmptyString(dto);
            return dto;
        }

        private void ConvertNullToEmptyString(TDto dto)
        {
            var type = typeof (TDto);
            var pies = type.GetProperties();
            foreach (var pi in pies
                .Where(pi => pi.PropertyType == typeof(string))
                .Where(pi => pi.GetValue(dto, null) == null))
            {
                pi.SetValue(dto, "", null);
            }
        }
    }
}