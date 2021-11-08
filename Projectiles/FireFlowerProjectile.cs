using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MarioLandMod.Projectiles
{
    public class FireFlowerProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Flower Fireball");
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.penetrate = 1;
            Projectile.friendly = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.direction != Projectile.oldDirection)
            {
                return true;
            }

            if (Projectile.velocity.X != 0 && Projectile.oldVelocity.Y > 0 && Projectile.velocity.Y != oldVelocity.Y)
            {
                if (Projectile.velocity.X != oldVelocity.X && Math.Abs(oldVelocity.X) > 1f)
                {
                    Projectile.velocity.X = oldVelocity.X * -0.9f;
                }
                if (Projectile.velocity.Y != oldVelocity.Y && Math.Abs(oldVelocity.Y) > 1f)
                {
                    Projectile.velocity.Y = -4f;
                }

                return false;
            }
            else
            {
                return true;
            }
        }

        public override void AI()
        {
            Projectile.rotation += 0.4f * Projectile.direction;

            for (int d = 0; d < 3; d++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.FlameBurst, Projectile.velocity.X, Projectile.velocity.Y);
            }

            Projectile.velocity.Y += 0.4f;

            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
        }
    }
}