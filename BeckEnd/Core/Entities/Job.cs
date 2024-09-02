using BeckEnd.Core.Enums;

namespace BeckEnd.Core.Entities;

public class Job : BaseEntity
{
    public string Title { get; set; }
    public JobLevel Level { get; set; }

    // Relationships

    public long CompanyId { get; set; }
    public Company Company { get; set; }
    public ICollection<Candidate> Candidates { get; set; }


}
