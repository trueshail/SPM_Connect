namespace SearchDataSPM
{
    internal class Stats
    {
        public int Accurate;
        public int Correct;
        public int Missed;
        public int Total;

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