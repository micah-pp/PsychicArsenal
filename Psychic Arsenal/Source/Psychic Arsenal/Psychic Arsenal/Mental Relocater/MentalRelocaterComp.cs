using Verse;
using RimWorld;

namespace Psychic_Arsenal
{
    public class MentalRelocaterComp : CompTargetEffect
    {
        public ModExtension_MentalR Props => this.parent.def.GetModExtension<ModExtension_MentalR>();

        public override void DoEffectOn(Pawn user, Thing target)
        {
            Pawn pawn = (Pawn)target;
            if (pawn.Dead || pawn.GetStatValue(StatDefOf.PsychicSensitivity, true) <= 0)
            {
                return;
            }
            Hediff hediff = HediffMaker.MakeHediff(Props.coma, user);
            HediffComp_Disappears hediffComp_Disappears = hediff.TryGetComp<HediffComp_Disappears>();
            if (user.psychicEntropy.CurrentPsyfocus <= 0.25f || user.GetPsylinkLevel() < 3)
            {
                pawn.health.AddHediff(HediffMaker.MakeHediff(Props.mindGraft1, pawn));
                hediffComp_Disappears.ticksToDisappear = 10000;
                user.health.AddHediff(hediff);
            }
            else if (user.psychicEntropy.CurrentPsyfocus <= 0.5f || user.GetPsylinkLevel() < 4)
            {
                pawn.health.AddHediff(HediffMaker.MakeHediff(Props.mindGraft2, pawn));
                hediffComp_Disappears.ticksToDisappear = 15000;
                user.health.AddHediff(hediff);
            }
            else if (user.psychicEntropy.CurrentPsyfocus < 0.9f || user.GetPsylinkLevel() < 5)
            {
                pawn.health.AddHediff(HediffMaker.MakeHediff(Props.mindGraft3, pawn));
                hediffComp_Disappears.ticksToDisappear = 30000;
                user.health.AddHediff(hediff);
            }
            else if (user.psychicEntropy.CurrentPsyfocus >= 0.9f && user.GetPsylinkLevel() > 4)
            {
                pawn.health.AddHediff(HediffMaker.MakeHediff(Props.mindGraft4, pawn));
                hediffComp_Disappears.ticksToDisappear = 60000;
                user.health.AddHediff(hediff);
            }
        }
    }
}
