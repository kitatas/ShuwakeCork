using Furu.Base.Data.DataStore;
using UnityEngine;

namespace Furu.Common.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(SeTable), menuName = "DataTable/" + nameof(SeTable))]
    public sealed class SeTable : BaseTable<SeData>
    {
    }
}