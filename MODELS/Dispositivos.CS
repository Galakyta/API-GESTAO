namespace prototipoGestao.Models
//se der ruim em algo confere se ta alinhado com isso aq e compara com o erro do banco de dados
{
    public enum TiposDeDispositivo //usei o enum pro id pq nn se repete e fica mai facil de implementar doq auto increment
    {
        outro = 0,
        maquinaDeVendas = 1, //Dda pra adicionar mais codigos de maquina aqui mas eu fiz com 3, vo bota 30 maquinas autonomas 30 de venda e 20 de chiclete eu achho
        maquinaDeChicletes = 2,
        maquinaDeLavarAUTONOMA = 3


    }

    public class Dispositivo
    {
        public Guid Id { get; set; }
        public string Fabricante { get; set; } = default!;
        public string Empresa { get; set; } = default!;
        public string Local { get; set; } = default!;
        public TiposDeDispositivo Tipo { get; set; }
        public bool Ativo { get; set; } = true;

        public string? TipoPersonalizado { get; set; }

        public decimal Lucro { get; set; }     // lucro total ou mensal da máquina
        public decimal CustoDeOperacao { get; set; } // custos de manutenção, energia, etc.
        //depois a gente pode fazer uma funcao onde ele calcula se ta dando lucro ou nn, e litar por unidade, mas isso fica pra dps
    }
}
