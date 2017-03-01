namespace Dashboard
{
    internal interface IGesture
    {
        void SampleOrCancel();
        bool CheckAndClear();
    }
}