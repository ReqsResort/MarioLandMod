using Terraria;
using Terraria.ModLoader;

namespace MarioLandMod.Buffs.Transformation
{
    public class TransformationBuffMario : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mario Time!");
            Description.SetDefault($"You have the powers of Super Mario! You can now:\n- Double jump and triple jump for some extra height\n- Jump on enemies to damage them (8 or more consecutive jumps will heal you)\n- Wall kick to climb steep surfaces (you cannot climb the same wall more than once in a row)\n- Swim indefinitely and jump down from great heights without damaging yourself\n- Ground pound enemies for some extra damage (hold {Main.cDown} to ground pound)");

            Main.buffNoTimeDisplay[Type] = true;
            CanBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            MarioLandModPlayer MarioLandModPlayer = player.GetModPlayer<MarioLandModPlayer>();

            MarioLandModPlayer.TransformationActive_Mario = true;

            player.buffTime[buffIndex] = 10;
        }
    }
}