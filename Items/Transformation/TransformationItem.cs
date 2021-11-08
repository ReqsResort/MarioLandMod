using MarioLandMod.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarioLandMod.Items.Transformation
{
    public abstract class PowerUpItem : ModItem
    {
        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            if (SlotUI.TransformationSlot.Item.type != ItemID.None && SlotUI.TransformationSlot.Item != Item)
            {
                for (int i = 0; i < Main.InventorySlotsTotal; i++)
                {
                    if (player.inventory[i] == Item && player.inventory[i] != SlotUI.TransformationSlot.Item)
                    {
                        player.inventory[i] = SlotUI.TransformationSlot.Item;
                    }
                }
            }

            SlotUI.TransformationSlot.Item = Item.Clone();
        }
    }
}
