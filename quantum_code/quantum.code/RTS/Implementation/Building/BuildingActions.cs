using Photon.Deterministic;

namespace Quantum
{
    public static unsafe class BuildingActions
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

            bool destroyed;
            bool success = health->Damage(damage, unitData.maxHealth, out destroyed);

            if (success)
            {
                if (destroyed)
                {
                    frame.Signals.OnDestroyed(unitEntity);
                }
                else
                {
                    frame.Signals.OnDamaged(unitEntity);
                }
            }
        }

        public static void RecieveRepair(Frame frame, EntityRef unitEntity, FP repair)
        {
            Health* health = frame.Unsafe.GetPointer<Health>(unitEntity);
            Unit* unit = frame.Unsafe.GetPointer<Unit>(unitEntity);
            UnitData unitData = frame.FindAsset<UnitData>(unit->unitData.Id);

            bool success = health->Heal(repair, unitData.maxHealth);
            if (success)
            {
                frame.Signals.OnRepaired(unitEntity);
            }
        }

        public static void SetSkin(Frame frame, EntityRef unitEntity, int newSkinID)
        {
            Skin* skin = frame.Unsafe.GetPointer<Skin>(unitEntity);

            skin->ChangeSkinID(newSkinID);
        }

        public static void SetSkinAnimation(Frame frame, EntityRef unitEntity, int animationID, float startTime, float endTime)
        {
            SkinAnimator* animator = frame.Unsafe.GetPointer<SkinAnimator>(unitEntity);

            animator->Animate(animationID, startTime, endTime);
        }
    }
}
