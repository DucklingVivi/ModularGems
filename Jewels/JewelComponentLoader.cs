using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularGems.Jewels
{
    public static class JewelComponentLoader
    {

        internal static readonly IList<JewelComponent> jewelComponents;
        private static int nextComponent;
        public static int ComponentCount => nextComponent;
        internal static int ReserveComponentID()
        {
            int result = nextComponent;
            nextComponent++;
            return result;
        }

        public static JewelComponent GetComponent(int type)
        {
            if (type >= ComponentCount)
            {
                return null;
            }
            return jewelComponents[type];
        }

        static JewelComponentLoader()
        {
            nextComponent = 0;
            jewelComponents = new List<JewelComponent>();
        }

        internal static void Unload()
        {
            nextComponent = 0;
            jewelComponents.Clear();
        }

    }
}
