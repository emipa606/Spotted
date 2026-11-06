# GitHub Copilot Instructions for RimWorld Modding: Spotted (Continued)

## Mod Overview and Purpose
Spotted (Continued) is an updated version of the TGPArchers mod. Its primary purpose is to enhance the surveillance and security features in the RimWorld game. By adding a chance for colonists to spot raiders, caravans, and animals before they reach the colony, the mod aims to make players more prepared and less vulnerable.

## Key Features and Systems
### Spotted Research Tree
Colonists can unlock new researches that improve their ability to spot movements outside the colony. Each research increases the detail level of spotted movements, enhancing the overall surveillance capabilities.

### Buildings
Several new buildings are added to increase the chances of spotting movements. These buildings' spotting capabilities are cumulative, meaning that constructing more structures will significantly boost the colony's overall surveillance effectiveness.

### Discovery Capabilities
Each colonist increases the discovery chance by 0.5%. The discovery power and range of buildings are also cumulative, making the system flexible and scalable.

### Difficulty Adjustments
The discovery chance is adjusted according to the game difficulty, ensuring that higher difficulties require more spotting buildings or colonists to maintain the same level of security.

## Coding Patterns and Conventions
### File Structure
The mod is structured with clear separation of concerns. C# files contain the logic and Harmony patches, while XML files define the game objects and their properties.

### C# File Naming
C# files are named according to their functionality. For example:
- `ConfigDef.cs`: Contains configuration definitions.
- `CenterDrop_TryResolveRaidSpawnCenter.cs`: Contains Harmony patches for specific scenarios.

### XML File Naming
XML files are named according to the type of definitions they contain. For example:
- `ResearchProjects_1.xml`: Contains definitions for research projects.
- `Buildings_Misc.xml`: Contains definitions for miscellaneous buildings.

## XML Integration
The mod extensively uses XML files to define various game objects. These files are located in the `DefInjected` directory and include:
- `ResearchProjectDef`: Research projects related to surveillance.
- `ResearchTabDef`: Research tabs for organizing research projects.
- `StoryDef`: Custom story definitions.
- `StatDef`: Statistics for special buildings.
- `ThingDef`: Definitions for various buildings.

### Example XML Definition
xml
<ThingDef ParentName="Building" Name="MotionScanner">
    <label>Motion Scanner</label>
    <description>A building that increases the chance to spot movements outside the colony.</description>
    <defName>MotionScanner</defName>
    <statBases>
        <StatBase>
            <stat>DiscoveryChance</stat>
            <value>5</value>
        </StatBase>
    </statBases>
</ThingDef>


## Harmony Patching
The mod uses Harmony patches to modify the game's behavior without altering the original code. Harmony patches are located in the `Harmony` directory and include:
- `CenterDrop_TryResolveRaidSpawnCenter.cs`: Modifies the resolution of raid spawn centers.
- `EdgeDrop_TryResolveRaidSpawnCenter.cs`: Modifies the resolution of edge drop scenarios.
- `EdgeDropGroups_TryResolveRaidSpawnCenter.cs`: Modifies the resolution of edge drop group scenarios.
- `EdgeWalkIn_TryResolveRaidSpawnCenter.cs`: Modifies the resolution of edge walk-in scenarios.

### Example Harmony Patch
csharp
using HarmonyLib;
using Verse;

namespace MotionScanner.Harmony
{
    [HarmonyPatch(typeof(CenterDrop), "TryResolveRaidSpawnCenter")]
    public static class CenterDrop_TryResolveRaidSpawnCenter
    {
        public static void Postfix(ref bool __result)
        {
            if (__result)
            {
                // Custom logic to handle resolved raid spawn center
            }
        }
    }
}


## Suggestions for Copilot
### C# Code Generation
1. **Harmony Patches**: Generate Harmony patches for modifying specific game behaviors.
2. **Configuration Definitions**: Generate configuration definitions for new game objects.
3. **Utility Functions**: Generate utility functions for common tasks, such as calculating discovery chances.

### XML Code Generation
1. **Game Object Definitions**: Generate XML definitions for new research projects, buildings, and other game objects.
2. **Stat Definitions**: Generate XML definitions for statistics related to special buildings.
3. **Story Definitions**: Generate XML definitions for custom story scenarios.

### Example Requests
- **Generate a Harmony patch to modify the behavior of a specific game event.**
- **Create an XML definition for a new research project related to surveillance.**
- **Write a utility function to calculate the cumulative discovery chance of all active buildings and colonists.**

## Conclusion
By following these guidelines, developers can contribute effectively to the Spotted (Continued) mod. Whether you're working on Harmony patches, XML definitions, or utility functions, maintaining consistency and adhering to established patterns will ensure a smooth and efficient development process.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

