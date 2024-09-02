using BeckEnd.Core.Enums;

namespace BeckEnd.Core.Dtos.Company;

public class CompanyCreateDto
{
    public string Name { get; set; }

    public CompanySize Size { get; set; }

}
