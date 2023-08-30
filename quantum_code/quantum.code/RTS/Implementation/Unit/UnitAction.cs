using Photon.Deterministic;

namespace Quantum
{
    public static unsafe class UnitAction
    {
        public static EntityRef Spawn(Frame frame, UnitData unitData, FPVector3 position, FPQuaternion rotation, int skinID, FP colliderSize, FP healthPercentage)
        {
            EntityRef unitEntity = frame.Create();
            frame.Set(unitEntity, new Unit(unitData));
            frame.Set(unitEntity, Transform3D.Create(position, rotation));
            Shape3D shape = Shape3D.CreateSphere(colliderSize, new FPVector3(0, colliderSize, 0));
            frame.Set(unitEntity, PhysicsCollider3D.Create(frame, shape, null, false, 0));
            frame.Set(unitEntity, new Health(healthPercentage));
            frame.Set(unitEntity, new Skin(skinID));
            frame.Set(unitEntity, new SkinAnimator());
            return unitEntity;
        }

        public static void RecieveDamage(Frame frame, EntityRef unitEntity, FP damage)
        {
            Health* health = frame.Unsafe.GetPointer<Health>(unitEntity);
            Unit* unit = frame.Unsafe.GetPointer<Unit>(unitEntity);
            UnitData unitData = frame.FindAsset<UnitData>(unit->unitData.Id);

            if (health->currentPercentage == 0)
            {
                return;
            }

            bool killed;
            bool success = health->Damage(damage, unitData.maxHealth, out killed);

            if (success)
            {
                if (killed)
                {
                    frame.Signals.OnDestroyed(unitEntity);
                }
                else
                {
                    frame.Signals.OnDamaged(unitEntity);
                }
            }
        }

        public static void RecieveHealing(Frame frame, EntityRef unitEntity, FP healing)
        {
            Health* health = frame.Unsafe.GetPointer<Health>(unitEntity);
            Unit* unit = frame.Unsafe.GetPointer<Unit>(unitEntity);
            UnitData unitData = frame.FindAsset<UnitData>(unit->unitData.Id);

            bool success = health->Heal(healing, unitData.maxHealth);
            if (success)
            {
                frame.Signals.OnRepaired(unitEntity);
            }
        }
    }
}
