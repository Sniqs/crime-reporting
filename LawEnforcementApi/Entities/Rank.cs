namespace LawEnforcementApi.Entities;

public class Rank
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Officer> Officers { get; set; } = null!;
}
