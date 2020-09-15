using System;
using System.Collections.Generic;
using System.Linq;
using Ticketmanagement.BusinessLogic.Data_Transfer_Objects;

namespace Ticketmanagement.BusinessLogic.Validations
{
    public static class EventServiceValidation
    {
        public static void CheckName(IEnumerable<EventDto> events, EventDto item)
        {
            if (events.Any(x => x.Name == item.Name))
            {
                Exception exception = new Exception("An event with this name already exists");
                throw exception;
            }
        }

        public static void CheckDate(IEnumerable<EventDto> events, EventDto item)
        {
            if (events.Any(x => x.StartDate >= item.StartDate && x.StartDate <= item.EndDate))
            {
                Exception exception = new Exception("Change the date or location of the event. Another event is already planned for this period at this place");
                throw exception;
            }

            if (events.Any(x => x.EndDate >= item.StartDate && x.EndDate <= item.EndDate))
            {
                Exception exception = new Exception("Change the end date or venue of the event. At this time, another event will take place in this place");
                throw exception;
            }

            if (events.Any(x => (x.EndDate >= item.EndDate && x.StartDate <= item.StartDate) || (x.EndDate <= item.EndDate && x.StartDate >= item.StartDate)))
            {
                Exception exception = new Exception("At this time, another event will take place in this place");
                throw exception;
            }
        }

        public static bool CheckId(int length, int id)
        {
            if ((id > 0) && (id < length))
            {
                return true;
            }

            return false;
        }

        public static bool CheckNull(EventDto item)
            => item == null;
    }
}
