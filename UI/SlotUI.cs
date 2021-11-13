using Terraria.UI;
using Terraria;
using MarioLandMod.UI.Elements;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using MarioLandMod.Items.Transformation;
using Terraria.ID;
using MarioLandMod.Items.PowerUp;

namespace MarioLandMod.UI
{
    public class SlotUI : UIState
    {
        public static bool Visible
        {
            get => Main.playerInventory;
        }

        public TexturedCustomItemSlot TransformationSlot;
        public TexturedCustomItemSlot PowerupSlot;
        public CustomItemSlot DyeSlot;

        public override void OnInitialize()
        {
            TransformationSlot = new TexturedCustomItemSlot("MarioLandMod/UI/Textures/SlotUI/TransformationSlotEmptyTexture", "Transformation", ItemSlot.Context.BankItem, 0.85f)
            {
                ValidItemFunc = item => item.IsAir || !item.IsAir && (item.type == ModContent.ItemType<TransformationItemMario>())
            };

            PowerupSlot = new TexturedCustomItemSlot("MarioLandMod/UI/Textures/SlotUI/PowerupSlotEmptyTexture", "Powerup", ItemSlot.Context.BankItem, 0.85f)
            {
                ValidItemFunc = item => item.IsAir || !item.IsAir && (item.type == ModContent.ItemType<FireFlower>())
            };

            DyeSlot = new CustomItemSlot("Transformation Dye", ItemSlot.Context.EquipDye, 0.85f)
            {
                ValidItemFunc = item => item.IsAir || !item.IsAir && (item.type == ItemID.RedDye || item.type == ItemID.OrangeDye || item.type == ItemID.YellowDye || item.type == ItemID.LimeDye || item.type == ItemID.GreenDye || item.type == ItemID.TealDye || item.type == ItemID.CyanDye || item.type == ItemID.SkyBlueDye || item.type == ItemID.BlueDye || item.type == ItemID.PurpleDye || item.type == ItemID.VioletDye || item.type == ItemID.PinkDye || item.type == ItemID.BlackDye || item.type == ItemID.BrownDye || item.type == ItemID.SilverDye || item.type == ItemID.BrightRedDye || item.type == ItemID.BrightOrangeDye || item.type == ItemID.BrightYellowDye || item.type == ItemID.BrightLimeDye || item.type == ItemID.BrightGreenDye || item.type == ItemID.BrightTealDye || item.type == ItemID.BrightCyanDye || item.type == ItemID.BrightSkyBlueDye || item.type == ItemID.BrightBlueDye || item.type == ItemID.BrightPurpleDye || item.type == ItemID.BrightVioletDye || item.type == ItemID.BrightPinkDye || item.type == ItemID.BrightBrownDye || item.type == ItemID.BrightSilverDye || item.type == ItemID.FlameDye || item.type == ItemID.GreenFlameDye || item.type == ItemID.BlueFlameDye || item.type == ItemID.YellowGradientDye || item.type == ItemID.CyanGradientDye || item.type == ItemID.VioletGradientDye || item.type == ItemID.RainbowDye || item.type == ItemID.IntenseFlameDye || item.type == ItemID.IntenseGreenFlameDye || item.type == ItemID.IntenseBlueFlameDye || item.type == ItemID.IntenseRainbowDye || item.type == ItemID.RedandBlackDye || item.type == ItemID.OrangeandBlackDye || item.type == ItemID.YellowandBlackDye || item.type == ItemID.LimeandBlackDye || item.type == ItemID.GreenandBlackDye || item.type == ItemID.TealandBlackDye || item.type == ItemID.CyanandBlackDye || item.type == ItemID.SkyBlueandBlackDye || item.type == ItemID.BlueandBlackDye || item.type == ItemID.PurpleandBlackDye || item.type == ItemID.VioletandBlackDye || item.type == ItemID.PinkandBlackDye || item.type == ItemID.BrownAndBlackDye || item.type == ItemID.SilverAndBlackDye || item.type == ItemID.FlameAndBlackDye || item.type == ItemID.GreenFlameAndBlackDye || item.type == ItemID.BlueFlameAndBlackDye || item.type == ItemID.RedandSilverDye || item.type == ItemID.OrangeandSilverDye || item.type == ItemID.YellowandSilverDye || item.type == ItemID.LimeandSilverDye || item.type == ItemID.GreenandSilverDye || item.type == ItemID.TealandSilverDye || item.type == ItemID.CyanandSilverDye || item.type == ItemID.SkyBlueandSilverDye || item.type == ItemID.BlueandSilverDye || item.type == ItemID.PurpleandSilverDye || item.type == ItemID.VioletandSilverDye || item.type == ItemID.PinkandSilverDye || item.type == ItemID.BrownAndSilverDye || item.type == ItemID.BlackAndWhiteDye || item.type == ItemID.FlameAndSilverDye || item.type == ItemID.GreenFlameAndSilverDye || item.type == ItemID.BlueFlameAndSilverDye || item.type == ItemID.AcidDye || item.type == ItemID.BlueAcidDye || item.type == ItemID.RedAcidDye || item.type == ItemID.ChlorophyteDye || item.type == ItemID.GelDye || item.type == ItemID.MushroomDye || item.type == ItemID.GrimDye || item.type == ItemID.HadesDye || item.type == ItemID.BurningHadesDye || item.type == ItemID.ShadowflameHadesDye || item.type == ItemID.LivingOceanDye || item.type == ItemID.LivingFlameDye || item.type == ItemID.LivingRainbowDye || item.type == ItemID.MartianArmorDye || item.type == ItemID.MidnightRainbowDye || item.type == ItemID.MirageDye || item.type == ItemID.NegativeDye || item.type == ItemID.PixieDye || item.type == ItemID.PhaseDye || item.type == ItemID.PurpleOozeDye || item.type == ItemID.ReflectiveDye || item.type == ItemID.ReflectiveCopperDye || item.type == ItemID.ReflectiveGoldDye || item.type == ItemID.ReflectiveObsidianDye || item.type == ItemID.ReflectiveMetalDye || item.type == ItemID.ReflectiveSilverDye || item.type == ItemID.ShadowDye || item.type == ItemID.ShiftingSandsDye || item.type == ItemID.DevDye || item.type == ItemID.TwilightDye || item.type == ItemID.WispDye || item.type == ItemID.InfernalWispDye || item.type == ItemID.UnicornWispDye || item.type == ItemID.NebulaDye || item.type == ItemID.SolarDye || item.type == ItemID.StardustDye || item.type == ItemID.VortexDye || item.type == ItemID.LokisDye || item.type == ItemID.PinkGelDye || item.type == ItemID.ShiftingPearlSandsDye || item.type == ItemID.TeamDye || item.type == ItemID.BloodbathDye || item.type == ItemID.FogboundDye || item.type == ItemID.HallowBossDye || item.type == ItemID.VoidDye || item.type == ItemID.ColorOnlyDye)
            };

            TransformationSlot.MarginTop = PowerupSlot.MarginTop = DyeSlot.MarginTop = 258;

            Append(TransformationSlot);
            Append(PowerupSlot);
            Append(DyeSlot);
        }

        public override void Update(GameTime gameTime)
        {
            Player player = Main.LocalPlayer;

            #region Slot Alignment

            if (player.chest != -1)
            {
                TransformationSlot.MarginLeft = 20;
                PowerupSlot.MarginLeft = 20;
                DyeSlot.MarginLeft = 20;

                PowerupSlot.MarginTop = 305.8f;
                DyeSlot.MarginTop = 353.6f;
            }
            else
            {
                if (Main.GameMode == 3)
                {
                    TransformationSlot.MarginLeft = 67;
                    PowerupSlot.MarginLeft = 115;
                    DyeSlot.MarginLeft = 162;

                    TransformationSlot.MarginTop = PowerupSlot.MarginTop = DyeSlot.MarginTop = 258;
                }
                else
                {
                    TransformationSlot.MarginLeft = 20;
                    PowerupSlot.MarginLeft = 67;
                    DyeSlot.MarginLeft = 115;

                    TransformationSlot.MarginTop = PowerupSlot.MarginTop = DyeSlot.MarginTop = 258;
                }
            }

            #endregion
        }
    }
}
