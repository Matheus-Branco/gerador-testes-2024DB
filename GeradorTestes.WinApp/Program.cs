using GeradorTestes.Dominio.ModuloDisciplina;

namespace GeradorTestes.WinApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new TelaPrincipalForm());

            GeradorDeTestesDBContext dBContext = new GeradorDeTestesDBContext();

            List<Disciplina> disciplinas = dBContext.Disciplinas.Include(x => x.Materias).ToList();

            foreach (var d in disciplinas)
            {
                Console.WriteLine(d);

                foreach (var m in d.Materias)
                {
                    Console.WriteLine(m);
                }
            }
            Console.ReadLine();
        }
    }
}