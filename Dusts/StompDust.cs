using Terraria;
using Terraria.ModLoader;

namespace MarioLandMod.Dusts
{
    public class StompDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noGravity = true;
            dust.noLight = false;
            dust.scale *= 1.5f;
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;
            dust.scale -= 0.025f;
            if (dust.scale < 0.005f)
            {
                dust.active = false;
            }
            return true;
        }
    }
}