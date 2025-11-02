namespace DSA.Shared.Models
{
    public enum ItemStatus
    {
        Default,
        Reference,
        Comparing,
        Sorted
    }

    public class VisualItem
    {
        public int Value { get; set; }
        public ItemStatus Status { get; set; }
    }
}
