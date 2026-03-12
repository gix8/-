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
//https://www.youtube.com/watch?v=9n8sXo2l5j0&list=PLbIBhQqvHqEJYy7mLh3a1Zt6u9e7k8A&index=1
//heil ananas!
