using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotPad
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string DisplayName { get; set; }


        //-----------------------------------

        public virtual ICollection<Note> Notes { get; set; }

        public User()
        {
            Notes = new HashSet<Note>();
        }
    }
}
