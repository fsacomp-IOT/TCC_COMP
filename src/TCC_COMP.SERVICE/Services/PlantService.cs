using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TCC_COMP.DOMAIN.Entities;
using TCC_COMP.SERVICE.Interfaces.Repository;
using TCC_COMP.SERVICE.Interfaces.Service;

namespace TCC_COMP.SERVICE.Services
{
    public class PlantService : IPlantService
    {
        private readonly IPlantRepository _plantRepository;

        public PlantService(IPlantRepository plantRepository)
        {
            _plantRepository = plantRepository;
        }

        public Task<bool> InserirPlanta(Plant plant)
        {
            return _plantRepository.InserirPlanta(plant);
        }

        public Task<bool> AtualizarPlanta(Plant plant)
        {
            return _plantRepository.AtualizarPlanta(plant);
        }

        public Task<bool> RemoverPlanta(int id)
        {
            return _plantRepository.RemoverPlanta(id);
        }
    }
}
