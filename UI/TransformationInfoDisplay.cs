using Terraria;
using Terraria.ModLoader;

namespace MarioLandMod.UI.Textures.TransformationInfoDisplay
{
    public class PowerUpInfoDisplay : InfoDisplay
    {
        public override void SetStaticDefaults()
        {
            InfoName.SetDefault("Equipped PowerUp");
        }

        public override bool Active()
        {
            return Main.LocalPlayer.GetModPlayer<MarioLandModPlayer>().PowerUpActive;
        }

        public override string DisplayValue()
        {
            return $"PowerUp: {ModContent.GetInstance<SlotUI>().VanityItem.Name}";
        }
    }

    public class TransformationInfoDisplay : InfoDisplay
    {
        public override void SetStaticDefaults()
        {
            InfoName.SetDefault("Equipped Transformation");
        }

        public override bool Active()
        {
            return Main.LocalPlayer.GetModPlayer<MarioLandModPlayer>().TransformationActive;
        }

        public override string DisplayValue()
        {
            return $"Transformation: {ModContent.GetInstance<SlotUI>().FunctionalItem.Name.Split("'")[0]}";
        }
    }
}