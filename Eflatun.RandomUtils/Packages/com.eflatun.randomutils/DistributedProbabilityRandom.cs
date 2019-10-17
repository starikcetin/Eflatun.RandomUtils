using System;
using System.Collections.Generic;
using System.Linq;

namespace Eflatun.RandomUtils
{
    //
    // Original Source: https://stackoverflow.com/a/9958717/6301627
    // Slightly modified.
    //

    /// <summary>
    /// Distributed Probability Random Number Generator (Just like a cheated/weighted dice)
    /// </summary>
    public class DistributedProbabilityRandom
    {
        // Usage Example:
        //  var disProbRandom = new DistributedProbabilityRandom (new int[]{150,40,15,3});   // list of probabilities for each number: 0 is 150, 1 is 40, and so on.
        //  int number = disProbRandom.nextValue();                           // return a number from 0-3 according to given probabilities; the number can be an index to another array, if needed.

        /// <summary>
        /// Initializes a new loaded die. Probs
        /// is an array of numbers indicating the relative
        /// probability of each choice relative to all the
        /// others. For example, if probs is [3,4,2], then
        /// the chances are 3/9, 4/9, and 2/9, since the
        /// probabilities add up to 9.
        /// </summary>
        public DistributedProbabilityRandom(int probs, int seed)
        {
            _prob = new List<long>();
            _alias = new List<int>();
            _total = 0;
            _n = probs;
            _even = true;
            _random = new System.Random(seed);
        }

        private readonly System.Random _random;
        private readonly List<long> _prob;
        private readonly List<int> _alias;
        private readonly long _total;
        private readonly int _n;
        private readonly bool _even;

        public DistributedProbabilityRandom(IEnumerable<int> probs, int seed)
        {
            // Raise an error if null
            if (probs == null) throw new ArgumentNullException("probs");
            _prob = new List<long>();
            _alias = new List<int>();
            _total = 0;
            _even = false;
            _random = new System.Random(seed);
            var small = new List<int>();
            var large = new List<int>();
            var tmpprobs = probs.Select(p => (long) p).ToList();

            _n = tmpprobs.Count;

            // Get the max and min choice and calculate _total
            long mx = -1, mn = -1;
            foreach (var p in tmpprobs)
            {
                if (p < 0) throw new ArgumentException("probs contains a negative probability.");
                mx = (mx < 0 || p > mx) ? p : mx;
                mn = (mn < 0 || p < mn) ? p : mn;
                _total += p;
            }

            // We use a shortcut if all probabilities are equal
            if (mx == mn)
            {
                _even = true;
                return;
            }

            // Clone the probabilities and scale them by
            // the number of probabilities
            for (var i = 0; i < tmpprobs.Count; i++)
            {
                tmpprobs[i] *= _n;
                _alias.Add(0);
                _prob.Add(0);
            }

            // Use Michael Vose's _alias method
            for (var i = 0; i < tmpprobs.Count; i++)
            {
                if (tmpprobs[i] < _total)
                    small.Add(i); // Smaller than probability sum
                else
                    large.Add(i); // Probability sum or greater
            }

            // Calculate probabilities and aliases
            while (small.Count > 0 && large.Count > 0)
            {
                var l = small[small.Count - 1];
                small.RemoveAt(small.Count - 1);
                var g = large[large.Count - 1];
                large.RemoveAt(large.Count - 1);
                _prob[l] = tmpprobs[l];
                _alias[l] = g;
                var newprob = (tmpprobs[g] + tmpprobs[l]) - _total;
                tmpprobs[g] = newprob;
                if (newprob < _total)
                    small.Add(g);
                else
                    large.Add(g);
            }

            foreach (var g in large)
                _prob[g] = _total;
            foreach (var l in small)
                _prob[l] = _total;
        }

        /// <summary>
        /// Returns the number of choices.
        /// </summary>
        public int Count
        {
            get { return _n; }
        }

        /// <summary>
        /// Chooses a choice at _random, ranging from 0 to the number of choices minus 1.
        /// </summary>
        public int NextValue()
        {
            var i = _random.Next(_n);
            return (_even || _random.Next((int) _total) < _prob[i]) ? i : _alias[i];
        }
    }
}
