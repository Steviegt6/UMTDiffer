using System.Windows.Controls;

using AakStudio.Shell.UI.Showcase.Shell;

namespace UndertaleModTool.ViewModels.Solution;

internal class MainViewModel : AakAnchorable
{
    public SolutionViewModel Solution { get; private set; }

    public MainViewModel(WorkSpaceViewModel parent)
    {
        Title    = "My Solution";
        Solution = new SolutionViewModel("My Solution");

        var project1 = new ProjectViewModel("Project1", new Button(), "Project 1", parent);
        project1.Items.Add(new ItemViewModel("File1.cs", new Button(), "File 1", parent));
        project1.Items.Add(new ItemViewModel("File2.cs", new Button(), "File 2", parent));

        var folder = new ItemViewModel("Folder1",         new Button(), "Folder 1", parent);
        folder.Children.Add(new ItemViewModel("File3.cs", new Button(), "File 3",   parent));
        project1.Items.Add(folder);

        Solution.Projects.Add(project1);

        var project2 = new ProjectViewModel("Project2", new Button(), "Project 2", parent);
        project2.Items.Add(new ItemViewModel("File4.cs", new Button(), "File 4", parent));
        Solution.Projects.Add(project2);
        
        View = new SolutionView { DataContext = Solution };
    }
}