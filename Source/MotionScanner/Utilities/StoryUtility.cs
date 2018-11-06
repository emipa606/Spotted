using System.Collections.Generic;
using System.Linq;

namespace Spotted
{
    static class StoryUtility
    {
        public static bool MeetsRequirements(StoryDef story)
        {
            if (story == null)
            {
                return false;
            }

            if (story.required != null)
                foreach (var requirement in story.required)
                {
                    if (!requirement.RequirementIsMeet())
                    {
                        return false;
                    }
                }

            if (story.required?.Count > 0)
            {
                return true;
            }

            if (story.optional != null)
                foreach (var option in story.optional)
                {
                    if (option.RequirementIsMeet())
                    {
                        return true;
                    }
                }

            return false;
        }

        public static IEnumerable<StoryDef> MeetRequirements(this IEnumerable<StoryDef> stories)
        {
            return stories.Where(story => MeetsRequirements(story));
        }
    }
}
