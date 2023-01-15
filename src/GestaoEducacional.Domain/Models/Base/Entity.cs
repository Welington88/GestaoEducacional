using System;
namespace GestaoEducacional.Domain.Entities.Base;

public abstract class Entity<T> where T : Entity<T>
{
    protected Entity()
    {
    }
}

