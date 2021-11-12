using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace MarioLandMod.UI.Elements
{
    public class TransformationInfoPanel : UIPanel
    {
        Color panelColour;
        UIText infoText;

        public TransformationInfoPanel(string text, Color colour)
        {
            Width = StyleDimension.FromPercent(0.3575f);
            Height = StyleDimension.FromPixels(30f);
            panelColour = colour;

            infoText = new UIText(text, 0.8f, false)
            {
                HAlign = 0.5f,
                VAlign = 0.5f
            };
            Append(infoText);
        }

        public void SetText(string text)
        {
            infoText.SetText(text);
        }

        public void SetColour(Color newColour)
        {
            panelColour = newColour;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            Rectangle rectangle = GetDimensions().ToRectangle();

            Utils.DrawSplicedPanel(spriteBatch, Main.Assets.Request<Texture2D>("Images/UI/CharCreation/PanelGrayscale", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, 10, 10, 10, 10, panelColour);
        }
    }
}
