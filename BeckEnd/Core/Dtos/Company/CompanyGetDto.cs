using BeckEnd.Core.Enums;

namespace BeckEnd.Core.Dtos.Company;

public class CompanyGetDto
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public string Name { get; set; }
    public CompanySize Size { get; set; }
}
