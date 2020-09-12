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
    public class LayoutService : ICrud<LayoutDto>
    {
        // FIELDS
        private readonly SqlLayoutRepository _layoutRepository;

        // CONSTRUCTORS
        public LayoutService(SqlLayoutRepository repository)
            => _layoutRepository = repository;

        // METHODS
        public void CreateElement(LayoutDto item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            LayoutServiceValidation.CheckDescription(GetAllElements().Where(x => x.VenueId == item.VenueId), item);

            _layoutRepository.Create(Mapping().Map<LayoutDto, LayoutEntity>(item));
        }

        public void DeleteElement(int id)
        {
            if (LayoutServiceValidation.CheckId(GetAllElements().Count(), id))
            {
                _layoutRepository.Delete(id);
            }
        }

        public IEnumerable<LayoutDto> GetAllElements()
        {
            List<LayoutDto> elements = new List<LayoutDto>();

            foreach (var i in Mapping().Map<List<LayoutDto>>(_layoutRepository.GetAll()))
            {
                elements.Add(i);
            }

            return elements;
        }

        public LayoutDto GetElementById(int id)
        {
            if (LayoutServiceValidation.CheckId(GetAllElements().Count(), id))
            {
                return Mapping().Map<LayoutDto>(_layoutRepository.GetById(id));
            }

            return null;
        }

        public void UpdateElement(LayoutDto item)
        {
            if (item != null)
            {
                _layoutRepository.Update(Mapping().Map<LayoutDto, LayoutEntity>(item));
            }
        }

        private static Mapper Mapping()
        {
            Mapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<LayoutEntity, LayoutDto>()));

            return mapper;
        }
    }
}
