using System;
using System.ComponentModel.DataAnnotations;

namespace AniversarioApplication.Entidade {
    public class Amigo {
        public string Id { get; set; }

        [Required(ErrorMessage = "Nome Obrigatório")]
        [StringLength(20, ErrorMessage = "Campo Nome não pode ter mais de 20 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Sobrenome Obrigatório")]
        [StringLength(50, ErrorMessage = "Campo Sobrenome não pode ter mais de 50 caracteres")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Data de Aniversario Obrigatório")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public string Aniversario { get; set;}
    }
}
