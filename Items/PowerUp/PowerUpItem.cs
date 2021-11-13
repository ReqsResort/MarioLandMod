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
            if (MarioLandModSystem.SlotUIInstance.PowerupSlot.Item.type != ItemID.None && MarioLandModSystem.SlotUIInstance.PowerupSlot.Item != Item)
            {
                for (int i = 0; i < Main.InventorySlotsTotal; i++)
                {
                    if (player.inventory[i] == Item && player.inventory[i] != MarioLandModSystem.SlotUIInstance.PowerupSlot.Item)
                    {
                        player.inventory[i] = MarioLandModSystem.SlotUIInstance.PowerupSlot.Item;
                    }
                }
            }

            MarioLandModSystem.SlotUIInstance.PowerupSlot.Item = Item.Clone();
        }
    }
}
