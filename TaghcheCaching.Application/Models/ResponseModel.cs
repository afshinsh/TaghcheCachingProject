using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaghcheCaching.Application.Models
{
    public class ResponseModel
    {
        public object Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
