namespace UndertaleModTool.ViewModels.Solution;

public class MainViewModel
{
    public SolutionViewModel Solution { get; private set; }

    public MainViewModel()
    {
        Solution = new SolutionViewModel("My Solution");

        var project1 = new ProjectViewModel("Project1");
        project1.Items.Add(new ItemViewModel("File1.cs"));
        project1.Items.Add(new ItemViewModel("File2.cs"));

        var folder = new ItemViewModel("Folder1");
        folder.Children.Add(new ItemViewModel("File3.cs"));
        project1.Items.Add(folder);

        Solution.Projects.Add(project1);

        var project2 = new ProjectViewModel("Project2");
        project2.Items.Add(new ItemViewModel("File4.cs"));
        Solution.Projects.Add(project2);
    }
}