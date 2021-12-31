using MarioLandMod.UI;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarioLandMod.Items.Transformation
{
    public class TransformationItemLuigi : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Luigi's Cap");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            SetupDrawing();
            ItemID.Sets.Deprecated[Type] = true;
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
        }

        public override void Load()
        {
            Mod.AddEquipTexture(new EquipTexture(), this, EquipType.Head, "MarioLandMod/TransformationTextures/Luigi/Normal/Luigi_Normal_Head");
            Mod.AddEquipTexture(new EquipTexture(), this, EquipType.Body, "MarioLandMod/TransformationTextures/Luigi/Normal/Luigi_Normal_Body");
            Mod.AddEquipTexture(new EquipTexture(), this, EquipType.Legs, "MarioLandMod/TransformationTextures/Luigi/Normal/Luigi_Normal_Legs");
        }

        private void SetupDrawing()
        {
            int LuigiNormalHead = Mod.GetEquipSlot(Name, EquipType.Head);
            int LuigiNormalBody = Mod.GetEquipSlot(Name, EquipType.Body);
            int LuigiNormalLegs = Mod.GetEquipSlot(Name, EquipType.Legs);


            ArmorIDs.Head.Sets.DrawHead[LuigiNormalHead] = false;
            ArmorIDs.Body.Sets.HidesTopSkin[LuigiNormalBody] = true;
            ArmorIDs.Body.Sets.HidesArms[LuigiNormalBody] = true;
            ArmorIDs.Legs.Sets.HidesBottomSkin[LuigiNormalLegs] = true;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            return modded && slot == ModContent.GetInstance<SlotUI>().Type;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MarioLandModPlayer>().TransformationActive_Luigi = true;
            player.GetModPlayer<MarioLandModPlayer>().TransformationVisualActive = !hideVisual;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.RemoveAll(tooltip => tooltip.Name != "ItemName");
            tooltips.Add(new TooltipLine(Mod, "Transformation", "Transforms the player into Super Luigi"));
        }
    }
}