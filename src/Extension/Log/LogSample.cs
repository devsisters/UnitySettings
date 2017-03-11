namespace Settings.Log
{
    internal struct Sample
    {
        public readonly float Time;
        public readonly string Scene;
        public string TimeToDisplay { get { return Time.ToString("0.000"); } }

        public Sample(float time, string scene)
        {
            Time = time;
            Scene = scene;
        }
    }

    internal interface ISampler
    {
        Sample Sample();
    }

    internal class Sampler : ISampler
    {
        public Sample Sample()
        {
            var time = UnityEngine.Time.realtimeSinceStartup;
            var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            return new Sample(time, scene);
        }
    }
}