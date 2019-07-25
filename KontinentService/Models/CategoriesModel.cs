using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KontinentService.Models
{
    public class CategoriesModel
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

        // В БД столбец Title через конструктор NOT NULL
    }
}
