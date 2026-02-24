var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("database-server")
    .WithDataVolume(isReadOnly: false)
    .WithPgAdmin();

var db = postgres.AddDatabase("db-application");


builder.AddProject<Projects.TCG_EMS>("tcg-ems")
    .WithReference(db)
    .WaitFor(db);

builder.AddProject<Projects.TCG_Website>("tcg-website")
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();
