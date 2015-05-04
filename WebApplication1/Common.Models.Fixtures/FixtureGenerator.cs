using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace Common.Models.Fixtures
{
    public abstract class FixtureGenerator<T> where T : ObjectBase
    {
        protected static readonly IFixture Fixture = new Fixture().Customize(new AutoMoqCustomization());

        public ICollection<T> GenerateFixtures()
        {
            return GenerateFixtures(Fixture.Create<int>());
        }

        public ICollection<T> GenerateFixtures(int numberOfFixtures)
        {
            ICollection<T> fixtures = new List<T>();

            for (int i = 0; i < numberOfFixtures; i++)
            {
                fixtures.Add(GenerateFixture());
            }

            return fixtures;
        }

    }
}
