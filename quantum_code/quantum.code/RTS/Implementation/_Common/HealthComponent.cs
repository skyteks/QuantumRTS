using Photon.Deterministic;

namespace Quantum
{
    public partial struct Health
    {
        public Health(FP percentage)
        {
            currentPercentage = percentage; 
        }
    }
}