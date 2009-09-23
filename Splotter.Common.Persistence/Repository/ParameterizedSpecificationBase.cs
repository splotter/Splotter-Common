using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Splotter.Common.Persistence
{
    public abstract class ParameterizedSpecificationBase<T, TParam> : SpecificationBase<T>
        where T: class
    {
        protected TParam _param;

        public void InitParam(TParam param)
        {
            _param = param;
        }

    }
}
