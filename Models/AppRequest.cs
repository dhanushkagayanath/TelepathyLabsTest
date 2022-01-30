using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class AppRequest<T>
    {
        public T Content { get; set; }
    }
}
