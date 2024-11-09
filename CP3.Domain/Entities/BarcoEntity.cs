using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace CP3.Domain.Entities
{
    [Table("tb_barcos")]  
    public class BarcoEntity : IValidatableObject
    {
        [Key]  
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  
        public int Id { get; set; }

        [Required]  
        [MaxLength(100)]  
        public string Nome { get; set; }

        [Required]  
        [MaxLength(100)]  
        public string Modelo { get; set; }

        [Required]  
        [Range(1900, 9999)]  
        public int Ano { get; set; }

        [Required]
        [Range(0.1, double.MaxValue)]  
        public double Tamanho { get; set; }


        public BarcoEntity(string nome, string modelo, int ano, double tamanho)
        {
            Nome = nome;
            Modelo = modelo;
            Ano = ano;
            Tamanho = tamanho;
        }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (string.IsNullOrEmpty(Nome))
            {
                yield return new ValidationResult("Nome é obrigatório", new[] { nameof(Nome) });
            }

            if (string.IsNullOrEmpty(Modelo))
            {
                yield return new ValidationResult("Modelo é obrigatório", new[] { nameof(Modelo) });
            }

            if (Ano < 1900 || Ano > 9999)
            {
                yield return new ValidationResult("Ano deve ser entre 1900 e 9999", new[] { nameof(Ano) });
            }

            if (Tamanho <= 0)
            {
                yield return new ValidationResult("Tamanho deve ser maior que zero", new[] { nameof(Tamanho) });
            }
        }
    }
}
