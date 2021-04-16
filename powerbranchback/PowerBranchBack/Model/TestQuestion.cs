namespace PowerBranchBack.Model
{
    public class TestQuestion
    {
        public int Id { get; set; }
        public Question Question { get; set; }
        public Test Test { get; set; }
        public int UserAnswerId { get; set; }
    }
}
