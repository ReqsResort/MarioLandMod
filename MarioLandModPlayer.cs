using MarioLandMod.Buffs.PowerUp;
using MarioLandMod.Buffs.Transformation;
using MarioLandMod.Dusts;
using MarioLandMod.Items.PowerUp;
using MarioLandMod.Items.Transformation;
using MarioLandMod.Items.Transformation.PowerUp;
using MarioLandMod.Projectiles;
using MarioLandMod.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace MarioLandMod
{
    public class MarioLandModPlayer : ModPlayer
    {
        public bool TransformationActive;
        public bool TransformationActive_Mario;

        public bool PowerUpActive;
        public bool PowerUpActive_FireFlower;

        public int jumpCounter = 0;
        public int jumpCooldown;
        public float jumpDamageValue;
        public float jumpKnockbackValue;

        public bool JustPressedUseItem = false;

        public int FireFlowerCooldownTimer = 60;
        public int BurstCount = 0;
        public int BurstCooldownTimer = 0;

        #region Slot UI

        public override void SaveData(TagCompound tag)
        {
            tag["TransformationItem"] = SlotUI.TransformationSlot.Item;
            tag["PowerupItem"] = SlotUI.PowerupSlot.Item;
            tag["DyeItem"] = SlotUI.DyeSlot.Item;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("TransformationItem"))
            {
                SlotUI.TransformationSlot.Item = tag.Get<Item>("TransformationItem");
            }

            if (tag.ContainsKey("PowerupItem"))
            {
                SlotUI.PowerupSlot.Item = tag.Get<Item>("PowerupItem");
            }

            if (tag.ContainsKey("DyeItem"))
            {
                SlotUI.DyeSlot.Item = tag.Get<Item>("DyeItem");
            }
        }

        #endregion

        #region Character Select UI

        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            Item Nothing = new(ItemID.None);

            Item TransformationItem_Mario = new(ModContent.ItemType<TransformationItemMario>());

            if (ModContent.GetInstance<MarioLandMod>().MarioSelected)
            {
                return new List<Item>() { TransformationItem_Mario };
            }
            else
            {
                return new List<Item>() { Nothing };
            }
        }

        #endregion

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
            if (TransformationActive)
            {
                if (SlotUI.DyeSlot.Item.IsAir)
                {
                    Player.cHead = Player.cBody = Player.cLegs = 0;
                }
                else
                {
                    Player.cHead = Player.cBody = Player.cLegs = SlotUI.DyeSlot.Item.dye;
                }
            }
        }

        public int[] TransformationItems = new int[] { ModContent.ItemType<TransformationItemMario>() };
        public int[] TransformationBuffs = new int[] { ModContent.BuffType<TransformationBuffMario>() };

        public int[] PowerUpItems = new int[] { ModContent.ItemType<FireFlower>() };
        public int[] PowerUpBuffs = new int[] { ModContent.BuffType<PowerupBuffFireFlower>() };

        private void TransformationSwitch(int index, string name, bool on)
        {
            if (on)
            {
                if (!Player.HasBuff(TransformationBuffs[index]))
                {
                    for (int i = 0; i < 15; i++)
                    {
                        SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Custom/Transformation/TransformationOn"), Main.LocalPlayer.Center);
                        SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, $"Sounds/Custom/Transformation/Transformation{name}"), Main.LocalPlayer.Center);
                        Dust.NewDust(Player.position, Player.width, Player.height, ModContent.DustType<TransformationDust>());
                    }

                    Player.AddBuff(TransformationBuffs[index], 2);
                }
            }
            else
            {
                if (Player.HasBuff(TransformationBuffs[index]))
                {
                    for (int i = 0; i < 15; i++)
                    {
                        SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Custom/Transformation/TransformationOff"), Main.LocalPlayer.Center);
                        Dust.NewDust(Player.position, Player.width, Player.height, ModContent.DustType<TransformationDust>());
                    }

                    Player.ClearBuff(TransformationBuffs[index]);
                }
            }
        }

        private void PowerupSwitch(int index, bool on)
        {
            if (on)
            {
                if (!Player.HasBuff(PowerUpBuffs[index]))
                {
                    for (int i = 0; i < 15; i++)
                    {
                        SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Custom/Transformation/TransformationOn"), Main.LocalPlayer.Center);
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
                        SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Custom/Transformation/TransformationOff"), Main.LocalPlayer.Center);
                        Dust.NewDust(Player.position, Player.width, Player.height, ModContent.DustType<TransformationDust>());
                    }

                    Player.ClearBuff(PowerUpBuffs[index]);
                }
            }
        }

        public override void PreUpdate()
        {
            TransformationActive_Mario = SlotUI.TransformationSlot.Item.type == ModContent.ItemType<TransformationItemMario>();
            TransformationActive = TransformationActive_Mario;

            PowerUpActive_FireFlower = (TransformationActive_Mario) && SlotUI.PowerupSlot.Item.type == ModContent.ItemType<FireFlower>();
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

            TransformationSwitch(0, "Mario", TransformationActive_Mario);
            PowerupSwitch(0, PowerUpActive_FireFlower);

            if (TransformationActive)
            {
                JumpAndStompMechanics();
            }

            FireFlowerMechanics();
        }

        public override void FrameEffects()
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
        }

        private void JumpAndStompMechanics()
        {
            Player.armorEffectDrawShadow = false;

            if (jumpCooldown > 0)
            {
                jumpCooldown--;
            }

            if (Player.velocity.Y == 0)
            {
                jumpCounter = 0;
                Player.fullRotation = 0;
            }

            for (int i = 0; i < 200; i++)
            {
                NPC npc = Main.npc[i];

                if (Collision.CanHit(Player.position, Player.width, Player.height, npc.position, npc.width, npc.height) && Player.Hitbox.Bottom > npc.Hitbox.Top && Player.velocity.Y > 0f && npc.active && !npc.townNPC && Math.Atan2(npc.Center.Y - Player.Center.Y, npc.Center.X - Player.Center.X) > 1 && Math.Atan2(npc.Center.Y - Player.Center.Y, npc.Center.X - Player.Center.X) < 2)
                {
                    Player.fullRotation = 0;

                    if (!Player.controlDown)
                    {
                        jumpKnockbackValue = 2.5f;
                    }
                    else
                    {
                        jumpKnockbackValue = 7.5f;
                    }

                    if (jumpCounter <= 3)
                    {
                        if (!Player.controlDown)
                        {
                            jumpDamageValue = (20f - (jumpCounter - 1) * 5);
                        }
                        else
                        {
                            jumpDamageValue = 2 * (20f - (jumpCounter - 1) * 5);
                        }
                    }
                    else
                    {
                        jumpDamageValue = 8;
                    }

                    if (!Player.controlDown)
                    {
                        if (Player.controlJump)
                        {
                            Player.velocity.Y = -8f;
                        }
                        else
                        {
                            Player.velocity.Y = -6f;
                        }
                    }

                    if (jumpCooldown <= 0)
                    {
                        if ((!(Player.controlDown && Player.controlJump)))
                        {
                            Player.immune = true;
                            Player.immuneNoBlink = true;
                            Player.immuneTime = 10;
                            Player.ApplyDamageToNPC(npc, (int)jumpDamageValue, jumpKnockbackValue, Player.direction, false);

                            for (int d = 0; d < 3; d++)
                            {
                                Dust.NewDust(npc.Top, Player.width, Player.height, ModContent.DustType<StompDust>());
                            }

                            if (jumpCounter < 7)
                            {
                                SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, $"Sounds/Custom/Actions/Stomps/Stomp_{jumpCounter}"), Main.LocalPlayer.Center);
                            }
                            else
                            {
                                Player.statLife += Player.statLifeMax2 / (jumpCounter * 3);
                                Player.HealEffect(Player.statLifeMax2 / (jumpCounter * 3), true);

                                SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Custom/Actions/Stomps/StompHeal"), Main.LocalPlayer.Center);

                            }

                            jumpCounter++;
                            jumpCooldown = 28;
                        }
                    }
                }
            }
        }

        private void FireFlowerMechanics()
        {
            if (!Main.mouseLeft) JustPressedUseItem = false;

            if (BurstCount >= 3)
            {
                FireFlowerCooldownTimer--;
            }
            else
            {
                BurstCooldownTimer++;
            }

            if (FireFlowerCooldownTimer <= 0 || BurstCooldownTimer >= 15)
            {
                BurstCount = 0;
                FireFlowerCooldownTimer = 60;
            }

            if (PowerUpActive_FireFlower && Main.mouseLeft && Player.HeldItem.IsAir && BurstCount <= 3 && FireFlowerCooldownTimer == 60)
            {
                if (!JustPressedUseItem)
                {
                    SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Custom/PowerUps/FireFlowerFireball"), Main.LocalPlayer.Center);
                    Projectile.NewProjectile(Player.GetProjectileSource_Buff(Player.FindBuffIndex(ModContent.BuffType<PowerupBuffFireFlower>())), Player.Center, new Vector2(5f * Player.direction, default), ModContent.ProjectileType<FireFlowerProjectile>(), 10, 2.5f, Player.whoAmI);

                    JustPressedUseItem = true;
                    BurstCount++;
                    BurstCooldownTimer = 0;
                }
            }
        }
    }
}