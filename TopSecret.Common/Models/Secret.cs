namespace TopSecret.Common.Models;

public class Secret
{
    public DateTime? CreatedOn { get; set; }
    public string? Description { get; set; }
    public int? Id { get; set; }
    public string? Notes { get; set; }
    public string? Password { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string? Username { get; set; }
}
