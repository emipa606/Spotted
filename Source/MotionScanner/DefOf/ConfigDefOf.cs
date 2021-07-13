using RimWorld;

namespace Spotted
{
    [DefOf]
    internal static class ConfigDefOf
    {
        public static ConfigDef IncidentConfig;

        static ConfigDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(ConfigDefOf));
        }
    }
}