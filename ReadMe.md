
### Database Configuration

When you run the application the database will be automatically created (if necessary) and the latest migrations will be applied.

### Database Migrations

To use `dotnet-ef` for your migrations first ensure that "UseInMemoryDatabase" is disabled.
Then, add the following flags to your command (values assume you are executing from repository root)

* `--project src/Infrastructure` (optional if in this folder)
* `--startup-project src/WebUI`
* `--output-dir Persistence/Migrations`

For example, to add a new migration from the root folder:

 `dotnet ef migrations add "InitialCreate" --project src\Infrastructure --startup-project src\Presentation\ConsoleUi --output-dir Persistence\Migrations`


### Running the Benchmarks

`dotnet run --project .\src\Presentation\ConsoleUi\ConsoleUi.csproj -c release`