using MarioLandMod.UI;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarioLandMod.Items.PowerUp
{
    public class FireFlower : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            Item.vanity = true;
            Item.canBePlacedInVanityRegardlessOfConditions = true;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            return modded;
        }

        public override void UpdateVanity(Player player)
        {
            player.GetModPlayer<MarioLandModPlayer>().PowerUpActive_FireFlower = player.GetModPlayer<MarioLandModPlayer>().TransformationActive_Mario;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.RemoveAll(tooltip => tooltip.Name != "ItemName");
            tooltips.Add(new TooltipLine(Mod, "Transformation", "Transforms Super Mario or Super Luigi into Fire Mario or Fire Luigi respectively"));
        }
    }
}