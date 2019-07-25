using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KontinentService.Models
{
    public class OrdersModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string QuestionText { get; set; }
        public DateTime DateAdd { get; set; }
        public bool IsAnswered { get; set; }
        public string Comment { get; set; }

        public int TourId { get; set; }
        public virtual ToursModel Tour { get; set; }
    }
}
