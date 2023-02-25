namespace SimpleFileOrganiser {
    internal class Program {

        static async Task Main(string[] args) {
            await DirectoryIsValid();
        }

        private static async Task DirectoryIsValid() {
            bool directoryIsValid = false;
            string directoryMessage = "Enter a valid path you want to organize :: ";
            while (!directoryIsValid) {
                Console.WriteLine(directoryMessage);
                var consolePath = Console.ReadLine();
                if (Directory.Exists(consolePath)) {
                    directoryIsValid = await RootPathAndChildsHaveFiles(consolePath);
                }
                else {
                    directoryMessage = "Directory is invalid. Enter a valid path you want to organize :: ";
                }
            }
        }

        private static async Task<bool> RootPathAndChildsHaveFiles(string rootPath) {
            var files = Directory.EnumerateFiles(rootPath, "*", new EnumerationOptions() { RecurseSubdirectories = true });
            Console.WriteLine($"{files.Count()} files found in root and subdirectories in :: {rootPath}");

            return files.Count() > 0;
        }
    }
}