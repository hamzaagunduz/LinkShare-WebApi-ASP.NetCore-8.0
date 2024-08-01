using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Dto.ApiResponseDtos
{
    public class ApiResponseDto<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public Dictionary<string, List<string>> Errors { get; set; }


    }
}
