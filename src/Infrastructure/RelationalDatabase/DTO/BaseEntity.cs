using System.ComponentModel.DataAnnotations;

namespace RelationalDatabase.DTO;

public class BaseEntity
{
    [Key] 
    public int Id { get; set; }
}