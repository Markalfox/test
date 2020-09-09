﻿namespace TicketManagement.DataAccess.Entities
{
    public class EventSeatEntity
    {
        public int Id { get; set; }

        public int EventAreaId { get; set; }

        public int Row { get; set; }

        public int Number { get; set; }

        public int State { get; set; }
    }
}
