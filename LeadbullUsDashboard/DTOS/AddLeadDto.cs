using Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.DTOS
{
    public class AddLeadDto
    {
        public string sheetUrl { get; set; }
        public int ServiceProfileId { get; set; }
    }
}
