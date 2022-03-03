using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace MarioLand
{
    public class MarioLandModGlobalItem : GlobalItem
    {
        public bool Transformation;
        public bool PowerUp;

        public MarioLandModGlobalItem()
        {
            Transformation = false;
            PowerUp = false;
        }

        public override bool InstancePerEntity => true;

        public override GlobalItem Clone(Item item, Item itemClone)
        {
            MarioLandModGlobalItem MarioLandModGlobalItem = (MarioLandModGlobalItem)base.Clone(item, itemClone);
            MarioLandModGlobalItem.Transformation = Transformation;
            MarioLandModGlobalItem.PowerUp = PowerUp;
            return MarioLandModGlobalItem;
        }
    }

    public sealed class ArmsLayer : PlayerDrawLayer
    {
        public override Position GetDefaultPosition()
        {
            return new AfterParent(PlayerDrawLayers.HeldItem);
        }

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            MarioLandModPlayer MarioLandModPlayer = drawInfo.drawPlayer.GetModPlayer<MarioLandModPlayer>();
            return (!drawInfo.drawPlayer.dead || !drawInfo.drawPlayer.invis) && MarioLandModPlayer.TransformationActive;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;

            Texture2D texture = drawPlayer.GetModPlayer<MarioLandModPlayer>().armsPath;
            Vector2 drawPos = drawInfo.Position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.bodyPosition;
            Vector2 bodyVect = drawInfo.bodyVect;
            DrawData drawData = new(texture, drawPos.Floor() + bodyVect, drawPlayer.bodyFrame, Color.White, drawPlayer.bodyRotation, bodyVect, 1f, drawInfo.playerEffect, 0)
            {
                shader = drawInfo.cBody
            };
            drawInfo.DrawDataCache.Add(drawData);
        }
    }

    public sealed class HeadLayer : PlayerDrawLayer
    {
        public override Position GetDefaultPosition()
        {
            return new BeforeParent(PlayerDrawLayers.HeldItem);
        }

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            MarioLandModPlayer MarioLandModPlayer = drawInfo.drawPlayer.GetModPlayer<MarioLandModPlayer>();
            return (!drawInfo.drawPlayer.dead || !drawInfo.drawPlayer.invis) && MarioLandModPlayer.TransformationActive;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;

            Texture2D texture = drawPlayer.GetModPlayer<MarioLandModPlayer>().headPath;
            Vector2 drawPos = drawInfo.Position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.headPosition;
            Vector2 headVect = drawInfo.headVect;
            DrawData drawData = new(texture, drawPos.Floor() + headVect, drawPlayer.bodyFrame, Color.White, drawPlayer.headRotation, headVect, 1f, drawInfo.playerEffect, 0)
            {
                shader = drawInfo.cHead
            };
            drawInfo.DrawDataCache.Add(drawData);
        }
    }

    public sealed class BodyLayer : PlayerDrawLayer
    {
        public override Position GetDefaultPosition()
        {
            return new BeforeParent(PlayerDrawLayers.HeldItem);
        }

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            MarioLandModPlayer MarioLandModPlayer = drawInfo.drawPlayer.GetModPlayer<MarioLandModPlayer>();
            return (!drawInfo.drawPlayer.dead || !drawInfo.drawPlayer.invis) && MarioLandModPlayer.TransformationActive;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;

            Texture2D texture = drawPlayer.GetModPlayer<MarioLandModPlayer>().bodyPath;
            Vector2 drawPos = drawInfo.Position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.bodyPosition;
            Vector2 bodyVect = drawInfo.bodyVect;
            DrawData drawData = new(texture, drawPos.Floor() + bodyVect, drawPlayer.bodyFrame, Color.White, drawPlayer.bodyRotation, bodyVect, 1f, drawInfo.playerEffect, 0)
            {
                shader = drawInfo.cBody
            };
            drawInfo.DrawDataCache.Add(drawData);
        }
    }

    public sealed class LegsLayer : PlayerDrawLayer
    {
        public override Position GetDefaultPosition()
        {
            return new BeforeParent(PlayerDrawLayers.HeldItem);
        }

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            MarioLandModPlayer MarioLandModPlayer = drawInfo.drawPlayer.GetModPlayer<MarioLandModPlayer>();
            return (!drawInfo.drawPlayer.dead || !drawInfo.drawPlayer.invis) && MarioLandModPlayer.TransformationActive;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;

            Texture2D texture = drawPlayer.GetModPlayer<MarioLandModPlayer>().legsPath;
            Vector2 drawPos = drawInfo.Position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.legPosition;
            Vector2 legVect = drawInfo.legVect;
            DrawData drawData = new(texture, drawPos.Floor() + legVect, drawPlayer.legFrame, Color.White, drawPlayer.legRotation, legVect, 1f, drawInfo.playerEffect, 0)
            {
                shader = drawInfo.cLegs
            };
            drawInfo.DrawDataCache.Add(drawData);
        }
    }
}