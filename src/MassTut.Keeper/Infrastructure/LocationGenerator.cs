using Bogus;

using MassTut.Commons.Entities;

namespace MassTut.Keeper.Infrastructure
{
    /// <summary>
    /// A Location creatable with an empty ctor.
    /// </summary>
    class GeneratableLocation : Location
    {
        public GeneratableLocation() : base()
        {

        }
    }

    /// <summary>
    /// Generator for random Locations.
    /// </summary>
    public class LocationGenerator
    {
        private readonly Faker<GeneratableLocation> _faker;

        protected virtual void SetupFaker()
        {
            _faker.Rules((f, o) =>
            {
                o.Latitude = f.Address.Latitude();
                o.Longitude = f.Address.Longitude();
                o.Title = f.Lorem.Word();
            });
        }

        public LocationGenerator()
        {
            _faker = new Faker<GeneratableLocation>();
            SetupFaker();
        }

        public virtual Location FetchOne() => _faker.Generate();
    }
}
