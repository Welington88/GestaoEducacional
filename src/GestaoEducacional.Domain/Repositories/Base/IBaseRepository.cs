using System;
namespace GestaoEducacional.Domain.Repositories.Base;

public interface IBaseRepository<T>
{
    public T _service { get; set; }
}

