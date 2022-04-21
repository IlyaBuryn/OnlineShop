namespace BusinessLogic.Services
{
    public class Splitter
    {
        public static string[,] SplitString(string argString, int colCount, char splitterSymbol)
        {
            var strings = argString.Split(splitterSymbol);
            var result = new string[strings.Length / colCount, colCount];
            for (int i = 0; i < strings.Length; ++i)
            {
                for (int j = 0; j < colCount; ++j, ++i)
                {
                    result[i, j] = strings[i];
                }
            }
            return result;
        }

        public static Dictionary<string, Dictionary<string, string>> ConvertExpandObjectToDictionary(Object obj)
        {
            var result = new Dictionary<string, Dictionary<string, string>>();

            foreach (var property in (IDictionary<String, Object>)obj)
            {
                var itemInfo = new Dictionary<string, string>();

                foreach (var item in (IDictionary<String, Object>)property.Value)
                {
                    itemInfo.Add(item.Key, item.Value.ToString());
                }
                result.Add(property.Key, itemInfo);
            }

            return result;
        }

        public static List<string> ConvertEcpandObjectToStringList(Object obj)
        {
            var result = new List<string>();
            foreach (var property in (List<Object>)obj)
            {
                result.Add(property.ToString());
            }
            return result;
        }
    }
}
