using MarioLandMod.Buffs.PowerUp;
using MarioLandMod.Buffs.Transformation;
using MarioLandMod.Dusts;
using MarioLandMod.Items.PowerUp;
using MarioLandMod.Items.Transformation;
using MarioLandMod.Items.Transformation.PowerUp.Mario;
using MarioLandMod.Items.Transformation.PowerUp.Luigi;
using MarioLandMod.Projectiles;
using MarioLandMod.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarioLandMod
{
    public class MarioLandModPlayer : ModPlayer
    {
        public float SlotUILeft;
        public float SlotUITop;

        public bool TransformationActive;
        public bool TransformationVisualActive;

        public bool TransformationActive_Mario = false;
        public bool TransformationActive_Luigi = false;

        public bool PowerUpActive;
        public bool PowerUpActive_FireFlower = false;

        public int jumpCounter = 0;
        public float jumpDamageValue;

        public float oldPlayerPositionX;
        public int spikedBoots = 1;

        public bool JustPressedUseItem = false;

        public int FireFlowerCooldownTimer = 60;
        public int BurstCount = 0;
        public int BurstCooldownTimer = 0;

        #region Character Select UI

        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            var itemList = new List<Item>();

            Item Nothing = new(ItemID.None);

            Item TransformationItem_Mario = new(ModContent.ItemType<TransformationItemMario>());
            Item FireFlower = new(ModContent.ItemType<FireFlower>());

            if (!ModContent.GetInstance<MarioLandMod>().MarioSelected && !ModContent.GetInstance<MarioLandMod>().LuigiSelected && !ModContent.GetInstance<MarioLandMod>().WarioSelected && !ModContent.GetInstance<MarioLandMod>().WaluigiSelected && !ModContent.GetInstance<MarioLandMod>().FireFlowerSelected && !ModContent.GetInstance<MarioLandMod>().SuperLeafSelected && !ModContent.GetInstance<MarioLandMod>().CapeFeatherSelected && !ModContent.GetInstance<MarioLandMod>().FrogSuitSelected)
            {
                return itemList;
            }
            else
            {
                return itemList;
            }
        }

        #endregion

        public override void ResetEffects()
        {
            TransformationVisualActive = false;
            TransformationActive_Mario = false;
            TransformationActive_Luigi = false;

            PowerUpActive_FireFlower = false;
        }

        /* public override void SetControls()
        {
            if (TransformationActive)
            {
                Player.controlHook = false;
                Player.controlMount = false;
            }
        } */

        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
        {
            if (TransformationVisualActive)
            {
                if (ModContent.GetInstance<SlotUI>().DyeItem.IsAir)
                {
                    Player.cHead = Player.cBody = Player.cLegs = 0;
                }
                else
                {
                    Player.cHead = Player.cBody = Player.cLegs = ModContent.GetInstance<SlotUI>().DyeItem.dye;
                }
            }
        }

        public int[] TransformationItems = new int[] { ModContent.ItemType<TransformationItemMario>(), ModContent.ItemType<TransformationItemLuigi>() };
        public int[] TransformationBuffs = new int[] { ModContent.BuffType<TransformationBuffMario>(), ModContent.BuffType<TransformationBuffLuigi>() };

        public int[] PowerUpItems = new int[] { ModContent.ItemType<FireFlower>() };
        public int[] PowerUpBuffs = new int[] { ModContent.BuffType<PowerUpBuffFireFlower>() };

        private void TransformationSwitch(int index, string name, bool on)
        {
            if (on)
            {
                if (!Player.HasBuff(TransformationBuffs[index]))
                {
                    for (int i = 0; i < 15; i++)
                    {
                        SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Transformation/TransformationOn"), Main.LocalPlayer.Center);
                        SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, $"Sounds/Transformation/Transformation{name}"), Main.LocalPlayer.Center);
                        Dust.NewDust(Player.position, Player.width, Player.height, ModContent.DustType<TransformationDust>());
                    }

                    Player.AddBuff(TransformationBuffs[index], 2);
                }

                if (Player.wet || Player.lavaWet || Player.lavaWet)
                {
                    if (PlayerInput.Triggers.JustPressed.Jump) SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Actions/Swim"), Main.LocalPlayer.Center);
                }
                else
                {
                    if (Player.justJumped) SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Actions/Jumps/SingleJump"), Main.LocalPlayer.Center);

                    if ((Player.legFrame.Y == 11 * 56 || Player.legFrame.Y == 18 * 56) && (Player.velocity.X > 0.5f || Player.velocity.X < -0.5f))
                    {
                        SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Actions/Walk"), Main.LocalPlayer.Center);
                    }
                }
            }
            else
            {
                if (Player.HasBuff(TransformationBuffs[index]))
                {
                    for (int i = 0; i < 15; i++)
                    {
                        SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Transformation/TransformationOff"), Main.LocalPlayer.Center);
                        Dust.NewDust(Player.position, Player.width, Player.height, ModContent.DustType<TransformationDust>());
                    }

                    Player.ClearBuff(TransformationBuffs[index]);
                }
            }
        }

        private void PowerUpSwitch(int index, bool on)
        {
            if (on)
            {
                if (!Player.HasBuff(PowerUpBuffs[index]))
                {
                    for (int i = 0; i < 15; i++)
                    {
                        SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Transformation/TransformationOn"), Main.LocalPlayer.Center);
                        Dust.NewDust(Player.position, Player.width, Player.height, ModContent.DustType<TransformationDust>());
                    }
                    Player.AddBuff(PowerUpBuffs[index], 2);
                }
            }
            else
            {
                if (Player.HasBuff(PowerUpBuffs[index]))
                {
                    for (int i = 0; i < 15; i++)
                    {
                        SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Transformation/TransformationOff"), Main.LocalPlayer.Center);
                        Dust.NewDust(Player.position, Player.width, Player.height, ModContent.DustType<TransformationDust>());
                    }

                    Player.ClearBuff(PowerUpBuffs[index]);
                }
            }
        }

        public override void PreUpdate()
        {
            TransformationActive = TransformationActive_Mario || TransformationActive_Luigi;

            PowerUpActive = PowerUpActive_FireFlower;

            /* for (int i = 0; i < Player.MaxBuffs; i++)
            {
                if (Player.buffType[i] != ModContent.BuffType<TransformationBuffMario>())
                {
                    if (Main.buffNoTimeDisplay[i])
                    {
                        Player.ClearBuff(Player.buffType[i]);
                    }
                }
            } */

            if (Player.difficulty == 3)
            {
                SlotUILeft = 162;
                SlotUITop = 258;
            }
            else
            {
                SlotUILeft = 115;
                SlotUITop = 258;
            }

            NerfedWallKick();

            TransformationSwitch(0, "Mario", TransformationActive_Mario);
            TransformationSwitch(1, "Luigi", TransformationActive_Luigi);

            PowerUpSwitch(0, PowerUpActive_FireFlower);
            FireFlowerMechanics();

            if (Player.velocity.Y == 0) jumpCounter = 0;

            if (jumpCounter <= 3) jumpDamageValue = 2 * (20f - (jumpCounter - 1) * 5);
            else jumpDamageValue = 8;
        }

        public override void FrameEffects()
        {
            if (TransformationVisualActive)
            {
                if (TransformationActive_Mario)
                {
                    if (PowerUpActive_FireFlower)
                    {
                        var DummyItemMarioFireFlower = ModContent.GetInstance<DummyItemMarioFireFlower>();

                        Player.head = Mod.GetEquipSlot(DummyItemMarioFireFlower.Name, EquipType.Head);
                        Player.body = Mod.GetEquipSlot(DummyItemMarioFireFlower.Name, EquipType.Body);
                        Player.legs = Mod.GetEquipSlot(DummyItemMarioFireFlower.Name, EquipType.Legs);
                    }
                    else
                    {
                        var TransformationItemMario = ModContent.GetInstance<TransformationItemMario>();

                        Player.head = Mod.GetEquipSlot(TransformationItemMario.Name, EquipType.Head);
                        Player.body = Mod.GetEquipSlot(TransformationItemMario.Name, EquipType.Body);
                        Player.legs = Mod.GetEquipSlot(TransformationItemMario.Name, EquipType.Legs);
                    }
                }

                if (TransformationActive_Luigi)
                {
                    if (PowerUpActive_FireFlower)
                    {
                        var DummyItemLuigiFireFlower = ModContent.GetInstance<DummyItemLuigiFireFlower>();

                        Player.head = Mod.GetEquipSlot(DummyItemLuigiFireFlower.Name, EquipType.Head);
                        Player.body = Mod.GetEquipSlot(DummyItemLuigiFireFlower.Name, EquipType.Body);
                        Player.legs = Mod.GetEquipSlot(DummyItemLuigiFireFlower.Name, EquipType.Legs);
                    }
                    else
                    {
                        var TransformationItemLuigi = ModContent.GetInstance<TransformationItemLuigi>();

                        Player.head = Mod.GetEquipSlot(TransformationItemLuigi.Name, EquipType.Head);
                        Player.body = Mod.GetEquipSlot(TransformationItemLuigi.Name, EquipType.Body);
                        Player.legs = Mod.GetEquipSlot(TransformationItemLuigi.Name, EquipType.Legs);
                    }
                }
            }
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            // Stomping mechanics

            if (damageSource.SourceNPCIndex > -1)
            {
                NPC npc = Main.npc[damageSource.SourceNPCIndex];
                if (TransformationActive)
                {
                    if (Main.myPlayer == Player.whoAmI && Player.velocity.Y > 0 && Player.Hitbox.Bottom > npc.Hitbox.Top && Math.Atan2(npc.Center.Y - Player.Center.Y, npc.Center.X - Player.Center.X) > 1 && !Player.immune)
                    {
                        Player.armorEffectDrawShadow = false;

                        if (Player.controlJump) Player.velocity.Y = -11f;
                        else Player.velocity.Y = -9f;

                        for (int d = 0; d < 3; d++) Dust.NewDust(npc.Top, Player.width, Player.height, ModContent.DustType<StompDust>());

                        if (jumpCounter < 7) SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, $"Sounds/Actions/Stomps/Stomp_{jumpCounter}"), Main.LocalPlayer.Center);
                        else
                        {
                            Player.statLife += Player.statLifeMax2 / (jumpCounter * 3);
                            Player.HealEffect(Player.statLifeMax2 / (jumpCounter * 3), true);

                            SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Actions/Stomps/StompHeal"), Main.LocalPlayer.Center);
                        }

                        Player.immune = true;
                        Player.immuneNoBlink = true;
                        Player.immuneTime = 5;
                        Player.ApplyDamageToNPC(npc, (int)jumpDamageValue * 2, 0, 0, false);
                        jumpCounter++;
                        return false;
                    }
                }
            }
            return true;
        }

        private void NerfedWallKick()
        {
            if (TransformationActive)
            {
                if (Player.velocity.Y == 0) oldPlayerPositionX = 0;

                if (Player.sliding && PlayerInput.Triggers.JustPressed.Jump) oldPlayerPositionX = Player.position.X;

                if (oldPlayerPositionX == Player.position.X || oldPlayerPositionX == Player.position.X) spikedBoots = 0;
                else spikedBoots = 1;
            }
        }

        private void FireFlowerMechanics()
        {
            if (!Main.mouseLeft) JustPressedUseItem = false;

            if (BurstCount >= 2) FireFlowerCooldownTimer--;
            else BurstCooldownTimer++;

            if (FireFlowerCooldownTimer <= 0 || BurstCooldownTimer >= 15)
            {
                BurstCount = 0;
                FireFlowerCooldownTimer = 100;
            }

            if (PowerUpActive_FireFlower && Main.mouseLeft && Player.HeldItem.IsAir && BurstCount <= 2 && FireFlowerCooldownTimer == 100 && !Player.mouseInterface)
            {
                if (!JustPressedUseItem)
                {
                    SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/PowerUps/FireFlowerFireball"), Main.LocalPlayer.Center);
                    Projectile.NewProjectile(Player.GetProjectileSource_Buff(Player.FindBuffIndex(ModContent.BuffType<PowerUpBuffFireFlower>())), Player.Center, new Vector2(5f * Player.direction, default), ModContent.ProjectileType<FireFlowerProjectile>(), 10, 2.5f, Player.whoAmI);

                    JustPressedUseItem = true;
                    BurstCount++;
                    BurstCooldownTimer = 0;
                }
            }
        }
    }
}