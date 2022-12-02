using System.Collections.Generic;

namespace adventofcode.Models
{
    public class Bag
    {        
        public string BagColor { get; set; }
        public List<KeyValuePair<string, int>> InnerBags { get; set; }        
        public bool CanContainShinyGoldBag { get; set; }

        public override string ToString()
        {
            var innerBagString = string.Empty;
            foreach(var innerBag in InnerBags)
            {
                innerBagString = (string.IsNullOrWhiteSpace(innerBagString)) ? $"{innerBag.Key}:{innerBag.Value}" : $"{innerBagString},{innerBag.Key}:{innerBag.Value}";
            }

            return $"{BagColor}|{innerBagString}|{CanContainShinyGoldBag}";
        }
    }
}
