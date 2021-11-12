using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;

namespace MarioLandMod.UI.Elements
{
    public class TransformationSelectButton : UIImageButton
    {
        public bool Clicked = false;
        public bool Hovering = false;

        private Texture2D Texture;
        readonly Texture2D NormalTexture = ModContent.Request<Texture2D>("MarioLandMod/UI/Textures/TransformationSelectionUI/TransformationButtonNormal", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        readonly Texture2D ClickedTexture = ModContent.Request<Texture2D>("MarioLandMod/UI/Textures/TransformationSelectionUI/TransformationButtonClicked", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        readonly Texture2D HoverFrameTexture = ModContent.Request<Texture2D>("MarioLandMod/UI/Textures/TransformationSelectionUI/TransformationButtonHoverFrame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;

        public TransformationSelectButton(string texture) : base(ModContent.Request<Texture2D>("MarioLandMod/UI/Textures/TransformationSelectionUI/TransformationButtonNormal", ReLogic.Content.AssetRequestMode.ImmediateLoad))
        {
            Texture = ModContent.Request<Texture2D>(texture, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        }

        public void SetClicked(bool state)
        {
            Clicked = state;
        }

        public void SetHovering(bool state)
        {
            Hovering = state;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            Rectangle rectangle = GetDimensions().ToRectangle();

            if (!Clicked)
            {
                spriteBatch.Draw(NormalTexture, rectangle, Color.White);
                spriteBatch.Draw(Texture, rectangle, Color.White);

                if (Hovering)
                {
                    spriteBatch.Draw(HoverFrameTexture, rectangle, Color.White);
                    spriteBatch.Draw(Texture, rectangle, Color.White);
                }
            }
            else
            {
                spriteBatch.Draw(ClickedTexture, rectangle, Color.White);
                spriteBatch.Draw(Texture, rectangle, Color.White);

                if (Hovering)
                {
                    spriteBatch.Draw(HoverFrameTexture, rectangle, Color.White);
                    spriteBatch.Draw(Texture, rectangle, Color.White);
                }
            }
        }
    }
}
