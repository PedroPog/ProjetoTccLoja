using LojaCamisa.Models;
namespace LojaCamisa.Repository.Interface.Contract;

public interface IEnderecoRepository
{
    IEnumerable<Endereco> ObterTodosEndereco(int IdUsuario);
    Endereco ObterEndereco(int IdEndereco);
    void Cadastrar(Endereco endereco);
    void Atualizar(Endereco endereco);
    void Excluir(int IdEndereco);
}