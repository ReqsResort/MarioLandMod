using Terraria.ID;
using Terraria.ModLoader;

namespace MarioLandMod.Items.Transformation.PowerUp.Luigi
{
    public class DummyItemLuigiFireFlower : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.Deprecated[Type] = true;

            SetupDrawing();
        }

        public override void Load()
        {
            Mod.AddEquipTexture(new EquipTexture(), this, EquipType.Head, "MarioLandMod/TransformationTextures/Luigi/FireFlower/Luigi_FireFlower_Head");
            Mod.AddEquipTexture(new EquipTexture(), this, EquipType.Body, "MarioLandMod/TransformationTextures/Luigi/FireFlower/Luigi_FireFlower_Body");
            Mod.AddEquipTexture(new EquipTexture(), this, EquipType.Legs, "MarioLandMod/TransformationTextures/Luigi/FireFlower/Luigi_FireFlower_Legs");
        }

        private void SetupDrawing()
        {
            int LuigiFireFlowerHead = Mod.GetEquipSlot(Name, EquipType.Head);
            int LuigiFireFlowerBody = Mod.GetEquipSlot(Name, EquipType.Body);
            int LuigiFireFlowerLegs = Mod.GetEquipSlot(Name, EquipType.Legs);


            ArmorIDs.Head.Sets.DrawHead[LuigiFireFlowerHead] = false;
            ArmorIDs.Body.Sets.HidesTopSkin[LuigiFireFlowerBody] = true;
            ArmorIDs.Body.Sets.HidesArms[LuigiFireFlowerBody] = true;
            ArmorIDs.Legs.Sets.HidesBottomSkin[LuigiFireFlowerLegs] = true;
        }
    }
}