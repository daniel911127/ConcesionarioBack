using ConcesionarioBack.Domain.DTOs;

namespace ConcesionarioBack.Domain.Interfaces
{
    public interface ICommonService <T>
    {
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task<T> Add(T listadoDto);
        Task<T> Update(int id,T listadoDto);
        Task<T> Delete(int listadoId);

    }
}
