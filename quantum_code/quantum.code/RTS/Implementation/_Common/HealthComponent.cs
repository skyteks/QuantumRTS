using Photon.Deterministic;

namespace Quantum
{
    public partial struct Health
    {
        public Health(FP percentage)
        {
            currentPercentage = percentage;
        }

        public bool Damage(FP damage, FP maxHealth, out bool killed)
        {
            if (currentPercentage == 0)
            {
                killed = false;
                return false;
            }
            currentPercentage -= FPMath.Clamp01(damage / maxHealth);
            killed = currentPercentage == 0;
            return true;
        }

        public bool Heal(FP healing, FP maxHealth)
        {
            if (currentPercentage == 0 || currentPercentage == 1)
            {
                return false;
            }
            currentPercentage += FPMath.Clamp01(healing / maxHealth);
            return true;
        }
    }
}