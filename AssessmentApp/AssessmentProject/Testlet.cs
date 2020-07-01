using System;
using System.Collections.Generic;
using System.Linq;

namespace AssessmentProject
{
    public class Testlet
    {
        private List<Item> Items { get; }
        public string TestletId { get; }

        public Testlet(string testletId, List<Item> items)
        {
            TestletId = testletId;
            Items = items;
        }

        public List<Item> Randomize()
        {
            if (Items.Count <= 1)
            {
                return Items;
            }

            var itemsWithPredefinedPositionMaxCount = TestletConstants.NumberOfPretestItemsWithPredefinedPosition;

            if (Items.Count <= itemsWithPredefinedPositionMaxCount)
            {
                itemsWithPredefinedPositionMaxCount = 0;
            }

            var randomizedIndexes = GenerateRandomSequence(Items.Count - itemsWithPredefinedPositionMaxCount - 1);

            var shuffledItems = new Item[Items.Count];
            var itemsWithPredefinedPositionCount = 0;
            var index = 0;

            foreach (var item in Items)
            {
                if (item.ItemType == ItemTypeEnum.Pretest && itemsWithPredefinedPositionCount < itemsWithPredefinedPositionMaxCount)
                {
                    shuffledItems[itemsWithPredefinedPositionCount] = item;
                    itemsWithPredefinedPositionCount++;
                }
                else
                {
                    shuffledItems[randomizedIndexes[index] + itemsWithPredefinedPositionMaxCount] = item;
                    index++;
                }
            }

            return shuffledItems.ToList();
        }

        private static List<int> GenerateRandomSequence(int maxValue)
        {
            var values = new List<int>();

            for (var i = 0; i <= maxValue; i++)
            {
                values.Add(i);
            }

            var sequence = new List<int>();

            var randomGenerator = new Random();

            while (values.Count > 0)
            {
                var index = randomGenerator.Next(0, values.Count);
                sequence.Add(values[index]);
                values.RemoveAt(index);
            }

            return sequence;
        }
    }
}
