namespace BelaSopa.Models.DomainModels.Assistente
{
    public enum RefeicaoDoDia
    {
        Almoco,
        Jantar
    }

    public static class RefeicaoDoDiaMetodos
    {
        public static string GetNome(this RefeicaoDoDia refeicaoDoDia)
        {
            switch (refeicaoDoDia)
            {
                case RefeicaoDoDia.Almoco: return "Almoço";
                case RefeicaoDoDia.Jantar: return "Jantar";

                default: return null;
            }
        }
    }
}
