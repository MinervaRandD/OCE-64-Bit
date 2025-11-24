namespace Utilities
{
    using System.Collections.Generic;
    
    public static class GuidMaintenance
    {
        public static Dictionary<string, object> GuidDict = new Dictionary<string, object>();


        public static string GenerateGuid()
        {
            string guid = System.Guid.NewGuid().ToString();

            while (GuidDict.ContainsKey(guid))
            {
                guid = System.Guid.NewGuid().ToString();
            }

            return guid;

        }

        public static void AddGuid(string guid, object obj)
        {
            if (obj.GetType().ToString() == "Graphics.GraphicsCounter")
            {
                int x = 1;
            }

            GuidDict.Add(guid, obj);
        }

        public static bool ContainsGuid(string guid)
        {
            return GuidDict.ContainsKey(guid);
        }

        public static void DeleteGuid(string guid)
        {
            if (GuidDict.ContainsKey(guid))
            {
                GuidDict.Remove(guid);
            }
        }

        public static void ClearGuids()
        {
            GuidDict.Clear();
        }

    }
}