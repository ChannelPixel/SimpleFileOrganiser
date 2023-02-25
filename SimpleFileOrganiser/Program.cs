using System.Transactions;

namespace SimpleFileOrganiser {
    internal class Program {

        static async Task Main(string[] args) {

            string scanDirectoryPath = string.Empty;
            string newDirectoryPath = string.Empty;
            string newFolderName = string.Empty;

            bool scanDirPathHasChildren = false;
            while (!scanDirPathHasChildren) {
                scanDirectoryPath = DirectoryIsValid("Enter a valid path you want to organize :: ");
                scanDirPathHasChildren = RootPathAndChildsHaveFiles(scanDirectoryPath);
            }

            Console.WriteLine();

            bool newFolderPathExists = false;
            while(!Directory.Exists(newDirectoryPath)) {
                newDirectoryPath = DirectoryIsValid("Enter a valid path you want to move the files to :: ");
            }

            while (string.IsNullOrWhiteSpace(newFolderName)) {
                Console.WriteLine("Enter the name of the folder you would like to make :: ");
                newFolderName = Console.ReadLine();
            }


        }

        private static string DirectoryIsValid(string consoleMessage) {
            string directoryMessage = consoleMessage;
            string consolePath = string.Empty;
            while (!Directory.Exists(consolePath)) {
                Console.WriteLine(directoryMessage);
                consolePath = Console.ReadLine();
                if (!Directory.Exists(consolePath)) {
                    Console.WriteLine("ERROR:Invalid directory.");
                }
            }

            return consolePath;
        }

        private static bool RootPathAndChildsHaveFiles(string rootPath) {
            var files = Directory.EnumerateFiles(rootPath, "*", new EnumerationOptions() { RecurseSubdirectories = true });
            Console.WriteLine($"{files.Count()} files found in root and subdirectories in :: {rootPath}");

            return files.Count() > 0;
        }
    }
}