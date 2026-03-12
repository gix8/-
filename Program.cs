namespace Revisao
{
    class Program
    {
        static void Main(string[] args)
        {
            using var context = new AppDbContext();
            var controller = new ProdutoController(context);
            var view = new ProdutoView(controller);
            view.Menu();
        }
    }
}

//heil ananas!
