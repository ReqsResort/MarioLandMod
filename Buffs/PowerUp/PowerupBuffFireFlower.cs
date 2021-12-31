using Terraria;
using Terraria.ModLoader;

namespace MarioLandMod.Buffs.PowerUp
{
    public class PowerUpBuffFireFlower : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Flower");
            Description.SetDefault($"You have the powers of the Fire Flower! You can now:\n- Press LMB with any weapon in your hand to set targets on fire for a few seconds\n- Press LMB with nothing on hand to fire out bouncing fireballs");

            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 10;
        }
    }
}