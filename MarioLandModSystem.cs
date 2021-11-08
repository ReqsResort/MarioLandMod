using MarioLandMod.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace MarioLandMod
{
    public class MarioLandModSystem : ModSystem
    {
        #region Slot UI

        private UserInterface SlotUIUserInterface;
        internal SlotUI SlotUI = new();

        public override void Load()
        {
            if (!Main.dedServ)
            {
                SlotUI.Activate();
                SlotUIUserInterface = new UserInterface();
                SlotUIUserInterface.SetState(SlotUI);
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (SlotUI.Visible)
            {
                SlotUIUserInterface?.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer("MarioLandMod: Slot UI", delegate
                {
                    if (SlotUI.Visible)
                    {
                        SlotUIUserInterface.Draw(Main.spriteBatch, new GameTime());
                    }
                    return true;
                }, InterfaceScaleType.UI));
            }
        }

        #endregion
    }
}
