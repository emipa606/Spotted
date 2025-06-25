using System.Collections.Generic;
using System.Linq;

namespace Spotted;

internal static class StoryUtility
{
    private static T clearAndReturn<T>(T value, object[] args)
    {
        if (args != null)
        {
            ConditionEvaluator.ClearArgs();
        }

        return value;
    }

    private static bool meetsRequirements(StoryDef story, object[] args = null)
    {
        if (story == null)
        {
            return false;
        }

        if (args != null)
        {
            ConditionEvaluator.SetArgs(args);
        }

        if (story.required != null)
        {
            foreach (var requirement in story.required)
            {
                if (!requirement.RequirementIsMeet())
                {
                    return clearAndReturn(false, args);
                }
            }
        }

        if (story.required?.Count > 0)
        {
            return clearAndReturn(true, args);
        }

        if (story.optional == null)
        {
            return clearAndReturn(!(story.optional?.Count > 0), args);
        }

        foreach (var option in story.optional)
        {
            if (option.RequirementIsMeet())
            {
                return clearAndReturn(true, args);
            }
        }

        return clearAndReturn(!(story.optional?.Count > 0), args);
    }

    public static IEnumerable<StoryDef> MeetRequirements(this IEnumerable<StoryDef> stories, object[] args = null)
    {
        if (args != null)
        {
            ConditionEvaluator.SetArgs(args);
        }

        return clearAndReturn(stories.Where(story => meetsRequirements(story)).ToList(), args);
    }
}