namespace PirateCave.Enums
{
    /// <summary>
    /// Todas as cahves utilizadas para guardar os dados no PlayerPrefs
    /// </summary>
    public static class PlayerPrefsKeys
    {
        /// <summary>
        /// Chave utilizada para armazenar os dados do jogador que está logado
        /// </summary>
        public static string Player = "player";

        /// <summary>
        /// Chave utilizada para armazenar pontos que já foram salvos no banco de dados
        /// </summary>
        public static string PlayerPoints = "playerPoints";

        /// <summary>
        /// Chave utilizada para armazenar pontos que ainda não foram salvos no banco de dados
        /// </summary>
        public static string PointsToSave = "pointsToSave";
    }
}