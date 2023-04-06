using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    [Table("to_instructions")]
    public class Instruction : IPasportable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public TechPassport TechPassport { get; set; }
    }
}
