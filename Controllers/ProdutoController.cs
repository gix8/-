using Microsoft.EntityFrameworkCore;
namespace Revisao
{
    public class ProdutoController
    {
        private readonly AppDbContext _context;

        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        public Produto CriarProduto(string nome, decimal preco, int estoque)
        {
            var produto = new Produto
            {
                Nome = nome,
                Preco = preco,
                Estoque = estoque
            };

            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return produto;
        }

        public List<Produto> ListarProdutos()
        {
            return _context.Produtos
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .ToList();
        }
    }
}