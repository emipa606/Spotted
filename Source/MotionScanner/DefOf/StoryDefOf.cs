using RimWorld;

namespace Spotted
{
    [DefOf]
    public class StoryDefOf
    {
        static StoryDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(StoryDefOf));
        }
    }
}
