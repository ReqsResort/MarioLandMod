using DiscordRPC;
using Steamworks;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using System.ComponentModel;
using Terraria.ID;
using MarioLandMod.UI;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Microsoft.Xna.Framework;

namespace MarioLandMod
{
    public class MarioLandModGlobalItem : GlobalItem
    {
        /* public override bool CanUseItem(Item item, Player player)
        {
            return !Main.LocalPlayer.GetModPlayer<MarioLandModPlayer>().TransformationActive;
        } */

        public override void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (player.GetModPlayer<MarioLandModPlayer>().PowerUpActive_FireFlower)
            {
                target.AddBuff(BuffID.OnFire, 180);
            }
        }

        public override void OnHitPvp(Item item, Player player, Player target, int damage, bool crit)
        {
            if (player.GetModPlayer<MarioLandModPlayer>().PowerUpActive_FireFlower)
            {
                target.AddBuff(BuffID.OnFire, 180);
            }
        }
    }

    public class MarioLandModGlobalProjectile : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            if (Main.LocalPlayer.GetModPlayer<MarioLandModPlayer>().PowerUpActive_FireFlower)
            {
                target.AddBuff(BuffID.OnFire, 180);
            }
        }

        public override void OnHitPvp(Projectile projectile, Player target, int damage, bool crit)
        {
            if (Main.LocalPlayer.GetModPlayer<MarioLandModPlayer>().PowerUpActive_FireFlower)
            {
                target.AddBuff(BuffID.OnFire, 180);
            }
        }
    }

    #region Discord RPC

    [Label("Mario Land Config")]
    public class MarioLandModConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public static MarioLandModConfig Instance;

        [Label("Discord Rich Presence")]
        [Tooltip("Mario Land Mod Discord RPC toggle (requires mod reload)")]
        [DefaultValue(false)]
        [ReloadRequired]
        public bool DiscordRPCOn;
    }

    public class MarioLandModDiscordRichPresence
    {
        private static RichPresence Presence;
        private static DiscordRpcClient Client;

        public static void Initialize()
        {
            Presence = new RichPresence
            {
                Details = "In Main Menu",
                Assets = new Assets
                {
                    LargeImageText = "Mario Land Mod (1.4 Alpha)",
                    LargeImageKey = "mlmdefault",
                    SmallImageKey = "mainmenu"
                },
                State = ""
            };

            Client = new DiscordRpcClient("901990118196396082");

            Presence.Timestamps = new Timestamps
            {
                Start = DateTime.UtcNow
            };

            Client?.Initialize();

            if (MarioLandModConfig.Instance.DiscordRPCOn) Client?.SetPresence(Presence);
        }

        private static int Cooldown;

        public static void Update()
        {
            if (MarioLandModConfig.Instance.DiscordRPCOn) Cooldown++;

            if (Client == null || Cooldown < 60 || !MarioLandModConfig.Instance.DiscordRPCOn) return;

            Client.Invoke();

            if (Main.gameMenu)
            {
                Client.UpdateDetails("In Main Menu");
                Client.UpdateSmallAsset("mainmenu");
                Client.UpdateState("");
            }
            else
            {
                MarioLandModPlayer MarioLandModPlayer = Main.LocalPlayer.GetModPlayer<MarioLandModPlayer>();

                if (!MarioLandModPlayer.TransformationActive)
                {
                    Client.UpdateDetails("No Transformation Equipped");
                    Client.UpdateSmallAsset("none");
                    Client.UpdateState("");
                }
                else
                {
                    Client.UpdateDetails($"Running around as {ModContent.GetInstance<SlotUI>().FunctionalItem.Name.Split("'")[0]}");
                    Client.UpdateSmallAsset(ModContent.GetInstance<SlotUI>().FunctionalItem.Name.Split("'")[0].ToLower());
                    Client.UpdateState("");
                }
            }

            if (MarioLandModConfig.Instance.DiscordRPCOn) Client?.SetPresence(Presence);

            Cooldown = 0;
        }

        public static void Deinitialize()
        {
            Client?.UpdateEndTime(DateTime.UtcNow);
            Client?.Dispose();
        }
    }

    #endregion

    public class TransformationPlayerLayer : PlayerDrawLayer
    {
        private Asset<Texture2D> headTexture;
        private Asset<Texture2D> bodyTexture;
        private Asset<Texture2D> legsTexture;

        private int headFrame;
        private int bodyFrame;
        private int legsFrame;

        public void RequestTextures(string head, string body, string legs)
        {
            headTexture = ModContent.Request<Texture2D>(head);
            bodyTexture = ModContent.Request<Texture2D>(body);
            legsTexture = ModContent.Request<Texture2D>(legs);
        }

        public void RequestFrames(int head, int body, int legs)
        {
            headFrame = head;
            bodyFrame = body;
            legsFrame = legs;
        }

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            // !Main.gameMenu && Main.LocalPlayer.GetModPlayer<MarioLandModPlayer>().TransformationVisualActive;

            Player drawPlayer = drawInfo.drawPlayer;
            return !(drawPlayer.dead || drawPlayer.invis || drawPlayer.head == -1);
        }

        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Head);

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            Vector2 position = drawInfo.Position - Main.screenPosition + new Vector2(drawPlayer.width / 2 - drawPlayer.bodyFrame.Width / 2, drawPlayer.height - drawPlayer.bodyFrame.Height + 4f) + drawPlayer.headPosition;
            Vector2 headVect = drawInfo.headVect;

            RequestTextures("MarioLandMod/Items/Transformation/TransformationItemMario", "MarioLandMod/NewTextures/Mario_Normal_Body", "MarioLandMod/NewTextures/Mario_Normal_Legs");
            RequestFrames(0, 0, 0);

            DrawData drawData = new DrawData(headTexture.Value, position.Floor() + headVect, drawPlayer.bodyFrame, Color.White, drawPlayer.headRotation, headVect, 1f, drawInfo.playerEffect, 0);
        }
    }
}