---
name: jbopale-extensions
description: Utiliser la bibliothèque NuGet JBOpale.Extensions (espace de noms JBOpale) en C#. À employer pour manipuler des chaînes (accents, slug, troncature, Base64, SHA-256), des collections (lots, doublons), des dates, des nombres, des types MIME, générer un mot de passe ou envoyer une liste à SQL Server via un paramètre table.
---

# JBOpale.Extensions

Bibliothèque de méthodes d'extension C#, sous licence MIT. Cibles : .NET Framework 4.8 et .NET 10.

```
dotnet add package JBOpale.Extensions
```

```csharp
using JBOpale;
```

Avant d'écrire une fonction utilitaire, vérifier qu'elle n'existe pas déjà ci-dessous.

## Chaînes

| Méthode | Rôle |
|---|---|
| `IsNullOrEmpty()`, `IsNullOrWhiteSpace()` | Tests sur la chaîne, utilisables sur une valeur null |
| `NullIfEmpty()`, `IfEmpty(valeur)` | Valeur null ou de repli si vide |
| `Truncate(max, suffixe = "...")` | Coupe en gardant `max` caractères suffixe compris |
| `Left(n)`, `Right(n)` | Premiers ou derniers caractères, sans exception |
| `RemoveAccents()` | `"Élève"` donne `"Eleve"` |
| `ToSlug()` | `"Crème Brûlée & Co"` donne `"creme-brulee-co"` |
| `ToTitleCase()` | Majuscule à chaque mot (culture courante) |
| `ContainsIgnoreCase(x)`, `EqualsIgnoreCase(x)` | Comparaisons sans casse |
| `ToInt(defaut)`, `ToIntOrNull()` | Conversion sans exception |
| `ToDecimalOrNull()` | Accepte `12,5` et `12.5` |
| `ToBool()` | Vrai pour `true`, `1`, `oui`, `o`, `yes`, `y`, `vrai` |
| `ConvertReal()` | Réel depuis `,` ou `.` ; `"15%"` donne `0.15` (lève une exception si invalide) |
| `IsValidEmail()`, `IsDigitsOnly()` | Contrôles de syntaxe simples |
| `ToBase64()`, `FromBase64()` | Base64 en UTF-8 |
| `ToSha256()` | Empreinte hexadécimale (pas pour stocker des mots de passe) |
| `NormalizeSpaces()` | Réduit les espaces multiples |
| `ToSafeFileName(remplacement = '_')` | Retire les caractères interdits Windows |

## Collections

| Méthode | Rôle |
|---|---|
| `IsNullOrEmpty()`, `OrEmpty()`, `WhereNotNull()` | Gestion du null sur les séquences |
| `ForEach(action)` | Action sur chaque élément |
| `Batch(taille)` | Lots de taille fixe, par exemple pour une clause SQL `IN` |
| `DistinctBy(cle)` | Doublons selon une clé (natif depuis .NET 6) |
| `JoinToString(separateur = ", ")` | Concaténation |
| `AddRange(elements)` | Sur toute `ICollection<T>` |
| `valeur.In(a, b, c)` | Équivalent du `IN` SQL |
| `GetValueOrDefault(cle, defaut)`, `AddOrUpdate(cle, valeur)` | Sur `IDictionary` (`GetValueOrDefault` est natif sous .NET 10) |

## Dates

`StartOfWeek(premierJour = Monday)`, `StartOfMonth()`, `EndOfMonth()`, `EndOfDay()`, `IsWeekend()`, `IsBetween(de, a)`, `Age(auJour = null)`, `ToUnixTimeSeconds()`, `FromUnixTimeSeconds()` (sur un `long`), `ToRelativeTime()` (`"il y a 5 minutes"`).

## Nombres et valeurs comparables

`IsBetween(min, max)`, `Clamp(min, max)`, `ToFileSize()` (sur un `long`, `"1,5 Ko"`), `RoundHalfUp(decimales)` (sur un `decimal`), `PercentOf(total)` (sur un `double`).

## Énumérations et exceptions

`GetDescription()` (attribut `[Description]`), `"texte".ToEnum<T>(defaut)`, `exception.GetFullMessage()` (inclut les exceptions internes).

## MIME et mot de passe

```csharp
string mime = Extensions.GetMime(".pdf", out string description); // "application/pdf"
string mdp  = Extensions.GeneratePassword(12, LettresSpecials: false, CaracteresSpeciaux: true);
```

`GeneratePassword` utilise un générateur cryptographique (`RandomNumberGenerator`) ; à partir de 10 caractères, le mot de passe contient au moins une minuscule, une majuscule, un chiffre et, si demandé, un caractère spécial.

## Envoyer une liste à SQL Server (paramètre table)

Déclarer le type côté base, par exemple :

```sql
CREATE TYPE [dbo].[Int64List] AS TABLE (Id bigint);
```

```csharp
// Liste de valeurs (struct) : Int64, DateTime, ...
SqlParameter p = ids.ToDataTableS("[dbo].[Int64List]", "Id", "@Ids");

// Liste de chaînes ou d'objets (les champs publics deviennent les colonnes)
SqlParameter q = noms.ToDataTableC("[dbo].[StringList]", "@Noms");

cmd.CommandType = CommandType.StoredProcedure;
cmd.Parameters.Add(p);
```

`SqlParameter` vient de `System.Data.SqlClient` sous .NET Framework 4.8 et de `Microsoft.Data.SqlClient` sous .NET 10. Pour `ToDataTableC` avec une classe, seuls les **champs publics** sont lus (pas les propriétés), dans l'ordre de déclaration, qui doit correspondre au type table SQL.

## Pièges

- Sous .NET 6+, `DistinctBy` et `GetValueOrDefault` viennent du framework : la bibliothèque ne les fournit que sous .NET Framework 4.8, ce qui évite les appels ambigus.
- Une chaîne invalide fait échouer `ConvertReal` ; préférer `ToDecimalOrNull` pour une saisie utilisateur.
