using System.Collections.Generic;
using RimWorld;
using Verse;

namespace Psychic_Arsenal
{

    class Psychic_CanEquip_Patch
    {
        public static bool CanEquipPsychic(Pawn pawn, ModExtension_PsychicArsenal checkList)
        {
            if (checkList != null && checkList.isPsychicWeapon)
            {
                var abilityTracker = pawn.abilities;
                if(abilityTracker == null)
                {
                    return false;
                }
                List<string> abilities = checkList.compatibleAbilities;
                List<string> secondaries = checkList.secondaryAbilities;
                bool primaryTracker = false;
                bool secondaryTracker = false;
                if (secondaries == null)
                {
                    secondaryTracker = true;
                }
                foreach (Ability ability in abilityTracker.abilities)
                {
                    if (abilities.Contains(ability.def.defName) && ability.def.level <= PawnUtility.GetPsylinkLevel(pawn))
                    {
                        primaryTracker = true;
                    }
                    if (!secondaryTracker && secondaries.Contains(ability.def.defName) && ability.def.level <= PawnUtility.GetPsylinkLevel(pawn))
                    {
                        secondaryTracker = true;
                    }
                    if (primaryTracker && secondaryTracker)
                    {
                        return true;
                    }

                }
                
                return false;
            }
            else
            {
                return true;
            }
        }
        public static void Postfix(Thing thing, Pawn pawn, ref string cantReason, ref bool __result)
        {
            if (__result)
            {
                ModExtension_PsychicArsenal modExtension = thing.def.GetModExtension<ModExtension_PsychicArsenal>();
                 if (!CanEquipPsychic(pawn, modExtension))
                 {
                    __result = false;
                    cantReason = "PPPsychicArsenal".Translate();
                    return;
                 }
            }
        }
    }
}
