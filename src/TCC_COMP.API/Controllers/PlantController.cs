using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC_COMP.DOMAIN.Entities;
using TCC_COMP.SERVICE.Interfaces;
using TCC_COMP.SERVICE.Interfaces.Repository;
using TCC_COMP.SERVICE.Interfaces.Service;
using TCC_COMP.SERVICE.ViewModels;

namespace TCC_COMP.API.Controllers
{
    [Route("api/plants")]
    public class PlantController : MainController
    {
        private readonly IPlantService _plantService;
        private readonly IPlantRepository _plantRepository;
        private readonly IMapper _mapper;

        public PlantController(IPlantService plantService,
                               IPlantRepository plantRepository,
                               IMapper mapper,
                               INotificador notificador) : base(notificador)
        {
            _plantService = plantService;
            _plantRepository = plantRepository;
            _mapper = mapper;
        }


        [HttpGet("obterPlantas/{filtro:int}")]
        public async Task<ActionResult<List<PlantViewModel>>> ObterTodasPlantas(int filtro)
        {
            List<PlantViewModel> retorno = new List<PlantViewModel>();

            try
            {
                if (filtro == 0)
                {
                    retorno = _mapper.Map<List<PlantViewModel>>(await _plantRepository.ObterTodosSimples());
                }
                else
                {
                    retorno = _mapper.Map<List<PlantViewModel>>(await _plantRepository.ObterTodosDetalhado());
                }

                return CustomResponse(retorno);

            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PlantViewModel>> ObterPlantaPorId(int id)
        {
            try
            {
                var retorno = _mapper.Map<PlantViewModel>(await _plantRepository.ObterPlanta(id));
                return CustomResponse(retorno);
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PlantViewModel>> IncluirPlanta(PlantViewModel newPlant)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            try
            {
                var retorno = await _plantService.InserirPlanta(_mapper.Map<Plant>(newPlant));
                return CustomResponse(newPlant);
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<PlantViewModel>> AtualizarPlanta(int id, PlantViewModel updatePlant)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (id != updatePlant.id) return BadRequest();

            try
            {
                var retorno = await _plantService.AtualizarPlanta(_mapper.Map<Plant>(updatePlant));
                return CustomResponse(updatePlant);
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<string>> DeletarPlanta(int id)
        {
            var retorno = _mapper.Map<PlantViewModel>(await _plantRepository.ObterPlanta(id));

            try
            {
                if (retorno != null)
                {
                    var Delete = await _plantService.RemoverPlanta(id);
                    return CustomResponse();
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse(ex.Message);
            }
        }
    }
}
