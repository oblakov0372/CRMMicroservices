using CRM.Common;

namespace CRM.Account.Entities
{
  public enum Role
  {
    Admin,
    User
  }
  public class AccountEntity : IEntity
  {
    public Guid Id { get; set; }
    public Guid CreatedById { get; set; }
    public Guid UpdatedById { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset UpdatedDate { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string UserName { get; set; }
    public Role Role { get; set; }
  }
}