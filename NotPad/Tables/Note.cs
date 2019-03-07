using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotPad
{
    public class Note
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Content { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateTime { get; set; }

        [Required, Range(0,int.MaxValue)]
        public int UserID { get; set; }

        //-------------------------------

        public virtual User User { get; set; }
    }
    
}
