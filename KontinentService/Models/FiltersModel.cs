using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KontinentService.Models
{
    public class FiltersModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Title { get; set; }

        public bool IsSpecial { get; set; }
        public bool IsTour { get; set; }
        public bool IsBustour { get; set; }
        public bool IsCruise { get; set; }

        public int CategoryId { get; set; }

        [Display(Name = "Категория")]
        public virtual CategoriesModel Category { get; set; }
    }
}
