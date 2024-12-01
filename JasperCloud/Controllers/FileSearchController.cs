using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JasperCloud.Models;

namespace JasperCloud.Controllers
{
    public class FileSearchController : Controller
    {
        public JasperCloud.Models.File[]? files;
        public HashSet<string> fileNames = [];

        //
        // Not intended to be used anywhere else; simply splits up the string given a few options.
        //
        private static string[] SplitMe(string input, bool symbols = false)
        {
            char[]? delimiters;

            if (symbols)
            {
                // Only account for spaces, and other "separator" symbols.
                delimiters = [' ', ',', '.', '_'];
            }
            else
            {
                // Include even "impossible" ASCII characters.
                delimiters = [ '!', '@', '#', '$',
                                      '%', '^', '&', '*',
                                      '(', ')', '[', ']',
                                      '{', '}', ':', ';',
                                      '\'', '\"', '\\', '/',
                                      '<', '>', '~', '`',
                                      '+', '-', '_', ' ',
                                      '.', ',' ];
            }

            string[] substrings = input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            return substrings;
        }

        public void FindFiles(string query, string directory, bool symbols = false)
        {
            /* TODO: 
             * Connect to database and gather file IDs, make sure found files are linked to the user via user_id.
             * Relay search results to the files variable, so that they can be displayed on the frontend.
             */
            // https://stackoverflow.com/questions/51135520/search-for-file-in-c-sharp

            string[] splitQuery = SplitMe(query, symbols);

            List<string> foundFilenames;

            if (fileNames == null)
            {
               fileNames = [];
            }

            fileNames.Clear();

            foreach (string substring in splitQuery)
            {
                foundFilenames = (List<string>)Directory.EnumerateFiles(directory, "*" + substring + "*", SearchOption.AllDirectories);

                if (fileNames != null)
                {
                    // TODO: O(n); optimize this
                    foreach (string fname in foundFilenames)
                    {
                        // Add the filename to our filename list.
                        fileNames.Add(fname);
                    }
                }
            }
        }
    }
}
