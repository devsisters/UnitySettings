using System.Linq;
using Lens = Test.Lens<Dashboard.Log.Mask, bool>;
using Target = Dashboard.Log.Mask;

namespace Test.Log
{
    public class Mask : ITest
    {
        public Mask()
            : base("LogMask")
        { }

        protected override void TestImpl()
        {
            var log = new Lens(t => t.Log, (v, t) => { t.Log = v; return t; });
            var warning = new Lens(t => t.Warning, (v, t) => { t.Warning = v; return t; });
            var error = new Lens(t => t.Error, (v, t) => { t.Error = v; return t; });
            var assert = new Lens(t => t.Assert, (v, t) => { t.Assert = v; return t; });
            var exception = new Lens(t => t.Exception, (v, t) => { t.Exception = v; return t; });
            var all = new[] { log, warning, error, assert, exception };

            var target = new Target();

            target.AllTrue();
            Assert.True(all.All(l => l.From(target)));

            target.AllFalse();
            Assert.False(all.Any(l => l.From(target)));

            all.ToList().ForEach(l => target = TestFlag(l, target));
            Assert.True(all.All(l => l.From(target)));
        }

        private static Target TestFlag(Lens flag, Target target)
        {
            target = flag.To(false, target);
            Assert.False(flag.From(target));
            target = flag.To(true, target);
            Assert.True(flag.From(target));
            target = flag.To(false, target);
            Assert.False(flag.From(target));
            target = flag.To(true, target);
            Assert.True(flag.From(target));
            return target;
        }
    }
}