using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace BelaSopa.Shared
{
    public class GestorImagens
    {
        private readonly string webRootPath;

        public GestorImagens(IHostingEnvironment hostingEnvironment)
        {
            this.webRootPath = hostingEnvironment.WebRootPath;
        }

        public void AdicionarImagemReceita(int idReceita, byte[] imagem) =>
            AdicionarImagem("receitas", idReceita, imagem);

        public void AdicionarImagemIngrediente(int idIngrediente, byte[] imagem) =>
            AdicionarImagem("ingredientes", idIngrediente, imagem);

        public void AdicionarImagemTecnica(int idTecnica, byte[] imagem) =>
            AdicionarImagem("tecnicas", idTecnica, imagem);

        public void AdicionarImagemUtensilio(int idUtensilio, byte[] imagem) =>
            AdicionarImagem("utensilios", idUtensilio, imagem);

        private void AdicionarImagem(string tipo, int id, byte[] imagem)
        {
            var pathDiretoria = Path.Combine(this.webRootPath, $"imagens/{tipo}");
            var pathImagem = Path.Combine(pathDiretoria, $"{id}");

            Directory.CreateDirectory(pathDiretoria);
            File.WriteAllBytes(pathImagem, imagem);
        }

        public static string GetPathImagemReceita(IUrlHelper urlHelper, int idReceita) =>
            GetPathImagem(urlHelper, "receitas", idReceita);

        public static string GetPathImagemIngrediente(IUrlHelper urlHelper, int idIngrediente) =>
            GetPathImagem(urlHelper, "ingredientes", idIngrediente);

        public static string GetPathImagemTecnica(IUrlHelper urlHelper, int idTecnica) =>
            GetPathImagem(urlHelper, "tecnicas", idTecnica);

        public static string GetPathImagemUtensilio(IUrlHelper urlHelper, int idUtensilio) =>
            GetPathImagem(urlHelper, "utensilios", idUtensilio);

        private static string GetPathImagem(IUrlHelper urlHelper, string tipo, int id) =>
            urlHelper.Content($"~/imagens/{tipo}/{id}");
    }
}
