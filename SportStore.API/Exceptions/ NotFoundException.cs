using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Application.Exceptions;

    public class  NotFoundException : Exception
    {
        public  NotFoundException(string name,object key) : base($"{name}({key}) not fount")
        {
            
        }
    }
