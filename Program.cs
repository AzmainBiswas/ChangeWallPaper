using System;
using System.Runtime.InteropServices;

namespace ChangeWallPaper
{
    // StackOverFlow Shit
    class Magician
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int HIDE = 0;
        const int SHOW = 5;

        public static void DisappearConsole()
        {
            ShowWindow(GetConsoleWindow(), HIDE);
        }
    }

    class WallPaper
    {
        // Global Constant Variable for changing the wallpaper
        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 1;
        const int SPIF_SENDCHANGE = 2;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SystemParametersInfo(int uiAction, int uiParam, string pvParam, int fWinIni);

        static void Main()
        {
            // wallpaper directory
            string pictureFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures); // uses C:\Users\{username}\Picture folder.
            string wallPaperPath = pictureFolder + @"\wallpapers"; // you can change this according to your likeings.
            // string wallPaperPath = @"E:\wallpapers"; // like this.

            if (Directory.Exists(wallPaperPath))
            {
                // Console.WriteLine("Changing WallPaper!!!!");
                Magician.DisappearConsole();
                // a array of name of all wallpapers
                string[] allWallPaper = Directory.GetFiles(wallPaperPath);
                // generate a random integer
                Random random = new Random();
                int index = random.Next(0, allWallPaper.Length);
                // random Wallpaper
                string wallPaper = allWallPaper[index];
                // set wallpaper
                SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, wallPaper, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);
                // Console.WriteLine("Wallpaper Changed to " + wallPaper);
            }
            Console.ReadKey();
        }
    }
}