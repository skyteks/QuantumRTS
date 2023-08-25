namespace Quantum
{
    public static unsafe class PlayerActions
    {
        public static EntityRef Spawn(Frame frame)
        {
            EntityRef player = frame.Create();
            frame.Set(player, new Player());
            frame.Set(player, new ClientRef());
            return player;
        }
    }
}
