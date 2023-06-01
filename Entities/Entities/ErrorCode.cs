using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("to_error_codes")]
    public class ErrorCode
    {
        public int Id { get; set; }
        public string? Code { get; set; }
    }
}
