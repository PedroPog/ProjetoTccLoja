namespace LojaCamisa.Repository.Utils
{
    public enum SituacaoEnum
    {
        ATIVO ,DESATIVO
    }
    public enum TipoEnum
    {
        CLIENTE, ADM
    }
    public enum StatusEnum
    {
        EM_PROCESSO ,SEPARACAO ,PREPARACAO ,ROTA_DE_ENTREGA ,
        PEDIDO_ENTREGUE ,FALHA_ENTREGA ,FINALIZADO ,CANCELADO ,
        ERRO_404 = 404
    }

}
