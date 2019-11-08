namespace SearchDataSPM
{
    internal class Stats
    {
        public int Total = 0;
        public int Missed = 0;
        public int Accurate = 0;
        public int Correct = 0;

        public void Update(bool correctKey)
        {
            Total++;

            if (!correctKey)
            {
                Missed++;
            }
            else
            {
                Correct++;
            }
            Accurate = 100 * Correct / Total;
        }
    }
}