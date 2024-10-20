using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.API.Entities;

public abstract class Base   
{
    public int Id { get; set; }
    public DateTime CreatedAt  { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set;} = DateTime.UtcNow;
}