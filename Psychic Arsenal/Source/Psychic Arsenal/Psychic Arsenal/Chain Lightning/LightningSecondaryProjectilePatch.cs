using Verse;
using UnityEngine;

namespace Psychic_Arsenal
{
    class LightningSecondaryProjectilePatch : SpawnedProjectile
    {
        public void PreLaunch(Thing launcher, Vector3 origin)
        {
            if (!CheckTarget(launcher, origin, base.def.GetModExtension<Psychic_Arsenal.SpawnerProjectileModExtension>().checkRadius, out Pawn pawn))
            {
                this.Destroy(DestroyMode.Vanish);
            }
            else
            {
                Launch(launcher, origin, pawn, pawn, ProjectileHitFlags.IntendedTarget, false, base.def.GetModExtension<Psychic_Arsenal.SpawnerProjectileModExtension>().originalLauncher.GetConcreteExample());
            }
        }

        protected override void Impact(Thing hitThing)
        {
            base.Impact(hitThing);
            if (hitThing != null && hitThing is Pawn pawn)
            {
                float rand = Rand.Value;
                if (rand >= 0.5f)
                {
                    pawn?.stances?.stunner.StunFor(90, this, true, true);
                }
                pawnList.Add(pawn);
                LightningSecondaryProjectilePatch tertiaryProjectile = (LightningSecondaryProjectilePatch)GenSpawn.Spawn(base.def.GetModExtension<Psychic_Arsenal.SpawnerProjectileModExtension>().spawnedProjectile, hitThing.Position, launcher.Map);
                tertiaryProjectile.pawnList = pawnList;
                tertiaryProjectile.usedTarget = this.launcher;
                tertiaryProjectile.PreLaunch(launcher, pawn.DrawPos);
            }
        }

    }
}
