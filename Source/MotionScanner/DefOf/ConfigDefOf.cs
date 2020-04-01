using RimWorld;

namespace Spotted
{
    [DefOf]
    static class ConfigDefOf
    {
        public static ConfigDef IncidentConfig;

        static ConfigDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(ConfigDefOf));
        }
    }
}
