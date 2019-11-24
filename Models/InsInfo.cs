using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DownInsImg.Models
{
    public class InsInfo
    {
        [Required]
        public string Url;
        [Required]
        public int SelectId;

    }
}
