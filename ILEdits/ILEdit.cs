using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularGems.ILEdits
{
    internal abstract class ILEdit
    {
        public abstract void Apply();

        public static IEnumerable<Type> GetILEdits()
            => GetViviansPlaygroundNonAbstractClasses(x => x.IsSubclassOf(typeof(ILEdit)));

        public static IEnumerable<Type> GetViviansPlaygroundNonAbstractClasses(Func<Type, bool> fun = null)
            => ModularGems.Instance.Code.GetTypes().Where(x => x.IsClass && !x.IsAbstract && (fun?.Invoke(x) ?? true));
    }
}
