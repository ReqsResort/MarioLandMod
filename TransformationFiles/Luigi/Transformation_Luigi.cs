using MarioLand;
using MarioLandMod.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace MarioLandMod.TransformationFiles.Luigi
{
    public class TransformationItem_Luigi : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Luigi's Cap");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 20;
            Item.value = 0;
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
            Item.vanity = false;
            Item.canBePlacedInVanityRegardlessOfConditions = false;
            Item.GetGlobalItem<MarioLandModGlobalItem>().Transformation = true;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            return modded && slot == ModContent.GetInstance<TransformationSlot>().Type;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MarioLandModPlayer>().TransformationActive_Luigi = true;
            player.AddBuff(ModContent.BuffType<TransformationBuff_Luigi>(), 2);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.RemoveAll(tooltip => tooltip.Name != "ItemName");
            tooltips.Add(new TooltipLine(Mod, "Type", "Transformation") { overrideColor = Color.OrangeRed });
            tooltips.Add(new TooltipLine(Mod, "Transformation", "Transforms the player into Luigi"));
        }

        public override bool? PrefixChance(int pre, UnifiedRandom rand)
        {
            return !(pre == -1 || pre == -3);
        }
    }

    public class TransformationBuff_Luigi : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Luigi Time!");
            Description.SetDefault($"You have the powers of Luigi");

            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.accFlipper = true;
            player.noFallDmg = true;
            // player.spikedBoots = player.GetModPlayer<MarioLandModPlayer>().spikedBoots;
            player.buffTime[buffIndex] = 10;
        }
    }
}