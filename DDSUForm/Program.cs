namespace DDSUForm
{
    internal static class Program
    {
        // This is the method to run when the timer is raised.
            /// <summary>
            ///  The main entry point for the application.
            /// </summary>
            [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}