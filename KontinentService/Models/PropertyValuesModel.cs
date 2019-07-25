using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KontinentService.Models
{
    public class PropertyValuesModel
    {
        public int Id { get; set; }

        public int? TourId { get; set; }
        public ToursModel Tour { get; set; }

        public int PropertyId { get; set; }
        public PropertiesModel Property { get; set; }
    }
}
