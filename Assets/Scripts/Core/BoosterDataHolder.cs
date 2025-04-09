using System.Collections.Generic;
using System.Linq;
using Core.Data;

namespace Core
{
    public class BoosterDataHolder
    {
        public Dictionary<BoosterType, BoosterData> BoosterDataMap { get; private set; }
        public BoosterType[] BoosterTypes { get; private set; }

        public BoosterDataHolder(BoosterData[] boostersData)
        {
            BoosterDataMap = boostersData.ToDictionary(k => k.Type, v => v);
            BoosterTypes = boostersData.Select(v => v.Type).ToArray();
        }
    }
}