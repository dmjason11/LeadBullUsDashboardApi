
using Core.Identity;
using System.ComponentModel.DataAnnotations.Schema;


namespace Core
{
    public class Company: BaseEntity
    {
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? TaxId { get; set; }
        public string? Type { get; set; }
        public string? Bundle { get; set; }
        public DateTime StartDate { get; set; }
        [ForeignKey("ServiceProfile")]
        public string ServiceProfileId { get; set; }
        public AppUser ServiceProfile { get; set; }
    }
}