using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KontinentService.Helpers
{
    public class ImageHelper
    {
        public static string AddImage(IHostingEnvironment AppEnvironment, IFormFile Image)
        {
            string path = "/Content/" + Image.FileName;
            using (var fileStream = new FileStream(AppEnvironment.WebRootPath + path, FileMode.Create))
            {
                Image.CopyTo(fileStream);
            }
            return path; 
        }
        //TODO: Написать сравнение уже загруженной картинки и новой, добавить удаление старой из Content

        //if (imageFile != null)
        //{
        //    string path = "/Content/" + imageFile.FileName;
        //    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
        //    {
        //        await imageFile.CopyToAsync(fileStream);
        //    }
        //    toursModel.Image = path;
        //}
    }
}
