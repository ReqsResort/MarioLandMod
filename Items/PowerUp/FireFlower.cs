using Terraria.ModLoader;
using Terraria.ID;
using Terraria;

namespace MarioLandMod.Items.PowerUp
{
    public class FireFlower : PowerUpItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Orange;
        }
    }
}