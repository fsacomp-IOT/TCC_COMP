namespace TCC_COMP.SERVICE.Interfaces.Service
{

    using System.Threading.Tasks;
    using TCC_COMP.DOMAIN.Entities;

    public interface IPlantService
    {
        Task<bool> InserirPlanta(Plant plant);
        Task<bool> AtualizarPlanta(Plant plant);
        Task<bool> RemoverPlanta(int id);
    }
}
