namespace JasperCloud.Models;

public class FileMetadata
{
    required public string name;
    required public string fileType;

    public DateTime dateCreated;
    public DateTime dateModified;

    public int size = 0x7FFFFFFF;
}