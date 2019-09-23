namespace BelaSopa.Models.DomainModels.Assistente
{
    public enum Dificuldade
    {
        Facil,
        Media,
        Dificil
    }

    public static class DificuldadeMetodos
    {
        public static string GetNome(this Dificuldade dificuldade)
        {
            switch (dificuldade)
            {
                case Dificuldade.Facil: return "Fácil";
                case Dificuldade.Media: return "Média";
                case Dificuldade.Dificil: return "Difícil";

                default: return null;
            }
        }
    }
}
