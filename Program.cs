namespace PPSSPPTextureDumpTool
{
    class Program
    {
        static string currentDirectory => Directory.GetCurrentDirectory();
        static string outputFile => Path.Combine(currentDirectory, "textures.txt");
        
        static void Main()
        {
            if(File.Exists(outputFile))
                File.Delete(outputFile);
            ProcessTextures();
        }

        static void ProcessTextures()
        {
            string lastDirectory = string.Empty;
            int counter = 0;
            
            foreach (string file in Directory.GetFiles(currentDirectory, "*.png", SearchOption.AllDirectories))
            {
                string fileName = Path.GetFileName(file);
                string? directory = Path.GetDirectoryName(file);
                string relativePath = "";

                if(directory != null)
                {
                    relativePath = Path.GetRelativePath(currentDirectory, directory.ToString()).Replace("\\", "/");
                }

                PreProcessAnimations(ref lastDirectory, ref counter, file, ref fileName, relativePath);
                TextOutput(fileName, relativePath);
                FileRename(fileName, relativePath);
            }
        }

        static void PreProcessAnimations(ref string lastDirectory, ref int counter, string file, ref string fileName, string relativePath)
        {
            if (relativePath.EndsWith("anim") && fileName.Length == 28)
            {
                if (lastDirectory != relativePath)
                {
                    lastDirectory = relativePath;
                    counter = 0;
                }

                string number = counter.ToString("D3");
                fileName = $"{fileName.Insert(fileName.Length - 4, $" {number}")}";
                string newFilePath = Path.Combine(relativePath, fileName);
                File.Move(file, newFilePath);
                counter++;
            }
        }

        static void TextOutput(string fileName, string relativePath)
        {
            string textureHash = fileName.Substring(0, 24);
            string outputLine;
            if (fileName.Length >= 29)
            {
                fileName = fileName.Substring(24);// + " = " + relativePath;
                if (fileName[0] == ' ')
                {
                    fileName = fileName.Substring(1);
                }
            }
            outputLine = $"{textureHash} = {relativePath}/{fileName}";
            File.AppendAllText(outputFile, outputLine + Environment.NewLine);
        }
        
        static void FileRename(string fileName, string relativePath)
        {
            string oldFileName = fileName;
            if (fileName.Length >= 29)
            {
                fileName = fileName.Substring(24);// + " = " + relativePath;
                if (fileName[0] == ' ')
                {
                    fileName = fileName.Substring(1);
                }
                File.Move($"{relativePath}/{oldFileName}", $"{relativePath}/{fileName}");
            }
        }
    }
}