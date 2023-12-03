namespace CommitionalConvents
{
    public partial record Issue
    {
        public Issue.Type IssueType { get; private set; }
        public float Size { get; private set; }
        
        Issue(float size, Issue.Type type)
        {
            Size = size;
            IssueType = type;
        }

        internal static Issue Emerge(float size, Type type)
            => new(size, type);
    }
}