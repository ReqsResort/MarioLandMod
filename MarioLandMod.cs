using MarioLandMod.Items.Transformation;
using MarioLandMod.Items.Transformation.PowerUp;
using MarioLandMod.UI;
using MarioLandMod.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using ReLogic.Content;
using System;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.ModLoader;
using Terraria.UI;

namespace MarioLandMod
{
    public class MarioLandMod : Mod
    {
        #region Transformation Selection UI

        int UIIndex;

        bool MarioLandModCategorySelected;

        UIElement MarioLandModCategoryContainer;
        UIColoredImageButton marioLandModCategoryButton;

        string[] Transformations = new string[] { "Mario", "Luigi", "Wario", "Waluigi" };
        string[] PowerUps = new string[] { "Fire Flower", "Super Leaf", "Cape Feather", "Frog Suit", "Jet Pot", "Jet Pot", "Jet Pot", "Jet Pot" };

        TransformationSelectButton TransformationButton;
        readonly TransformationSelectButton[] TransformationButtons = new TransformationSelectButton[4];

        TransformationSelectButton PowerUpButton;
        readonly TransformationSelectButton[] PowerUpButtons = new TransformationSelectButton[12];

        public bool MarioSelected = false;
        public bool LuigiSelected = false;
        public bool WarioSelected = false;
        public bool WaluigiSelected = false;

        public bool FireFlowerSelected = false;
        public bool SuperLeafSelected = false;
        public bool CapeFeatherSelected = false;
        public bool FrogSuitSelected = false;
        public bool JetPotSelected = false;
        public bool BullPotSelected = false;
        public bool DragonPotSelected = false;
        public bool GarlicBundleSelected = false;

        TransformationInfoPanel TransformationNamePanel;
        TransformationInfoPanel PowerUpNamePanel;

        UIText SelectedTransformationInfoText;

        private void MakeMarioLandModCategory(UIElement container)
        {
            MarioSelected = false;
            LuigiSelected = false;
            WarioSelected = false;
            WaluigiSelected = false;

            FireFlowerSelected = false;
            SuperLeafSelected = false;
            CapeFeatherSelected = false;
            FrogSuitSelected = false;
            JetPotSelected = false;
            BullPotSelected = false;
            DragonPotSelected = false;
            GarlicBundleSelected = false;

            UIElement uIElement = new()
            {
                Width = StyleDimension.FromPixelsAndPercent(-10f, 1f),
                Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
                HAlign = 0.5f,
                VAlign = 0.5f
            };

            container.Append(uIElement);
            MarioLandModCategoryContainer = uIElement;

            for (int i = 0; i < 4; i++)
            {
                TransformationButton = new($"MarioLandMod/UI/Textures/TransformationSelectionUI/TransformationButtonTextures/TransformationButtonTexture{Transformations[i].Replace(" ", string.Empty)}")
                {
                    Left = StyleDimension.FromPercent(((float)i / 11) + 0.01f)
                };
                TransformationButton.OnClick += TransformationButton_OnClick;
                TransformationButton.OnMouseOver += TransformationButton_OnMouseOver;
                TransformationButton.OnMouseOut += TransformationButton_OnMouseOut;

                MarioLandModCategoryContainer.Append(TransformationButton);

                TransformationButtons[i] = TransformationButton;
            }

            for (int i = 0; i < 8; i++)
            {
                PowerUpButton = new($"MarioLandMod/UI/Textures/TransformationSelectionUI/PowerUpButtonTextures/TransformationButtonTexture{PowerUps[i].Replace(" ", string.Empty)}")
                {
                    Left = StyleDimension.FromPercent((float)i / 11 + 0.01f),
                    Top = StyleDimension.FromPixels(47f)
                };

                PowerUpButton.OnClick += PowerUpButton_OnClick; ;
                PowerUpButton.OnMouseOver += TransformationButton_OnMouseOver;
                PowerUpButton.OnMouseOut += TransformationButton_OnMouseOut;

                PowerUpButtons[i] = PowerUpButton;
            }

            for (int i = 0; i < 4; i++)
            {
                PowerUpButtons[i + 4].Left = StyleDimension.FromPercent((float)i / 11 + 0.01f);
                MarioLandModCategoryContainer.Append(PowerUpButtons[i]);
            }

            TransformationNamePanel = new("No transformation selected", new Color(99, 107, 153))
            {
                Left = StyleDimension.FromPercent(0.01f),
                Top = StyleDimension.FromPixels(96f)
            };
            MarioLandModCategoryContainer.Append(TransformationNamePanel);

            PowerUpNamePanel = new("No PowerUp selected", new Color(99, 107, 153))
            {
                Left = StyleDimension.FromPercent(0.01f),
                Top = StyleDimension.FromPixels(128f)
            };
            MarioLandModCategoryContainer.Append(PowerUpNamePanel);

            UIPanel SelectedTransformationPanel = new()
            {
                Width = StyleDimension.FromPercent(0.61f),
                Height = StyleDimension.FromPercent(1f),
                Left = StyleDimension.FromPercent(0.375f),
                BackgroundColor = new Color(99, 107, 153),
                BorderColor = new Color(99, 107, 153)
            };

            MarioLandModCategoryContainer.Append(SelectedTransformationPanel);

            SelectedTransformationInfoText = new("Select a transformation and / or PowerUp to begin with on your journey, if you want.\n(You need to have a transformation selected in order to select a PowerUp)", 0.9f, false)
            {
                HAlign = 0f,
                VAlign = 0.5f,
                PaddingLeft = 10f,
                PaddingRight = 10f,
                Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
                Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
                Top = StyleDimension.FromPixelsAndPercent(15f, 0f),
                IsWrapped = true
            };

            SelectedTransformationPanel.Append(SelectedTransformationInfoText);
        }

        private void TransformationButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(12);

            for (int i = 0; i < 4; i++)
            {
                if (listeningElement == TransformationButtons[i])
                {
                    TransformationButtons[i].SetClicked(!TransformationButtons[i].Clicked);

                    if (i == 0) MarioSelected = TransformationButtons[i].Clicked;
                    if (i == 1) LuigiSelected = TransformationButtons[i].Clicked;
                    if (i == 2) WarioSelected = TransformationButtons[i].Clicked;
                    if (i == 3) WaluigiSelected = TransformationButtons[i].Clicked;
                }
                else
                {
                    TransformationButtons[i].SetClicked(false);
                    if (i == 0) MarioSelected = false;
                    if (i == 1) LuigiSelected = false;
                    if (i == 2) WarioSelected = false;
                    if (i == 3) WaluigiSelected = false;
                }
            }

            if (MarioSelected || LuigiSelected)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (i < 4) MarioLandModCategoryContainer.Append(PowerUpButtons[i]);
                    else PowerUpButtons[i].Remove();
                }

                JetPotSelected = false;
                BullPotSelected = false;
                DragonPotSelected = false;
                GarlicBundleSelected = false;

                for (int i = 0; i < 4; i++)
                {
                    PowerUpButtons[i + 4].SetClicked(false);
                }
            }

            if (WarioSelected)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (i < 4) PowerUpButtons[i].Remove();
                    else MarioLandModCategoryContainer.Append(PowerUpButtons[i]);
                }

                FireFlowerSelected = false;
                SuperLeafSelected = false;
                CapeFeatherSelected = false;
                FrogSuitSelected = false;

                for (int i = 0; i < 4; i++)
                {
                    PowerUpButtons[i].SetClicked(false);
                }
            }

            if (!MarioSelected && !LuigiSelected && !WarioSelected && !WaluigiSelected)
            {
                FireFlowerSelected = false;
                SuperLeafSelected = false;
                CapeFeatherSelected = false;
                FrogSuitSelected = false;
                JetPotSelected = false;
                BullPotSelected = false;
                DragonPotSelected = false;
                GarlicBundleSelected = false;

                for (int i = 0; i < 8; i++)
                {
                    PowerUpButtons[i].SetClicked(false);
                }
            }
        }

        private void PowerUpButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(12);

            if (MarioSelected || LuigiSelected || WarioSelected || WaluigiSelected)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (listeningElement == PowerUpButtons[i])
                    {
                        PowerUpButtons[i].SetClicked(!PowerUpButtons[i].Clicked);

                        if (i == 0) FireFlowerSelected = PowerUpButtons[i].Clicked;
                        if (i == 1) SuperLeafSelected = PowerUpButtons[i].Clicked;
                        if (i == 2) CapeFeatherSelected = PowerUpButtons[i].Clicked;
                        if (i == 3) FrogSuitSelected = PowerUpButtons[i].Clicked;
                        if (i == 4) JetPotSelected = PowerUpButtons[i].Clicked;
                        if (i == 5) BullPotSelected = PowerUpButtons[i].Clicked;
                        if (i == 6) DragonPotSelected = PowerUpButtons[i].Clicked;
                        if (i == 7) GarlicBundleSelected = PowerUpButtons[i].Clicked;
                    }
                    else
                    {
                        PowerUpButtons[i].SetClicked(false);

                        if (i == 0) FireFlowerSelected = false;
                        if (i == 1) SuperLeafSelected = false;
                        if (i == 2) CapeFeatherSelected = false;
                        if (i == 3) FrogSuitSelected = false;
                        if (i == 4) JetPotSelected = false;
                        if (i == 5) BullPotSelected = false;
                        if (i == 6) DragonPotSelected = false;
                        if (i == 7) GarlicBundleSelected = false;
                    }
                }
            }
        }

        private void TransformationButton_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
        {
            for (int i = 0; i < 4; i++)
            {
                TransformationButtons[i].SetHovering(listeningElement == TransformationButtons[i]);
            }

            for (int i = 0; i < 8; i++)
            {
                PowerUpButtons[i].SetHovering(listeningElement == PowerUpButtons[i]);
            }
        }

        private void TransformationButton_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
        {
            for (int i = 0; i < 4; i++)
            {
                TransformationButtons[i].SetHovering(false);
            }

            for (int i = 0; i < 8; i++)
            {
                PowerUpButtons[i].SetHovering(false);
            }
        }

        private void Click_MarioLandModCategory(UICharacterCreation self)
        {
            SoundEngine.PlaySound(12);
            Type A = typeof(UICharacterCreation);
            A.GetMethod("UnselectAllCategories", BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke(self, null);
            A.GetField("_selectedPicker", BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(self, Enum.ToObject(typeof(UICharacterCreation).GetNestedType("CategoryId", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static), UIIndex));
            (A.GetField("_middleContainer", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(self) as UIElement)
                .Append(MarioLandModCategoryContainer);
            marioLandModCategoryButton.SetSelected(selected: true);
            MarioLandModCategorySelected = true;
        }

        private void BuildPage(ILContext il)
        {
            ILCursor c = new(il);
            if (!c.TryGotoNext(i => i.MatchCall(typeof(StyleDimension).GetMethod("FromPixels"))))
            {
                throw new Exception("OSDHGSD");
            }
            c.Emit(OpCodes.Ldc_R4, 50f);
            c.Emit(OpCodes.Add);
            if (!c.TryGotoNext(i => i.MatchCallOrCallvirt(typeof(UICharacterCreation).GetMethod("MakeCategoriesBar", BindingFlags.NonPublic | BindingFlags.Instance))))
            {
                throw new Exception("AAAAAAAAAAAAAAAAALKJDGPVOS");
            }
            c.Index++;
            c.Emit(OpCodes.Ldloc, 4);
            c.EmitDelegate<Action<UIElement>>(container => MakeMarioLandModCategory(container));
        }

        private void MakeCategoriesBar(ILContext il)
        {
            ILCursor c = new(il);
            if (!c.TryGotoNext(i => i.MatchStloc(0)))
            {
            }
            c.Emit(OpCodes.Ldc_R4, 24f);
            c.Emit(OpCodes.Sub);
            if (!c.TryGotoNext(i => i.MatchNewarr(typeof(UIColoredImageButton))))
            {
            }
            c.Emit(OpCodes.Dup);
            c.EmitDelegate<Action<int>>(index => UIIndex = index);
            c.Emit(OpCodes.Ldc_I4_1);
            c.Emit(OpCodes.Add);

            while (c.TryGotoNext(i => i.MatchCallOrCallvirt(typeof(UIElement).GetMethod(nameof(UIElement.Append))))) ;
            c.Index++;
            c.Emit(OpCodes.Ldarg_0);
            c.Emit(OpCodes.Ldarg_1);
            c.Emit(OpCodes.Ldloc_0);
            c.Emit(OpCodes.Ldloc_1);
            c.EmitDelegate<Action<UICharacterCreation, UIElement, float, float>>((button, categoryContainer, xPositionStart, xPositionPerId) =>
            {

                categoryContainer.Append(marioLandModCategoryButton = CreateColorPicker3(button, 10, ModContent.Request<Texture2D>("MarioLandMod/Items/Transformation/TransformationItemMario"), xPositionStart, xPositionPerId));
            });
        }

        private void MakeClothStylesMenu(ILContext il)
        {
            ILCursor c = new(il);
            if (!c.TryGotoNext(i => i.MatchLdcI4(20)))
            {
                // il failed
            }
            c.Index++;
            c.Emit(OpCodes.Ldc_I4, 48);
            c.Emit(OpCodes.Add);
            if (!c.TryGotoNext(i => i.MatchLdcI4(20)))
            {
                // il failed
            }
            c.Index++;
            c.Emit(OpCodes.Ldc_I4, 48);
            c.Emit(OpCodes.Add);
        }

        private void MakeHairsylesMenu(ILContext il)
        {
            ILCursor c = new(il);
            if (!c.TryGotoNext(i => i.MatchNewobj(typeof(UIHairStyleButton).GetConstructor(new Type[] { typeof(Player), typeof(int) }))))
            {
            }
            if (!c.TryGotoNext(i => i.MatchLdcI4(10)))
            {
                // il failed
            }
            c.Index++;
            c.Emit(OpCodes.Ldc_I4_1);
            c.Emit(OpCodes.Add);
            if (!c.TryGotoNext(i => i.MatchLdcI4(10)))
            {
                // il failed
            }
            c.Index++;
            c.Emit(OpCodes.Ldc_I4_1);
            c.Emit(OpCodes.Add);
        }

        private void UnselectAllCategories(On.Terraria.GameContent.UI.States.UICharacterCreation.orig_UnselectAllCategories orig, UICharacterCreation self)
        {
            orig(self);
            marioLandModCategoryButton.SetSelected(false);
            MarioLandModCategoryContainer.Remove();
            MarioLandModCategorySelected = false;
        }

        private static UIColoredImageButton CreateColorPicker3(UICharacterCreation uicharCreation, int id, Asset<Texture2D> asset, float xPositionStart, float xPositionPerId)
        {
            UIColoredImageButton uIColoredImageButton = new(asset);
            (typeof(UICharacterCreation).GetField("_colorPickers", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(uicharCreation) as UIColoredImageButton[])[(int)id] = uIColoredImageButton;
            uIColoredImageButton.VAlign = 0f;
            uIColoredImageButton.HAlign = 0f;
            uIColoredImageButton.Left.Set(xPositionStart + (float)id * xPositionPerId, 0.5f);
            uIColoredImageButton.OnMouseDown += (UIElement.MouseEvent)typeof(UICharacterCreation).GetMethod("Click_ColorPicker", BindingFlags.NonPublic | BindingFlags.Instance).CreateDelegate(typeof(UIElement.MouseEvent), uicharCreation);
            uIColoredImageButton.SetSnapPoint("Top", (int)id);
            return uIColoredImageButton;
        }

        private void SelectColorPicker(ILContext il)
        {
            ILCursor c = new(il);
            if (!c.TryGotoNext(i => i.MatchStfld(out _)))
            {
            }
            c.Index++;
            c.Emit(OpCodes.Ldarg_0);
            c.Emit(OpCodes.Ldarg_1);
            c.EmitDelegate<Func<UICharacterCreation, int, bool>>((self, categoryId) =>
            {
                if (categoryId == UIIndex)
                {
                    Click_MarioLandModCategory(self);
                    return true;
                }
                return false;
            });
            var statementFalse = c.DefineLabel();
            c.Emit(OpCodes.Brfalse, statementFalse);
            c.Emit(OpCodes.Ret);
            c.MarkLabel(statementFalse);
        }

        private void UICharacterCreation_Draw(On.Terraria.GameContent.UI.States.UICharacterCreation.orig_Draw orig, UICharacterCreation self, SpriteBatch spriteBatch)
        {
            if (MarioLandModCategorySelected)
            {
                if (MarioSelected)
                {
                    TransformationNamePanel.SetText("Mario selected");
                    TransformationNamePanel.SetColour(new Color(255, 0, 0));

                    if (FireFlowerSelected)
                    {
                        var DummyItemMarioFireFlower = ModContent.GetInstance<DummyItemMarioFireFlower>();

                        Main.PendingPlayer.head = GetEquipSlot(DummyItemMarioFireFlower.Name, EquipType.Head);
                        Main.PendingPlayer.body = GetEquipSlot(DummyItemMarioFireFlower.Name, EquipType.Body);
                        Main.PendingPlayer.legs = GetEquipSlot(DummyItemMarioFireFlower.Name, EquipType.Legs);
                    }
                    else
                    {
                        var TransformationItemMario = ModContent.GetInstance<TransformationItemMario>();

                        Main.PendingPlayer.head = GetEquipSlot(TransformationItemMario.Name, EquipType.Head);
                        Main.PendingPlayer.body = GetEquipSlot(TransformationItemMario.Name, EquipType.Body);
                        Main.PendingPlayer.legs = GetEquipSlot(TransformationItemMario.Name, EquipType.Legs);
                    }
                }
                
                if (!MarioSelected && !LuigiSelected && !WarioSelected && !WaluigiSelected)
                {
                    TransformationNamePanel.SetText("No transformation selected");
                    TransformationNamePanel.SetColour(new Color(99, 107, 153));
                }
                else
                {
                    if (MarioSelected)
                    {
                        TransformationNamePanel.SetText("Mario selected");
                        TransformationNamePanel.SetColour(new Color(255, 0, 0));
                    }

                    if (LuigiSelected)
                    {
                        TransformationNamePanel.SetText("Luigi selected");
                        TransformationNamePanel.SetColour(new Color(0, 255, 0));
                    }

                    if (WarioSelected)
                    {
                        TransformationNamePanel.SetText("Wario selected");
                        TransformationNamePanel.SetColour(new Color(255, 255, 0));
                    }

                    if (WaluigiSelected)
                    {
                        TransformationNamePanel.SetText("Waluigi selected");
                        TransformationNamePanel.SetColour(new Color(160, 32, 240));
                    }
                }

                if (!FireFlowerSelected && !SuperLeafSelected && !CapeFeatherSelected && !FrogSuitSelected && (!MarioSelected || !LuigiSelected))
                {
                    PowerUpNamePanel.SetText("No PowerUp selected");
                    PowerUpNamePanel.SetColour(new Color(99, 107, 153));
                }
                else
                {
                    if (FireFlowerSelected)
                    {
                        PowerUpNamePanel.SetText("Fire Flower selected");
                        PowerUpNamePanel.SetColour(new Color(242, 123, 53));
                    }

                    if (SuperLeafSelected)
                    {
                        PowerUpNamePanel.SetText("Super Leaf selected");
                        PowerUpNamePanel.SetColour(new Color(140, 60, 31));
                    }

                    if (CapeFeatherSelected)
                    {
                        PowerUpNamePanel.SetText("Cape Feather selected");
                        PowerUpNamePanel.SetColour(new Color(242, 195, 53));
                    }

                    if (FrogSuitSelected)
                    {
                        PowerUpNamePanel.SetText("Frog Suit selected");
                        PowerUpNamePanel.SetColour(new Color(25, 115, 82));
                    }
                }
            }

            orig(self, spriteBatch);
        }

        #endregion

        /* private void UpdateEquips(On.Terraria.Player.orig_UpdateEquips orig, Player self, int i)
        {
            if (!Main.LocalPlayer.GetModPlayer<MarioLandModPlayer>().TransformationActive)
            {
                orig(self, i);
            }
        }

        private void UpdateArmorSets(On.Terraria.Player.orig_UpdateArmorSets orig, Player self, int i)
        {
            if (!Main.LocalPlayer.GetModPlayer<MarioLandModPlayer>().TransformationActive)
            {
                orig(self, i);
            }
        }

        private void UpdateVisibleAccessories(On.Terraria.Player.orig_UpdateVisibleAccessories orig, Player self)
        {
            if (Main.gameMenu)
            {
                orig(self);
            }
        } */

        private void UnpackShader(On.Terraria.DataStructures.PlayerDrawHelper.orig_UnpackShader orig, int packedShaderIndex, out int localShaderIndex, out Terraria.DataStructures.PlayerDrawHelper.ShaderConfiguration shaderType)
        {
            if (!Main.gameMenu)
            {
                if (Main.LocalPlayer.GetModPlayer<MarioLandModPlayer>().TransformationActive)
                {
                    orig(MarioLandModSystem.SlotUIInstance.DyeSlot.Item.dye, out localShaderIndex, out shaderType);
                    return;
                }
            }

            orig(packedShaderIndex, out localShaderIndex, out shaderType);
        }

        public override void Load()
        {
            IL.Terraria.GameContent.UI.States.UICharacterCreation.BuildPage += BuildPage;
            IL.Terraria.GameContent.UI.States.UICharacterCreation.MakeCategoriesBar += MakeCategoriesBar;
            IL.Terraria.GameContent.UI.States.UICharacterCreation.SelectColorPicker += SelectColorPicker;
            IL.Terraria.GameContent.UI.States.UICharacterCreation.MakeClothStylesMenu += MakeClothStylesMenu;
            IL.Terraria.GameContent.UI.States.UICharacterCreation.MakeHairsylesMenu += MakeHairsylesMenu;
            On.Terraria.GameContent.UI.States.UICharacterCreation.UnselectAllCategories += UnselectAllCategories;
            On.Terraria.GameContent.UI.States.UICharacterCreation.Draw += UICharacterCreation_Draw;

            /* On.Terraria.Player.UpdateEquips += UpdateEquips;
            On.Terraria.Player.UpdateArmorSets += UpdateArmorSets;
            On.Terraria.Player.UpdateVisibleAccessories += UpdateVisibleAccessories; */
            On.Terraria.DataStructures.PlayerDrawHelper.UnpackShader += UnpackShader;

            if (!Main.dedServ)
            {
                MarioLandModDiscordRichPresence.Initialize();
                Main.OnTickForThirdPartySoftwareOnly += MarioLandModDiscordRichPresence.Update;
            }
        }

        public override void Unload()
        {
            if (!Main.dedServ)
            {
                MarioLandModDiscordRichPresence.Deinitialize();
                Main.OnTickForThirdPartySoftwareOnly -= MarioLandModDiscordRichPresence.Update;
            }
        }
    }
}