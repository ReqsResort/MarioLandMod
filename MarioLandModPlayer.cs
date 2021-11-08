using MarioLandMod.Buffs.PowerUp;
using MarioLandMod.Buffs.Transformation;
using MarioLandMod.Dusts;
using MarioLandMod.Items.PowerUp;
using MarioLandMod.Items.Transformation;
using MarioLandMod.Items.Transformation.PowerUp;
using MarioLandMod.UI;
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

            PowerUpActive_FireFlower = SlotUI.PowerupSlot.Item.type == ModContent.ItemType<FireFlower>();
            PowerUpActive = PowerUpActive_FireFlower;

            if (TransformationActive)
            {
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

                if (TransformationActive_Mario)
                {
                    TransformationSwitch(0, "Mario", true);

                    if (PowerUpActive_FireFlower)
                    {
                        PowerupSwitch(0, true);
                    }
                    else
                    {
                        PowerupSwitch(0, false);
                    }
                }
                else
                {
                    TransformationSwitch(0, "Mario", false);
                }
            }
            else
            {
                TransformationSwitch(0, "Mario", false);
            }
        }

        public override void FrameEffects()
        {
            if (TransformationActive_Mario)
            {
                if (PowerUpActive_FireFlower)
                {
                    var DummyHatMarioFireFlower = ModContent.GetInstance<DummyHatMarioFireFlower>();

                    Player.head = Mod.GetEquipSlot(DummyHatMarioFireFlower.Name, EquipType.Head);
                    Player.body = Mod.GetEquipSlot(DummyHatMarioFireFlower.Name, EquipType.Body);
                    Player.legs = Mod.GetEquipSlot(DummyHatMarioFireFlower.Name, EquipType.Legs);
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
    }
}
