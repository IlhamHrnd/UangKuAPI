namespace UangKuAPI.BusinessObjects.Filter
{
    public class AppProgramFilter : Base.Base
    {
        public string? ProgramID { get; set; }
        public bool? IsVisible { get; set; }
        public bool? IsUsedBySystem { get; set; }
        public AppProgramFilter()
        {
            ProgramID = string.Empty;
            IsVisible = null;
            IsUsedBySystem = null;
        }

        public AppProgramFilter(int pageNumber, int pageSize, string programID, bool? isVisible, bool? isUsedBySystem) : base(pageNumber, pageSize)
        {
            ProgramID = !string.IsNullOrEmpty(programID) ? programID : string.Empty;
            IsVisible = isVisible.HasValue ? isVisible : null;
            isUsedBySystem = isUsedBySystem.HasValue ? isUsedBySystem : null;
        }
    }
}