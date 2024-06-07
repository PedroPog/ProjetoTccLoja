namespace LojaCamisa.GerenciadorArquivos
{
    public class GerenciadorArquivo
    {
        public static string CadastrarImagemProduto(IFormFile formFile)
        {
            var NomeArquivo = Path.GetFileName(formFile.FileName);
            var Caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", NomeArquivo);

            using (var stream = new FileStream(Caminho, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }
            return Path.Combine("/imagens", NomeArquivo).Replace("\\", "/");
        }
    }
}
