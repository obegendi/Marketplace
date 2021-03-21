namespace Marketplace.Domain.Product
{
    public class SubtractiveGenerator
    {
        public static int MAX = 1000000000;
        public string last;
        private int pos;
        private readonly int[] state;

        public SubtractiveGenerator(int seed)
        {
            state = new int[55];

            var temp = new int[55];
            temp[0] = mod(seed);
            temp[1] = 1;
            for (var i = 2; i < 55; ++i)
                temp[i] = mod(temp[i - 2] - temp[i - 1]);

            for (var i = 0; i < 55; ++i)
                state[i] = temp[34 * (i + 1) % 55];

            pos = 54;
            for (var i = 55; i < 220; ++i)
                last = next().ToString();
        }
        private int mod(int n)
        {
            return (n % MAX + MAX) % MAX;
        }

        public int next()
        {
            var temp = mod(state[(pos + 1) % 55] - state[(pos + 32) % 55]);
            pos = (pos + 1) % 55;
            state[pos] = temp;
            return temp;
        }
    }
}
