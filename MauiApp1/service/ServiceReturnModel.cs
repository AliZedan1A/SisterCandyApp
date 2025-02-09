using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.service
{
    public class ServiceReturnModel<T>
    {
        public T Value { get; set; }
        public bool IsSucceeded { get; set; }
        public string Comment { get; set; }
    }
}
