using MarioLandMod.PowerUpFiles;
using MarioLandMod.TransformationFiles;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace MarioLand
{
    public class MarioLandModPlayer : ModPlayer
    {
        public Texture2D TransformationTexture;

        public bool TransformationActive;
        public List<bool> ActiveTransformations = new();
        public bool TransformationActive_Mario;
        public bool TransformationActive_Luigi;

        public bool PowerUpActive;
        public List<bool> ActivePowerUps = new();
        public bool PowerUpActive_FireFlower;
        public bool PowerUpActive_IceFlower;

        public override void Load()
        {
            ActiveTransformations.AddRange(new bool[] { TransformationActive_Mario, TransformationActive_Luigi });
            ActivePowerUps.AddRange(new bool[] { PowerUpActive_FireFlower, PowerUpActive_IceFlower });
        }

        public void ChangeTransformationTexture(string transformation, string type)
        {
            TransformationTexture = ModContent.Request<Texture2D>($"MarioLandMod/TransformationFiles/Textures/{transformation}/{transformation}_{type}").Value;
        }

        public override void ResetEffects()
        {
            ActiveTransformations.ForEach(item => item = false);
            ActivePowerUps.ForEach(item => item = false);
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
            TransformationActive = ActiveTransformations.Any(item => item);
            PowerUpActive = ActivePowerUps.Any(item => item);

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

            if (!PowerUpActive_FireFlower) Player.ClearBuff(ModContent.BuffType<PowerUpBuff_FireFlower>());
            if (!PowerUpActive_IceFlower) Player.ClearBuff(ModContent.BuffType<PowerUpBuff_IceFlower>());
        }
    }
}