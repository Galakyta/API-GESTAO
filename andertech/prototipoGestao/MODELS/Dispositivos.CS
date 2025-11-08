namespace prototipoGestao.Models;



/*funcao especial em C# q transforma numeros inteiros em termos legivei, criando um novo tipo, entao 
maquina de vendass vira 1 e a maquina de chicletes vira 2, dps vai ficar tipo variavel maquina3.maquinaDeChicletes*/
public enum TiposDeDispositivo
{
    maquinaDeVendas = 1,
    maquinaDeChicletes = 2,

    maquinaDeLavarAUTONOMA = 3
}
public class Dispositivo { 
    public Guid Id { get; set; } /*guid é um outro tipo especifico de C# q serve pra gerar um id unico com 3 barrinha
    o lance é ele n se replicar*/
    public string Fabricante { get; set; } = default!; /*default com exclamacao serve pro copilador n encher o saco com string nula, o ! manda ele ignorar*/
    public string Empresa { get; set; } = default!; //deixei como empresa pq nem sempre a maquina ou oq seja vai ser do adm do lugar
    public string Local { get; set; } = default!;
    public TiposDeDispositivo Tipo { get; set; }
    public bool Ativo { get; set; } = true; }
