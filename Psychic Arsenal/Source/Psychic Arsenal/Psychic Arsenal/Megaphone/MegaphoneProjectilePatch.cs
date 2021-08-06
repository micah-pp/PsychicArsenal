using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace Psychic_Arsenal
{
    class MegaphoneProjectilePatch : Projectile_Explosive
    {
        public ModExtension_MegaphoneProjectile Props => base.def.GetModExtension<ModExtension_MegaphoneProjectile>();

        protected override void Explode()
        {
            base.Explode();
            IEnumerable<Thing> list = GenRadial.RadialCellsAround(base.Position, 2, true).SelectMany(Position => Position.GetThingList(launcher.Map)).Where(t => t is Pawn);
            if (list.Any())
            {
                float stunChance = Props.addStunChance;
                float dazeChance = Props.addDazeChance;
                float fleeChance = Props.addFleeChance;
                foreach (Pawn pawn in list)
                {
                    float rand = Rand.Value;
                    if ((rand <= dazeChance) && (pawn.GetStatValue(StatDefOf.PsychicSensitivity, true) > 0))
                    {
                        pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Wander_Psychotic, null, true, false, null, false);
                        Messages.Message("PP_Wander".Translate(this.launcher.Label, pawn.Label), MessageTypeDefOf.NeutralEvent);
                    }
                    else if (((rand - dazeChance) <= fleeChance) && (pawn.Faction != Faction.OfPlayer) && (pawn.GetStatValue(StatDefOf.PsychicSensitivity, true) > 0))
                    {
                        pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.PanicFlee, null, true, false, null, false);
                        Messages.Message("PP_Flee".Translate(this.launcher.Label, pawn.Label), MessageTypeDefOf.NeutralEvent);
                    }
                    else if ((rand - dazeChance - fleeChance) <= stunChance)
                    {
                        pawn.stances.stunner.StunFor(100, this, true, true);
                    }
                }
            }
        }
    }
}
