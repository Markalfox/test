using System;
using System.Collections.Generic;
using System.Linq;
using Ticketmanagement.BusinessLogic.Data_Transfer_Objects;

namespace Ticketmanagement.BusinessLogic.Validations
{
    public static class LayoutServiceValidation
    {
        public static void CheckDescription(IEnumerable<LayoutDto> elements, LayoutDto item)
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
