using System;

namespace myMD.Model.ParserModel
{
    /// <summary>
    /// 
    /// </summary>
    static class FileFormatExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
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


