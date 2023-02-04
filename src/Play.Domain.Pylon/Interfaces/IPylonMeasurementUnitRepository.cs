using PylonDatabaseHandler.models.pylon;

namespace Play.Domain.Pylon.Interfaces;

public interface IPylonMeasurementUnitRepository
{
    Task<Hemeasurementunits?> GetHemeasurementunits(Guid heid);
}