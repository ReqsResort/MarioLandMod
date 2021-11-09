using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace MarioLandMod.Buffs.PowerUp
{
    public class PowerupBuffFireFlower : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Flower");
            Description.SetDefault($"You have the powers of the Fire Flower! You can now:\n- Press LMB with any weapon in your hand to set targets on fire for a few seconds\n- Press LMB with nothing on hand to fire out bouncing fireballs");

            Main.buffNoTimeDisplay[Type] = true;
            CanBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 10;
        }
    }
}