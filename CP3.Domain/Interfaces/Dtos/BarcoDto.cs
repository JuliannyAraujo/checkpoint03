using CP3.Domain.Interfaces.Dtos;
using System.ComponentModel.DataAnnotations;

public class BarcoDto : IBarcoDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Modelo { get; set; }
    public int Ano { get; set; }
    public double Tamanho { get; set; }

    public void Validate()
    {
        if (string.IsNullOrEmpty(Nome)) throw new ValidationException("Nome é obrigatório.");
        if (string.IsNullOrEmpty(Modelo)) throw new ValidationException("Modelo é obrigatório.");
        if (Ano < 1900 || Ano > DateTime.Now.Year) throw new ValidationException("Ano inválido.");
        if (Tamanho <= 0) throw new ValidationException("Tamanho deve ser maior que 0.");
    }
}
