using Photon.Deterministic;

namespace Quantum
{
    public static unsafe class BuildingActions
    {
        public static EntityRef Spawn(Frame frame, UnitData unitData, FPVector3 position, FPQuaternion rotation, FP colliderSize, FP healthPercentage)
        {
            EntityRef unitEntity = frame.Create();
            frame.Set(unitEntity, new Unit(unitData));
            frame.Set(unitEntity, Transform3D.Create(position, rotation));
            Shape3D shape = Shape3D.CreateSphere(colliderSize, new FPVector3(0, colliderSize, 0));
            frame.Set(unitEntity, PhysicsCollider3D.Create(frame, shape, null, false, 0));
            frame.Set(unitEntity, new Health(healthPercentage));
            return unitEntity;
        }

        public static void TakeDamage(Frame frame, EntityRef unitEntity, FP damage)
        {
            Health* health = frame.Unsafe.GetPointer<Health>(unitEntity);
            Unit* unit = frame.Unsafe.GetPointer<Unit>(unitEntity);
            UnitData unitData = frame.FindAsset<UnitData>(unit->unitData.Id);

            if (health->currentPercentage == 0)
            {
                return;
            }

            health->currentPercentage -= FPMath.Clamp01(damage / unitData.maxHealth);
            if (health->currentPercentage == 0)
            {
                frame.Signals.OnDestroyed(unitEntity);
            }
            else
            {
                frame.Signals.OnDamaged(unitEntity);
            }
        }

        public static void TakeRepair(Frame frame, EntityRef unitEntity, FP repair)
        {
            Health* health = frame.Unsafe.GetPointer<Health>(unitEntity);
            Unit* unit = frame.Unsafe.GetPointer<Unit>(unitEntity);
            UnitData unitData = frame.FindAsset<UnitData>(unit->unitData.Id);

            if (health->currentPercentage == 0 || health->currentPercentage == 1)
            {
                return;
            }

            health->currentPercentage -= FPMath.Clamp01(repair / unitData.maxHealth);
            frame.Signals.OnRepaired(unitEntity);
        }
    }
}
