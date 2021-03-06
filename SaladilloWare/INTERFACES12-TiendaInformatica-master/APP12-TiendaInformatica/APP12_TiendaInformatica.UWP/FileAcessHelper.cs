﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP12_TiendaInformatica.UWP
{
    class FileAcessHelper
    {
        /// <summary>
        /// Metodo que devuelve la ruta de la carpeta localFolder de windowsPhone
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>carpeta personal</returns>
        public static string GetLocalPath(String filename)
        {
            string path = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            return System.IO.Path.Combine(path, filename);
        }
    }
}
