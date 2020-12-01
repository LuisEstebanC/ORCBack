using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace organicCare.Models
{
 
    public class UsuarioDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string apellido { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
