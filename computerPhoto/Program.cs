//此文件用于指定唯一的一个启动框体
namespace computerPhoto
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {

            ApplicationConfiguration.Initialize();
            Application.Run(new 计算器());
        }
    }
}