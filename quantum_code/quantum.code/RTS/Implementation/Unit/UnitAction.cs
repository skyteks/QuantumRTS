using Photon.Deterministic;

namespace Quantum
{
    public static unsafe class UnitAction
    {
        public static EntityRef Spawn(Frame frame, FPVector3 position, FPQuaternion rotation, FP colliderSize)
        {
            EntityRef unit = frame.Create();
            frame.Set(unit, new Unit());
            frame.Set(unit, Transform3D.Create(position, rotation));
            Shape3D shape = Shape3D.CreateSphere(colliderSize, new FPVector3(0, colliderSize, 0));
            frame.Set(unit, PhysicsCollider3D.Create(frame, shape, null, false, 0));
            return unit;
        }
    }
}
