using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRESHY.Main.Contract.Requests.OrderRequests
{
    public record GetAllCustomerOrdersRequest
    ( int PageNumber,
         int PageSize,
         Guid CustomerId
    );
         
}
