using Verse;
using RimWorld;

namespace Psychic_Arsenal
{
    static class NeuralHeatAdd
    {
        public static bool ShootWithHeat(Pawn pawn, ThingWithComps weapon)
        {
            if (pawn != null )
            {
                float projectedEntropy = pawn.psychicEntropy.EntropyValue + weapon.def.GetModExtension<ModExtension_PsychicArsenal>().entropyGain;
                if (projectedEntropy <= pawn.psychicEntropy.MaxEntropy || pawn.psychicEntropy.limitEntropyAmount == false)
                {
                    return true;
                }
                else
                {
                    Messages.Message("PP_NeuralHeatHigh".Translate(pawn.Label), MessageTypeDefOf.NeutralEvent);
                    pawn.jobs.EndCurrentJob(Verse.AI.JobCondition.Incompletable, false, false);
                }
            }
            return false;
        }
    }
}
