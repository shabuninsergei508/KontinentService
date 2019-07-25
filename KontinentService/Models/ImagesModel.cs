using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KontinentService.Models
{
    public class ImagesModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }

        public int TourId { get; set; }
        public virtual ToursModel Tour { get; set; }
    }
}
