using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Common
{
    public class BaseApiResponse
    {
        public BaseApiResponse()
        {

        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }

        public int Id { get; set; }

    }
    public class ApiResponse<T> : BaseApiResponse       //Here T represents it can take any type of datatype
    {
        public virtual IList<T> Data { get; set; }      //We have to make List<T> i.e new List<Datatype>();
        public virtual T AnyData { get; set; }

    }

    public class ApiPostResponse<T> : BaseApiResponse
    {
        public virtual T Data { get; set; }
    }
}
