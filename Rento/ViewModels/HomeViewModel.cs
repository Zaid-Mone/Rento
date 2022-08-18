using Rento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rento.ViewModels
{
    public class HomeViewModel
    {
        public List<Car> Cars { get; set; }
        public List<CarType> CarTypes { get; set; }
    }
}
