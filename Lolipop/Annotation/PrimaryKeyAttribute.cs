using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lolipop.Annotation
{
    // 定义一个可以在多个地方使用的，标注在属性上的特性PrimaryKey
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class PrimaryKeyAttribute : System.Attribute
    {
    }
}
