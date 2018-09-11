namespace ImageRebuild
{   
    /// <summary>
    /// Class containing main program.
    /// </summary>
    public class ImageRebuild
    {
        /// <summary>
        /// Main program.
        /// </summary>
        /// <param name="args">Commands entered.</param>
        public static void Main(string[] args)
        {
            Controller controller = new Controller();
            controller.ExecuteProgram(args);
        }
    }
}