using Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.DTOS
{
    public class LeadDto
    {
        public int Id { get; set; }
        public string sheetIdentifier { get; set; }
    }
}
