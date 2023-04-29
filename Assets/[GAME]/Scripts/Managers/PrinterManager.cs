using OfficeFever.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OfficeFever.Exceptions;
using Unity.VisualScripting;

namespace OfficeFever.Managers
{
    public class PrinterManager : MonoSingleton<PrinterManager>
    {
        #region Variables

        [SerializeField] private List<PrinterController> printers = new List<PrinterController>();

        #endregion


        public List<PrinterController> GetPrinters()
        {
            return printers;
        }

    }
}


