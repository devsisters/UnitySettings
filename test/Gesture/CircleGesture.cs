using System;
using Vector2 = UnityEngine.Vector2;

namespace Test.Util
{
    public class CircleGesture : ITest
    {
        public CircleGesture()
            : base("CircleGesture")
        { }

        public class TouchProvider : Settings.ITouchProvider
        {
            public Vector2 Next;

            public bool GetDown(out Vector2 result)
            {
                result = Next;
                return true;
            }
        }

        private static Vector2 V2(float x, float y)
        {
            return new Vector2(x, y);
        }

        protected override void TestImpl()
        {
            var touch = new TouchProvider();
            var target = new Settings.CircleGesture(touch, 100);

            Action<float, float, int> test_Sample = (x, y, cnt) =>
            {
                touch.Next = V2(x, y);
                target.SampleOrCancel();
                Assert.Equals(target.TouchCount, cnt);
            };

            Action<bool> test_Check = shouldTrue =>
            {
                var result = target.CheckAndClear();
                Assert.Equals(result, shouldTrue);
                if (result) Assert.Equals(target.TouchCount, 0);
            };

            test_Sample(0, 0, 1);
            test_Sample(0, 0, 1);
            test_Sample(0, 5, 1);
            test_Sample(0, 10, 2);
            test_Sample(0, -10, 0);

            test_Sample(0, 0, 1);
            test_Sample(0, 10, 2);
            test_Sample(10, 10, 3);
            test_Sample(20, 20, 4);
            test_Check(false);

            test_Sample(20, 20, 4);
            test_Sample(10, 20, 0);
            test_Check(false);

            var radius = 60;
            var slice = 20;
            var sliceRad = 2 * Math.PI / slice;
            for (var i = 0; i != slice; ++i)
            {
                var x = (float)(radius * Math.Sin(sliceRad * i));
                var y = (float)(radius * Math.Cos(sliceRad * i));
                test_Sample(x, y, i + 1);
            }
            test_Check(true);
            test_Check(false);

            var startIndex = 3;
            for (var i = startIndex; i != slice - 2; ++i)
            {
                var x = (float)(radius * Math.Sin(sliceRad * i));
                var y = (float)(radius * Math.Cos(sliceRad * i));
                test_Sample(x, y, i + 1 - startIndex);
            }
            test_Check(false);
            test_Check(false);
        }
    }
}