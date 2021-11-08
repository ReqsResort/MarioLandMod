using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.UI;

namespace MarioLandMod.UI.Elements
{
    public class TexturedCustomItemSlot : UIElement
    {
        internal Item Item;
        internal string HoverText;
        internal string TextureLocation;
        private readonly int _context;
        private readonly float _scale;
        internal Func<Item, bool> ValidItemFunc;

        public TexturedCustomItemSlot(string emptyTexture, string hoverText, int context = ItemSlot.Context.BankItem, float scale = 1f)
        {
            HoverText = hoverText;
            TextureLocation = emptyTexture;

            _context = context;
            _scale = scale;
            Item = new Item();
            Item.SetDefaults(0);

            Width.Set(TextureAssets.InventoryBack9.Value.Width * scale, 0f);
            Height.Set(TextureAssets.InventoryBack9.Value.Height * scale, 0f);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            float oldScale = Main.inventoryScale;
            Main.inventoryScale = _scale;
            Rectangle rectangle = GetDimensions().ToRectangle();
            Texture2D Texture = ModContent.Request<Texture2D>(TextureLocation).Value;

            if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (ValidItemFunc == null || ValidItemFunc(Main.mouseItem))
                {
                    ItemSlot.Handle(ref Item, _context);
                }
            }

            if (IsMouseHovering)
            {
                Main.hoverItemName = HoverText;
            }

            ItemSlot.Draw(spriteBatch, ref Item, _context, rectangle.TopLeft());
            Main.inventoryScale = oldScale;

            if (Item.IsAir)
            {
                spriteBatch.Draw(Texture, rectangle.Center(), null, Color.White * 0.45f, 0f, Texture.Size() / 2f, _scale, SpriteEffects.None, 0f);
            }
        }
    }
}