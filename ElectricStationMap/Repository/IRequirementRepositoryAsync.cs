﻿using ElectricStationMap.Models.EntityFramework;

namespace ElectricStationMap.Repository
{
    public interface IRequirementRepositoryAsync : IGenericRepositoryAsync<RequirementInfo>
    {
        Task<bool> HasRequirement(int id);
    }
}
