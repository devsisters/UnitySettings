namespace Settings
{
    internal interface IGesture
    {
        void SampleOrCancel();
        bool CheckAndClear();
    }
}