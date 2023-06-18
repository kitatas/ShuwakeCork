using System.Collections.Generic;

namespace Furu.Base.Data.Container
{
    public abstract class BaseContainer<T>
    {
        protected readonly List<T> _list;

        public BaseContainer()
        {
            _list = new List<T>();
        }

        public void Add(T t)
        {
            _list.Add(t);
        }

        public void Clear()
        {
            _list.Clear();
        }
    }
}