namespace myMD.Model.ParserModel
{
    /// <summary>
    /// Diese Klasse enthält Methoden, die als Erweiterungen auf Werten des FileFormat enums aufgerufen werden können.
    /// </summary>
    /// <see>myMD.Model.ParserModel.FileFormat</see>
    public static class FileFormatExtensions
    {
        /// <summary>
        /// Wählt einen FileToDatabaseParser basierend auf dem gegebene Dateiformat
        /// </summary>
        /// <param name="format">Das Dateiformat auf dem die Methode aufgerufen wird</param>
        /// <returns>Den FileToDatabaseParser für dieses Dateiformat oder null falls kein solcher Parser existiert</returns>
        public static FileToDatabaseParser GetFileToDatabaseParser(this FileFormat format)
        {
            switch (format)
            {
                case FileFormat.hl7:
                    return new Hl7ToDatabaseParser();
                default:
                    return null;
            }
        }
    }
}