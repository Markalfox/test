using System;
using System.Collections.Generic;
using System.Linq;
using Ticketmanagement.BusinessLogic.Data_Transfer_Objects;

namespace Ticketmanagement.BusinessLogic.Validations
{
    public static class SeatServiceValidation
    {
        public static void CheckRow(IEnumerable<SeatDto> seats, SeatDto item)
        {
            if (seats.Any(x => x.Row == item.Row))
            {
                Exception exception = new Exception("This row already exists in the current zone");
                throw exception;
            }
        }

        public static void CheckRow(SeatDto item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            else
            {
                if (item.Row < 0)
                {
                    var exception = new Exception("The row cannot be negative");
                    throw exception;
                }
            }
        }

        public static void CheckNumber(IEnumerable<SeatDto> seats, SeatDto item)
        {
            if (seats.Any(x => x.Number == item.Number))
            {
                Exception exception = new Exception("This number is already in the current row");
                throw exception;
            }
        }

        public static void CheckNumber(SeatDto item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            else
            {
                if (item.Number < 0)
                {
                    var exception = new Exception("The number cannot be negative");
                    throw exception;
                }
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
    }
}
