namespace TCC_COMP.SERVICE.Interfaces.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepository<T>
    {
        Task<bool> Adicionar(T entidade);
        Task<List<T>> ObterTodos();
        Task<bool> Atualizar(T entidade);
    }
}
