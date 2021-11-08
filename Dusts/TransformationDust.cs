using Terraria;
using Terraria.ModLoader;

namespace MarioLandMod.Dusts
{
    public class TransformationDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = true;
            dust.scale *= 2f;

            dust.velocity *= 0.4f;
            dust.velocity.Y -= 0.4f;
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X * 0.2f;
            dust.scale -= 0.05f;
            if (dust.scale < 0.2f)
            {
                dust.active = false;
            }
            return true;
        }
    }
}