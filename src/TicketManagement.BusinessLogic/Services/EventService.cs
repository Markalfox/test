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
    public class EventService : ICrud<EventDto>
    {
        // FIELDS
        private readonly SqlEventRepository _eventRepository;

        // CONSTRUCTORS
        public EventService(SqlEventRepository repository)
            => _eventRepository = repository;

        // METHODS
        public void CreateElement(EventDto item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            else
            {
                if (item.StartDate > item.EndDate)
                {
                    var exception = new Exception("The event start date cannot be greater than the event end date");
                    throw exception;
                }
            }

            EventServiceValidation.CheckName(GetAllElements(), item);
            EventServiceValidation.CheckDate(GetAllElements().Where(x => x.LayoutId == item.LayoutId), item);

            _eventRepository.Create(Mapping().Map<EventDto, EventEntity>(item));
        }

        public void DeleteElement(int id)
        {
            if (EventServiceValidation.CheckId(GetAllElements().Count(), id))
            {
                _eventRepository.Delete(id);
            }
        }

        public IEnumerable<EventDto> GetAllElements()
        {
            List<EventDto> elements = new List<EventDto>();

            foreach (var i in Mapping().Map<List<EventDto>>(_eventRepository.GetAll()))
            {
                elements.Add(i);
            }

            return elements;
        }

        public EventDto GetElementById(int id)
        {
            if (EventServiceValidation.CheckId(GetAllElements().Count(), id))
            {
                return Mapping().Map<EventDto>(_eventRepository.GetById(id));
            }

            return null;
        }

        public void UpdateElement(EventDto item)
        {
            if (!EventServiceValidation.CheckNull(item))
            {
                _eventRepository.Update(Mapping().Map<EventDto, EventEntity>(item));
            }
        }

        private static Mapper Mapping()
        {
            Mapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<EventEntity, EventDto>()));

            return mapper;
        }
    }
}
