using DiscordRPC;
using Steamworks;
using MarioLandMod.UI;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using System.ComponentModel;
using Terraria.ID;

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
        private static Button button;
        private static DiscordRpcClient Client;

        public static void Initialize()
        {
            button = new Button
            {
                Label = "Mod Homepage (Discord)",
                Url = "https://discord.gg/FdkEw35fye"
            };

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

            if (SteamUser.GetSteamID().ToString() == "76561198855979235")
            {
                Presence.Buttons = new Button[] { button };
            }

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
                    Client.UpdateDetails($"Running around as {MarioLandModSystem.SlotUIInstance.TransformationSlot.Item.Name.Split("'")[0]}");
                    Client.UpdateSmallAsset(MarioLandModSystem.SlotUIInstance.TransformationSlot.Item.Name.Split("'")[0].ToLower());
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
}