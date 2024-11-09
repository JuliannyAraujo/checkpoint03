using CP3.Domain.Entities;

namespace CP3.Infrastructure.Repositories
{
    public interface IBarcoRepository
    {
        BarcoEntity Adicionar(BarcoEntity barcoEntity);  
        BarcoEntity Editar(BarcoEntity barcoExistente);  
        BarcoEntity ObterPorId(int id);  
        List<BarcoEntity>? ObterTodos();  
        BarcoEntity Remover(int id);  
    }
}
