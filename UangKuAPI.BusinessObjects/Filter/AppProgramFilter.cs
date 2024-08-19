namespace UangKuAPI.BusinessObjects.Filter
{
    public class AppProgramFilter : Base
    {
        public string ProgramID { get; set; }
        public int FirstID { get; set; }
        public int MiddleID { get; set; }
        public int LastID { get; set; }
        public AppProgramFilter() : base()
        {
            ProgramID = string.Empty;
            FirstID = 0;
            MiddleID = 0;
            LastID = 0;
        }

        public AppProgramFilter(int pageNumber, int pageSize, string? programID, int firstID, int middleID, int lastID) : base(pageNumber, pageSize)
        {
            ProgramID = !string.IsNullOrEmpty(programID) ? programID : string.Empty;
            FirstID = firstID;
            MiddleID = middleID;
            LastID = lastID;
        }
    }
}
