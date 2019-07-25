using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KontinentService.Models
{
    public class SubcategoriesModel
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Краткое описание")]
        public string ShortDescription { get; set; }

        [Display(Name = "Изображение")]
        public string Image { get; set; }

        [Display(Name = "URL")]
        public string UrlRus { get; set; }

        public int CategoryId { get; set; }

        [Display(Name = "Категория")]
        public virtual CategoriesModel Category { get; set; }
    }
}
