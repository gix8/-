namespace Revisao
{
    public class ProdutoView
    {
        private readonly ProdutoController _controller;

        public ProdutoView(ProdutoController controller)
        {
            _controller = controller;
        }

        public void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Revisão C# ananas ===");
                Console.WriteLine("1. Criar Produto");
                Console.WriteLine("2. Listar Produtos");
                Console.WriteLine("0. Sair");
                Console.Write("opção: ");

                var op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        CadastrarProduto();
                        break;
                    case "2":
                        ListarProdutos();
                        break;
                    case "0":
                        Console.WriteLine("Saindo...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        private void CadastrarProduto()
        {
            Console.WriteLine("=== Cadastrar Produto ===");
            Console.Write("Nome do produto: ");
            var nome = Console.ReadLine() ?? string.Empty;

            Console.Write("Preço do produto: ");
            decimal preco = decimal.Parse(Console.ReadLine());

            Console.Write("Estoque: ");
            int estoque = int.Parse(Console.ReadLine());

            var produto = _controller.CadastrarProduto(nome, preco, estoque);
            Console.WriteLine($"Produto '{produto.Nome}' criado com ID {produto.Id}.");
        }

        private void ListarProdutos()
        {
            Console.Clear();
            Console.WriteLine("=== Lista de Produtos ===");
            var produtos = _controller.ListarProdutos();

            if (!produtos.Any())
            {
                Console.WriteLine("Nenhum produto cadastrado.");
                return;
            }

            foreach (var produto in produtos)
            {
                Console.WriteLine($"ID: {produto.Id} | Nome: {produto.Nome} | Preço: {produto.Preco:C} | Estoque: {produto.Estoque}");
            }
        }
    }
}