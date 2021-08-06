using Verse;
using RimWorld;

namespace Psychic_Arsenal
{
    class PsychicShoot : Verb_Shoot
    {
        public override bool TryStartCastOn(LocalTargetInfo castTarg, LocalTargetInfo destTarg, bool surpriseAttack = false, bool canHitNonTargetPawns = true, bool preventFriendlyFire = false)
        {
            if(NeuralHeatAdd.ShootWithHeat(this.CasterPawn, base.EquipmentSource))
            {
                return base.TryStartCastOn(castTarg, destTarg, surpriseAttack, canHitNonTargetPawns, preventFriendlyFire);
            }
            return false;
        }

        protected override bool TryCastShot()
        {
            this.CasterPawn.psychicEntropy.TryAddEntropy(base.EquipmentSource.def.GetModExtension<ModExtension_PsychicArsenal>().entropyGain);
            return base.TryCastShot();
        }
    }

    class PsychicSingleShoot : Verb_ShootOneUse
    {
        public override bool TryStartCastOn(LocalTargetInfo castTarg, LocalTargetInfo destTarg, bool surpriseAttack = false, bool canHitNonTargetPawns = true, bool preventFriendlyFire = false)
        {
            if (NeuralHeatAdd.ShootWithHeat(this.CasterPawn, base.EquipmentSource))
            {
                return base.TryStartCastOn(castTarg, destTarg, surpriseAttack, canHitNonTargetPawns, preventFriendlyFire);
            }
            return false;
        }

        protected override bool TryCastShot()
        {
            this.CasterPawn.psychicEntropy.TryAddEntropy(base.EquipmentSource.def.GetModExtension<ModExtension_PsychicArsenal>().entropyGain);
            return base.TryCastShot();
        }
    }
}
