using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;

namespace DIPMADE
{
    /// <summary>
    /// Ensemble des méthodes de simplification du code
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Récupérer le mime,le ContentType et une Description par rapport à une extension.
        /// </summary>
        /// <param name="ext">Extension du fichier</param>
        /// <param name="Description">Description de l'extension (entrée\sortie)</param>
        public static string GetMime(string ext, out string Description)
        {
            string Mime = "application/octet-stream";

            Description = "";
            switch (ext)
            {
                case ".aac":
                    Description = "fichier audio AAC";
                    Mime = "audio/aac";
                    break;
                case ".abw":
                    Description = "document AbiWord";
                    Mime = "application/x-abiword";
                    break;
                case ".arc":
                    Description = "archive (contenant plusieurs fichiers)";
                    Mime = "application/octet-stream";
                    break;
                case ".avi":
                    Description = "AVI : Audio Video Interleave";
                    Mime = "video/x-msvideo";
                    break;
                case ".azw":
                    Description = "format pour eBook Amazon Kindle";
                    Mime = "application/vnd.amazon.ebook";
                    break;
                case ".bin":
                    Description = "n'importe quelle donnée binaire";
                    Mime = "application/octet-stream";
                    break;
                case ".bmp":
                    Description = "Images bitmap Windows OS/2";
                    Mime = "image/bmp";
                    break;
                case ".bz":
                    Description = "archive BZip";
                    Mime = "application/x-bzip";
                    break;
                case ".bz2":
                    Description = "archive BZip2";
                    Mime = "application/x-bzip2";
                    break;
                case ".csh":
                    Description = "script C-Shell";
                    Mime = "application/x-csh";
                    break;
                case ".css":
                    Description = "fichier Cascading Style Sheets (CSS)";
                    Mime = "text/css";
                    break;
                case ".csv":
                    Description = "fichier Comma-separated values (CSV)";
                    Mime = "text/csv";
                    break;
                case ".doc":
                    Description = "Microsoft Word";
                    Mime = "application/msword";
                    break;
                case ".docx":
                    Description = "Microsoft Word (OpenXML)";
                    Mime = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;
                case ".eot":
                    Description = "police MS Embedded OpenType";
                    Mime = "application/vnd.ms-fontobject";
                    break;
                case ".epub":
                    Description = "fichier Electronic publication (EPUB)";
                    Mime = "application/epub+zip";
                    break;
                case ".gif":
                    Description = "fichier Graphics Interchange Format (GIF)";
                    Mime = "image/gif";
                    break;
                case ".htm":
                    Description = "fichier HyperText Markup Language (HTML)";
                    Mime = "text/html";
                    break;
                case ".html":
                    Description = "";
                    Mime = "";
                    break;
                case ".ico":
                    Description = "icône";
                    Mime = "image/x-icon";
                    break;
                case ".ics":
                    Description = "élément iCalendar";
                    Mime = "text/calendar";
                    break;
                case ".jar":
                    Description = "archive Java (JAR)";
                    Mime = "application/java-archive";
                    break;
                case ".jpeg":
                    Description = "image JPEG";
                    Mime = "image/jpeg";
                    break;
                case ".jpg":
                    Description = "";
                    Mime = "";
                    break;
                case ".js":
                    Description = "JavaScript (ECMAScript)";
                    Mime = "application/javascript";
                    break;
                case ".json":
                    Description = "donnée au format JSON";
                    Mime = "application/json";
                    break;
                case ".mid":
                    Description = "fichier audio Musical Instrument Digital Interface (MIDI)";
                    Mime = "audio/midi";
                    break;
                case ".midi":
                    Description = "";
                    Mime = "";
                    break;
                case ".mpeg":
                    Description = "vidéo MPEG";
                    Mime = "video/mpeg";
                    break;
                case ".mpkg":
                    Description = "paquet Apple Installer";
                    Mime = "application/vnd.apple.installer+xml";
                    break;
                case ".odp":
                    Description = "présentation OpenDocument";
                    Mime = "application/vnd.oasis.opendocument.presentation";
                    break;
                case ".ods":
                    Description = "feuille de calcul OpenDocument";
                    Mime = "application/vnd.oasis.opendocument.spreadsheet";
                    break;
                case ".odt":
                    Description = "document texte OpenDocument";
                    Mime = "application/vnd.oasis.opendocument.text";
                    break;
                case ".oga":
                    Description = "fichier audio OGG";
                    Mime = "audio/ogg";
                    break;
                case ".ogv":
                    Description = "fichier vidéo OGG";
                    Mime = "video/ogg";
                    break;
                case ".ogx":
                    Description = "OGG";
                    Mime = "application/ogg";
                    break;
                case ".otf":
                    Description = "police OpenType";
                    Mime = "font/otf";
                    break;
                case ".png":
                    Description = "fichier Portable Network Graphics";
                    Mime = "image/png";
                    break;
                case ".pdf":
                    Description = "Adobe Portable Document Format (PDF)";
                    Mime = "application/pdf";
                    break;
                case ".ppt":
                    Description = "présentation Microsoft PowerPoint";
                    Mime = "application/vnd.ms-powerpoint";
                    break;
                case ".pptx":
                    Description = "présentation Microsoft PowerPoint (OpenXML)";
                    Mime = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                    break;
                case ".rar":
                    Description = "archive RAR";
                    Mime = "application/x-rar-compressed";
                    break;
                case ".rtf":
                    Description = "Rich Text Format (RTF)";
                    Mime = "application/rtf";
                    break;
                case ".sh":
                    Description = "script shell";
                    Mime = "application/x-sh";
                    break;
                case ".svg":
                    Description = "fichier Scalable Vector Graphics (SVG)";
                    Mime = "image/svg+xml";
                    break;
                case ".swf":
                    Description = "fichier Small web format (SWF) ou Adobe Flash";
                    Mime = "application/x-shockwave-flash";
                    break;
                case ".tar":
                    Description = "fichier d'archive Tape Archive (TAR)";
                    Mime = "application/x-tar";
                    break;
                case ".tif":
                    Description = "image au format Tagged Image File Format (TIFF)";
                    Mime = "image/tiff";
                    break;
                case ".tiff":
                    Description = "";
                    Mime = "";
                    break;
                case ".ts":
                    Description = "fichier Typescript";
                    Mime = "application/typescript";
                    break;
                case ".ttf":
                    Description = "police TrueType";
                    Mime = "font/ttf";
                    break;
                case ".vsd":
                    Description = "Microsoft Visio";
                    Mime = "application/vnd.visio";
                    break;
                case ".wav":
                    Description = "Waveform Audio Format";
                    Mime = "audio/x-wav";
                    break;
                case ".weba":
                    Description = "fichier audio WEBM";
                    Mime = "audio/webm";
                    break;
                case ".webm":
                    Description = "fichier vidéo WEBM";
                    Mime = "video/webm";
                    break;
                case ".webp":
                    Description = "image WEBP";
                    Mime = "image/webp";
                    break;
                case ".woff":
                    Description = "police Web Open Font Format (WOFF)";
                    Mime = "font/woff";
                    break;
                case ".woff2":
                    Description = "police Web Open Font Format (WOFF)";
                    Mime = "font/woff2";
                    break;
                case ".xhtml":
                    Description = "XHTML";
                    Mime = "application/xhtml+xml";
                    break;
                case ".xls":
                    Description = "Microsoft Excel";
                    Mime = "application/vnd.ms-excel";
                    break;
                case ".xlsx":
                    Description = "Microsoft Excel (OpenXML)";
                    Mime = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
                case ".xml":
                    Description = "XML";
                    Mime = "application/xml";
                    break;
                case ".xul":
                    Description = "XUL";
                    Mime = "application/vnd.mozilla.xul+xml";
                    break;
                case ".zip":
                    Description = "archive ZIP";
                    Mime = "application/zip";
                    break;
                case ".3gp":
                    Description = "conteneur audio/vidéo 3GPP";
                    Mime = "video/3gpp";
                    break;
                case ".3g2":
                    Description = "conteneur audio/vidéo 3GPP2";
                    Mime = "video/3gpp2";
                    break;
                case ".7z":
                    Description = "archive 7-zip";
                    Mime = "application/x-7z-compressed";
                    break;
                case ".mp3":
                    Description = "fichier audio MP3";
                    Mime = "audio/mpeg";
                    break;
                case ".mp4":
                    Description = "fichier video MP4";
                    Mime = "video/mp4";
                    break;
                case ".wmv":
                    Description = "fichier video Windows Media Video";
                    Mime = "video/x-ms-wmv";
                    break;
                case ".mov":
                    Description = "fichier vidéo Quicktime";
                    Mime = "video/quicktime";
                    break;
                case ".flv":
                    Description = "fichier Flash Video";
                    Mime = "video/x-flv";
                    break;
                default:
                    Mime = "text/plain";
                    break;
            }
            return Mime;
        }


        /// <summary>
        /// Génére un mot de passe aléatoire
        /// </summary>
        /// <param name="NBCaractere">Nombre de caractére souhaité</param>
        /// <param name="LettresSpecials">Booléen définissant si l'on souhaite les lettes spécials (éèëêâäàöôîïçùûü) </param>
        /// <param name="CaracteresSpeciaux">Booléen définissant si l'on souhaite les caratères spéciaux (\/\"?,-_#{'([|@])}=+$£%*!:,)</param>
        /// <param name="ListeLettresSpeciales">Modification de la liste des lettres spéciales</param>
        /// <param name="ListeCaracteresSpeciaux">Modification de la liste des caractères spéciaux</param>
        public static string GeneratePassword(int NBCaractere = 8
            , bool LettresSpecials = false
            , bool CaracteresSpeciaux = true
            , string ListeLettresSpeciales = null
            , string ListeCaracteresSpeciaux = null
            )
        {
            string sMinuscule = "azertnwxcvbyuiophjklmqsdfg";
            string sMajuscule = sMinuscule.ToUpper();
            string sLettresSpeciales = "éèëêâäàöôîïçùûü";
            if (ListeLettresSpeciales != null) sLettresSpeciales = ListeLettresSpeciales;
            string sCaracteresSpeciaux = "&\\/\"?,-_#{'([|@])}=+$£%*!:,";
            if (ListeCaracteresSpeciaux != null) sCaracteresSpeciaux = ListeCaracteresSpeciaux;

            Random aleatoire = new Random();

            int boucle = aleatoire.Next(10);
            for (int i = 0; i < boucle; i++)
            {
                aleatoire.Next(26);
            }

            int info = 5;

            string MotPasse = "";
            int milieu = NBCaractere / 2;
            bool[] bInfo = { false, false, false, false, false };
            for (int i = 0; i < NBCaractere; i++)
            {
               int type = aleatoire.Next(info);

                if(i >= milieu)
                {
                    if (bInfo[0] == false)
                        type = 0;
                    else if (bInfo[1] == false)
                        type = 1;
                    else if (bInfo[2] == false)
                        type = 2;
                    else if (LettresSpecials && bInfo[3] == false)
                        type = 3;
                    else if (CaracteresSpeciaux && bInfo[4] == false)
                        type = 4;
                }

                int val;
                switch (type)
                {
                    case 0:
                        bInfo[0] = true;
                        val = aleatoire.Next(26);
                        MotPasse += sMinuscule[val];
                        break;
                    case 1:
                        bInfo[1] = true;
                        val = aleatoire.Next(26);
                        MotPasse += sMajuscule[val];
                        break;
                    case 2:
                        bInfo[2] = true;
                        val = aleatoire.Next(10);
                        MotPasse += val + "";
                        break;
                    case 3:
                        if (LettresSpecials)
                        {
                            bInfo[3] = true;
                            val = aleatoire.Next(sLettresSpeciales.Length);
                            MotPasse += sLettresSpeciales[val];
                        }
                        else
                            i--;
                        break;
                    case 4:
                        if (CaracteresSpeciaux)
                        {
                            bInfo[4] = true;
                            val = aleatoire.Next(sCaracteresSpeciaux.Length);
                            MotPasse += sCaracteresSpeciaux[val];
                        }
                        else
                            i--;
                        break;
                    default:
                        val = aleatoire.Next(26);
                        MotPasse += sMinuscule[val];
                        break;
                }
            }


            return MotPasse;
        }

        /// <summary>
        /// Convertit une chaine en réel ( méthode implémenté directement au String)
        /// Permet de gérer le "." et la ","
        /// SI il y a un "%", la valeur est divisé par 100
        /// </summary>
        public static double ConvertReal(this String monstring)
        {
            bool Prc = monstring.Contains("%");
            string tmp = monstring.Replace(",", ".").Replace("%", "");
            double r = Convert.ToDouble(tmp, CultureInfo.InvariantCulture);
            return Prc ? r / 100.0 : r;
        }

        /// <summary>
        /// Convertit un IEnumerable(Int64) en SqlParametre
        /// Permet d'envoyer une liste à une commande SQL
        /// Voir notre site internet pour l'ajustement dans la base de données
        /// </summary>
        public static SqlParameter ToDataTableS<T>(this IEnumerable<T> lst, string NomClasse = @"[dbo].[TypeList]", string NomVariable = "Id", string NomParametre = "@list") where T : struct
        {
            var table = new DataTable();
            table.Columns.Add(NomVariable, typeof(T));

            foreach (var item in lst)
            {
                table.Rows.Add(item.ToString());
            }

            var pList = new SqlParameter(NomParametre, SqlDbType.Structured);
            pList.TypeName = NomClasse;
            pList.Value = table;

            return pList;
        }

        /// <summary>
        /// Convertit un IEnumerable(T) en SqlParametre
        /// Permet d'envoyer une liste à une commande SQL
        /// Voir notre site internet pour l'ajustement dans la base de données
        /// </summary>
        public static SqlParameter ToDataTableC<T>(this IEnumerable<T> lst, string NomClasse = @"[dbo].[TypeList]", string NomParametre = "@list") where T : class
        {
            System.Type tp = typeof(T);

            var table = new DataTable();
            if (tp == typeof(string))
            {
                table.Columns.Add("s", tp);
                foreach (var item in lst)
                {
                    table.Rows.Add(item.ToString());
                }
            }
            else
            {
                var ls = tp.GetFields();
                foreach (var item in ls)
                {
                    //Debug.WriteLine(item.Name + " " + item.FieldType+ " ");
                    table.Columns.Add(item.Name, item.FieldType);
                }
                foreach (var item in lst)
                {
                    object[] o = new object[ls.Length];
                    int nb = 0;

                    foreach (var item2 in ls)
                    {
                        o[nb] = item2.GetValue(item);
                        nb++;
                    }

                    table.Rows.Add(o);
                }
            }


            var pList = new SqlParameter(NomParametre, SqlDbType.Structured);
            pList.TypeName = NomClasse;
            pList.Value = table;

            return pList;
        }
    }
}
