using MarioLandMod.UI;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarioLandMod.Items.Transformation
{
    public class TransformationItemMario : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mario's Cap");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            SetupDrawing();
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 20;
            Item.value = 0;
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
            Item.vanity = false;
            Item.canBePlacedInVanityRegardlessOfConditions = false;
        }

        public override void Load()
        {
            Mod.AddEquipTexture(new EquipTexture(), this, EquipType.Head, "MarioLandMod/TransformationTextures/Mario/Normal/Mario_Normal_Head");
            Mod.AddEquipTexture(new EquipTexture(), this, EquipType.Body, "MarioLandMod/TransformationTextures/Mario/Normal/Mario_Normal_Body");
            Mod.AddEquipTexture(new EquipTexture(), this, EquipType.Legs, "MarioLandMod/TransformationTextures/Mario/Normal/Mario_Normal_Legs");
        }

        private void SetupDrawing()
        {
            int MarioNormalHead = Mod.GetEquipSlot(Name, EquipType.Head);
            int MarioNormalBody = Mod.GetEquipSlot(Name, EquipType.Body);
            int MarioNormalLegs = Mod.GetEquipSlot(Name, EquipType.Legs);


            ArmorIDs.Head.Sets.DrawHead[MarioNormalHead] = false;
            ArmorIDs.Body.Sets.HidesTopSkin[MarioNormalBody] = true;
            ArmorIDs.Body.Sets.HidesArms[MarioNormalBody] = true;
            ArmorIDs.Legs.Sets.HidesBottomSkin[MarioNormalLegs] = true;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            return modded && slot == ModContent.GetInstance<SlotUI>().Type;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MarioLandModPlayer>().TransformationActive_Mario = true;
            player.GetModPlayer<MarioLandModPlayer>().TransformationVisualActive = !hideVisual;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.RemoveAll(tooltip => tooltip.Name != "ItemName");
            tooltips.Add(new TooltipLine(Mod, "Transformation", "Transforms the player into Super Mario"));
            tooltips.Add(new TooltipLine(Mod, "Disclaimer", "Currently isn't functional. You will only get the buff."));
        }
    }
}