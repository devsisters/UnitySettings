using Vector2 = UnityEngine.Vector2;

namespace Test.Util
{
    public class CircleGesture : ITest
    {
        public CircleGesture()
            : base("CircleGesture")
        {
        }

        public class TouchProvider : Dashboard.ITouchProvider
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
            var target = new Dashboard.CircleGesture(touch, 100);

            System.Action<float, float, int> sampleAndCheck = (x, y, cnt) =>
            {
                touch.Next = V2(x, y);
                target.SampleOrCancel();
                Assert.Equals(target.TouchCount, cnt);
            };

            sampleAndCheck(0, 0, 1);
            sampleAndCheck(0, 0, 1);
            sampleAndCheck(0, 5, 1);
            sampleAndCheck(0, 10, 2);
            sampleAndCheck(0, -10, 0);

            sampleAndCheck(0, 0, 1);
        }
    }
}