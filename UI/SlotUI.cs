using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using MarioLandMod.Items.Transformation;
using MarioLandMod.Items.PowerUp;

namespace MarioLandMod.UI
{
    public class SlotUI : ModAccessorySlot
    {
        public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
        {
            if (context == AccessorySlotType.FunctionalSlot) return checkItem.type == ModContent.ItemType<TransformationItemMario>() || checkItem.type == ModContent.ItemType<TransformationItemLuigi>();
            if (context == AccessorySlotType.VanitySlot) return checkItem.type == ModContent.ItemType<FireFlower>();
            else return false;
        }

        public override Vector2? CustomLocation => new Vector2(Main.LocalPlayer.GetModPlayer<MarioLandModPlayer>().SlotUILeft, Main.LocalPlayer.GetModPlayer<MarioLandModPlayer>().SlotUITop);

        public override string FunctionalTexture => "MarioLandMod/UI/Textures/SlotUI/TransformationSlotEmptyTexture";
        public override string FunctionalBackgroundTexture => "MarioLandMod/UI/Textures/SlotUI/SlotBackgroundTexture";

        public override string VanityTexture => "MarioLandMod/UI/Textures/SlotUI/PowerUpSlotEmptyTexture";
        public override string VanityBackgroundTexture => "MarioLandMod/UI/Textures/SlotUI/SlotBackgroundTexture";

        public override string DyeBackgroundTexture => "MarioLandMod/UI/Textures/SlotUI/SlotBackgroundTexture";

        public override bool PreDraw(AccessorySlotType context, Item item, Vector2 position, bool isHovered)
        {
            return Player.chest == -1;
        }

        public override void OnMouseHover(AccessorySlotType context)
        {
            if (context == AccessorySlotType.FunctionalSlot) Main.hoverItemName = "Transformation";
            if (context == AccessorySlotType.VanitySlot) Main.hoverItemName = "PowerUp";
            if (context == AccessorySlotType.DyeSlot) Main.hoverItemName = "Transformation Dye";
        }
    }
}
