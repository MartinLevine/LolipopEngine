using Lolipop.Annotation;
using Lolipop.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LolipopTestSimple.Model
{
    public class TestModel: LolipopEntity<TestModel>
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
