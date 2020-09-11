using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Ticketmanagement.BusinessLogic.Data_Transfer_Objects;
using Ticketmanagement.BusinessLogic.Interfaces;
using Ticketmanagement.BusinessLogic.Validations;
using TicketManagement.DataAccess.Entities;
using TicketManagement.DataAccess.Repositories.SqlRepository;

namespace Ticketmanagement.BusinessLogic.Services
{
    public class AreaService : ICrud<AreaDto>
    {
        // FIELDS
        private readonly SqlAreaRepository _areaRepository;

        // CONSTRUCTORS
        public AreaService(SqlAreaRepository repository)
            => _areaRepository = repository;

        // METHODS
        public void CreateElement(AreaDto item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            AreaServiceValidation.CheckCoordX(item);
            AreaServiceValidation.CheckCoordY(item);
            AreaServiceValidation.CheckDescription(GetAllElements().Where(x => x.LayoutId == item.LayoutId), item);

            _areaRepository.Create(Mapping().Map<AreaDto, AreaEntity>(item));
        }

        public void DeleteElement(int id)
        {
            if (AreaServiceValidation.CheckId(GetAllElements().Count(), id))
            {
                _areaRepository.Delete(id);
            }
        }

        public IEnumerable<AreaDto> GetAllElements()
        {
            List<AreaDto> elements = new List<AreaDto>();

            foreach (var i in Mapping().Map<List<AreaDto>>(_areaRepository.GetAll()))
            {
                elements.Add(i);
            }

            return elements;
        }

        public AreaDto GetElementById(int id)
        {
            if (AreaServiceValidation.CheckId(GetAllElements().Count(), id))
            {
                return Mapping().Map<AreaDto>(_areaRepository.GetById(id));
            }

            return null;
        }

        public void UpdateElement(AreaDto item)
        {
            if (item != null)
            {
                _areaRepository.Update(Mapping().Map<AreaDto, AreaEntity>(item));
            }
        }

        private static Mapper Mapping()
        {
            Mapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<AreaEntity, AreaDto>()));

            return mapper;
        }
    }
}
