using System.Diagnostics.CodeAnalysis;

namespace DataAccess.DbConfigureManagement;

/// <summary>
/// Провайдеры СУБД.
/// </summary>
[SuppressMessage("ReSharper", "IdentifierTypo")]
public enum DbProviderEnm
{
    MariaDb = 1,
    MongoDb,
    MsSqlServer,
    MySql,
    Oracle,
    PostgreSql,
    Sqlite,
}
