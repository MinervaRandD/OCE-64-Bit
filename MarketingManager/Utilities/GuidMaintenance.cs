namespace Utilities
{
    using System.Collections.Generic;
    
    public static class GuidMaintenance
    {
        public static Dictionary<string, HashSet<string>> GuidDict = new Dictionary<string, HashSet<string>>();

        public static string GenerateGuid(string userId)
        {
            HashSet<string> guidSet;

            if (!GuidDict.ContainsKey(userId))
            {
                guidSet = new HashSet<string>();

                GuidDict.Add(userId, guidSet);
            }

            else
            {
                guidSet = GuidDict[userId]; 
            }

            string guid = System.Guid.NewGuid().ToString();

            while (guidSet.Contains(guid))
            {
                guid = System.Guid.NewGuid().ToString();
            }

            guidSet.Add(guid); 

            return guid;

        }

    }
}