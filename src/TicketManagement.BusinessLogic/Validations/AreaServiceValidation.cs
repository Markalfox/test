using System;
using System.Collections.Generic;
using System.Linq;
using Ticketmanagement.BusinessLogic.Data_Transfer_Objects;

namespace Ticketmanagement.BusinessLogic.Validations
{
    public static class AreaServiceValidation
    {
        public static void CheckCoordX(AreaDto item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            else
            {
                if (item.CoordX < 0)
                {
                    Exception exception = new Exception("The X coordinate cannot be less than 0");
                    throw exception;
                }
            }
        }

        public static void CheckCoordY(AreaDto item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            else
            {
                if (item.CoordY < 0)
                {
                    Exception exception = new Exception("The Y coordinate cannot be less than 0");
                    throw exception;
                }
            }
        }

        public static void CheckDescription(IEnumerable<AreaDto> elements, AreaDto item)
        {
            if (elements.Any(x => x.Description == item.Description))
            {
                Exception exception = new Exception("The same description");
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
    }
}
