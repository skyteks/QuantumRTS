using Photon.Deterministic;

namespace Quantum
{
    public static unsafe class BuildingActions
    {
        public static EntityRef Spawn(Frame frame, FPVector3 position, FPQuaternion rotation, FP colliderSize)
        {
            EntityRef building = frame.Create();
            frame.Set(building, new Unit());
            frame.Set(building, Transform3D.Create(position, rotation));
            Shape3D shape = Shape3D.CreateSphere(colliderSize, new FPVector3(0, colliderSize, 0));
            frame.Set(building, PhysicsCollider3D.Create(frame, shape, null, false, 0));
            return building;
        }
    }
}
