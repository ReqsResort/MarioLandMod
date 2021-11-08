using MarioLandMod.UI;
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

        Color Default = new(60, 72, 143);
        Color Selected = new(156, 164, 229);

        UIElement MarioLandModCategoryContainer;
        UIColoredImageButton marioLandModCategoryButton;

        UIImage TransformationButton;
        UIImage TransformationButtonTexture;
        readonly UIImage[] TransformationButtons = new UIImage[4];
        readonly UIImage[] TransformationButtonTextures = new UIImage[4];

        UIText TransformationButtonInfoText;

        public bool MarioSelected = false;
        public bool LuigiSelected = false;
        public bool WarioSelected = false;
        public bool WaluigiSelected = false;

        public bool MarioHover = false;
        public bool LuigiHover = false;
        public bool WarioHover = false;
        public bool WaluigiHover = false;

        private void MakeMarioLandModCategory(UIElement container)
        {
            MarioSelected = false;
            LuigiSelected = false;
            WarioSelected = false;
            WaluigiSelected = false;

            UIElement uIElement = new()
            {
                Width = StyleDimension.FromPixelsAndPercent(-10f, 1f),
                Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
                HAlign = 0.5f,
                VAlign = 0.5f
            };

            container.Append(uIElement);

            MarioLandModCategoryContainer = uIElement;

            string[] TransformationButtonImages = { "MarioLandMod/UI/Textures/TransformationSelectionUI/TransformationSelectionButtonTexture_Mario", "MarioLandMod/UI/Textures/TransformationSelectionUI/TransformationSelectionButtonTexture_Luigi", "MarioLandMod/UI/Textures/TransformationSelectionUI/TransformationSelectionButtonTexture_Wario", "MarioLandMod/UI/Textures/TransformationSelectionUI/TransformationSelectionButtonTexture_Waluigi" };

            for (int i = 0; i < 4; i++)
            {
                TransformationButton = new(ModContent.Request<Texture2D>("MarioLandMod/UI/Textures/TransformationSelectionUI/TransformationSelectionButtonBackground", AssetRequestMode.ImmediateLoad));

                TransformationButtons[i] = TransformationButton;

                TransformationButton.Color = Default;

                if (i == 0)
                {
                    TransformationButton.HAlign = 0.01f;
                    TransformationButton.VAlign = 0f;
                }
                else if (i == 1)
                {
                    TransformationButton.HAlign = 0.1425f;
                    TransformationButton.VAlign = 0f;
                }
                else if (i == 2)
                {
                    TransformationButton.HAlign = 0.01f;
                    TransformationButton.VAlign = 1f;
                }
                else if (i == 3)
                {
                    TransformationButton.HAlign = 0.1425f;
                    TransformationButton.VAlign = 1f;
                }

                TransformationButtonTexture = new(ModContent.Request<Texture2D>(TransformationButtonImages[i], AssetRequestMode.ImmediateLoad))
                {
                    HAlign = 0.5f,
                    VAlign = 0.5f
                };

                TransformationButtonTextures[i] = TransformationButtonTexture;

                TransformationButton.Append(TransformationButtonTexture);

                MarioLandModCategoryContainer.Append(TransformationButton);
            }

            UIPanel TransformationButtonInfoPanel = new()
            {
                Width = StyleDimension.FromPercent(0.73f),
                Height = StyleDimension.FromPercent(1f),
                VAlign = 0.5f,
                HAlign = 0.97f,
                BackgroundColor = new Color(99, 107, 153),
                BorderColor = new Color(99, 107, 153)
            };

            TransformationButtonInfoText = new("Select a transformation to begin with on your journey. If you don't want to start with a transformation, leave the menu like this.", 1f, false)
            {
                HAlign = 0f,
                VAlign = 0.5f,
                PaddingLeft = 20f,
                PaddingRight = 20f,
                Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
                Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
                Top = StyleDimension.FromPixelsAndPercent(15f, 0f),
                IsWrapped = true
            };

            MarioLandModCategoryContainer.Append(TransformationButtonInfoPanel);
            TransformationButtonInfoPanel.Append(TransformationButtonInfoText);


            TransformationButtons[0].OnMouseDown += TransformationButton_OnMouseDown_Mario;
            TransformationButtons[1].OnMouseDown += TransformationButton_OnMouseDown_Luigi;
            TransformationButtons[2].OnMouseDown += TransformationButton_OnMouseDown_Wario;
            TransformationButtons[3].OnMouseDown += TransformationButton_OnMouseDown_Waluigi;
        }

        private void TransformationButton_OnMouseDown_Mario(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(12);
            MarioSelected = !MarioSelected;

            if (MarioSelected == true)
            {
                LuigiSelected = false;
                WarioSelected = false;
                WaluigiSelected = false;

                TransformationButtons[0].Color = Selected;
                TransformationButtons[1].Color = Default;
                TransformationButtons[2].Color = Default;
                TransformationButtons[3].Color = Default;
                TransformationButtonInfoText.SetText("Mario is an \"all-around\" transformation, with average stats. He can double and triple jump, hurt enemies from above with a jump or ground pound, and even swim indefinitely.");
            }
            else
            {
                TransformationButtons[0].Color = Default;
                TransformationButtonInfoText.SetText("Select a transformation to begin with on your journey. If you don't want to start with a transformation, leave the menu like this.");
            }
        }

        private void TransformationButton_OnMouseDown_Luigi(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(12);
            LuigiSelected = !LuigiSelected;

            if (LuigiSelected == true)
            {
                MarioSelected = false;
                WarioSelected = false;
                WaluigiSelected = false;

                TransformationButtons[0].Color = Default;
                TransformationButtons[1].Color = Selected;
                TransformationButtons[2].Color = Default;
                TransformationButtons[3].Color = Default;
                TransformationButtonInfoText.SetText("Luigi is very similar to Mario, but has a much higher triple jump. He's also the most slippy of the bunch, having the highest deceleration time");
            }
            else
            {
                TransformationButtons[1].Color = Default;
                TransformationButtonInfoText.SetText("Select a transformation to begin with on your journey. If you don't want to start with a transformation, leave the menu like this.");
            }
        }

        private void TransformationButton_OnMouseDown_Wario(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(12);
            WarioSelected = !WarioSelected;

            if (WarioSelected == true)
            {
                MarioSelected = false;
                LuigiSelected = false;
                WaluigiSelected = false;

                TransformationButtons[0].Color = Default;
                TransformationButtons[1].Color = Default;
                TransformationButtons[2].Color = Selected;
                TransformationButtons[3].Color = Default;
                TransformationButtonInfoText.SetText("To be determined");
            }
            else
            {
                TransformationButtons[2].Color = Default;
                TransformationButtonInfoText.SetText("Select a transformation to begin with on your journey. If you don't want to start with a transformation, leave the menu like this.");

            }
        }

        private void TransformationButton_OnMouseDown_Waluigi(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(12);
            WaluigiSelected = !WaluigiSelected;

            if (WaluigiSelected == true)
            {
                MarioSelected = false;
                LuigiSelected = false;
                WarioSelected = false;

                TransformationButtons[0].Color = Default;
                TransformationButtons[1].Color = Default;
                TransformationButtons[2].Color = Default;
                TransformationButtons[3].Color = Selected;
                TransformationButtonInfoText.SetText("To be determined");
            }
            else
            {
                TransformationButtons[3].Color = Default;
                TransformationButtonInfoText.SetText("Select a transformation to begin with on your journey. If you don't want to start with a transformation, leave the menu like this.");
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
                    orig(SlotUI.DyeSlot.Item.dye, out localShaderIndex, out shaderType);
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