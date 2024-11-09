using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using CP3.Domain.Interfaces.Dtos;
using CP3.Infrastructure.Repositories;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace CP3.Application.Services
{
    public class BarcoApplicationService : IBarcoApplicationService
    {
        private readonly IBarcoRepository _barcoRepository;
        private readonly IValidator<IBarcoDto> _barcoValidator;

        
        public BarcoApplicationService(IBarcoRepository barcoRepository, IValidator<IBarcoDto> barcoValidator)
        {
            _barcoRepository = barcoRepository;
            _barcoValidator = barcoValidator;
        }

        
        public IEnumerable<BarcoEntity> ObterTodosBarcos()
        {
            return _barcoRepository.ObterTodos() ?? new List<BarcoEntity>();
        }

        
        public BarcoEntity ObterBarcoPorId(int id)
        {
            var barco = _barcoRepository.ObterPorId(id);
            if (barco == null)
                throw new KeyNotFoundException("Barco não encontrado.");
            return barco;
        }

        
        public BarcoEntity AdicionarBarco(IBarcoDto dto)
        {
            
            var validationResult = _barcoValidator.Validate(dto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            
            var barcoEntity = new BarcoEntity(dto.Nome, dto.Modelo, dto.Ano, dto.Tamanho);

            
            return _barcoRepository.Adicionar(barcoEntity);
        }

        
        public BarcoEntity EditarBarco(int id, IBarcoDto dto)
        {
        
            var validationResult = _barcoValidator.Validate(dto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

        
            var barcoExistente = _barcoRepository.ObterPorId(id);
            if (barcoExistente == null)
                throw new KeyNotFoundException("Barco não encontrado.");

        
            barcoExistente.Nome = dto.Nome;
            barcoExistente.Modelo = dto.Modelo;
            barcoExistente.Ano = dto.Ano;
            barcoExistente.Tamanho = dto.Tamanho;

         
            return _barcoRepository.Editar(barcoExistente);
        }

        
        public BarcoEntity RemoverBarco(int id)
        {
            var barco = _barcoRepository.Remover(id);
            if (barco == null)
                throw new KeyNotFoundException("Barco não encontrado.");
            return barco;
        }
    }
}
