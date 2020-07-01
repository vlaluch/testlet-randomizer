using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AssessmentProject
{
    public class Tests
    {
        private Testlet target;

        private readonly List<Item> items = new List<Item>
        {
            new Item{ItemId = "item 1", ItemType = ItemTypeEnum.Operational},
            new Item{ItemId = "item 2", ItemType = ItemTypeEnum.Operational},
            new Item{ItemId = "item 3", ItemType = ItemTypeEnum.Pretest},
            new Item{ItemId = "item 4", ItemType = ItemTypeEnum.Operational},
            new Item{ItemId = "item 5", ItemType = ItemTypeEnum.Pretest},
            new Item{ItemId = "item 6", ItemType = ItemTypeEnum.Operational},
            new Item{ItemId = "item 7", ItemType = ItemTypeEnum.Operational},
            new Item{ItemId = "item 8", ItemType = ItemTypeEnum.Pretest},
            new Item{ItemId = "item 9", ItemType = ItemTypeEnum.Pretest},
            new Item{ItemId = "item 10", ItemType = ItemTypeEnum.Operational}
        };

        [Test]
        public void Randomize_ShouldGenerateSequenceWithCorrectOrder()
        {
            target = new Testlet("testlet", items);

            var result = target.Randomize();

            Assert.True(items.All(item => result.Contains(item)));

            var resultItemsWithPredefinedPosition = result.Take(TestletConstants.NumberOfPretestItemsWithPredefinedPosition);
            Assert.True(resultItemsWithPredefinedPosition.All(item => item.ItemType == ItemTypeEnum.Pretest));

            var remainingItems = items.Skip(TestletConstants.NumberOfPretestItemsWithPredefinedPosition);
            var remainingResultItems = result.Skip(TestletConstants.NumberOfPretestItemsWithPredefinedPosition);
            Assert.False(remainingItems.SequenceEqual(remainingResultItems));
        }

        [Test]
        public void Randomize_ShouldGenerateDifferentSequencesEveryTime()
        {
            target = new Testlet("testlet", items);

            var firstResult = target.Randomize();
            var secondResult = target.Randomize();
            Assert.False(firstResult.SequenceEqual(secondResult));
        }


        [Test]
        public void Randomize_WorksWithEmptyItemsList()
        {
            target = new Testlet("testlet", new List<Item>());
            target.Randomize();
            Assert.Pass();
        }

        [Test]
        public void Randomize_WorksWithTooSmallCollection()
        {
            var sourceItems = new List<Item>
            {
                new Item{ItemId = "item 1", ItemType = ItemTypeEnum.Operational},
                new Item{ItemId = "item 2", ItemType = ItemTypeEnum.Pretest}
            };

            target = new Testlet("testlet", sourceItems);
            target.Randomize();

            // Items should be shuffled, but for just 2 elements result of randomize will often produce the same sequence
            // That's why only check that it works without exceptions
            Assert.Pass();
        }
    }
}