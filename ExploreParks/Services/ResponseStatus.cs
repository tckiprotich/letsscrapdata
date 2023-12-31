using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreParks.Services
{
    public class ResponseStatus<T>
    {
        
            public bool Success { get; set; }
            public string? Message { get; set; }
            public List<ParkModel>? Data { get; set; }
       
    }
}