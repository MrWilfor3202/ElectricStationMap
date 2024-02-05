using ElectricStationMap.Services.Guid;

namespace ElectricStationMap.Models.EF
{
    public abstract class BaseEntity
    {
        private static readonly ISequentialGuidGenerator _guidGenerator = new CustomSequentialGuidGenerator();

        public Guid Id { get; set; }

        public BaseEntity() => Id = _guidGenerator.CreateGuid();

    }
}
