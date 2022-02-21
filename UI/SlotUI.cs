using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using MarioLand;

namespace MarioLandMod.UI
{
    public class TransformationSlot : ModAccessorySlot
    {
        public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
        {
            if (context == AccessorySlotType.FunctionalSlot) return checkItem.GetGlobalItem<MarioLandModGlobalItem>().Transformation == true;
            else return false;
        }

        public override Vector2? CustomLocation => new Vector2(Main.LocalPlayer.difficulty == 3 ? 67 : 20, 258);

        public override string FunctionalTexture => "MarioLandMod/UI/Textures/SlotUI/TransformationSlotEmptyTexture";
        public override string FunctionalBackgroundTexture => "MarioLandMod/UI/Textures/SlotUI/SlotBackgroundTexture";

        public override bool PreDraw(AccessorySlotType context, Item item, Vector2 position, bool isHovered)
        {
            return Player.chest == -1;
        }

        public override bool DrawVanitySlot => false;
        public override bool DrawDyeSlot => false;

        public override void OnMouseHover(AccessorySlotType context)
        {
            if (context == AccessorySlotType.FunctionalSlot) Main.hoverItemName = "Transformation";
        }

        public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo)
        {
            return item.GetGlobalItem<MarioLandModGlobalItem>().Transformation;
        }
    }

    public class PowerUpSlot : ModAccessorySlot
    {
        public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
        {
            if (context == AccessorySlotType.FunctionalSlot) return checkItem.GetGlobalItem<MarioLandModGlobalItem>().PowerUp == true;
            else return false;
        }

        public override Vector2? CustomLocation => new Vector2(Main.LocalPlayer.difficulty == 3 ? 115 : 67, 258);

        public override string FunctionalTexture => "MarioLandMod/UI/Textures/SlotUI/PowerUpSlotEmptyTexture";
        public override string FunctionalBackgroundTexture => "MarioLandMod/UI/Textures/SlotUI/SlotBackgroundTexture";

        public override bool PreDraw(AccessorySlotType context, Item item, Vector2 position, bool isHovered)
        {
            return Player.chest == -1;
        }

        public override bool DrawVanitySlot => false;
        public override bool DrawDyeSlot => false;

        public override void OnMouseHover(AccessorySlotType context)
        {
            if (context == AccessorySlotType.FunctionalSlot) Main.hoverItemName = "Power-Up";
        }

        public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo)
        {
            return item.GetGlobalItem<MarioLandModGlobalItem>().PowerUp;
        }
    }

    public class DyeSlot : ModAccessorySlot
    {
        public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
        {
            return false;
        }

        public override Vector2? CustomLocation => new Vector2(Main.LocalPlayer.difficulty == 3 ? 162 : 115, 258);

        public override string DyeBackgroundTexture => "MarioLandMod/UI/Textures/SlotUI/SlotBackgroundTexture";

        public override bool PreDraw(AccessorySlotType context, Item item, Vector2 position, bool isHovered)
        {
            return Player.chest == -1;
        }

        public override bool DrawFunctionalSlot => false;
        public override bool DrawVanitySlot => false;
    }
}
