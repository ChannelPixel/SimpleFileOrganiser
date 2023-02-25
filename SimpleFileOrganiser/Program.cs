using System.Transactions;

namespace SimpleFileOrganiser {
    internal class Program {

        static async Task Main(string[] args) {

            string scanDirectoryPath = string.Empty;
            string newDirectoryPath = string.Empty;
            string newFolderName = string.Empty;
            int filesPerNewFolder = 30;

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

            Console.WriteLine();

            while (string.IsNullOrWhiteSpace(newFolderName)) {
                Console.WriteLine("Enter the name of the folder you would like to make :: ");
                newFolderName = Console.ReadLine();
            }

            var files = Directory.EnumerateFiles(scanDirectoryPath, "*", new EnumerationOptions() { RecurseSubdirectories = true });
            var existingFileInfos = files.Select(f => new FileInfo(f)).OrderBy(f => f.CreationTimeUtc).ToList();

            //int subDirectorySuffix = 0;
            string newDirectoryFolderPath = newDirectoryPath + "\\" + newFolderName;

            if (!Directory.Exists(newDirectoryFolderPath)) {
                Directory.CreateDirectory(newDirectoryFolderPath); ;
            }

            var neededFolderCount = (existingFileInfos.Count() / filesPerNewFolder) + 1;
            var fileListIndex = 0;

            for (int subIndex = 1; subIndex < neededFolderCount; subIndex++) {
                var subDirectoryPath = newDirectoryFolderPath + "\\" + newFolderName + subIndex.ToString("000");

                if (!Directory.Exists(subDirectoryPath)) {
                    Directory.CreateDirectory(subDirectoryPath);
                }

                var fileSuffix = 0;
                while(fileSuffix < filesPerNewFolder) {
                    File.Copy(existingFileInfos[fileListIndex].FullName, $"{subDirectoryPath}\\{newFolderName}{fileSuffix.ToString("000")}{existingFileInfos[fileListIndex].Extension}");
                    fileSuffix++;
                    fileListIndex++;
                }
            }
            Console.WriteLine("Bing bang boom.");
            return;
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