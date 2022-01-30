using System;

namespace Models
{
    public class AppResponse<T>
    {
        public T Content { get; set; }
        public string Message { get; set; }
        public bool Success = true;
    }
}
