using MarioLandMod.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarioLandMod.Items.PowerUp
{
    public abstract class PowerUpItem : ModItem
    {
        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            if (SlotUI.PowerupSlot.Item.type != ItemID.None && SlotUI.PowerupSlot.Item != Item)
            {
                for (int i = 0; i < Main.InventorySlotsTotal; i++)
                {
                    if (player.inventory[i] == Item && player.inventory[i] != SlotUI.PowerupSlot.Item)
                    {
                        player.inventory[i] = SlotUI.PowerupSlot.Item;
                    }
                }
            }

            SlotUI.PowerupSlot.Item = Item.Clone();
        }
    }
}
