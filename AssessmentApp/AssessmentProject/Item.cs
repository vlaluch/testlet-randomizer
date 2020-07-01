namespace AssessmentProject
{
    public class Item
    {
        public string ItemId;
        public ItemTypeEnum ItemType;

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var item = (Item)obj;
            return ItemId == item.ItemId && ItemType == item.ItemType;
        }

        public override int GetHashCode()
        {
            return ItemId.GetHashCode() + (int)ItemType + 1;
        }
    }
}
