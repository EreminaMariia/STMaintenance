namespace Entities
{
    public class Department
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public ICollection<TechPassport> TechPassports { get; set; }
    }
}