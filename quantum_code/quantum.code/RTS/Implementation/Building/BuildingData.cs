﻿using Photon.Deterministic;
using Quantum.Inspector;

namespace Quantum
{
    public partial class BuildingData
    {
        public string name;

        [Header("Visuals")]
        public int skinID;
        public int iconID;

        [Space]

        [Header("Stats")]
        public FP maxHealth;

        public FP visionRadius;
    }
}
