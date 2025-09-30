using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grajciar.InternetBanking.Domain.Entities
{
    [Table(nameof(User))]
    public class User
    {
        public int Id { get; set; }
        public string Username  { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
