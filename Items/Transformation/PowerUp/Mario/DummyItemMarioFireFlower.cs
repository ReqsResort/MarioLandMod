using Terraria.ID;
using Terraria.ModLoader;

namespace MarioLandMod.Items.Transformation.PowerUp.Mario
{
    public class DummyItemMarioFireFlower : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.Deprecated[Type] = true;

            SetupDrawing();
        }

        public override void Load()
        {
            Mod.AddEquipTexture(new EquipTexture(), this, EquipType.Head, "MarioLandMod/TransformationTextures/Mario/FireFlower/Mario_FireFlower_Head");
            Mod.AddEquipTexture(new EquipTexture(), this, EquipType.Body, "MarioLandMod/TransformationTextures/Mario/FireFlower/Mario_FireFlower_Body");
            Mod.AddEquipTexture(new EquipTexture(), this, EquipType.Legs, "MarioLandMod/TransformationTextures/Mario/FireFlower/Mario_FireFlower_Legs");
        }

        private void SetupDrawing()
        {
            int MarioFireFlowerHead = Mod.GetEquipSlot(Name, EquipType.Head);
            int MarioFireFlowerBody = Mod.GetEquipSlot(Name, EquipType.Body);
            int MarioFireFlowerLegs = Mod.GetEquipSlot(Name, EquipType.Legs);


            ArmorIDs.Head.Sets.DrawHead[MarioFireFlowerHead] = false;
            ArmorIDs.Body.Sets.HidesTopSkin[MarioFireFlowerBody] = true;
            ArmorIDs.Body.Sets.HidesArms[MarioFireFlowerBody] = true;
            ArmorIDs.Legs.Sets.HidesBottomSkin[MarioFireFlowerLegs] = true;
        }
    }
}