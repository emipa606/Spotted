using System.Collections.Generic;
using System.Linq;

namespace Spotted
{
    static class StoryUtility
    {
        private static T ClearAndReturn<T>(T value, object[] args)
        {
            if(args != null)
            {
                ConditionEvaluator.ClearArgs();
            }

            return value;
        }

        public static bool MeetsRequirements(StoryDef story, object[] args = null)
        {
            if (story == null)
            {
                return false;
            }
            if(args != null)
            {
                ConditionEvaluator.SetArgs(args);
            }

            if (story.required != null)
                foreach (var requirement in story.required)
                {
                    if (!requirement.RequirementIsMeet())
                    {
                        return ClearAndReturn(false, args);
                    }
                }

            if (story.required?.Count > 0)
            {
                return ClearAndReturn(true, args);
            }

            if (story.optional != null)
                foreach (var option in story.optional)
                {
                    if (option.RequirementIsMeet())
                    {
                        return ClearAndReturn(true, args);
                    }
                }

            if(story.optional?.Count > 0)
            {
                return ClearAndReturn(false, args);
            }
            
            return ClearAndReturn(true, args);
        }

        public static IEnumerable<StoryDef> MeetRequirements(this IEnumerable<StoryDef> stories, object[] args = null)
        {
            if(args != null)
            {
                ConditionEvaluator.SetArgs(args);
            }
            
            return ClearAndReturn(stories.Where(story => MeetsRequirements(story)).ToList(), args);
        }
    }
}
