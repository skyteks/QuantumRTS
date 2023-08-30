namespace Quantum
{
    public partial struct Skin
    {
        public Skin(int _skinID)
        {
            skinID = _skinID;
        }

        public void ChangeSkinID(int newSkinID)
        {
            skinID = newSkinID;
        }
    }
}
