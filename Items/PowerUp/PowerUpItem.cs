using MarioLandMod.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarioLandMod.Items.PowerUp
{
    public abstract class PowerUpItem : ModItem
    {
        MarioLandModPlayer MarioLandModPlayer = new();
        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            if (MarioLandModPlayer.SlotUIInstance.PowerUpSlot.Item.type != ItemID.None && MarioLandModPlayer.SlotUIInstance.PowerUpSlot.Item != Item)
            {
                for (int i = 0; i < Main.InventorySlotsTotal; i++)
                {
                    if (player.inventory[i] == Item && player.inventory[i] != MarioLandModPlayer.SlotUIInstance.PowerUpSlot.Item)
                    {
                        player.inventory[i] = Item.CloneWithModdedDataFrom(MarioLandModPlayer.SlotUIInstance.PowerUpSlot.Item);
                    }
                }
            }

            MarioLandModPlayer.SlotUIInstance.PowerUpSlot.Item = Item.Clone();
        }
    }
}
