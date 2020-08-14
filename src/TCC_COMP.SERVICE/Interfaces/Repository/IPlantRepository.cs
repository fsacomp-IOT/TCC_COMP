using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TCC_COMP.DOMAIN.Entities;

namespace TCC_COMP.SERVICE.Interfaces.Repository
{
    public interface IPlantRepository
    {
        Task<List<Plant>> ObterTodosDetalhado();
        Task<List<Plant>> ObterTodosSimples();
        Task<Plant> ObterPlanta(int id);
        Task<bool> InserirPlanta(Plant plant);
        Task<bool> AtualizarPlanta(Plant plant);
        Task<bool> RemoverPlanta(int id);
    }
}
