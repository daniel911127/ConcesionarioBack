using ConcesionarioBack.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ConcesionarioBack.Domain.Interfaces
{
    public interface ICommonVehiculosService<T>
    {
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task<T> Add(T carroDto);
        Task<T> Update(int id, T carroDto);
        Task<T> Delete(int id);
        Task<string> SumarValores();
    }
}
