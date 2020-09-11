using System;

namespace Ticketmanagement.BusinessLogic.Data_Transfer_Objects
{
    public class EventDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int LayoutId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
