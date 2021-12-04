using MarioLandMod.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarioLandMod.Items.Transformation
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
            if (MarioLandModPlayer.SlotUIInstance.TransformationSlot.Item.type != ItemID.None && MarioLandModPlayer.SlotUIInstance.TransformationSlot.Item != Item)
            {
                for (int i = 0; i < Main.InventorySlotsTotal; i++)
                {
                    if (player.inventory[i] == Item && player.inventory[i] != MarioLandModPlayer.SlotUIInstance.TransformationSlot.Item)
                    {
                        player.inventory[i] = Item.CloneWithModdedDataFrom(MarioLandModPlayer.SlotUIInstance.TransformationSlot.Item);
                    }
                }
            }

            MarioLandModPlayer.SlotUIInstance.TransformationSlot.Item = Item.Clone();
        }
    }
}
