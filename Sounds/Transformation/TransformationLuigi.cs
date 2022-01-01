using Microsoft.Xna.Framework.Audio;
using Terraria.ModLoader;

namespace MLSandbox.Sounds.Transformation
{
    public class TransformationLuigi : ModSound
    {
        public override SoundEffectInstance PlaySound(ref SoundEffectInstance soundInstance, float volume, float pan)
        {
            if (soundInstance.State == SoundState.Playing)
            {
                return null;
            }

            return soundInstance;
        }
    }
}
