using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JBOpale;

namespace UnitTest_Extensions
{
    [TestClass]
    public class ExtensionsTests
    {
        [TestMethod]
        public void String_Accents_Slug_Truncate()
        {
            Assert.AreEqual("Eleve a Noel c", "Élève à Noël ç".RemoveAccents());
            Assert.AreEqual("creme-brulee-co", "Crème Brûlée & Co!".ToSlug());
            Assert.AreEqual("abcde...", "abcdefghij".Truncate(8));
            Assert.AreEqual("abc", "abc".Truncate(8));
            Assert.AreEqual("ab", "abcdef".Left(2));
            Assert.AreEqual("ef", "abcdef".Right(2));
        }

        [TestMethod]
        public void String_Conversions()
        {
            Assert.AreEqual(12.5m, "12,5".ToDecimalOrNull());
            Assert.IsNull("x".ToIntOrNull());
            Assert.AreEqual(42, "42".ToInt());
            Assert.IsTrue("Oui".ToBool());
            Assert.IsFalse("non".ToBool());
            Assert.IsTrue("a@b.fr".IsValidEmail());
            Assert.IsFalse("a@b".IsValidEmail());
            Assert.AreEqual("héllo", "héllo".ToBase64().FromBase64());
            Assert.AreEqual("ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad", "abc".ToSha256());
            Assert.AreEqual("a_b_c", "a/b:c".ToSafeFileName());
            Assert.AreEqual("a b", " a  \n b ".NormalizeSpaces());
        }

        [TestMethod]
        public void Enumerable_Helpers()
        {
            List<List<int>> batches = Enumerable.Range(1, 7).Batch(3).ToList();
            Assert.AreEqual(3, batches.Count);
            Assert.AreEqual(1, batches[2].Count);
            Assert.AreEqual(2, new[] { "a", "A", "b" }.DistinctBy(x => x.ToLower()).Count());
            Assert.AreEqual("1-2-3", new[] { 1, 2, 3 }.JoinToString("-"));
            Assert.IsTrue(2.In(1, 2, 3));
            Assert.IsTrue(((IEnumerable<int>)null).IsNullOrEmpty());
            Dictionary<string, int> d = new Dictionary<string, int> { { "a", 1 } };
            Assert.AreEqual(9, d.GetValueOrDefault("z", 9));
        }

        [TestMethod]
        public void DateTime_Helpers()
        {
            DateTime dimanche = new DateTime(2026, 10, 25);
            Assert.AreEqual(new DateTime(2026, 10, 19), dimanche.StartOfWeek());
            Assert.AreEqual(new DateTime(2026, 10, 31), dimanche.EndOfMonth());
            Assert.IsTrue(dimanche.IsWeekend());
            Assert.AreEqual(25, new DateTime(2000, 10, 26).Age(dimanche));
            Assert.AreEqual(26, new DateTime(2000, 10, 25).Age(dimanche));
            Assert.AreEqual(86400L, new DateTime(1970, 1, 2, 0, 0, 0, DateTimeKind.Utc).ToUnixTimeSeconds());
            Assert.AreEqual("il y a 5 minutes", dimanche.AddMinutes(-5).ToRelativeTime(dimanche));
        }

        [TestMethod]
        public void Numeric_Helpers()
        {
            Assert.IsTrue(5.IsBetween(1, 5));
            Assert.AreEqual(10, 15.Clamp(1, 10));
            Assert.AreEqual("1.5 Ko", 1536L.ToFileSize(CultureInfo.InvariantCulture));
            Assert.AreEqual(3m, 2.5m.RoundHalfUp());
        }

        [TestMethod]
        public void Object_Helpers()
        {
            Assert.AreEqual(DayOfWeek.Monday, "monday".ToEnum(DayOfWeek.Sunday));
            Assert.AreEqual(DayOfWeek.Sunday, "blue".ToEnum(DayOfWeek.Sunday));
            Assert.AreEqual("a > b", new Exception("a", new Exception("b")).GetFullMessage());
        }

        [TestMethod]
        public void GeneratePassword_Securise()
        {
            var mdps = new HashSet<string>();
            for (int i = 0; i < 200; i++)
            {
                string p = Extensions.GeneratePassword(12);
                Assert.AreEqual(12, p.Length);
                Assert.IsTrue(p.Any(char.IsLower));
                Assert.IsTrue(p.Any(char.IsUpper));
                Assert.IsTrue(p.Any(char.IsDigit));
                Assert.IsTrue(p.Any(c => !char.IsLetterOrDigit(c)));
                mdps.Add(p);
            }
            Assert.AreEqual(200, mdps.Count);

            string sansSpeciaux = Extensions.GeneratePassword(16, CaracteresSpeciaux: false);
            Assert.IsTrue(sansSpeciaux.All(char.IsLetterOrDigit));
        }
    }
}
