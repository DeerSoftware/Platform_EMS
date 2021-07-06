using EMService.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMService
{
    /// <summary>
    /// 所有服务接口的入参类
    /// </summary>
    public class ParameterInfo
    {
        public ParameterInfo()
        {

        }


        public StringObjectPairs Filters { get; set; }

        public StringObjectPairs OrderBySorts { get; set; }

        public StringObjectPairs PageInfos { get; set; }
    }

    public class ParameterInfo<T> : ParameterInfo
    {
        public T Model { get; set; }
    }
}
