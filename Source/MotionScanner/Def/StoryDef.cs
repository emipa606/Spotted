using System.Collections.Generic;
using Verse;

namespace Spotted;

public class StoryDef : Def
{
    public List<StoryCondition> optional;
    public List<StoryCondition> required;
    public string storyKey;
    public string storyType;
}