namespace BelaSopa.Models.DomainModels.Assistente
{
    public enum DiaDaSemana
    {
        Domingo,
        SegundaFeira,
        TercaFeira,
        QuartaFeira,
        QuintaFeira,
        SextaFeira,
        Sabado
    }

    public static class DiaDaSemanaMetodos
    {
        public static string GetNome(this DiaDaSemana diaDaSemana)
        {
            switch (diaDaSemana)
            {
                case DiaDaSemana.Domingo: return "Domingo";
                case DiaDaSemana.SegundaFeira: return "Segunda-feira";
                case DiaDaSemana.TercaFeira: return "Terça-feira";
                case DiaDaSemana.QuartaFeira: return "Quarta-feira";
                case DiaDaSemana.QuintaFeira: return "Quinta-feira";
                case DiaDaSemana.SextaFeira: return "Sexta-feira";
                case DiaDaSemana.Sabado: return "Sábado";

                default: return null;
            }
        }
    }
}
