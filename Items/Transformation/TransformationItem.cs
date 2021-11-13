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
            if (MarioLandModSystem.SlotUIInstance.TransformationSlot.Item.type != ItemID.None && MarioLandModSystem.SlotUIInstance.TransformationSlot.Item != Item)
            {
                for (int i = 0; i < Main.InventorySlotsTotal; i++)
                {
                    if (player.inventory[i] == Item && player.inventory[i] != MarioLandModSystem.SlotUIInstance.TransformationSlot.Item)
                    {
                        player.inventory[i] = MarioLandModSystem.SlotUIInstance.TransformationSlot.Item;
                    }
                }
            }

            MarioLandModSystem.SlotUIInstance.TransformationSlot.Item = Item.Clone();
        }
    }
}
