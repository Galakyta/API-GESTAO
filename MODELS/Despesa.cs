namespace prototipoGestao.Models
{
    
public class Despesa
    {

       public int Id {get; set;}

       public string Nome {get; set;} = String.Empty;
       
       public decimal Valor {get; set;} = default;

       public DateTime Data {get; set;} = default!;

       public string? Categoria {get; set;} = default!;

       public decimal Imposto {get; set;} = default;

    }


}
//1. Criar nova entidade no domínio (Model)

//Implemente uma nova entidade chamada Despesa, contendo os seguintes campos:

//Id (inteiro, chave primária)

//Nome (texto, obrigatório)

//Valor (decimal, obrigatório)

//Data (DateTime, obrigatório)

//Categoria (texto, opcional)

//A entidade deve ser criada dentro da pasta Models da aplicação.