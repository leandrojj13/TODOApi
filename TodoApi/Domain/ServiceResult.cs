using System.Collections.Generic;

namespace TodoApi.Domain
{
    public class ServiceResult<T>
    {
        public ServiceResult()
        {
            Messages = new List<string>();
        }

        public T Result { get; set; }

        public bool Success { get; set; }

        public List<string> Messages { get; set; }
    }
}
