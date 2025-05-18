using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.IO;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<FileSystemItem> items = new();

    [ObservableProperty]
    private string currentPath = "/";

    [RelayCommand]
    public void LoadDirectory(string? path = null)
    {
        Items.Clear();
        string dir = path ?? "/";

        try
        {
            foreach (var d in Directory.GetDirectories(dir))
            {
                Items.Add(new FileSystemItem
                {
                    Name = Path.GetFileName(d),
                    Path = d,
                    Type = "Каталог",
                    Size = "—"
                });
            }

            foreach (var f in Directory.GetFiles(dir))
            {
                var fi = new FileInfo(f);
                Items.Add(new FileSystemItem
                {
                    Name = fi.Name,
                    Path = f,
                    Type = "Файл",
                    Size = $"{fi.Length} байт"
                });
            }

            CurrentPath = dir;
        }
        catch
        {
            // handle error
        }
    }
}
