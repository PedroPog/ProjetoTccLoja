using LojaCamisa.Models;

namespace LojaCamisa.Repository.Interface.Contract
{
    public interface IFormaPagamentoRepository
    {
        IEnumerable<FormaPagamento> ObterTodosPag(int idUsuario);
        void Cadastrar(FormaPagamento formaPagamento);
        void Atualziar(FormaPagamento formaPagamento);
        void Excluir(int IdFormaPagamento);
        FormaPagamento ObterFormaPag(int IdFormaPagamento);
    }
}