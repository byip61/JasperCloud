namespace JasperCloud.Models
{
    public class FileSearch
    {
        public File[]? files;
        public List<string>? fileNames;

        public void FindFiles(String query, String directory)
        {
            /* TODO: 
             * Split query so individual words are searched on their own. (partial matches)
             * Concatenate string list to account for this split query.
             * Connect to database and gather file IDs, make sure found files are linked to the user via user_id.
             * Relay search results to the files variable, so that they can be displayed on the frontend.
             */
            // https://stackoverflow.com/questions/51135520/search-for-file-in-c-sharp
            fileNames = (List<string>?)Directory.EnumerateFiles(directory, "*"+(query)+"*", SearchOption.AllDirectories);
        }
    }
}
