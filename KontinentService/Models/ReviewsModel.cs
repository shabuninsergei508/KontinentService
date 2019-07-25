using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KontinentService.Models
{
    public class ReviewsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Text { get; set; }
        public string Photo { get; set; }
        public int Rating { get; set; }
    }
}
