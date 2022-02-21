using MarioLand;
using MarioLandMod.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace MarioLandMod.PowerUpFiles
{
    public class PowerUpItem_FireFlower : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Flower");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = 0;
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            Item.vanity = false;
            Item.canBePlacedInVanityRegardlessOfConditions = false;
            Item.GetGlobalItem<MarioLandModGlobalItem>().PowerUp = true;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            return modded && slot == ModContent.GetInstance<PowerUpSlot>().Type;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MarioLandModPlayer>().PowerUpActive_FireFlower = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.RemoveAll(tooltip => tooltip.Name != "ItemName");
            tooltips.Add(new TooltipLine(Mod, "Type", "Power-Up") { overrideColor = Color.OrangeRed });
            tooltips.Add(new TooltipLine(Mod, "Power-Up", "Transforms the player into Fire Mario / Fire Luigi"));
        }

        public override bool? PrefixChance(int pre, UnifiedRandom rand)
        {
            return !(pre == -1 || pre == -3);
        }
    }
}