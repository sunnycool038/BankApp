using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ProjectTemplate.Models;

namespace ProjectTemplate.Models
{
    
    public class Response
    {
        public string RequestId => $"{Guid.NewGuid()}";
        public string ResponseCode { get; set; } = null!;
        public string ResponseMessage { get; set; } = null!;
        public Object Data { get; set; } =null!;
        // public string  Data { get; set; } = null!;
    }   
}