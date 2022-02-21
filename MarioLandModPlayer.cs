using MarioLandMod.TransformationFiles.Luigi;
using MarioLandMod.TransformationFiles.Mario;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace MarioLand
{
    public class MarioLandModPlayer : ModPlayer
    {
        public Texture2D armsPath;
        public Texture2D headPath;
        public Texture2D bodyPath;
        public Texture2D legsPath;

        public bool TransformationActive;
        public bool TransformationActive_Mario;
        public bool TransformationActive_Luigi;

        public bool PowerUpActive;
        public bool PowerUpActive_FireFlower;
        public bool PowerUpActive_IceFlower;

        public void ChangeDrawTextures(string transformation, string type)
        {
            armsPath = ModContent.Request<Texture2D>($"MarioLandMod/TransformationTextures/{transformation}/{type}/{transformation}_{type}_Arms").Value;
            headPath = ModContent.Request<Texture2D>($"MarioLandMod/TransformationTextures/{transformation}/{type}/{transformation}_{type}_Head").Value;
            bodyPath = ModContent.Request<Texture2D>($"MarioLandMod/TransformationTextures/{transformation}/{type}/{transformation}_{type}_Body").Value;
            legsPath = ModContent.Request<Texture2D>($"MarioLandMod/TransformationTextures/{transformation}/{type}/{transformation}_{type}_Legs").Value;
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
                if (!PowerUpActive) ChangeDrawTextures("Mario", "Normal");
                if (PowerUpActive_FireFlower) ChangeDrawTextures("Mario", "FireFlower");
                if (PowerUpActive_IceFlower) ChangeDrawTextures("Mario", "IceFlower");
            }

            if (TransformationActive_Luigi)
            {
                if (!PowerUpActive) ChangeDrawTextures("Luigi", "Normal");
                if (PowerUpActive_FireFlower) ChangeDrawTextures("Luigi", "FireFlower");
                if (PowerUpActive_IceFlower) ChangeDrawTextures("Luigi", "IceFlower");
            }
        }

        public override void PostUpdateBuffs()
        {
            if (!TransformationActive_Mario) Player.ClearBuff(ModContent.BuffType<TransformationBuff_Mario>());
            if (!TransformationActive_Luigi) Player.ClearBuff(ModContent.BuffType<TransformationBuff_Luigi>());
        }
    }
}