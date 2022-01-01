using Terraria;
using Terraria.ModLoader;

namespace MarioLandMod.Buffs.Transformation
{
    public class TransformationBuffMario : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mario Time!");
            Description.SetDefault($"You have the powers of Super Mario! You can now:" +
                $"\n- Jump on enemies to damage them (8 or more consecutive jumps will heal you)" +
                $"\n- Wall kick to climb steep surfaces (you cannot climb the same wall more than once in a row)" +
                $"\n- Swim indefinitely and jump down from great heights without damaging yourself");

            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.accFlipper = true;
            player.noFallDmg = true;
            player.spikedBoots = player.GetModPlayer<MarioLandModPlayer>().spikedBoots;
            player.buffTime[buffIndex] = 10;
        }
    }
}