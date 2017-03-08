using System.Linq;
using LogType = UnityEngine.LogType;
using RawLog = Settings.Log.RawLog;
using Sample = Settings.Log.Sample;

namespace Test.Log
{
    public class Stash : ITest
    {
        public Stash()
            : base("LogStash")
        { }

        protected override void TestImpl()
        {
            var message = "message";
            var stacktrace = "stacktrace";
            var time = 1234;
            var scene = "scene";
            var sample = new Sample(time, scene);

            var target = new Settings.Log.Stash();
            var loop = 5;
            var logTypes = new[] { LogType.Log, LogType.Warning, LogType.Error, };
            for (var i = 0; i != loop; ++i)
            {
                logTypes.ToList().ForEach(t =>
                {
                    var rawLog = new RawLog(t, message, stacktrace);
                    target.Add(rawLog, sample);
                });
            }

            var allCount = loop * logTypes.Length;
            Assert.Equals(target.All().Count, allCount);
            {
                var mask = new Settings.Log.Mask();
                mask.AllTrue();
                var logs = target.Filter(mask);
                Assert.Equals(logs.Count, allCount);
            }

            {
                var mask = new Settings.Log.Mask();
                mask.AllFalse();
                var logs = target.Filter(mask);
                Assert.Equals(logs.Count, 0);
            }

            {
                var mask = new Settings.Log.Mask();
                mask.Warning = true;
                mask.Assert = true;
                Assert.Equals(target.Filter(mask).Count, loop * 1);
                mask.Log = true;
                Assert.Equals(target.Filter(mask).Count, loop * 2);
            }

            {
                var mask = new Settings.Log.Mask();
                mask.Log = true;
                var logs = target.Filter(mask);
                Assert.Equals(logs.Count, loop);
                var firstLog = logs[0];
                Assert.Equals(firstLog.Type, logTypes[0]);
                Assert.Equals(firstLog.Message, message);
                Assert.Equals(firstLog.Stacktrace, stacktrace);
                Assert.Equals(firstLog.Sample.Scene, scene);
                Assert.Equals(firstLog.Sample.Time, time);
            }
        }
    }
}
