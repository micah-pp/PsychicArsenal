using HarmonyLib;
using Verse;
using RimWorld;
using System.Reflection;

namespace Psychic_Arsenal
{
    public class HarmonyPatches : Verse.Mod
	{
		public HarmonyPatches(ModContentPack content) : base(content)
		{
			var harmony = new Harmony("PotPourri.PsychicArsenal");
			var assembly = Assembly.GetExecutingAssembly();
			harmony.PatchAll(assembly);

			var original = AccessTools.Method(typeof(EquipmentUtility), "CanEquip", new[] { typeof(Thing), typeof(Pawn), typeof(string).MakeByRefType(), typeof(bool) });
			var postfix = AccessTools.Method(typeof(Psychic_CanEquip_Patch), "Postfix", new[] { typeof(Thing), typeof(Pawn), typeof(string).MakeByRefType(), typeof(bool).MakeByRefType() });
			if (original != null && postfix != null)
				harmony.Patch(original, postfix: new HarmonyMethod(postfix));
		}
	}
}
