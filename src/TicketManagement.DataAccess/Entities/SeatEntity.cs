﻿namespace TicketManagement.DataAccess.Entities
{
    public class SeatEntity
    {
        public int Id { get; set; }

        public int AreaId { get; set; }

        public int Row { get; set; }

        public int Number { get; set; }
    }
}
