using MarioLandMod.TransformationFiles.Luigi;
using MarioLandMod.TransformationFiles.Mario;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace MarioLand
{
    public class MarioLandModPlayer : ModPlayer
    {
        public Texture2D TransformationTexture;

        public bool TransformationActive;
        public bool TransformationActive_Mario;
        public bool TransformationActive_Luigi;

        public bool PowerUpActive;
        public bool PowerUpActive_FireFlower;
        public bool PowerUpActive_IceFlower;

        public void ChangeTransformationTexture(string transformation, string type)
        {
            TransformationTexture = ModContent.Request<Texture2D>($"MarioLandMod/TransformationFiles/{transformation}/Textures/{transformation}_{type}").Value;
        }

        public override void ResetEffects()
        {
            TransformationActive_Mario = TransformationActive_Luigi = false;
            PowerUpActive_FireFlower = PowerUpActive_IceFlower = false;
        }

        public override void HideDrawLayers(PlayerDrawSet drawInfo)
        {
            if ((!Player.dead || !Player.invis) && TransformationActive)
            {
                PlayerDrawLayers.Skin.Hide();
                PlayerDrawLayers.ArmOverItem.Hide();
                PlayerDrawLayers.Head.Hide();
                PlayerDrawLayers.Torso.Hide();
                PlayerDrawLayers.Leggings.Hide();
            }
        }

        public override void FrameEffects()
        {
            TransformationActive = TransformationActive_Mario || TransformationActive_Luigi;
            PowerUpActive = PowerUpActive_FireFlower || PowerUpActive_IceFlower;

            if (TransformationActive_Mario)
            {
                if (!PowerUpActive) ChangeTransformationTexture("Mario", "Normal");
                if (PowerUpActive_FireFlower) ChangeTransformationTexture("Mario", "FireFlower");
                if (PowerUpActive_IceFlower) ChangeTransformationTexture("Mario", "IceFlower");
            }

            if (TransformationActive_Luigi)
            {
                if (!PowerUpActive) ChangeTransformationTexture("Luigi", "Normal");
                if (PowerUpActive_FireFlower) ChangeTransformationTexture("Luigi", "FireFlower");
                if (PowerUpActive_IceFlower) ChangeTransformationTexture("Luigi", "IceFlower");
            }
        }

        public override void PostUpdateBuffs()
        {
            if (!TransformationActive_Mario) Player.ClearBuff(ModContent.BuffType<TransformationBuff_Mario>());
            if (!TransformationActive_Luigi) Player.ClearBuff(ModContent.BuffType<TransformationBuff_Luigi>());
        }
    }
}