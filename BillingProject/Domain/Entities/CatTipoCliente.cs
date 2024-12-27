using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CatTipoCliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string TipoCliente { get; set; }

        public ICollection<TblClientes> Clientes { get; set; }
    }
}
