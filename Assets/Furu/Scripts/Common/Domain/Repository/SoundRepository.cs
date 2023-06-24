using Furu.Common.Data.DataStore;

namespace Furu.Common.Domain.Repository
{
    public sealed class SoundRepository
    {
        private readonly BgmTable _bgmTable;

        public SoundRepository(BgmTable bgmTable)
        {
            _bgmTable = bgmTable;
        }

        public BgmData Find(BgmType type)
        {
            return _bgmTable.data.Find(x => x.type == type);
        }
    }
}