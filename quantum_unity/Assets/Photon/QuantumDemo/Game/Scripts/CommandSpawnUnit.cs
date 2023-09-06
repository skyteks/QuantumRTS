using Photon.Deterministic;
namespace Quantum
{
    public class CommandSpawnUnit : DeterministicCommand
    {
        public long unitPrototypeGUID;

        public override void Serialize(BitStream stream)
        {
            stream.Serialize(ref unitPrototypeGUID);
        }

        public void Execute(Frame frame)
        {
            var unitPrototype = frame.FindAsset<EntityPrototype>(unitPrototypeGUID);
            //unitPrototype.Container.CreateEntity(frame);
        }
    }
}