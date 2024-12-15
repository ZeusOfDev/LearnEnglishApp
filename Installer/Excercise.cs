namespace LearnWordApp.Installer
{
    public abstract class Excercise
    {
        public static List<Type> excerciseTypes = new List<Type>(); 
        public static void InstallExcercise()
        {
            typeof(Program).Assembly.ExportedTypes.ToList().ForEach((Type type) =>
            {
                if (!type.IsAbstract && !type.IsInterface && type.IsAssignableTo(typeof(Excercise)))
                    excerciseTypes.Add(type);
            });
        }
    }
}
