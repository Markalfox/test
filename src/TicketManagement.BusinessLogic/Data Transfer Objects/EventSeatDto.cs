﻿namespace Ticketmanagement.BusinessLogic.Data_Transfer_Objects
{
    public class EventSeatDto
    {
        public int Id { get; set; }

        public int EventAreaId { get; set; }

        public int Row { get; set; }

        public int Number { get; set; }

        public int State { get; set; }
    }
}
