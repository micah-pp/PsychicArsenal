using System.Collections.Generic;
using Verse;
using RimWorld;

namespace Psychic_Arsenal
{
    class ChainLightningProjectilePatch : Bullet
    {
        protected override void Impact(Thing hitThing)
        {
            base.Impact(hitThing);
            if (hitThing != null && hitThing is Pawn pawn)
            {
                pawn?.stances?.stunner.StunFor(120, this, true, true);
                List<Pawn> hitPawns = new List<Pawn>();
                LightningSecondaryProjectilePatch secondaryProjectile = (LightningSecondaryProjectilePatch)GenSpawn.Spawn(base.def.GetModExtension<Psychic_Arsenal.SpawnerProjectileModExtension>().spawnedProjectile, hitThing.Position, launcher.Map);
                hitPawns.Add(pawn);
                hitPawns.Add(launcher as Pawn);
                secondaryProjectile.pawnList = hitPawns;
                secondaryProjectile.usedTarget = this.launcher;
                secondaryProjectile.PreLaunch(launcher, pawn.DrawPos);
            }
        }
    }
}
