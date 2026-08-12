using HarmonyLib;
using UnrealEngine.Engine;
using UnrealEngine.Runtime;
using WukongMp.Api;
using WukongMp.Api.Configuration;

namespace ExampleMod;

[HarmonyPatch(typeof(UGameplayStatics), nameof(UGameplayStatics.OpenLevel))]
[HarmonyPatchCategory(PatchCategory.Global)]
public static class ExamplePatch
{
    public static void Postfix(FName LevelName)
    {
        Logging.LogDebug("Entering level: {LevelName}", LevelName.ToString());
    }
}