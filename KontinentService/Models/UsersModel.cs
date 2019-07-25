using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KontinentService.Models
{
    public class UsersModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public DateTime RegDate { get; set; }
        public bool IsBlocked { get; set; }
    }
}
