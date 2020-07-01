namespace TCC_COMP.SERVICE.Interfaces.Repository
{
    using TCC_COMP.DOMAIN.Entities;
    using System.Threading.Tasks;
    using System;

    public interface ISensorRepository : IRepository<Sensor>
    {
        Task<Sensor> ObterPorId(Guid id);
    }
}