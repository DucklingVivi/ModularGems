using Terraria.ModLoader;

namespace ModularGems
{
	public class ModularGems : Mod
	{
        internal static ModularGems Instance;

        public override void Load()
        {
            Instance = this;
        }
    }
}