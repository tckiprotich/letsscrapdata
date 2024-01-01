using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreParks.Services.Interface
{
    public interface IExploreParksInterface 
    {
        Task<ResponseStatus<List<ParkModel>>> GetParksAsync();       
         Task<ResponseStatus<List<getDescription>>> GetDescriptionAsync();

        
    }
}