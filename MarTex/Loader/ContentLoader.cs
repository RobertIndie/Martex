using System;
using System.Collections.Generic;
using System.Text;

namespace MarTex.Loader
{
    public abstract class ContentLoader
    {
        public virtual Content Load()
        {
            throw new NotImplementedException();
        }
    }
}
