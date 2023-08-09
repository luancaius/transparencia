using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity;

public class BaseEntityGuid
{
    public Guid Uuid { get; set; }
}
