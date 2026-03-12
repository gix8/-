<!--
This file is meant for AI coding agents (Copilot, AutoGPT, ChatGPT, etc.) to
get up to speed quickly with the structure and patterns of the repository.
Only include facts that can be derived from reading the source and running the
project; avoid generic advice.
-->

# Repository Overview

`revisao` is a very small .NET 8 console application used for C# revision
(the author even left a YouTube link in `Program.cs`).  It mimics a minimal
Model–Controller–View structure but with everything wired manually.  The
primary domain entity is `Produto` and the app persists data to a local
MySQL/MariaDB database via Entity Framework Core.

Key directories:

* `Data/` – contains `AppDbContext`, the EF Core `DbContext` configured with a
  hard‑coded connection string to `server=localhost;database=revisao;user=root;`
  (password blank).  There are no EF migrations checked in.
* `Models/` – plain POCOs, currently just `Produto` with `Id`, `Nome`,
  `Preco`, `Estoque`.
* `Controllers/` – business logic classes that own an injected `AppDbContext`.
  e.g. `ProdutoController` has `CriarProduto` plus a `ListarProdutos` helper; it
  uses `AsNoTracking()` on reads and calls `_context.SaveChanges()` directly.
* `Views/` – console‑based user interface.  `ProdutoView` shows a simple menu
  and calls controller methods.  Note: the view method name (`CadastrarProduto`)
  must match the controller method name, which currently is `CriarProduto` –
  the mismatch is the cause of the lone build error.

`Program.cs` instantiates the context, controller and view by hand and then
starts the menu loop.  There is no hosting/DI framework; implicit usings and
nullable reference types are enabled via the SDK project file.

# Build & Run

* Restore and compile with `dotnet build` (from the solution/project root).
  Errors like the controller/view name mismatch surface here.
* Execute with `dotnet run`.  It will rebuild automatically if needed.
* No tests are present – the repo is a simple demo.
* You can add EF migrations if you need a schema: e.g.
  `dotnet tool install --global dotnet-ef` then
  `dotnet ef migrations add Initial` and `dotnet ef database update`.

# Conventions & Patterns

* Namespace for all sources is `Revisao` (mirrors the project name).
* Manual constructor injection is used instead of the generic host and
  `IServiceCollection`.
* Controllers return entity instances directly; there are no DTOs or async
  methods.  Insert operations call `_context.Produtos.Add(...)` followed by
  `_context.SaveChanges()`.
* Read operations always use `.AsNoTracking()` and sort by `Id`.
* Views are responsible for all console interaction.  They use
  `Console.ReadLine()` with minimal validation; conversion warnings exist for
  potential null inputs.
* Connection string is defined in `OnConfiguring` and is not pulled from
  configuration files.
* Packages in `revisao.csproj` include EF Core 9.0 and Pomelo MySql.

# Developer Workflows

* To add a new feature you typically:
  1. Create a new model in `Models/`.
  2. Add a corresponding `DbSet` to `AppDbContext`.
  3. Implement business logic in a controller under `Controllers/`.
  4. Add a menu option and handler in the view class under `Views/`.
  5. Update `Program.cs` if new controllers/views are needed.
* Database must be running locally; the default user is `root` with no
  password and the database name is `revisao`.
* Because the project is a console app, debugging is usually done via
  `dotnet run` or the VS Code C# debugger.

# Integration Points

* The only external dependency is MySQL/MariaDB accessed through EF Core and
  the Pomelo provider.  There is no HTTP, no queues, etc.

# Notes for AI Agents

* Pay attention to the artifact names (e.g. `CriarProduto` vs
  `CadastrarProduto`) – a small typo anywhere breaks the build.
* When modifying the `AppDbContext`, remember that EF migrations are not
  included; you may need to scaffold them yourself.
* The program is intentionally simple; do not expect complex abstractions or
  libraries beyond EF Core and MySql.
* .NET version is net8.0; if generating new files use top‑level statements only
  if the pattern is observed elsewhere (here it is not).


> **Tip:** run `dotnet build` early to catch naming mismatches between layers.

Please review these notes and let me know if any important detail is missing or
unclear.  I'm happy to iterate.
