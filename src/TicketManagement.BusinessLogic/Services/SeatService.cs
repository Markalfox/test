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
    public class SeatService : ICrud<SeatDto>
    {
        // FIELDS
        private readonly SqlSeatRepository _seatRepository;

        // CONSTRUCTORS
        public SeatService(SqlSeatRepository repository)
            => _seatRepository = repository;

        // METHODS
        public void CreateElement(SeatDto item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            else
            {
                if (item.Number < 0 || item.Row < 0)
                {
                    var exception = new Exception("The number or row cannot be negative");
                    throw exception;
                }
            }

            SeatServiceValidation.CheckNumber(GetAllElements().Where(x => x.AreaId == item.AreaId), item);
            SeatServiceValidation.CheckRow(GetAllElements().Where(x => x.AreaId == item.AreaId), item);

            _seatRepository.Create(Mapping().Map<SeatDto, SeatEntity>(item));
        }

        public void DeleteElement(int id)
        {
            if (SeatServiceValidation.CheckId(GetAllElements().Count(), id))
            {
                _seatRepository.Delete(id);
            }
        }

        public IEnumerable<SeatDto> GetAllElements()
        {
            List<SeatDto> elements = new List<SeatDto>();

            foreach (var i in Mapping().Map<List<SeatDto>>(_seatRepository.GetAll()))
            {
                elements.Add(i);
            }

            return elements;
        }

        public SeatDto GetElementById(int id)
        {
            if (SeatServiceValidation.CheckId(GetAllElements().Count(), id))
            {
                return Mapping().Map<SeatDto>(_seatRepository.GetById(id));
            }

            return null;
        }

        public void UpdateElement(SeatDto item)
        {
            if (item != null)
            {
                _seatRepository.Update(Mapping().Map<SeatDto, SeatEntity>(item));
            }
        }

        private static Mapper Mapping()
        {
            Mapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<SeatEntity, SeatDto>()));

            return mapper;
        }
    }
}
