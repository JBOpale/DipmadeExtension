# JBOpale.Extensions

Méthodes d'extension C# pour simplifier le code courant : chaînes, collections, dates, nombres, types MIME, génération de mots de passe et paramètres SQL (listes).

- Cibles : **.NET Framework 4.8** et **.NET 10**
- Espace de noms : `JBOpale`
- Licence : [MIT](LICENSE.md)

```csharp
using JBOpale;

"Crème Brûlée".ToSlug();                  // "creme-brulee"
Enumerable.Range(1, 7).Batch(3);          // [1,2,3] [4,5,6] [7]
new DateTime(2026, 10, 25).StartOfWeek(); // 19/10/2026
1536L.ToFileSize();                       // "1,5 Ko"
```

Sous .NET 10, les méthodes SQL (`ToDataTableS`, `ToDataTableC`) utilisent `Microsoft.Data.SqlClient` ; sous .NET Framework 4.8, `System.Data.SqlClient`.
